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
using OpenTK.Input;
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

        Point locationOld;
        bool rotating = false;
        bool zooming = false;
        bool panning = false;

        Int32 shaderProgram = 0;

        Vector4 lightDirection = -Vector4.UnitY;
        Color lightDiffuse = Color.White;

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

                    String extension = Path.GetExtension(file.Name);

                    Image icon = getIconFromFileExtension(extension);

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1, new object[] { icon, file.Name, file.Type, file.Length / 1024 });
                    row.Tag = file;
                    dataGridView1.Rows.Add(row);

                    ++fileCount;
                }
            }

            dataGridView1.ResumeLayout();

            fileCountStatusLabel.Text = dataGridView1.Rows.Count + "/" + fileCount;
            packCountStatusLabel.Text = packs.Count + "/" + PackManager.Instance.Packs.Count;
        }

        private Image getIconFromFileExtension(String extension)
        {
            if (extension == ".dme")
            {
                return Properties.Resources.tree;
            }
            else if (extension == ".dds")
            {
                return Properties.Resources.image;
            }
            else if (extension == ".txt")
            {
                return Properties.Resources.document_tex;
            }
            else if (extension == ".xml")
            {
                return Properties.Resources.document_xaml;
            }
            else if (extension == ".fsb")
            {
                return Properties.Resources.music;
            }

            return Properties.Resources.question;
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

            if (file.Type == PackFile.Types.DME)
            {
                MemoryStream memoryStream = file.Pack.CreateMemoryStreamByName(file.Name);
                Model model = Model.LoadFromStream(memoryStream);

                if (model != null)
                {
                    ModelBrowser.Instance.CurrentModel = model;

                    tabControl1.SelectedIndex = 1;

                    return;
                }
            }
            
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
            //createShaderProgram();

            fileCountMaxComboBox.SelectedIndex = 3;
            backgroundColorToolStripButton.BackColor = ModelBrowser.Instance.BackgroundColor;
        }

        private void createShaderProgram()
        {
            ErrorCode e;

            GL.GetError(); //clear error

            shaderProgram = GL.CreateProgram();
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }

            Int32 vertexShader = GL.CreateShader(ShaderType.VertexShader);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            Int32 fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }

            //using this shader as a test for now, will make selectable ones later.
            //http://www.lighthouse3d.com/tutorials/glsl-tutorial/directional-light-per-pixel/



            String vertexShaderSource = @"
varying vec3 normal;
varying vec3 lightDirection;

void main() 
{ 
    gl_Position = ftransform();

    normal = gl_Normal;

    lightDirection = vec3(0, -1, 0);
}
";
            String fragmentShaderSource = @"
varying vec3 normal; 
varying vec3 lightDirection;

