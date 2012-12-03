using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ps2ls.Properties;

namespace ps2ls
{
    public partial class Form1 : Form
    {
        #region Singleton
        private static Form1 _Instance = null;

        public static void CreateInstance()
        {
            _Instance = new Form1();
        }

        public static void DeleteInstance()
        {
            _Instance = null;
        }

        public static Form1 Instance { get { return _Instance; } }
        #endregion

        private List<DataGridViewRow> _RowPool;

        private DataGridViewRow _GetRowFromPool()
        {
            DataGridViewRow row = null;

            if (_RowPool.Count == 0)
            {
                for (Int32 i = 0; i < 1024; ++i)
                {
                    row = new DataGridViewRow();
                    row.CreateCells(dataGridView1, new object[] { "", "", "" });

                    _RowPool.Add(row);
                }
            }

            row = _RowPool.ElementAt(0);

            _RowPool.RemoveAt(0);

            return row;
        }

        private Form1()
        {
            InitializeComponent();

            ImageList imageList = new ImageList();
            imageList.Images.Add(ps2ls.Properties.Resources.box);

            _RowPool = new List<DataGridViewRow>();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void RefreshTreeView()
        {
            listBox1.ClearSelected();
            listBox1.Items.Clear();

            foreach (KeyValuePair<string, Pack> pack in PackManager.Instance.Packs)
            {
                listBox1.Items.Add(pack.Value);
            }
        }

        private void _RefreshFiles()
        {
            if (listBox1.SelectedItem == null)
            {
                for (Int32 i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    _RowPool.Add(dataGridView1.Rows[i]);
                }

                // move this to the pool
                dataGridView1.Rows.Clear();
            }

            ListBox.SelectedObjectCollection packs = listBox1.SelectedItems;

            Int32 rowMax = 0;

            try
            {
                rowMax = Int32.Parse(fileCountMaxComboBox.Items[fileCountMaxComboBox.SelectedIndex].ToString());
            }
            catch (FormatException)
            {
                rowMax = Int32.MaxValue;
            }

            Int32 fileCount = 0;
            Int32 rowCount = 0;

            for(Int32 j = 0; j < packs.Count; ++j)
            {
                Pack pack = null;

                try
                {
                    pack = (Pack)packs[j];
                }
                catch (InvalidCastException) { continue; }

                for (Int32 i = 0; i < pack.Files.Values.Count; ++i)
                {
                    ++fileCount;

                    if (rowCount >= rowMax)
                    {
                        continue;
                    }

                    PackFile file = pack.Files.Values.ElementAt(i);

                    if (file.Name.ToLower().Contains(searchFilesTextBox.Text.ToLower()) == false)
                    {
                        continue;
                    }

                    DataGridViewRow row = null;

                    if (dataGridView1.RowCount > rowCount)
                    {
                        row = dataGridView1.Rows[rowCount];
                    }
                    else
                    {
                        row = _GetRowFromPool();

                        dataGridView1.Rows.Add(row);
                    }

                    row.Tag = file;
                    row.Cells[0].Value = file.Name;
                    row.Cells[1].Value = file.Extension;
                    row.Cells[2].Value = file.Length / 1024;

                    ++rowCount;
                }
            }

            while (dataGridView1.Rows.Count > rowCount)
            {
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.Rows.Count - 1];
                _RowPool.Add(row);

                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            }

            fileCountStatusLabel.Text = dataGridView1.Rows.Count + "/" + fileCount;
            packCountStatusLabel.Text = packs.Count + "/" + PackManager.Instance.Packs.Count;
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                PackFile packFile = (PackFile)(e.Node.Tag);

                if (packFile != null)
                {
                    packFile.Pack.CreateTemporaryFileAndOpen(packFile.Name);
                }
            }
            catch (InvalidCastException) { }
        }

        private void importFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            PackFile file = (PackFile)(dataGridView1.Rows[e.RowIndex].Tag);

            file.Pack.CreateTemporaryFileAndOpen(file.Name);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            searchFilesTextBox.Clear();
        }

        private void extractSelectedFiles_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<PackFile> files = new List<PackFile>();

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    PackFile file = null;

                    try
                    {
                        file = (PackFile)row.Tag;
                    }
                    catch (InvalidCastException) { continue; }

                    files.Add(file);
                }

                PackManager.Instance.ExtractByPackFilesToDirectory(files, folderBrowserDialog1.SelectedPath);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox.Instance.ShowDialog();
        }

        private void searchFilesTextBox_TextChanged(object sender, EventArgs e)
        {
            searchFilesTimer.Stop();
            searchFilesTimer.Start();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            extractSelectedFilesButton.Enabled = dataGridView1.SelectedRows.Count > 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = listBox1.SelectedItem;

            removePacksButton.Enabled = listBox1.SelectedItems.Count > 0;
            extractSelectedPacksButton.Enabled = listBox1.SelectedItems.Count > 0;

            _RefreshFiles();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
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

        private void searchFilesTimer_Tick(object sender, EventArgs e)
        {
            if (searchFilesTextBox.Text.Length > 0)
            {
                searchFilesTextBox.BackColor = Color.Yellow;
                toolStripButton1.Enabled = true;
            }
            else
            {
                searchFilesTextBox.BackColor = Color.White;
                toolStripButton1.Enabled = false;
            }

            _RefreshFiles();

            searchFilesTimer.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileCountMaxComboBox.SelectedIndex = 0;
        }

        private void fileCountMaxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RefreshFiles();
        }

        private void removePacksButton_Click(object sender, EventArgs e)
        {
            foreach (object item in listBox1.SelectedItems)
            {
                Pack pack = null;

                try
                {
                    pack = (Pack)item;
                }
                catch (Exception) { continue; }

                PackManager.Instance.Packs.Remove(pack.Path);
            }

            RefreshTreeView();
            _RefreshFiles();
        }

        private void addPacksButton_Click(object sender, EventArgs e)
        {
            DialogResult result = PS2LS.Instance.PackOpenFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                PackManager.Instance.LoadBinaryFromPaths(PS2LS.Instance.PackOpenFileDialog.FileNames);
            }
        }

        private void extractSelectedPacksButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<PackFile> files = new List<PackFile>();

                foreach (object item in listBox1.SelectedItems)
                {
                    Pack pack = null;

                    try
                    {
                        pack = (Pack)item;
                    }
                    catch (Exception) { continue; }

                    foreach (PackFile file in pack.Files.Values)
                    {
                        files.Add(file);
                    }
                }

                PackManager.Instance.ExtractByPackFilesToDirectory(files, folderBrowserDialog1.SelectedPath);
            }
        }

        private void extractAllPacksButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                String directory = folderBrowserDialog1.SelectedPath;

                PackManager.Instance.ExtractAllToDirectory(directory);
            }
        }
    }
}
