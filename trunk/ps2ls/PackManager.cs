using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Security.Cryptography;

namespace ps2ls
{
    public class PackManager
    {
        #region Singleton
        private static PackManager _Instance = null;

        public static void CreateInstance()
        {
            _Instance = new PackManager();
        }

        public static void DeleteInstance()
        {
            _Instance = null;
        }

        public static PackManager Instance { get { return _Instance; } }
        #endregion

        public Dictionary<Int32, Pack> Packs { get; private set; }

        private GenericLoadingForm _LoadingForm;
        private BackgroundWorker _LoadBackgroundWorker;
        private BackgroundWorker _ExtractAllBackgroundWorker;
        private BackgroundWorker _ExtractSelectionBackgroundWorker;

        private PackManager()
        {
            Packs = new Dictionary<Int32, Pack>();

            _LoadBackgroundWorker = new BackgroundWorker();
            _LoadBackgroundWorker.WorkerReportsProgress = true;
            _LoadBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(_LoadProgressChanged);
            _LoadBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_LoadRunWorkerCompleted);
            _LoadBackgroundWorker.DoWork += new DoWorkEventHandler(_LoadDoWork);

            _ExtractAllBackgroundWorker = new BackgroundWorker();
            _ExtractAllBackgroundWorker.WorkerReportsProgress = true;
            _ExtractAllBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(_ExtractAllProgressChanged);
            _ExtractAllBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_ExtractAllRunWorkerCompleted);
            _ExtractAllBackgroundWorker.DoWork += new DoWorkEventHandler(_ExtractAllDoWork);

            _ExtractSelectionBackgroundWorker = new BackgroundWorker();
            _ExtractSelectionBackgroundWorker.WorkerReportsProgress = true;
            _ExtractSelectionBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(_ExtractSelectionProgressChanged);
            _ExtractSelectionBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_ExtractSelectionRunWorkerCompleted);
            _ExtractSelectionBackgroundWorker.DoWork += new DoWorkEventHandler(_ExtractSelectionDoWork);

            _LoadingForm = new GenericLoadingForm();
        }

        public void LoadBinaryFromPaths(IEnumerable<string> paths)
        {
            _LoadingForm.Show();
            _LoadBackgroundWorker.RunWorkerAsync(paths);
        }

        private void _LoadRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            _LoadingForm.Hide();

            Form1.Instance.RefreshTreeView();
        }
        
        private void _LoadProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            _LoadingForm.SetProgressBarPercent(args.ProgressPercentage);
            _LoadingForm.SetLabelText((String)args.UserState);
        }

        private void _LoadDoWork(object sender, DoWorkEventArgs args)
        {
            _LoadBinaryFromPaths(sender, args.Argument);
        }

        private void _LoadBinaryFromPaths(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            IEnumerable<string> paths = (IEnumerable<string>)arg;

            for(Int32 i = 0; i < paths.Count(); ++i)
            {
                String path = paths.ElementAt(i);
                Pack pack = null;

                if (Packs.TryGetValue(path.GetHashCode(), out pack) == false)
                {
                    pack = Pack.LoadBinary(path);

                    if (pack != null)
                    {
                        Packs.Add(path.GetHashCode(), pack);
                    }
                }

                Single percent = (Single)(i + 1) / (Single)paths.Count();
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(path));
            }
        }

        public void ExtractAllToDirectory(string directory)
        {
            _LoadingForm.Show();
            _ExtractAllBackgroundWorker.RunWorkerAsync(directory);
        }

        private void _ExtractAllToDirectory(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            String directory = (String)arg;

            for (Int32 i = 0; i < Packs.Count; ++i)
            {
                Pack pack = Packs.Values.ElementAt(i);

                pack.ExtractAllFilesToDirectory(directory);

                Single percent = (Single)(i + 1) / (Single)Packs.Count;
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(pack.Path));
            }
        }

        private void _ExtractAllRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            _LoadingForm.Hide();
        }

        private void _ExtractAllProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            _LoadingForm.SetProgressBarPercent(args.ProgressPercentage);
            _LoadingForm.SetLabelText((String)args.UserState);
        }

        private void _ExtractAllDoWork(object sender, DoWorkEventArgs args)
        {
            _ExtractAllToDirectory(sender, args.Argument);
        }

        public void ExtractByPackFilesToDirectory(IEnumerable<PackFile> files, string directory)
        {
            _LoadingForm.Show();

            object[] args = new object[] { files, directory };

            _ExtractSelectionBackgroundWorker.RunWorkerAsync(args);
        }

        private void _ExtractByPackFilesToDirectory(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            object[] args = (object[])arg;
            IEnumerable<PackFile> files = (IEnumerable<PackFile>)args[0];
            String directory = (String)args[1];

            for (Int32 i = 0; i < files.Count(); ++i)
            {
                PackFile file = files.ElementAt(i);

                file.Pack.ExtractFileByNameToDirectory(file.Name, directory);

                Single percent = (Single)(i + 1) / (Single)files.Count();
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(file.Name));
            }
        }

        private void _ExtractSelectionRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            _LoadingForm.Hide();
        }

        private void _ExtractSelectionProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            _LoadingForm.SetProgressBarPercent(args.ProgressPercentage);
            _LoadingForm.SetLabelText((String)args.UserState);
        }

        private void _ExtractSelectionDoWork(object sender, DoWorkEventArgs args)
        {
            _ExtractByPackFilesToDirectory(sender, args.Argument);
        }
    }
}
