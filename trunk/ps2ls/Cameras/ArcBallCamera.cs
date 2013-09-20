using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Cameras
{
    public class ArcBallCamera : Camera
    {
        private const float DISTANCE_DEFAULT = 10.0f;
        private const float DISTANCE_MIN = 1.0f;
        private const float DISTANCE_MAX = 1024.0f;

        private const float YAW_DEFAULT = -45.0f;
        private const float PITCH_DEFAULT = 45.0f;

        public float distance;
        public float Distance
        {
            get { return distance; }
            set
            {
                if (distance == value)
                    return;

                distance = Utils.Clamp(value, DISTANCE_MIN, DISTANCE_MAX);

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
        {
            Yaw = MathHelper.DegreesToRadians(YAW_DEFAULT);
            Pitch = MathHelper.DegreesToRadians(PITCH_DEFAULT);
            distance = DISTANCE_DEFAULT;
        }

        public override void Update()
        {
            distance = Utils.Clamp(distance, DISTANCE_MIN, DISTANCE_MAX);

            Matrix4 world = Matrix4.CreateRotationX(Pitch) * Matrix4.CreateRotationY(Yaw);
            Vector3 forward = Vector3.Transform(Vector3.UnitZ, world);

            Position = Target - (forward * Distance);

            base.Update();
        }
    }
}
