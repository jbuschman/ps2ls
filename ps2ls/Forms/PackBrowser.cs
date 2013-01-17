using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using ps2ls.Assets.Pack;

namespace ps2ls.Forms
{
    public partial class PackBrowser : UserControl
    {
        #region Singleton
        private static PackBrowser instance = null;

        public static void CreateInstance()
        {
            instance = new PackBrowser();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static PackBrowser Instance { get { return instance; } }
        #endregion

        public Dictionary<Int32, Pack> Packs { get; private set; }

        public Dictionary<Asset.Types, List<Asset>> AssetsByType { get; private set; }

        private GenericLoadingForm loadingForm;
        private BackgroundWorker loadBackgroundWorker;
        private BackgroundWorker extractAllBackgroundWorker;
        private BackgroundWorker extractSelectionBackgroundWorker;

        private PackBrowser()
        {
            InitializeComponent();

            Packs = new Dictionary<Int32, Pack>();
            AssetsByType = new Dictionary<Asset.Types, List<Asset>>();

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

            packOpenFileDialog.InitialDirectory = PS2LS.Instance.GameDirectory;

            Dock = DockStyle.Fill;
        }

        private void addPacksButton_Click(object sender, EventArgs e)
        {
            DialogResult result = packOpenFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                loadBinaryFromPaths(packOpenFileDialog.FileNames);
            }
        }

        private void extractSelectedPacksButton_Click(object sender, EventArgs e)
        {
            extractSelectedPacks();
        }

        private void extractSelectedPacks()
        {
            if (packFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<Asset> assets = new List<Asset>();

                foreach (object item in packsListBox.SelectedItems)
                {
                    Pack pack = null;

                    try
                    {
                        pack = (Pack)item;
                    }
                    catch (Exception) { continue; }

                    foreach (Asset file in pack.Assets.Values)
                    {
                        assets.Add(file);
                    }
                }

                extractByAssetsToDirectoryAsync(assets, packFolderBrowserDialog.SelectedPath);
            }
        }

        private void clearSearchButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
        }

        private void extractSelectedAssetsButton_Click(object sender, EventArgs e)
        {
            if (packFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<Asset> assets = new List<Asset>();

                foreach (DataGridViewRow row in assetsDataGridView.SelectedRows)
                {
                    Asset file = null;

                    try
                    {
                        file = (Asset)row.Tag;
                    }
                    catch (InvalidCastException) { continue; }

                    assets.Add(file);
                }

                extractByAssetsToDirectoryAsync(assets, packFolderBrowserDialog.SelectedPath);
            }
        }

