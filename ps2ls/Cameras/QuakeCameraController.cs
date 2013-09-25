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
    public class QuakeCameraController : CameraController
    {
        private Point mouseLocation;

        private QuakeCamera camera;

        private Vector3 localVelocity;
        private Vector3 localVelocityTarget;

        public QuakeCameraController(QuakeCamera camera)
        {
            this.camera = camera;
        }

        public override Camera Camera
        {
            get { return camera; }
        }

        public override void OnMouseDown(object sender, MouseEventArgs e)
        {
        }

        public override void OnMouseUp(object sender, MouseEventArgs e)
        {
        }

        public override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseLocation.X == 0 && mouseLocation.Y == 0)
            {
                mouseLocation = e.Location;
                return;
            }

            int deltaX = e.Location.X - mouseLocation.X;
            int deltaY = e.Location.Y - mouseLocation.Y;

            Camera.Yaw -= MathHelper.DegreesToRadians(0.125f * deltaX);
            Camera.Pitch += MathHelper.DegreesToRadians(0.125f * deltaY);

            mouseLocation = e.Location;
        }

        public override void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W:
                    localVelocityTarget.Z = 1.0f;
                    break;
                case Keys.A:
                    localVelocityTarget.X = 1.0f;
                    break;
                case Keys.S:
                    localVelocityTarget.Z = -1.0f;
                    break;
                case Keys.D:
                    localVelocityTarget.X = -1.0f;
                    break;
            }
        }

        public override void OnKeyUp(object sender, KeyEventArgs e)
        {
            localVelocityTarget = Vector3.Zero;
        }

        public override void Update()
        {
            Vector3 localVelocityTargetDelta = localVelocityTarget - localVelocity;

            if (localVelocityTargetDelta.Length <= 0.001f)
            {
                localVelocity = localVelocityTarget;
            }

            localVelocity += localVelocityTargetDelta * 0.125f;

            Matrix4 world = Matrix4.CreateRotationX(Camera.Pitch) * Matrix4.CreateRotationY(Camera.Yaw);
            Vector3 velocity = Vector3.Transform(localVelocity, world);

            camera.Position += velocity * 0.06125f;
        }
    }
}
