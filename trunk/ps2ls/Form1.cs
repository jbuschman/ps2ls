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
using OpenTK;
using OpenTK.Graphics.OpenGL;
using ps2ls.Dme;

namespace ps2ls
{
    public partial class Form1 : Form
    {
        #region Singleton
        private static Form1 instance = null;

        public static void CreateInstance()
        {
            instance = new Form1();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static Form1 Instance { get { return instance; } }
        #endregion

        private Form1()
        {
            InitializeComponent();

            ImageList imageList = new ImageList();
            imageList.Images.Add(ps2ls.Properties.Resources.box);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void RefreshTreeView()
        {
            listBox1.ClearSelected();
            listBox1.Items.Clear();

            foreach (KeyValuePair<Int32, Pack> pack in PackManager.Instance.Packs)
            {
                listBox1.Items.Add(pack.Value);
            }
        }

        private void refreshFiles()
        {
            dataGridView1.SuspendLayout();

            dataGridView1.Rows.Clear();

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
                    if (fileCount >= rowMax)
                    {
                        continue;
                    }

                    PackFile file = pack.Files.Values.ElementAt(i);

                    if (file.Name.ToLower().Contains(searchFilesTextBox.Text.ToLower()) == false)
                    {
                        continue;
                    }

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1, new object[] { file.Name, file.Extension, file.Length / 1024 });
                    row.Tag = file;
                    dataGridView1.Rows.Add(row);

                    ++fileCount;
                }
            }

            dataGridView1.ResumeLayout();

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

            MemoryStream memoryStream = file.Pack.CreateMemoryStreamByName(file.Name);

            Model model = Model.LoadFromStream(memoryStream);

            ModelBrowser.Instance.CurrentModel = model;
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

            refreshFiles();
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

            refreshFiles();

            searchFilesTimer.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileCountMaxComboBox.SelectedIndex = 3;
            backgroundColorToolStripButton.BackColor = ModelBrowser.Instance.BackgroundColor;
        }

        private void fileCountMaxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshFiles();
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

                PackManager.Instance.Packs.Remove(pack.Path.GetHashCode());
            }

            RefreshTreeView();
            refreshFiles();
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Application.Idle += applicationIdle;


        }

        private void applicationIdle(object sender, EventArgs e)
        {
            while (glControl1.Context != null && glControl1.IsIdle)
            {
                render();
            }
        }

        private void render()
        {
            glControl1.MakeCurrent();

            ModelBrowser.Instance.Camera.AspectRatio = (Single)glControl1.ClientSize.Width / (Single)glControl1.ClientSize.Height;
            ModelBrowser.Instance.Camera.Update();

            //clear
            GL.ClearColor(ModelBrowser.Instance.BackgroundColor);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //projection matrix
            Matrix4 projectionMatrix = ModelBrowser.Instance.Camera.ProjectionMatrix;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix);

            //view matrix
            Matrix4 viewMatrix = ModelBrowser.Instance.Camera.ViewMatrix;
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMatrix);

            // debug axes
            GL.Begin(BeginMode.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(Vector3.Zero);
            GL.Vertex3(Vector3.UnitX);
            GL.Color3(Color.Green);
            GL.Vertex3(Vector3.Zero);
            GL.Vertex3(Vector3.UnitY);
            GL.Color3(Color.Blue);
            GL.Vertex3(Vector3.Zero);
            GL.Vertex3(Vector3.UnitZ);
            GL.End();

            Model model = ModelBrowser.Instance.CurrentModel;

            GL.PushAttrib(AttribMask.PolygonBit);

            if (model != null)
            {
                for (Int32 i = 0; i < model.Meshes.Length; ++i)
                {
                    Mesh mesh = model.Meshes[i];

                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    GL.Color3(Color.White);

                    //GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.VertexBufferHandle);
                    //GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.IndexBufferHandle);
                    //GL.DrawElements(BeginMode.Triangles, mesh.Indices.Length, DrawElementsType.UnsignedShort, 0);
                    //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                    //GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

                    GL.Begin(BeginMode.Triangles);
                    for (Int32 j = 0; i < mesh.Indices.Length; ++i)
                    {
                        GL.Vertex3(mesh.Vertices[mesh.Indices[i]].Position);
                    }
                    GL.End();
                }
            }

            GL.PopAttrib();

            glControl1.SwapBuffers();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            render();
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            OpenTK.GLControl glControl = sender as OpenTK.GLControl;

            if (glControl.Height == 0)
                glControl.ClientSize = new System.Drawing.Size(glControl.ClientSize.Width, 1);

            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
        }

        private void backgroundColorToolStripButton_Click(object sender, EventArgs e)
        {
            Color color = ModelBrowser.Instance.ShowBackgroundColorDialog();

            backgroundColorToolStripButton.BackColor = color;
        }

        private void glControl1_KeyUp(object sender, KeyEventArgs e)
        {
            ArcBallCamera camera = (ArcBallCamera)ModelBrowser.Instance.Camera;

            switch (e.KeyCode)
            {
                case Keys.W:
                    camera.Distance -= 1.0f;
                    break;
                case Keys.S:
                    camera.Distance += 1.0f;
                    break;
            }
        }
    }
}
