using OpenTK.Graphics;
using ps2ls.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.Forms
{
    public class ZoneBrowserGLControl : CustomGLControl
    {
        public ZoneBrowserGLControl()
            : base()
        {
        }

        public override void Tick()
        {
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
