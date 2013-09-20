using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ps2ls.Cameras
{
    public class QuakeCamera : Camera
    {
        //Vector3 LocalVelocity;

        public QuakeCamera()
        {
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