        private void packsListBox_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            searchAssetsTimer.Stop();
            searchAssetsTimer.Start();
        }

        private void searchAssetsTimer_Tick(object sender, EventArgs e)
        {
            if (searchTextBox.Text.Length > 0)
            {
                searchTextBox.BackColor = Color.Yellow;
                clearSearchButton.Enabled = true;
            }
            else
            {
                searchTextBox.BackColor = Color.White;
                clearSearchButton.Enabled = false;
            }

            refreshAssetsDataGridView();

            searchAssetsTimer.Stop();
        }

        private void packsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            e.DrawBackground();

            String text = ((ListBox)sender).Items[e.Index].ToString();
            Image icon = Properties.Resources.box_small;
            Point point = new Point(0, e.Bounds.Y);

            e.Graphics.DrawImage(icon, point);

            point.X += icon.Width;

            e.Graphics.DrawString(text, e.Font, new SolidBrush(Color.Black), point);
            e.DrawFocusRectangle();
        }

        private void PackBrowserUserControl_Load(object sender, EventArgs e)
        {
            filesMaxComboBox.SelectedIndex = 3;

            if (PS2LS.Instance.GameDirectory != String.Empty)
            {
                if (DialogResult.Yes == MessageBox.Show(@"Do you want to load all *.pak files located in " + PS2LS.Instance.GameDirectory + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false))
                {
                    loadBinaryFromDirectory(PS2LS.Instance.GameDirectory);
                }
            }
        }

        private void refreshAssetsDataGridView()
        {
            assetsDataGridView.SuspendLayout();

            assetsDataGridView.Rows.Clear();

            ListBox.SelectedObjectCollection packs = packsListBox.SelectedItems;

            Int32 rowMax = 0;

            try
            {
                rowMax = Int32.Parse(filesMaxComboBox.Items[filesMaxComboBox.SelectedIndex].ToString());
            }
            catch (FormatException)
            {
                rowMax = Int32.MaxValue;
            }

            Int32 fileCount = 0;

            for (Int32 j = 0; j < packs.Count; ++j)
            {
                Pack pack = null;

                try
                {
                    pack = (Pack)packs[j];
                }
                catch (InvalidCastException) { continue; }

                for (Int32 i = 0; i < pack.Assets.Values.Count; ++i)
                {
                    if (fileCount >= rowMax)
                    {
                        continue;
                    }

                    ++fileCount;

                    Asset file = pack.Assets.Values.ElementAt(i);

                    if (file.Name.ToLower().Contains(searchTextBox.Text.ToLower()) == false)
                    {
                        continue;
                    }

                    String extension = System.IO.Path.GetExtension(file.Name);

                    Image icon = Asset.GetImageFromType(file.Type);

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(assetsDataGridView, new object[] { icon, file.Name, file.Type, file.Size / 1024 });
                    row.Tag = file;

                    assetsDataGridView.Rows.Add(row);
                }
            }

            assetsDataGridView.ResumeLayout();

            fileCountLabel.Text = assetsDataGridView.Rows.Count + "/" + fileCount;
            packCountLabel.Text = packs.Count + "/" + Packs.Count;
        }

        private void refreshPacksListBox()
        {
            packsListBox.ClearSelected();
            packsListBox.Items.Clear();

            foreach (KeyValuePair<Int32, Pack> pack in Packs)
            {
                packsListBox.Items.Add(pack.Value);
            }
        }

        private void assetsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            extractSelectedAssetsButton.Enabled = assetsDataGridView.SelectedRows.Count > 0;
        }

        private void packsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = packsListBox.SelectedItem;

            extractSelectedPacksButton.Enabled = packsListBox.SelectedItems.Count > 0;

            refreshAssetsDataGridView();
        }

        private void filesMaxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshAssetsDataGridView();
        }

        private void loadBinaryFromDirectory(string directory)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(PS2LS.Instance.GameDirectory, "*.pack", SearchOption.TopDirectoryOnly);

            loadBinaryFromPaths(files);
        }

        private void loadBinaryFromPaths(IEnumerable<string> paths)
        {
            loadingForm = new GenericLoadingForm();
            loadingForm.Show();

            loadBackgroundWorker.RunWorkerAsync(paths);
        }

        private void loadRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            refreshPacksListBox();
            loadingForm.Close();

            //refresh model browser
            ModelBrowser.Instance.Refresh(); //TODO: Regulate model browser refreshes in a more temporal way
            MaterialBrowser.Instance.Refresh();
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

            for (Int32 i = 0; i < paths.Count(); ++i)
            {
                String path = paths.ElementAt(i);
                Pack pack = null;

                if (Packs.TryGetValue(path.GetHashCode(), out pack) == false)
                {
                    pack = Pack.LoadBinary(path);

                    if (pack != null)
                    {
                        Packs.Add(path.GetHashCode(), pack);

                        foreach (Asset asset in pack.Assets.Values)
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

        public void ExtractAllToDirectory(string directory)
        {
            loadingForm = new GenericLoadingForm();
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
            loadingForm.Close();
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

        private void extractByAssetsToDirectoryAsync(IEnumerable<Asset> assets, string directory)
        {
            loadingForm = new GenericLoadingForm();
            loadingForm.Show();

            object[] args = new object[] { assets, directory };

            extractSelectionBackgroundWorker.RunWorkerAsync(args);
        }

        private void extractByAssetsToDirectory(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            object[] args = (object[])arg;
            IEnumerable<Asset> assets = (IEnumerable<Asset>)args[0];
            String directory = (String)args[1];

            for (Int32 i = 0; i < assets.Count(); ++i)
            {
                Asset file = assets.ElementAt(i);

                file.Pack.ExtractAssetByNameToDirectory(file.Name, directory);

                Single percent = (Single)(i + 1) / (Single)assets.Count();
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(file.Name));
            }
        }

        private void extractSelectionRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            loadingForm.Close();
        }

        private void extractSelectionProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        private void extractSelectionDoWork(object sender, DoWorkEventArgs args)
        {
            extractByAssetsToDirectory(sender, args.Argument);
        }

        private void packContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            extractPacksToolStripMenuItem.Text = "Extract " + packsListBox.SelectedItems.Count + " Pack(s)...";
            extractPacksToolStripMenuItem.Enabled = packsListBox.SelectedItems.Count > 0;
        }

        private void extractPacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractSelectedPacks();
        }

        public MemoryStream CreateAssetMemoryStreamByName(String name)
        {
            MemoryStream memoryStream = null;

            foreach (Pack pack in Packs.Values)
            {
                memoryStream = pack.CreateAssetMemoryStreamByName(name);

                if (memoryStream != null)
                {
                    break;
                }
            }

            return memoryStream;
        }
    }
}
