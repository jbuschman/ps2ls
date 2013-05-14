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

namespace ps2ls.Assets.Pack
{
    public class AssetManifestWriter
    {
        private const int version = 1;
        private GenericLoadingForm loadingForm = new GenericLoadingForm();
        private BackgroundWorker writeBackgroundWorker = new BackgroundWorker();

        public AssetManifestWriter()
        {
            writeBackgroundWorker.DoWork += writeDoWork;
            writeBackgroundWorker.WorkerReportsProgress = true;
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
            writeBackgroundWorker.RunWorkerAsync(path);

            loadingForm.Show();
        }

        public void writeToPath(object sender, object arg)
        {
            String path = (String)arg;

            int revision = 0;

            MemoryStream memoryStream = new MemoryStream();
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
            GZipStream gzipStream = new GZipStream(fileStream, CompressionMode.Compress);
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
            
            char[] header = { 'P', 'S', '2', 'L', 'S', 'A', 'M' };
            binaryWriter.Write(header);
            binaryWriter.Write(version);
            binaryWriter.Write(revision);

            int assetCount = 0;

            foreach (Pack pack in AssetManager.Instance.Packs)
                assetCount += pack.Assets.Count;

            binaryWriter.Write(assetCount);

            foreach (Pack pack in AssetManager.Instance.Packs)
            {
                for (int i = 0; i < pack.Assets.Count; ++i)
                {
                    Asset asset = pack.Assets[i];

                    writeBackgroundWorker.ReportProgress((int)(((float)i / (float)assetCount) * 100.0f), asset.Name);

                    binaryWriter.Write(asset.Name);
                    binaryWriter.Write(asset.Size);
                    binaryWriter.Write(asset.AbsoluteOffset);
                    binaryWriter.Write(asset.Crc32);
                }
            }

            writeBackgroundWorker.ReportProgress(100, "Compressing");

            byte[] buffer = new byte[memoryStream.Length];
            memoryStream.Read(buffer, 0, buffer.Length);
            gzipStream.Write(buffer, 0, buffer.Length);
            gzipStream.Close();
        }
    }
}
