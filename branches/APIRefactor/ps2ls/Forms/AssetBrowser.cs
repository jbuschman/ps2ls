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
    public partial class AssetBrowser : UserControl
    {
        public AssetBrowser()
        {
            InitializeComponent();

            packOpenFileDialog.InitialDirectory = Properties.Settings.Default.AssetDirectory;

            Dock = DockStyle.Fill;

            Program.AssetManager.LoadPacksComplete += new EventHandler(loadPacksCompleted);
        }

        private void addPacksButton_Click(object sender, EventArgs e)
        {
            DialogResult result = packOpenFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Program.AssetManager.LoadPacksFromPathsASync(packOpenFileDialog.FileNames);
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

                Program.AssetManager.ExtractAssetsToDirectoryAsync(assets, packFolderBrowserDialog.SelectedPath);
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

                Program.AssetManager.ExtractAssetsToDirectoryAsync(assets, packFolderBrowserDialog.SelectedPath);
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
            filesMaxComboBox.SelectedIndex = 3; //infinity

            if (Properties.Settings.Default.AssetDirectory != String.Empty)
            {
                if (DialogResult.Yes == MessageBox.Show(@"Would you like to load all *.pak files located in " + Properties.Settings.Default.AssetDirectory + "?", "ps2ls", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly, false))
                {
                    IEnumerable<string> files = Directory.EnumerateFiles(Properties.Settings.Default.AssetDirectory, "*.pack", SearchOption.TopDirectoryOnly);

                    Program.AssetManager.LoadPacksFromPathsASync(files);
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
            List<System.Windows.Forms.DataGridViewRow> rowsToBeAdded = new List<DataGridViewRow>();

            foreach (Pack pack in packs)
            {
                totalFileCount += pack.Assets.Count;

                foreach (Asset asset in pack.Assets)
                {
                    if (asset.Name.ToLower().Contains(searchTextBox.Text.ToLower()) == false)
                    {
                        continue;
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
            packCountLabel.Text = packs.Count + "/" + Program.AssetManager.Packs.Count;
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

        private void packContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            extractPacksToolStripMenuItem.Text = "Extract " + packsListBox.SelectedItems.Count + " Pack(s)...";
            extractPacksToolStripMenuItem.Enabled = packsListBox.SelectedItems.Count > 0;
        }

        private void extractPacksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractSelectedPacks();
        }

        public void loadPacksCompleted(object sender, EventArgs args)
        {
            RefreshPacksListBox();
        }

        public void RefreshPacksListBox()
        {
            packsListBox.ClearSelected();
            packsListBox.Items.Clear();

            foreach (Pack pack in Program.AssetManager.Packs)
            {
                packsListBox.Items.Add(pack);
            }
        }
    }
}
