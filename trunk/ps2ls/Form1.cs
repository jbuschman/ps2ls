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

        private Form1()
        {
            InitializeComponent();

            ImageList imageList = new ImageList();
            imageList.Images.Add(ps2ls.Properties.Resources.box);
            treeView1.ImageList = imageList;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void RefreshTreeView()
        {
            treeView1.Nodes.Clear();

            foreach (KeyValuePair<string, Pack> pack in PackManager.Instance.Packs)
            {
                String packNodeString = pack.Key;

                if (PS2LS.Instance.ShowFullPath == false)
                {
                    packNodeString = Path.GetFileNameWithoutExtension(packNodeString);
                }

                TreeNode packNode = new TreeNode(packNodeString);
                packNode.Tag = pack.Value;
                packNode.ImageIndex = 0;
                packNode.SelectedImageIndex = 0;

                //foreach (KeyValuePair<String, PackFile> file in pack.Value.Files)
                //{
                //    if (file.Key.Contains(searchFilesTextBox.Text) == false)
                //    {
                //        continue;
                //    }

                //    TreeNode fileNode = new TreeNode(file.Key);
                //    fileNode.Tag = file.Value;
                //    fileNode.ImageIndex = 1;
                //    fileNode.SelectedImageIndex = 1;

                //    packNode.Nodes.Add(fileNode);
                //}

                //if (packNode.Nodes.Count > 0)
                //{
                treeView1.Nodes.Add(packNode);
                //}
            }
        }

        public void RefreshDataGridView()
        {
            dataGridView1.Rows.Clear();

            if (treeView1.SelectedNode == null)
            {
                return;
            }

            Pack pack = null;

            try
            {
                pack = (Pack)treeView1.SelectedNode.Tag;
            }
            catch (InvalidCastException) { return; }

            foreach (PackFile file in pack.Files.Values)
            {
                if (file.Name.ToLower().Contains(searchFilesTextBox.Text.ToLower()) == false)
                {
                    continue;
                }

                DataGridViewRow row = new DataGridViewRow();
                row.Tag = file;
                row.CreateCells(dataGridView1, new object[] { file.Name, file.Extension, file.Length / 1024 });

                dataGridView1.Rows.Add(row);
            }

            fileCountStatusLabel.Text = dataGridView1.Rows.Count + "/" + pack.FileCount;
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                String directory = folderBrowserDialog1.SelectedPath;

                PackManager.Instance.ExtractAllToDirectory(directory);
            }
        }

        private void showFullPathButton_Click(object sender, EventArgs e)
        {
            PS2LS.Instance.ShowFullPath = showFullPathButton.Checked;

            RefreshTreeView();
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = treeView1.SelectedNode.Tag;

            RefreshDataGridView();
        }

        private void importFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = PS2LS.Instance.PackOpenFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                PackManager.Instance.LoadBinaryFromPaths(PS2LS.Instance.PackOpenFileDialog.FileNames);
            }
        }

        private void searchFilesTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RefreshDataGridView();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
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
            if (searchFilesTextBox.Text.Length > 0)
            {
                searchFilesTextBox.BackColor = Color.Yellow;
            }
            else
            {
                searchFilesTextBox.BackColor = Color.White;
            }

            RefreshDataGridView();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            extractSelectedFiles.Enabled = dataGridView1.SelectedRows.Count > 0;
        }
    }
}
