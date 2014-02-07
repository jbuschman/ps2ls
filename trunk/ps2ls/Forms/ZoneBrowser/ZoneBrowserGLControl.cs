using OpenTK.Graphics.OpenGL;
using ps2ls.Cameras;
using ps2ls.Forms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.Forms
{
    public class ZoneBrowserGLControl : CustomGLControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ArcBallCamera Camera { get; set; }

        public ZoneBrowserGLControl()
            : base()
        {
            Camera = new ArcBallCamera();
        }

        public override void Tick()
        {
            Camera.AspectRatio = (float)ClientSize.Width / (float)ClientSize.Height;
            Camera.Update();
        }

        public override void Render()
        {
            if (DesignMode)
                return;

            MakeCurrent();

            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            SwapBuffers();
        }
    }
}
