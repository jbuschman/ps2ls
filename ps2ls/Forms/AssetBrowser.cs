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
using System.Text.RegularExpressions;
using System.Configuration;
using ps2ls.Forms.Controls;

namespace ps2ls.Forms
{
    public partial class AssetBrowser : UserControl
    {
        private bool assetsDirty = false;

        public AssetBrowser()
        {
            InitializeComponent();

            packOpenFileDialog.InitialDirectory = Properties.Settings.Default.AssetDirectory;

            Dock = DockStyle.Fill;

            AssetManager.Instance.AssetsChanged += new EventHandler(onAssetsChanged);
        }

        private void onAssetsChanged(object sender, EventArgs e)
        {
            assetsDirty = true;
        }

        private void addPacksButton_Click(object sender, EventArgs e)
        {
            DialogResult result = packOpenFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                AssetManager.Instance.LoadBinaryFromPaths(packOpenFileDialog.FileNames);
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

                    assets.AddRange(pack.Assets);
                }

                AssetManager.Instance.ExtractByAssetsToDirectoryAsync(assets, packFolderBrowserDialog.SelectedPath);
            }
        }

        private void clearSearchButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();
        }

        private void extractSelectedAssetsButton_Click(object sender, EventArgs e)
        {
            extractSelectedAssets();
        }

        private void extractSelectedAssets()
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

                AssetManager.Instance.ExtractByAssetsToDirectoryAsync(assets, packFolderBrowserDialog.SelectedPath);
            }
        }

        private void packsListBox_SelectedValueChanged(object sender, EventArgs e)
        {
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
            filesMaxComboBox.SelectedIndex = 3; //infinity

            if (Properties.Settings.Default.AssetDirectory != String.Empty)
            {
                if (Properties.Settings.Default.LoadAssetsOnStartup)
                {
                    AssetManager.Instance.LoadBinaryFromDirectory(Properties.Settings.Default.AssetDirectory);
                }
                else
                {
                    GenericDialog genericDialog = new GenericDialog();
                    genericDialog.Message =
                        "ps2ls has detected your PlanetSide 2 assets directory." +
                        System.Environment.NewLine +
                        System.Environment.NewLine +
                        "Would you like to load all asset (*.pak) files found in " + Properties.Settings.Default.AssetDirectory + " ?";
                    genericDialog.OptionVisible = true;
                    genericDialog.OptionChecked = false;
                    genericDialog.OptionText = "Always perform this action (recommended)";

                    if(DialogResult.Yes == genericDialog.ShowDialog())
                    {
                        if (genericDialog.OptionChecked)
                        {
                            Properties.Settings.Default.LoadAssetsOnStartup = true;
                            Properties.Settings.Default.Save();
                        }
                        AssetManager.Instance.LoadBinaryFromDirectory(Properties.Settings.Default.AssetDirectory);
                    }
                }
            }
        }

        private void refreshAssetsDataGridView()
        {
            Cursor.Current = Cursors.WaitCursor;
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

            Int32 totalFileCount = 0;

            // Rather than adding the rows one at a time, do it in a batch
            List<DataGridViewRow> rowsToBeAdded = new List<DataGridViewRow>();

            Regex regex = null;

            try
            {
                regex = new Regex(searchTextBox.Text, RegexOptions.Compiled);
            }
            catch (Exception) { /* invalid regex */ }

            foreach (Pack pack in packs)
            {
                totalFileCount += pack.Assets.Count;

                foreach (Asset asset in pack.Assets)
                {
                    switch (searchTextTypeToolStripDrownDownButton1.SearchTextType)
                    {
                        case SearchTextTypeToolStripDrownDownButton.SearchTextTypes.Textual:
                            {
                                if (!asset.Name.ToLower().Contains(searchTextBox.Text.ToLower()))
                                {
                                    continue;
                                }
                            }
                            break;
                        case SearchTextTypeToolStripDrownDownButton.SearchTextTypes.RegularExpression:
                            {
                                if (regex == null || !regex.IsMatch(asset.Name))
                                {
                                    continue;
                                }
                            }
                            break;
                    }

                    String extension = System.IO.Path.GetExtension(asset.Name);

                    Image icon = Asset.GetImageFromType(asset.Type);

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(assetsDataGridView, new object[] { icon, asset.Name, asset.Type, asset.Size / 1024 });
                    row.Tag = asset;

                    rowsToBeAdded.Add(row);

                    if (rowsToBeAdded.Count >= rowMax)
                    {
                        break;
                    }
                }

                if (rowsToBeAdded.Count >= rowMax)
                {
                    break;
                }
            }

            assetsDataGridView.Rows.AddRange(rowsToBeAdded.ToArray());

            assetsDataGridView.ResumeLayout();
            Cursor.Current = Cursors.Default;

            fileCountLabel.Text = assetsDataGridView.Rows.Count + "/" + totalFileCount;
            packCountLabel.Text = packs.Count + "/" + AssetManager.Instance.Packs.Count;
        }

        private void assetsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void packsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshAssetsDataGridView();
        }

        private void filesMaxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshAssetsDataGridView();
        }

        private void packContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (packsListBox.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            if (packsListBox.SelectedItems.Count == 1)
            {
                extractPacksToolStripMenuItem.Text = "Extract...";
            }
            else
            {
                extractPacksToolStripMenuItem.Text = "Extract " + packsListBox.SelectedItems.Count + "...";
            }
        }

        private void extractPacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractSelectedPacks();
        }

        private void refreshPacksListBox()
        {
            packsListBox.ClearSelected();
            packsListBox.Items.Clear();

            foreach (Pack pack in AssetManager.Instance.Packs)
            {
                packsListBox.Items.Add(pack);
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            if (assetsDirty)
            {
                refreshPacksListBox();
                assetsDirty = false;
            }
        }

        private void searchTextBox1_CustomTextChanged(object sender, EventArgs e)
        {
            refreshAssetsDataGridView();

            clearSearchButton.Enabled = searchTextBox.Text.Length > 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AssetManifestWriter writer = new AssetManifestWriter();
            writer.Write(@"C:\Users\Colin\Desktop\" + 0 + @".ps2lsmanifest");
        }

        private void assetsDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            DataGridViewRow row = assetsDataGridView.Rows[e.RowIndex];

            if (row == null)
                return;

            Asset asset = (Asset)row.Tag;

            if(asset != null)
                asset.Pack.CreateTemporaryFileAndOpen(asset.Name);
        }

        private void assetContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if(assetsDataGridView.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            if (assetsDataGridView.SelectedRows.Count == 1)
            {
                openToolStripMenuItem.Text = "Open";
                extractToolStripMenuItem.Text = "Extract...";
            }
            else
            {
                openToolStripMenuItem.Text = "Open " + assetsDataGridView.SelectedRows.Count;
                extractToolStripMenuItem.Text = "Extract " + assetsDataGridView.SelectedRows.Count + "...";
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSelectedAssets();
        }

        private void openSelectedAssets()
        {
            if (assetsDataGridView.SelectedRows.Count >= 100)
            {
                GenericDialog genericDialog = new GenericDialog();
                genericDialog.Message = "You are about to open " + assetsDataGridView.SelectedRows.Count + " files simulatenously.  Doing so may cause system instability." +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Are you sure you want to continue?";
                genericDialog.Image = Properties.Resources.logo_48_warn;

                if (genericDialog.ShowDialog() != DialogResult.Yes)
                {
                    return;
                }
            }

            foreach (DataGridViewRow row in assetsDataGridView.SelectedRows)
            {
                if (row == null)
                    return;

                Asset asset = (Asset)row.Tag;

                if (asset != null)
                {
                    if (asset.Type == Asset.Types.CNK0)
                    {
                        ps2ls.Assets.Cnk.Cnk0 cnk0;
                        ps2ls.Assets.Cnk.Cnk0.LoadFromStream(asset.Pack.CreateAssetMemoryStreamByName(asset.Name), out cnk0);
                    }

                    asset.Pack.CreateTemporaryFileAndOpen(asset.Name);
                }
            }
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractSelectedAssets();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            AssetManager.Instance.WriteManifest(@"C:\Users\Colin\Desktop\ps2ls.manifest");
        }

        private void searchTextTypeToolStripDrownDownButton1_SearchTextTypeChanged(object sender, EventArgs e)
        {
            refreshAssetsDataGridView();
        }

        private void exportAssetListToCsvButton_Click(object sender, EventArgs e)
        {
            if (exportAssetFileToCsvSaveFileDialog.ShowDialog() != DialogResult.OK)
                return;

            FileStream fileStream = new FileStream(exportAssetFileToCsvSaveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            streamWriter.Write("Name, ");
            streamWriter.Write("Size, ");
            streamWriter.Write("CRC-32");
            streamWriter.Write(Environment.NewLine);

            foreach (DataGridViewRow row in assetsDataGridView.Rows)
            {
                if (row == null)
                    continue;

                Asset asset = (Asset)row.Tag;

                streamWriter.Write(asset.Name);
                streamWriter.Write(", ");
                streamWriter.Write(asset.Size);
                streamWriter.Write(", ");
                streamWriter.Write(asset.Crc32);
                streamWriter.Write(Environment.NewLine);
            }

            streamWriter.Close();
        }
    }
}
