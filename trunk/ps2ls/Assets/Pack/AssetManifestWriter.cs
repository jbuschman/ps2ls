using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.IO.Compression;
using System.Net;
using System.Collections.Specialized;

namespace ps2ls.Assets.Pack
{
    public class AssetManifestWriter
    {
        private const int version = 1;
        private GenericLoadingForm loadingForm;
        private BackgroundWorker writeBackgroundWorker;

        public AssetManifestWriter()
        {
            writeBackgroundWorker = new BackgroundWorker();
            writeBackgroundWorker.WorkerReportsProgress = true;
            writeBackgroundWorker.DoWork += writeDoWork;
            writeBackgroundWorker.ProgressChanged += writeProgressChanged;
            writeBackgroundWorker.RunWorkerCompleted += writeRunWorkerCompleted;
        }

        private void writeRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            loadingForm.Close();
        }

        private void writeProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        private void writeDoWork(object sender, DoWorkEventArgs args)
        {
            writeToPath(sender, args.Argument);
        }

        public void Write(string path)
        {
            loadingForm = new GenericLoadingForm();
            loadingForm.Show();

            writeBackgroundWorker.RunWorkerAsync(path);
        }

        public void writeToPath(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            String path = System.IO.Path.GetTempFileName();

            int revision = 0;

            MemoryStream memoryStream = new MemoryStream();
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
            GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress);
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
            
            char[] header = { 'P', 'S', '2', 'L', 'S', 'A', 'M', 'F' };
            binaryWriter.Write(header);
            binaryWriter.Write(version);
            binaryWriter.Write(revision);

            int checksum = 0;

            //calculate manifest checksum from all files
            foreach (Pack pack in AssetManager.Instance.Packs)
                foreach (Asset asset in pack.Assets)
                    checksum += asset.Crc32;
            
            binaryWriter.Write(checksum);

            int packCount = AssetManager.Instance.Packs.Count;

            binaryWriter.Write(packCount);

            foreach (Pack pack in AssetManager.Instance.Packs)
            {
                binaryWriter.Write((byte)pack.Name.Length);
                binaryWriter.Write(ASCIIEncoding.ASCII.GetBytes(pack.Name));
                binaryWriter.Write(pack.Checksum);

                // write length of this pack block so we can skip ahead
                // if the checksum is identical to whatever we have on
                // record.
                int length = getPackBlockLength(pack);
                binaryWriter.Write(length);
                binaryWriter.Write(pack.Assets.Count);

                for (int i = 0; i < pack.Assets.Count; ++i)
                {
                    Asset asset = pack.Assets[i];

                    //backgroundWorker.ReportProgress((int)(((float)i / (float)assetCount) * 100.0f), asset.Name);

                    binaryWriter.Write((byte)asset.Name.Length);
                    binaryWriter.Write(ASCIIEncoding.ASCII.GetBytes(asset.Name));
                    binaryWriter.Write(asset.Crc32);
                }
            }

            backgroundWorker.ReportProgress(100, "Compressing");

            byte[] buffer = memoryStream.ToArray();
            gzipStream.Write(buffer, 0, buffer.Length);
            gzipStream.Close();

            NameValueCollection nvc = new NameValueCollection();
            nvc["whatever"] = "whatever";
            HttpUploadFile(@"http://localhost/ps2ls/api/manifest_submit.php", path, "manifest", "application/octet-stream", null);
        }

        private int getPackBlockLength(Pack pack)
        {
            int length = 0;

            foreach (Asset asset in pack.Assets)
            {
                length += 1 + asset.Name.Length;    //name
                length += 4;                        //crc32
            }

            return length;
        }

        public static void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            if (nvc != null)
            {
                foreach (string key in nvc.Keys)
                {
                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                    string formitem = string.Format(formdataTemplate, key, nvc[key]);
                    byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                    rs.Write(formitembytes, 0, formitembytes.Length);
                }
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                string response = reader2.ReadToEnd();
                Console.WriteLine(response);
            }
            catch (Exception)
            {
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }
    }
}
