using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.Cameras;
using ps2ls.Forms.Controls;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using ps2ls.Assets.Dme;
using ps2ls.Graphics.Materials;

namespace ps2ls.Forms
{
    public partial class ModelBrowserGLControl : CustomGLControl
    {
        #region Mesh Colors
        // a series of nice pastel colors we'll use to color meshes
        private Color[] meshColors = {
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

        public enum RenderModes
        {
            WireFrame,
            Smooth
        }

        public enum InputTypes
        {
            None,
            Rotate,
            Pan,
            Zoom
        }

        private Model model;
        public Model Model
        {
            get { return model; }
            set
            {
                if (model == value)
                    return;

                model = value;

                if (ModelChanged != null)
                    ModelChanged.Invoke(this, EventArgs.Empty);

                Invalidate();
            }
        }
        public event EventHandler ModelChanged;

        private bool drawAxes;
        public bool DrawAxes
        {
            get { return drawAxes; }
            set
            {
                if(drawAxes == value)
                    return;

                drawAxes = value;

                if (DrawAxesChanged != null)
                    DrawAxesChanged.Invoke(this, EventArgs.Empty);

                Invalidate();
            }
        }
        public event EventHandler DrawAxesChanged;

        private RenderModes renderMode = RenderModes.Smooth;
        public RenderModes RenderMode
        {
            get { return renderMode; }
            set
            {
                if (renderMode == value)
                    return;

                renderMode = value;

                if (RenderModeChanged != null)
                    RenderModeChanged.Invoke(this, EventArgs.Empty);

                Invalidate();
            }
        }
        public event EventHandler RenderModeChanged;

        private InputTypes inputType { get; set; }
        private Point location;

        private int shaderProgram;
        private int currentTexture;

        public ArcBallCamera Camera { get; set; }

        public ModelBrowserGLControl()
        {
            Camera = new ArcBallCamera();

            InitializeComponent();

            KeyDown += ModelBrowserGLControl_KeyDown;
            MouseDown += ModelBrowserGLControl_MouseDown;
            MouseUp += ModelBrowserGLControl_MouseUp;
            MouseMove += ModelBrowserGLControl_MouseMove;
            MouseEnter += ModelBrowserGLControl_MouseEnter;
        }


        //TODO: move this elsehwere
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

        //TODO: move this elsehwere
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

            //TODO: Use external shader source files.
            String vertexShaderSource = @"
void main(void)
{
    gl_Position = ftransform();

    gl_TexCoord[0] = gl_MultiTexCoord0;
}
";
            String fragmentShaderSource = @"
uniform sampler2D colorMap;

void main(void)
{
   vec4 col = texture2D(colorMap, gl_TexCoord[0].st);
   if(col.a <= 0) discard;
   gl_FragColor = texture2D(colorMap, gl_TexCoord[0].st);
}
";

            compileShader(vertexShader, vertexShaderSource);
            int res = 0;
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out res);
            if ((All)res == All.False)
            {
                throw new Exception(GL.GetShaderInfoLog(vertexShader));
            }
            compileShader(fragmentShader, fragmentShaderSource);
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out res);
            if ((All)res == All.False)
            {
                throw new Exception(GL.GetShaderInfoLog(vertexShader));
            }

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


