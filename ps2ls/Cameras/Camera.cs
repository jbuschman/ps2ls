using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Cameras
{
    public class Camera
    {
        public enum Types
        {
            ArcBall,
            Quake
        };

        private Matrix4 view;
        public Matrix4 View
        {
            get { return view; }
            protected set
            {
                view = value;

                if (ViewChanged != null)
                    ViewChanged.Invoke(this, null);
            }
        }
        private Matrix4 projection;
        public Matrix4 Projection
        {
            get { return projection; }
            protected set
            {
                projection = value;

                if (ProjectionChanged != null)
                    ProjectionChanged.Invoke(this, null);
            }
        }
        private Single aspectRatio;
        public Single AspectRatio
        {
            get { return aspectRatio; }
            set
            {
                aspectRatio = value;

                if (AspectRatioChanged != null)
                    AspectRatioChanged.Invoke(this, null);
            }
        }
        private Single fieldOfView;
        public Single FieldOfView
        {
            get { return fieldOfView; }
            set
            {
                fieldOfView = Utils.Clamp(value, float.Epsilon, MathHelper.PiOver2);

                if(FieldOfViewChanged != null)
                    FieldOfViewChanged.Invoke(this, null);
            }
        }
        private Single nearPlaneDistance;
        public Single NearPlaneDistance
        {
            get
            {
                return nearPlaneDistance;
            }
            set
            {
                nearPlaneDistance = value;

                if(NearPlaneDistanceChanged != null)
                    NearPlaneDistanceChanged.Invoke(this, null);
            }
        }
        private Single farPlaneDistance;
        public Single FarPlaneDistance
        {
            get
            {
                return farPlaneDistance;
            }
            set
            {
                farPlaneDistance = value;

                if(FarPlaneDistanceChanged != null)
                    FarPlaneDistanceChanged.Invoke(this, null);
            }
        }
        private Vector3 position;
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;

                if (PositionChanged != null)
                    PositionChanged.Invoke(this, null);
            }
        }
        private Single pitch;
        public Single Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;

                if(PitchChanged != null)
                    PitchChanged.Invoke(this, null);
            }
        }
        private Single yaw;
        public Single Yaw
        {
            get { return yaw; }
            set
            {
                yaw = value;

                if(YawChanged != null)
                    YawChanged.Invoke(this, null);
            }
        }

        public event EventHandler ViewChanged;
        public event EventHandler ProjectionChanged;
        public event EventHandler AspectRatioChanged;
        public event EventHandler FieldOfViewChanged;
        public event EventHandler NearPlaneDistanceChanged;
        public event EventHandler FarPlaneDistanceChanged;
        public event EventHandler PositionChanged;
        public event EventHandler PitchChanged;
        public event EventHandler YawChanged;

        public Camera.Types CameraType { get; private set; }

        protected Camera(Camera.Types type)
        {
            CameraType = type;
            FieldOfView = MathHelper.DegreesToRadians(75.0f);
            NearPlaneDistance = (Single)Math.Pow(2, -8);
            FarPlaneDistance = (Single)Math.Pow(2, 8);
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