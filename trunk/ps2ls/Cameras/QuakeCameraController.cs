using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ps2ls.Cameras
{
    class QuakeCameraController : CameraController
    {
        private QuakeCamera camera;

        QuakeCameraController(QuakeCamera camera)
        {
            this.camera = camera;
        }

        public override Camera Camera
        {
            get { return camera; }
        }

        public override void OnMouseDown(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnMouseUp(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnKeyUp(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
