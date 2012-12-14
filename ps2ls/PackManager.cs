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
        private static PackManager instance = null;

        public static void CreateInstance()
        {
            instance = new PackManager();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static PackManager Instance { get { return instance; } }
        #endregion

        public Dictionary<Int32, Pack> Packs { get; private set; }

        public Dictionary<PackFile.Types, List<PackFile>> FilesByType { get; private set; }

        private GenericLoadingForm loadingForm;
        private BackgroundWorker loadBackgroundWorker;
        private BackgroundWorker extractAllBackgroundWorker;
        private BackgroundWorker extractSelectionBackgroundWorker;

        private PackManager()
        {
            Packs = new Dictionary<Int32, Pack>();
            FilesByType = new Dictionary<PackFile.Types, List<PackFile>>();

            loadBackgroundWorker = new BackgroundWorker();
            loadBackgroundWorker.WorkerReportsProgress = true;
            loadBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(loadProgressChanged);
            loadBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(loadRunWorkerCompleted);
            loadBackgroundWorker.DoWork += new DoWorkEventHandler(loadDoWork);

            extractAllBackgroundWorker = new BackgroundWorker();
            extractAllBackgroundWorker.WorkerReportsProgress = true;
            extractAllBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(extractAllProgressChanged);
            extractAllBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(extractAllRunWorkerCompleted);
            extractAllBackgroundWorker.DoWork += new DoWorkEventHandler(extractAllDoWork);

            extractSelectionBackgroundWorker = new BackgroundWorker();
            extractSelectionBackgroundWorker.WorkerReportsProgress = true;
            extractSelectionBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(extractSelectionProgressChanged);
            extractSelectionBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(extractSelectionRunWorkerCompleted);
            extractSelectionBackgroundWorker.DoWork += new DoWorkEventHandler(extractSelectionDoWork);

            loadingForm = new GenericLoadingForm();
        }

        public void LoadBinaryFromPaths(IEnumerable<string> paths)
        {
            loadingForm.Show();
            loadBackgroundWorker.RunWorkerAsync(paths);
        }

        private void loadRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            loadingForm.Hide();

            Form1.Instance.RefreshTreeView();
        }
        
        private void loadProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        private void loadDoWork(object sender, DoWorkEventArgs args)
        {
            loadBinaryFromPaths(sender, args.Argument);
        }

        private void loadBinaryFromPaths(object sender, object arg)
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

                        foreach (PackFile packFile in pack.Files.Values)
                        {
                            if (false == FilesByType.ContainsKey(packFile.Type))
                            {
                                FilesByType.Add(packFile.Type, new List<PackFile>());
                            }

                            FilesByType[packFile.Type].Add(packFile);
                        }
                    }
                }

                Single percent = (Single)(i + 1) / (Single)paths.Count();
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(path));
            }
        }

        public void ExtractAllToDirectory(string directory)
        {
            loadingForm.Show();
            extractAllBackgroundWorker.RunWorkerAsync(directory);
        }

        private void extractAllToDirectory(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            String directory = String.Empty;

            try
            {
                directory = (String)arg;
            }
            catch (InvalidCastException) { return; }

            for (Int32 i = 0; i < Packs.Count; ++i)
            {
                Pack pack = Packs.Values.ElementAt(i);

                pack.ExtractAllFilesToDirectory(directory);

                Single percent = (Single)(i + 1) / (Single)Packs.Count;
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(pack.Path));
            }
        }

        private void extractAllRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            loadingForm.Hide();
        }

        private void extractAllProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        private void extractAllDoWork(object sender, DoWorkEventArgs args)
        {
            extractAllToDirectory(sender, args.Argument);
        }

        public void ExtractByPackFilesToDirectory(IEnumerable<PackFile> files, string directory)
        {
            loadingForm.Show();

            object[] args = new object[] { files, directory };

            extractSelectionBackgroundWorker.RunWorkerAsync(args);
        }

        private void extractByPackFilesToDirectory(object sender, object arg)
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

        private void extractSelectionRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            loadingForm.Hide();
        }

        private void extractSelectionProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        private void extractSelectionDoWork(object sender, DoWorkEventArgs args)
        {
            extractByPackFilesToDirectory(sender, args.Argument);
        }
    }
}
