using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using ps2ls.Cameras;
using ps2ls.Files.Dme;
using ps2ls.Files.Pack;
using System.Diagnostics;

namespace ps2ls.Forms
{
    public partial class ModelBrowser : UserControl
    {
        #region Singleton
        private static ModelBrowser instance = null;

        public static void CreateInstance()
        {
            instance = new ModelBrowser();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static ModelBrowser Instance { get { return instance; } }
        #endregion

        enum RenderMode
        {
            Smooth,
            //Textured
        };

        private Model model = null;
        private ColorDialog backgroundColorDialog = new ColorDialog();
        private Int32 shaderProgram = 0;
        private List<ToolStripButton> renderModeButtons = new List<ToolStripButton>();

        #region Mesh Colors
        // a series of nice pastel colors we'll use to color meshes
        Color[] meshColors = {
                                 Color.FromArgb(162, 206, 250),
                                 Color.FromArgb(244, 228, 139),
                                 Color.FromArgb(206, 128, 236),
                                 Color.FromArgb(212, 201, 158),
                                 Color.FromArgb(252, 247, 158),
                                 Color.FromArgb(162, 140, 166),
                                 Color.FromArgb(224, 166, 157),
                                 Color.FromArgb(199, 188, 183),
                                 Color.FromArgb(226, 247, 150),
                                 Color.FromArgb(128, 197, 167),
                                 Color.FromArgb(219, 152, 223),
                                 Color.FromArgb(241, 167, 249),
                                 Color.FromArgb(131, 179, 175),
                                 Color.FromArgb(167, 167, 151),
                                 Color.FromArgb(230, 163, 139),
                                 Color.FromArgb(176, 165, 128),
                                 Color.FromArgb(168, 199, 185),
                                 Color.FromArgb(231, 166, 254),
                                 Color.FromArgb(153, 177, 250),
                                 Color.FromArgb(163, 251, 178),
                                 Color.FromArgb(246, 198, 243),
                                 Color.FromArgb(198, 220, 216),
                                 Color.FromArgb(242, 235, 193),
                                 Color.FromArgb(145, 195, 137),
                                 Color.FromArgb(135, 186, 207),
                                 Color.FromArgb(254, 187, 169),
                                 Color.FromArgb(238, 207, 158),
                                 Color.FromArgb(166, 178, 208),
                                 Color.FromArgb(165, 137, 128),
                                 Color.FromArgb(250, 218, 178),
                                 Color.FromArgb(144, 223, 183),
                                 Color.FromArgb(252, 175, 224)
                             };
        #endregion

        private ModelBrowser()
        {
            InitializeComponent();

            //HACK: Can't load ModelBrowser.cs in design mode unless we have at least one item for some reason.
            //Clear items after construction.
            modelsListBox.Items.Clear();

            ModelExportForm.CreateInstance();

            Dock = DockStyle.Fill;

            backgroundColorDialog.Color = Color.FromArgb(32, 32, 32);

            renderModeButtons.Add(renderModeWireframeButton);
            renderModeButtons.Add(renderModeSmoothButton);
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
                Console.WriteLine("Compile error!");
                Console.WriteLine(source);
            }
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

            //TODO: Use external file.

            String vertexShaderSource = @"
varying vec3 normal;
varying vec3 lightDirection;

void main() 
{ 
    gl_Position = ftransform();

    gl_FrontColor = gl_Color;

    vec3 eyeVec = vec3(gl_ModelViewProjectionMatrix * gl_Vertex);

    normal = gl_NormalMatrix * gl_Normal;

    lightDirection = -eyeVec;
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

    gl_FragColor = gl_Color * (ambientColor + (diffuseColor * diffuseTerm));
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

        private void update()
        {
            glControl1.Camera.AspectRatio = (Single)glControl1.ClientSize.Width / (Single)glControl1.ClientSize.Height;
            glControl1.Camera.Update();
        }

        private void render()
        {
            glControl1.MakeCurrent();

            //clear
            GL.ClearColor(backgroundColorDialog.Color);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //projection matrix
            Matrix4 projection = glControl1.Camera.Projection;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            //view matrix
            Matrix4 view = glControl1.Camera.View;
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref view);

            if (showAxesButton.Checked)
            {
                // debug axes
                GL.Begin(BeginMode.Lines);
                //x
                GL.Color3(Color.Red);
                GL.Vertex3(Vector3.Zero);
                GL.Vertex3(Vector3.UnitX);
                GL.Vertex3(Vector3.UnitX); GL.Vertex3(Vector3.UnitX + new Vector3(-0.125f, 0.125f, 0.0f));
                GL.Vertex3(Vector3.UnitX); GL.Vertex3(Vector3.UnitX + new Vector3(-0.125f, -0.125f, 0.0f));

                //y
                GL.Color3(Color.Green);
                GL.Vertex3(Vector3.Zero);
                GL.Vertex3(Vector3.UnitY);
                GL.Vertex3(Vector3.UnitY); GL.Vertex3(Vector3.UnitY + new Vector3(0.125f, -0.125f, 0.0f));
                GL.Vertex3(Vector3.UnitY); GL.Vertex3(Vector3.UnitY + new Vector3(-0.125f, -0.125f, 0.0f));

                //z
                GL.Color3(Color.Blue);
                GL.Vertex3(Vector3.Zero);
                GL.Vertex3(Vector3.UnitZ);
                GL.Vertex3(Vector3.UnitZ); GL.Vertex3(Vector3.UnitZ + new Vector3(0, -0.125f, -0.125f));
                GL.Vertex3(Vector3.UnitZ); GL.Vertex3(Vector3.UnitZ + new Vector3(0, 0.125f, -0.125f));

                GL.End();
            }

            if (model != null)
            {
                GL.PushMatrix();

                GL.PushAttrib(AttribMask.PolygonBit | AttribMask.EnableBit | AttribMask.LightingBit | AttribMask.CurrentBit);

                GL.UseProgram(shaderProgram);

                GL.Enable(EnableCap.DepthTest);
                GL.Enable(EnableCap.CullFace);
                GL.CullFace(CullFaceMode.Back);
                GL.FrontFace(FrontFaceDirection.Cw);

                for (Int32 i = 0; i < model.Meshes.Length; ++i)
                {
                    ps2ls.Files.Dme.Mesh mesh = model.Meshes[i];

                    if (renderModeWireframeButton.Checked)
                    {
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                    }
                    //else if (renderModeSmoothButton.Checked)
                    else
                    {
                        GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                    }

                    GL.Color3(meshColors[i % meshColors.Length]);

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

                GL.PushMatrix();

                //bounding box
                if (showBoundingBoxButton.Checked)
                {
                    GL.PushAttrib(AttribMask.CurrentBit | AttribMask.EnableBit);

                    GL.Color3(Color.Red);

                    GL.Enable(EnableCap.DepthTest);

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
            }

            glControl1.SwapBuffers();
        }

        private void ModelBrowserControl_Load(object sender, EventArgs e)
        {
            glControl1.CreateGraphics();

            createShaderProgram();

            Application.Idle += applicationIdle;
        }

        private void applicationIdle(object sender, EventArgs e)
        {
            while (glControl1.Context != null && glControl1.IsIdle)
            {
                update();
                render();
            }
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            OpenTK.GLControl glControl = sender as OpenTK.GLControl;

            if (glControl.Height == 0)
            {
                glControl.ClientSize = new System.Drawing.Size(glControl.ClientSize.Width, 1);
            }

            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            render();
        }

        public override void Refresh()
        {
            base.Refresh();

            refreshModelsListBox();
        }

        private void refreshModelsListBox()
        {
            modelsListBox.Items.Clear();

            List<PackFile> files = new List<PackFile>();
            List<PackFile> dmeFiles = null;

            PackBrowser.Instance.PacksByType.TryGetValue(PackFile.Types.DME, out dmeFiles);

            if (dmeFiles != null)
            {
                files.AddRange(dmeFiles);
            }

            files.Sort(new PackFile.NameComparer());

            if (files != null)
            {
                foreach (PackFile packFile in files)
                {
                    if (showAutoLODModelsButton.Checked == false)
                    {
                        if (packFile.Name.EndsWith("Auto.dme"))
                        {
                            continue;
                        }
                    }

                    if (packFile.Name.IndexOf(searchModelsText.Text, 0, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        modelsListBox.Items.Add(packFile);
                    }
                }
            }

            Int32 count = modelsListBox.Items.Count;
            Int32 max = files != null ? files.Count : 0;

            modelsCountToolStripStatusLabel.Text = count + "/" + max;
        }

        private void searchModelsText_TextChanged(object sender, EventArgs e)
        {
            searchModelsTimer.Stop();
            searchModelsTimer.Start();
        }

        private void searchModelsTimer_Tick(object sender, EventArgs e)
        {
            if (searchModelsText.Text.Length > 0)
            {
                searchModelsText.BackColor = Color.Yellow;
                clearSearchModelsText.Enabled = true;
            }
            else
            {
                searchModelsText.BackColor = Color.White;
                clearSearchModelsText.Enabled = false;
            }

            searchModelsTimer.Stop();

            refreshModelsListBox();
        }

        private void clearSearchModelsText_Click(object sender, EventArgs e)
        {
            searchModelsText.Clear();
        }

        private void modelsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PackFile packFile = null;

            exportSelectedModelsToolStripButton.Enabled = modelsListBox.SelectedItems.Count > 0;

            try
            {
                packFile = (PackFile)modelsListBox.SelectedItem;
            }
            catch (InvalidCastException) { return; }

            System.IO.MemoryStream memoryStream = packFile.Pack.CreateMemoryStreamByName(packFile.Name);

            model = Model.LoadFromStream(packFile.Name, memoryStream);

            ModelBrowserModelStats1.Model = model;

            snapCameraToModel();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            List<String> fileNames = new List<string>();

            foreach (object selectedItem in modelsListBox.SelectedItems)
            {
                PackFile packFile = null;

                try
                {
                    packFile = (PackFile)selectedItem;
                }
                catch (InvalidCastException) { continue; }

                fileNames.Add(packFile.Name);
            }

            ModelExportForm.Instance.FileNames = fileNames;
            ModelExportForm.Instance.ShowDialog();
        }

        private void snapCameraToModel()
        {
            if (model == null)
            {
                return;
            }

            Vector3 center = (model.Max + model.Min) / 2.0f;
            Vector3 extents = (model.Max - model.Min) / 2.0f;

            glControl1.Camera.DesiredTarget = center;
            glControl1.Camera.DesiredDistance = extents.Length * 1.75f;
        }

        private void showAxesButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void showWireframeButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void showAABBButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void renderModeWireframeButton_Click(object sender, EventArgs e)
        {
            foreach (ToolStripButton button in renderModeButtons)
            {
                button.Checked = (sender == button);
            }
        }

        private void showBoundingBoxButton_Click(object sender, EventArgs e)
        {
            glControl1.Invalidate();
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    renderModeWireframeButton.Checked = true;
                    break;
                case Keys.F6:
                    renderModeSmoothButton.Checked = true;
                    break;
            }
        }

        private void renderModeWireframeButton_CheckedChanged(object sender, EventArgs e)
        {
            if (renderModeWireframeButton.Checked)
            {
                foreach (ToolStripButton button in renderModeButtons)
                {
                    if (sender != button)
                    {
                        button.Checked = false;
                    }
                }
            }
        }

        private void renderModeSmoothButton_CheckedChanged(object sender, EventArgs e)
        {
            if (renderModeSmoothButton.Checked)
            {
                foreach (ToolStripButton button in renderModeButtons)
                {
                    if (sender != button)
                    {
                        button.Checked = false;
                    }
                }
            }
        }

        private void glControl1_MouseEnter(object sender, EventArgs e)
        {
            glControl1.Focus();
        }

        private void showAutoLODModelsButton_CheckedChanged(object sender, EventArgs e)
        {
            refreshModelsListBox();
        }
    }
}