void main()
{
    const vec4 ambientColor = vec4(0.25, 0.25, 0.25, 1.0);
    const vec4 diffuseColor = vec4(1.0, 1.0, 1.0, 1.0);

    vec3 normalizedNormal = normalize(normal);
    vec3 noralizedLightDirection = normalize(lightDirection);

    float diffuseTerm = clamp(dot(normalizedNormal, noralizedLightDirection), 0.0, 1.0);

    gl_FragColor = ambientColor + (diffuseColor * diffuseTerm);
}
";

            compileShader(vertexShader, vertexShaderSource);
            compileShader(fragmentShader, fragmentShaderSource);

            GL.AttachShader(shaderProgram, fragmentShader);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            GL.AttachShader(shaderProgram, vertexShader);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            GL.LinkProgram(shaderProgram);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }

            String info;
            GL.GetProgramInfoLog(shaderProgram, out info);
            Console.WriteLine(info);

            if (fragmentShader != 0)
            {
                GL.DeleteShader(fragmentShader);
                if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            }

            if (vertexShader != 0)
            {
                GL.DeleteShader(vertexShader);
                if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            }
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

            glControl1.CreateGraphics();    // initialize the GL context right up front.

            createShaderProgram();

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
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //projection matrix
            Matrix4 projection = ModelBrowser.Instance.Camera.Projection;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            //view matrix
            Matrix4 view = ModelBrowser.Instance.Camera.View;
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref view);

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

            if (model != null)
            {
                GL.PushAttrib(AttribMask.PolygonBit | AttribMask.EnableBit | AttribMask.LightingBit);

                GL.UseProgram(shaderProgram);

                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.Lighting);
                GL.Enable(EnableCap.Light0);

                GL.Light(LightName.Light0, LightParameter.Position, lightDirection);

                for (Int32 i = 0; i < model.Meshes.Length; ++i)
                {
                    Mesh mesh = model.Meshes[i];

                    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                    GL.Color3(Color.White);

                    GL.Begin(BeginMode.Triangles);
                    for (Int32 j = 0; j < mesh.Indices.Length; ++j)
                    {
                        GL.Normal3(mesh.Vertices[mesh.Indices[j]].Normal);
                        GL.Vertex3(mesh.Vertices[mesh.Indices[j]].Position);
                    }
                    GL.End();
                }

                GL.UseProgram(0);

                GL.PopAttrib();

                //bounding box
                GL.PushAttrib(AttribMask.CurrentBit | AttribMask.EnableBit);

                GL.Enable(EnableCap.DepthTest);

                GL.Color3(Color.Red);

                Vector3 min = model.Min;
                Vector3 max = model.Max;
                Vector3[] vertices = new Vector3[8];
                UInt32[] indices = { 0, 1, 1, 2, 2, 3, 3, 0, 0, 4, 1, 5, 2, 6, 3, 7, 4, 5, 5, 6, 6, 7, 7, 4 };

                vertices[0] = min;
                vertices[1] = new Vector3(max.X, min.Y, min.Z);
                vertices[2] = new Vector3(max.X, min.Y, max.Z);
                vertices[3] = new Vector3(min.X, min.Y, max.Z);
                vertices[4] = new Vector3(min.X, max.Y, min.Z);
                vertices[5] = new Vector3(max.X, max.Y, min.Z);
                vertices[6] = max;
                vertices[7] = new Vector3(min.X, max.Y, max.Z);

                GL.EnableClientState(ArrayCap.VertexArray);
                GL.VertexPointer(3, VertexPointerType.Float, 0, vertices);
                GL.DrawRangeElements(BeginMode.Lines, 0, 23, 24, DrawElementsType.UnsignedInt, indices);

                GL.PopAttrib();
            }

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

        private void glControl1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    zooming = true;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    panning = true;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    rotating = true;
                    break;
                default:
                    break;
            }
        }

        private void glControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (locationOld.X == 0 && locationOld.Y == 0)
            {
                locationOld = e.Location;
            }

            Int32 deltaX = e.Location.X - locationOld.X;
            Int32 deltaY = e.Location.Y - locationOld.Y;

            locationOld = e.Location;

            if (rotating)
            {
                ModelBrowser.Instance.Camera.Yaw -= MathHelper.DegreesToRadians(deltaX * 0.25f);
                ModelBrowser.Instance.Camera.Pitch += MathHelper.DegreesToRadians(deltaY * 0.25f);
            }
            else if (zooming)
            {
                ArcBallCamera arcBallCamera = (ArcBallCamera)ModelBrowser.Instance.Camera;

                arcBallCamera.Distance += deltaY * 0.0625f;
            }
            else if (panning)
            {
                ArcBallCamera arcBallCamera = (ArcBallCamera)ModelBrowser.Instance.Camera;

                Matrix4 world = Matrix4.CreateRotationX(arcBallCamera.Pitch) * Matrix4.CreateRotationY(arcBallCamera.Yaw);
                Vector3 forward = Vector3.Transform(Vector3.UnitZ, world);
                Vector3 right = Vector3.Cross(Vector3.UnitY, forward);

                arcBallCamera.Target += right * deltaX * 0.03125f;
                arcBallCamera.Target += Vector3.UnitY * deltaY * 0.03125f;
            }
        }

        private void glControl1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    zooming = false;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    panning = false;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    rotating = false;
                    break;
                default:
                    break;
            }
        }

        private void compileShader(Int32 shader, String source)
        {
            ErrorCode e;

            GL.ShaderSource(shader, source);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }
            GL.CompileShader(shader);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }

            String info = String.Empty;
            GL.GetShaderInfoLog(shader, out info);
            Console.WriteLine(info);

            Int32 compileResult;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out compileResult);
            if ((e = GL.GetError()) != ErrorCode.NoError) { Console.WriteLine(e); }

            if (compileResult != 1)
            {
                Console.WriteLine("CompileError!");
                Console.WriteLine(source);
            }
        }
    }
}
