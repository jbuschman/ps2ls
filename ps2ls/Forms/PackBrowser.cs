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

        private PackBrowser()
        {
            InitializeComponent();

            packOpenFileDialog.InitialDirectory = PS2LS.Instance.GameDirectory;

            Dock = DockStyle.Fill;
        }

        private void addPacksButton_Click(object sender, EventArgs e)
        {
            DialogResult result = packOpenFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                PackManager.Instance.LoadBinaryFromPaths(packOpenFileDialog.FileNames);
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

                PackManager.Instance.ExtractByAssetsToDirectoryAsync(assets, packFolderBrowserDialog.SelectedPath);
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

                PackManager.Instance.ExtractByAssetsToDirectoryAsync(assets, packFolderBrowserDialog.SelectedPath);
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
                    PackManager.Instance.LoadBinaryFromDirectory(PS2LS.Instance.GameDirectory);
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
            packCountLabel.Text = packs.Count + "/" + PackManager.Instance.Packs.Count;
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

        public void RefreshPacksListBox()
        {
            packsListBox.ClearSelected();
            packsListBox.Items.Clear();

            foreach (KeyValuePair<Int32, Pack> pack in PackManager.Instance.Packs)
            {
                packsListBox.Items.Add(pack.Value);
            }
        }
    }
}
