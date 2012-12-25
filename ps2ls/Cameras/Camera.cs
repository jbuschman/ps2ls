using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Cameras
{
    public class Camera
    {
        public enum Type
        {
            ArcBallCamera
        };

        public Matrix4 View { get; set; }
        public Matrix4 Projection { get; set; }
        public Single AspectRatio { get; set; }
        public Single FieldOfView { get; set; }
        public Single NearPlaneDistance { get; set; }
        public Single FarPlaneDistance { get; set; }
        public Vector3 Position { get; set; }
        public Single Pitch { get; set; }
        public Single Yaw { get; set; }

        public Camera.Type CameraType { get; private set; }

        protected Camera(Camera.Type type)
        {
            CameraType = type;
            FieldOfView = MathHelper.DegreesToRadians(74.0f);
            NearPlaneDistance = (Single)Math.Pow(2, -8);
            FarPlaneDistance = (Single)Math.Pow(2, 16);
        }

        public virtual void Update()
        {
            Projection = Matrix4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearPlaneDistance, FarPlaneDistance);
            
            Matrix4 world = Matrix4.CreateRotationX(Pitch) * Matrix4.CreateRotationY(Yaw);
            Vector3 forward = Vector3.Transform(Vector3.UnitZ, world);

            View = Matrix4.LookAt(Position, Position + forward, Vector3.UnitY);
        }
    }
}