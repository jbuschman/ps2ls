using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using ps2ls.Forms;
using ps2ls.Graphics.Materials;

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

        public List<Pack> Packs { get; private set; }
        public Dictionary<Asset.Types, List<Asset>> AssetsByType { get; private set; }

        // Internal cache to check whether a pack has already been loaded
        public Dictionary<Int32, Pack> packLookupCache = new Dictionary<Int32, Pack>();

        public EventHandler LoadPacksComplete = null;
        public EventHandler ExtractAssetsComplete = null;

        private GenericLoadingForm loadingForm;

        private AssetManager()
        {
            Packs = new List<Pack>();
            AssetsByType = new Dictionary<Asset.Types, List<Asset>>();
        }

        public Asset GetAssetByName(string name)
        {
            foreach (Pack pack in Packs)
            {
                Asset asset = pack.GetAssetByName(name);
                if (asset != null)
                {
                    return asset;
                }
            }
            return null;
        }

        public void LoadPacksFromPathsASync(IEnumerable<string> paths)
        {
            loadingForm = new GenericLoadingForm();
            loadingForm.Show();

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(loadPacksDoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(loadPacksRunWorkerCompleted);

            backgroundWorker.RunWorkerAsync(paths);
        }

        public void ExtractAssetsToDirectoryAsync(IEnumerable<Asset> assets, string directory)
        {
            loadingForm = new GenericLoadingForm();
            loadingForm.Show();

            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(extractSAssetsDoWork);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerProgressChanged);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(extractAssetsRunWorkerCompleted);

            object[] args = new object[] { assets, directory };
            backgroundWorker.RunWorkerAsync(args);
        }

        private void backgroundWorkerProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        #region Load Packs Background Worker

        private void loadPacksDoWork(object sender, DoWorkEventArgs args)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            IEnumerable<string> paths = (IEnumerable<string>)args.Argument;

            for (Int32 i = 0; i < paths.Count(); ++i)
            {
                String path = paths.ElementAt(i);
                Pack pack = null;

                if (packLookupCache.TryGetValue(path.GetHashCode(), out pack) == false)
                {
                    pack = Pack.LoadPackFromFile(path);

                    if (pack != null)
                    {
                        packLookupCache.Add(path.GetHashCode(), pack);
                        Packs.Add(pack);

                        foreach (Asset asset in pack.Assets)
                        {
                            if (false == AssetsByType.ContainsKey(asset.Type))
                            {
                                AssetsByType.Add(asset.Type, new List<Asset>());
                            }

                            AssetsByType[asset.Type].Add(asset);
                        }
                    }
                }

                Single percent = (Single)(i + 1) / (Single)paths.Count();
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(path));
            }
        }

        private void loadPacksRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            if (LoadPacksComplete != null)
            {
                LoadPacksComplete.Invoke(this, null);
            }

            loadingForm.Close();
        }

        #endregion // Load Packs Background Worker

        #region Extract Assets Background Worker

        private void extractSAssetsDoWork(object sender, DoWorkEventArgs eventArgs)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            object[] args = (object[])eventArgs.Argument;
            IEnumerable<Asset> assets = (IEnumerable<Asset>)args[0];
            String directory = (String)args[1];

            Dictionary<Pack, IList<Asset>> sortedAssetList = Pack.GetAssetListSortedByPack(assets);

            Int32 numAssetsExported = 0;
            foreach (Pack pack in sortedAssetList.Keys)
            {
                IList<Asset> assetsByPack = sortedAssetList[pack];
                pack.ExtractAssetsToDirectory(assetsByPack, directory);
                numAssetsExported += assetsByPack.Count;
                Single percent = numAssetsExported / (Single)assets.Count();
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(pack.Name));
            }
        }

        private void extractAssetsRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            if (ExtractAssetsComplete != null)
            {
                ExtractAssetsComplete.Invoke(this, null);
            }

            loadingForm.Close();
        }

        #endregion //Extract Assets Background Worker

    }
}
