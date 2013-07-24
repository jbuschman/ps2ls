using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Cameras
{
    public class ArcBallCamera : Camera
    {
        public float distance;
        public float Distance
        {
            get { return distance; }
            set
            {
                if (distance == value)
                    return;

                distance = value;

                if (DistanceChanged != null)
                    DistanceChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler DistanceChanged;

        private Vector3 target;
        public Vector3 Target
        {
            get { return target; }
            set
            {
                if (target == value)
                    return;

                target = value;

                if (TargetChanged != null)
                    TargetChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler TargetChanged;

        public ArcBallCamera()
            : base(Camera.Types.ArcBall)
        {
            Yaw = MathHelper.DegreesToRadians(-45.0f);
            Pitch = MathHelper.DegreesToRadians(45.0f);
            distance = 10.0f;
        }

        public override void Update()
        {
            distance = Math.Max(Single.Epsilon, Distance);

            Matrix4 world = Matrix4.CreateRotationX(Pitch) * Matrix4.CreateRotationY(Yaw);
            Vector3 forward = Vector3.Transform(Vector3.UnitZ, world);

            Position = Target - (forward * Distance);

            base.Update();
        }
    }
}