        void ModelBrowserGLControl_MouseEnter(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void ModelBrowserGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    inputType = InputTypes.Rotate;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    inputType = InputTypes.Pan;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    inputType = InputTypes.Zoom;
                    break;
            }
        }

        private void ModelBrowserGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            inputType = InputTypes.None;
        }

        private void ModelBrowserGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (location.X == 0 && location.Y == 0)
            {
                location = e.Location;
            }

            Int32 deltaX = e.Location.X - location.X;
            Int32 deltaY = e.Location.Y - location.Y;

            switch (inputType)
            {
                case InputTypes.Pan:
                    {
                        Matrix4 world = Matrix4.CreateFromAxisAngle(Vector3.UnitX, Camera.Pitch) * Matrix4.CreateFromAxisAngle(Vector3.UnitY, Camera.Yaw);

                        Vector3 forward = Vector3.Transform(Vector3.UnitZ, world);
                        forward.Y = 0;
                        forward.Normalize();

                        Vector3 up = Vector3.UnitY;
                        Vector3 left = Vector3.Cross(up, forward);

                        Camera.DesiredTarget += (up * deltaY) * 0.00390625f;
                        Camera.DesiredTarget += (left * deltaX) * 0.00390625f;
                    }
                    break;
                case InputTypes.Rotate:
                    {
                        Camera.DesiredYaw -= MathHelper.DegreesToRadians(0.25f * deltaX);
                        Camera.DesiredPitch += MathHelper.DegreesToRadians(0.25f * deltaY);
                    }
                    break;
                case InputTypes.Zoom:
                    {
                        Camera.DesiredDistance -= deltaY * 0.015625f;
                    }
                    break;
            }

            location = e.Location;
        }

        private void ModelBrowserGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    RenderMode = RenderModes.Smooth;
                    break;
                case Keys.F6:
                    RenderMode = RenderModes.WireFrame;
                    break;
            }
        }

        public override void Tick()
        {
            Camera.AspectRatio = (Single)ClientSize.Width / (Single)ClientSize.Height;
            Camera.Update();
        }

        public override void Render()
        {
            MakeCurrent();

            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            //clear
            GL.ClearColor(Color.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //projection matrix
            Matrix4 projection = Camera.Projection;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            //view matrix
            Matrix4 view = Camera.View;
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref view);

            if (DrawAxes)
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

            if (Model != null && Model.Version == 4)
            {
                GL.PushMatrix();

                GL.PushAttrib(AttribMask.PolygonBit | AttribMask.EnableBit | AttribMask.LightingBit | AttribMask.CurrentBit);

                GL.UseProgram(shaderProgram);

                GL.Enable(EnableCap.DepthTest);
                GL.Disable(EnableCap.CullFace);
                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                GL.FrontFace(FrontFaceDirection.Cw);

                GL.ActiveTexture(TextureUnit.Texture0);
                GL.BindTexture(TextureTarget.Texture2D, currentTexture);

                int loc = GL.GetUniformLocation(shaderProgram, "colorMap");
                GL.Uniform1(loc, 0);

                for (Int32 i = 0; i < Model.Meshes.Length; ++i)
                {
                    Mesh mesh = Model.Meshes[i];

                    //pin handles to stream data
                    GCHandle[] streamDataGCHandles = new GCHandle[mesh.VertexStreams.Length];

                    for (Int32 j = 0; j < streamDataGCHandles.Length; ++j)
                    {
                        streamDataGCHandles[j] = GCHandle.Alloc(mesh.VertexStreams[j].Data, GCHandleType.Pinned);
                    }

                    //fetch material definition and vertex layout
                    MaterialDefinition materialDefinition = MaterialDefinitionManager.Instance.MaterialDefinitions[model.Materials[(Int32)mesh.MaterialIndex].MaterialDefinitionHash];
                    VertexLayout vertexLayout = MaterialDefinitionManager.Instance.VertexLayouts[materialDefinition.DrawStyles[0].VertexLayoutNameHash];

                    GL.Color3(meshColors[i % meshColors.Length]);

                    switch (RenderMode)
                    {
                        case RenderModes.WireFrame:
                            {
                                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
                            }
                            break;
                        case RenderModes.Smooth:
                            {
                                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                            }
                            break;
                    }

                    //position
                    VertexLayout.Entry.DataTypes positionDataType = VertexLayout.Entry.DataTypes.None;
                    Int32 positionStream = 0;
                    Int32 positionOffset = 0;
                    bool positionExists = vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Position, 0, out positionDataType, out positionStream, out positionOffset);

                    if (positionExists)
                    {
                        IntPtr positionData = streamDataGCHandles[positionStream].AddrOfPinnedObject();

                        GL.EnableClientState(ArrayCap.VertexArray);
                        GL.VertexPointer(3, VertexPointerType.Float, mesh.VertexStreams[positionStream].BytesPerVertex, positionData + positionOffset);
                    }

                    //normal
                    VertexLayout.Entry.DataTypes normalDataType = VertexLayout.Entry.DataTypes.None;
                    Int32 normalStream = 0;
                    Int32 normalOffset = 0;
                    bool normalExists = vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Normal, 0, out normalDataType, out normalStream, out normalOffset);

                    if (normalExists)
                    {
                        IntPtr normalData = streamDataGCHandles[normalStream].AddrOfPinnedObject();

                        GL.EnableClientState(ArrayCap.NormalArray);
                        GL.NormalPointer(NormalPointerType.Float, mesh.VertexStreams[normalStream].BytesPerVertex, normalData + normalOffset);
                    }

                    //texture coordiantes
                    VertexLayout.Entry.DataTypes texCoord0DataType = VertexLayout.Entry.DataTypes.None;
                    Int32 texCoord0Stream = 0;
                    Int32 texCoord0Offset = 0;
                    bool texCoord0Exists = vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Texcoord, 0, out texCoord0DataType, out texCoord0Stream, out texCoord0Offset);

                    if (texCoord0Exists)
                    {
                        IntPtr texCoord0Data = streamDataGCHandles[texCoord0Stream].AddrOfPinnedObject();

                        GL.EnableClientState(ArrayCap.TextureCoordArray);

                        TexCoordPointerType texCoord0PointerType = TexCoordPointerType.Float;

                        switch (texCoord0DataType)
                        {
                            case VertexLayout.Entry.DataTypes.Float2:
                                texCoord0PointerType = TexCoordPointerType.Float;
                                break;
                            case VertexLayout.Entry.DataTypes.float16_2:
                                texCoord0PointerType = TexCoordPointerType.HalfFloat;
                                break;
                            default:
                                break;
                        }

                        GL.TexCoordPointer(2, texCoord0PointerType, mesh.VertexStreams[texCoord0Stream].BytesPerVertex, texCoord0Data + texCoord0Offset);
                    }

                    //indices
                    GCHandle indexDataHandle = GCHandle.Alloc(mesh.IndexData, GCHandleType.Pinned);
                    IntPtr indexData = indexDataHandle.AddrOfPinnedObject();

                    GL.DrawElements(BeginMode.Triangles, (Int32)mesh.IndexCount, DrawElementsType.UnsignedShort, indexData);

                    indexDataHandle.Free();

                    GL.DisableClientState(ArrayCap.VertexArray);
                    GL.DisableClientState(ArrayCap.NormalArray);
                    GL.DisableClientState(ArrayCap.TextureCoordArray);

                    //free stream data handles
                    for (Int32 j = 0; j < streamDataGCHandles.Length; ++j)
                    {
                        streamDataGCHandles[j].Free();
                    }
                }

                GL.UseProgram(0);

                GL.PopAttrib();
            }

            SwapBuffers();
        }
    }
}
