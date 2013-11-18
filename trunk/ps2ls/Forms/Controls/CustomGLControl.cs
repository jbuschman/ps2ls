using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using System.Windows.Forms;

namespace ps2ls.Forms.Controls
{
    public abstract class CustomGLControl : GLControl
    {
        public CustomGLControl()
            : base(new OpenTK.Graphics.GraphicsMode(32, 24, 8, 8), 2, 0, OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Tick();
            Render();
        }

        protected override void OnResize(EventArgs e)
        {
            if (Height == 0)
            {
                ClientSize = new System.Drawing.Size(ClientSize.Width, 1);
            }

            base.OnResize(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            Application.Idle += Application_Idle;

            base.OnLoad(e);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (ContainsFocus && Context != null && IsIdle)
            {
                Tick();
                Render();
            }
        }

        public abstract void Tick();
        public abstract void Render();
    }
}
