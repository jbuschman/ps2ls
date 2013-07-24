using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using ps2ls.Forms;
using ps2ls.Graphics.Materials;
using System.Net;
using System.Collections.Specialized;
using Microsoft.Win32;

namespace ps2ls.Assets.Pack
{
    class AssetManager
    {
        #region Singleton
        private static AssetManager instance = null;

        public static void CreateInstance()
        {
            instance = new AssetManager();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static AssetManager Instance { get { return instance; } }
        #endregion

        public Dictionary<Asset.Types, List<Asset>> AssetsByType { get; private set; }

        // Internal cache to check whether a pack has already been loaded
        public Dictionary<int, Pack> Packs = new Dictionary<int, Pack>();

        private GenericLoadingForm loadingForm;
        private List<BackgroundWorker> backgroundWorkers = new List<BackgroundWorker>();

        public event EventHandler AssetsChanged;

        private AssetManager()
        {
            AssetsByType = new Dictionary<Asset.Types, List<Asset>>();
        }

        private BackgroundWorker createExtractBackgroundWorker()
        {
            BackgroundWorker extractBackgroundWorker = new BackgroundWorker();
            extractBackgroundWorker.WorkerReportsProgress = true;
            extractBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(extractProgressChanged);
            extractBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(extractRunWorkerCompleted);
            extractBackgroundWorker.DoWork += new DoWorkEventHandler(extractDoWork);
            backgroundWorkers.Add(extractBackgroundWorker);
            return extractBackgroundWorker;
        }

        private BackgroundWorker createLoadBackgroundWorker()
        {
            BackgroundWorker loadBackgroundWorker = new BackgroundWorker();
            loadBackgroundWorker.WorkerReportsProgress = true;
            loadBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(loadProgressChanged);
            loadBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(loadRunWorkerCompleted);
            loadBackgroundWorker.DoWork += new DoWorkEventHandler(loadDoWork);
            backgroundWorkers.Add(loadBackgroundWorker);
            return loadBackgroundWorker;
        }

        public void LoadFromDirectory(string directory)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(Properties.Settings.Default.AssetDirectory, "*.pack", SearchOption.TopDirectoryOnly);

            LoadFromFileNames(files);
        }

        public void LoadFromFileNames(IEnumerable<string> fileNames)
        {
            loadingForm = new GenericLoadingForm();
            loadingForm.Show();

            BackgroundWorker loadBackgroundWorker = createLoadBackgroundWorker();
            loadBackgroundWorker.RunWorkerAsync(fileNames);
        }

        private void loadRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            loadingForm.Close();

            AssetsChanged.Invoke(sender, args);

            backgroundWorkers.Remove((BackgroundWorker)sender);
        }

        private void loadProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        private void loadDoWork(object sender, DoWorkEventArgs args)
        {
            loadFromFileNames(sender, args.Argument);
        }

        private void loadFromFileNames(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            IEnumerable<string> fileNames = (IEnumerable<string>)arg;

            for (int i = 0; i < fileNames.Count(); ++i)
            {
                string fileName = fileNames.ElementAt(i);
                Pack pack = null;

                if (!Packs.TryGetValue(fileName.GetHashCode(), out pack))
                {
                    pack = Pack.Load(fileName);

                    if (pack != null)
                    {
                        Packs.Add(fileName.GetHashCode(), pack);

                        foreach (Asset asset in pack.Assets.Values)
                        {
                            if (!AssetsByType.ContainsKey(asset.Type))
                                AssetsByType.Add(asset.Type, new List<Asset>());

                            AssetsByType[asset.Type].Add(asset);
                        }
                    }
                }

                float percent = (Single)(i + 1) / (Single)fileNames.Count();
                backgroundWorker.ReportProgress((int)(percent * 100.0f), System.IO.Path.GetFileName(fileName));
            }
        }

        public void ExtractToDirectory(IEnumerable<string> assetNames, string directory)
        {
            foreach (Pack pack in Packs.Values)
                pack.ExtractAssetsToDirectory(assetNames, directory);
        }

        public void ExtractToDirectoryAsync(IEnumerable<Asset> assets, string directory, EventHandler completedHandler)
        {
            loadingForm = new GenericLoadingForm();
            loadingForm.Show();

            object[] args = new object[] { assets, directory, completedHandler };

            BackgroundWorker extractBackgroundWorker = createExtractBackgroundWorker();
            extractBackgroundWorker.RunWorkerAsync(args);
        }

        private void extractAssetsToDirectory(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            object[] args = arg as object[];
            IEnumerable<Asset> assets = args[0] as IEnumerable<Asset>;
            string directory = args[1] as string;
            EventHandler completedHandler = args[2] as EventHandler;

            for (int i = 0; i < assets.Count(); ++i)
            {
                Asset file = assets.ElementAt(i);

                file.Pack.ExtractAssetToDirectory(file.Name, directory);

                float percent = (Single)(i + 1) / (Single)assets.Count();
                backgroundWorker.ReportProgress((int)(percent * 100), System.IO.Path.GetFileName(file.Name));
            }
        }

        private void extractRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            loadingForm.Close();

            EventHandler completedEventHandler = args.Result as EventHandler;

            if(completedEventHandler != null)
                completedEventHandler.Invoke(this, null);
        }

        private void extractProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        private void extractDoWork(object sender, DoWorkEventArgs args)
        {
            extractAssetsToDirectory(sender, args.Argument);

            object[] args_ = args.Argument as object[];
            EventHandler completedEventHandler = args_[2] as EventHandler;
            args.Result = completedEventHandler;
        }

        public MemoryStream CreateAssetMemoryStreamByName(string name)
        {
            MemoryStream memoryStream = null;

            foreach (Pack pack in Packs.Values)
            {
                memoryStream = pack.CreateAssetMemoryStreamByName(name);

                if (memoryStream != null)
                    break;
            }

            return memoryStream;
        }

        /// <summary>
        /// Try to detect the Planetside 2 asset directory by looking in the registry for Planetside 2 installation directories.
        /// </summary>
        public static string DetectAssetDirectory()
        {
            RegistryKey key = null;

            // non-steam install
            key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\LaunchPad.exe");

            if (key != null && key.GetValue("") != null)
            {
                string defaultDirectory;
                defaultDirectory = key.GetValue("").ToString();
                defaultDirectory = Path.GetDirectoryName(defaultDirectory) + @"\Resources\Assets";

                if (Directory.Exists(defaultDirectory))
                    return defaultDirectory;
            }

            // steam install
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 218230");

            if (key != null && key.GetValue("InstallLocation") != null)
            {
                string defaultDirectory;
                defaultDirectory = key.GetValue("InstallLocation").ToString();
                defaultDirectory += @"\Resources\Assets";

                if (Directory.Exists(defaultDirectory))
                    return defaultDirectory;
            }

            return String.Empty;
        }
    }
}
