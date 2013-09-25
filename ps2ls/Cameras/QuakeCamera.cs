using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ps2ls.Cameras
{
    public class QuakeCamera : Camera
    {
        private const float YAW_DEFAULT = -45.0f;
        private const float PITCH_DEFAULT = 45.0f;

        public QuakeCamera()
        {
            Pitch = PITCH_DEFAULT;
            Yaw = YAW_DEFAULT;
            Position = new Vector3(10, 10, 10);
        }

        public override void Update()
        {
            //Matrix4 rotation = Matrix4.CreateRotationX(Pitch) * Matrix4.CreateRotationY(Yaw);
            //Vector3 velocity = Vector3.Transform(LocalVelocity, rotation);

            //Position += velocity;

            base.Update();
        }
    }
}
