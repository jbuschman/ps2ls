using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ps2ls.Cameras
{
    public class ArcBallCameraController : CameraController, IDisposable
    {
        private Point location;
        private ArcBallCamera camera;
        public override Camera Camera
        {
            get { return camera; }
        }

        public enum InputTypes
        {
            None,
            Rotate,
            Pan,
            Zoom
        }
        private InputTypes inputType;

        public ArcBallCameraController(ArcBallCamera camera)
        {
            this.camera = camera;
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
        }

        public override void OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        public override void OnMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    inputType = InputTypes.Rotate;
                    break;
                case MouseButtons.Right:
                    inputType = InputTypes.Pan;
                    break;
                case MouseButtons.Middle:
                    inputType = InputTypes.Zoom;
                    break;
            }
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (location.X == 0 && location.Y == 0)
            {
                location = e.Location;
            }

            int deltaX = e.Location.X - location.X;
            int deltaY = e.Location.Y - location.Y;

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

                        camera.Target += (up * deltaY) * 0.00390625f;
                        camera.Target += (left * deltaX) * 0.00390625f;
                    }
                    break;
                case InputTypes.Rotate:
                    {
                        Camera.Yaw -= MathHelper.DegreesToRadians(0.25f * deltaX);
                        Camera.Pitch += MathHelper.DegreesToRadians(0.25f * deltaY);
                    }
                    break;
                case InputTypes.Zoom:
                    {
                        camera.Distance -= deltaY * 0.015625f;
                    }
                    break;
            }

            location = e.Location;
        }

        public override void OnMouseUp(object sender, MouseEventArgs e)
        {
            inputType = InputTypes.None;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
