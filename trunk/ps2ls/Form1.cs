﻿using System;
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
            //listBox1.ImageList = imageList;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void RefreshTreeView()
        {
            listBox1.Items.Clear();

            foreach (KeyValuePair<string, Pack> pack in PackManager.Instance.Packs)
            {
                String packNodeString = pack.Key;

                if (PS2LS.Instance.ShowFullPath == false)
                {
                    packNodeString = Path.GetFileNameWithoutExtension(packNodeString);
                }

                Int32 count = 0;

                foreach (KeyValuePair<String, PackFile> file in pack.Value.Files)
                {
                    if (file.Key.Contains(searchFilesTextBox.Text) == false)
                    {
                        continue;
                    }

                    ++count;
                }

                listBox1.Items.Add(pack.Value);
            }
        }

        public void RefreshDataGridView()
        {
            dataGridView1.Rows.Clear();

            if (listBox1.SelectedItem == null)
            {
                return;
            }

            ListBox.SelectedObjectCollection packs = listBox1.SelectedItems;

            Int32 fileCount = 0;

            foreach (object item in packs)
            {
                Pack pack = null;

                try
                {
                    pack = (Pack)item;
                }
                catch (InvalidCastException) { continue; }

                foreach (PackFile file in pack.Files.Values)
                {
                    ++fileCount;

                    if (file.Name.ToLower().Contains(searchFilesTextBox.Text.ToLower()) == false)
                    {
                        continue;
                    }

                    DataGridViewRow row = new DataGridViewRow();
                    row.Tag = file;
                    row.CreateCells(dataGridView1, new object[] { file.Name, file.Extension, file.Length / 1024 });

                    dataGridView1.Rows.Add(row);
                }
            }

            fileCountStatusLabel.Text = dataGridView1.Rows.Count + "/" + fileCount;
            packCountStatusLabel.Text = packs.Count + "/" + PackManager.Instance.Packs.Count;
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
                toolStripButton1.Enabled = true;
            }
            else
            {
                searchFilesTextBox.BackColor = Color.White;
                toolStripButton1.Enabled = false;
            }

            //RefreshTreeView();
            RefreshDataGridView();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            extractSelectedFiles.Enabled = dataGridView1.SelectedRows.Count > 0;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = listBox1.SelectedItem;

            listBox1.Invalidate();

            RefreshDataGridView();
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
        }
    }
}
