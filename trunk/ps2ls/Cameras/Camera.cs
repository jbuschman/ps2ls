using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Cameras
{
    public class Camera
    {
        private const float FIELD_OF_VIEW_DEFAULT = 75.0f;
        private const float FIELD_OF_VIEW_MIN = 10.0f;
        private const float FIELD_OF_VIEW_MAX = 90.0f;

        private const float PITCH_DEFAULT = 45.0f;
        private const float PITCH_MIN = -89.9f;
        private const float PITCH_MAX = 89.9f;

        private const float NEAR_PLANE_DISTANCE_DEFAULT = 0.00390625f;
        private const float FAR_PLANE_DISTANCE_DEFAULT = 256.0f;

        private Matrix4 view;
        private Matrix4 projection;
        private float aspectRatio;
        private float fieldOfView;
        private float nearPlaneDistance;
        private float farPlaneDistance;
        private Vector3 position;
        private float pitch;
        private float yaw;
        private CameraController controller;

        public Matrix4 View
        {
            get { return view; }
            protected set
            {
                if (view == value)
                    return;

                view = value;

                if (ViewChanged != null)
                    ViewChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public Matrix4 Projection
        {
            get { return projection; }
            protected set
            {
                if (projection == value)
                    return;

                projection = value;

                if (ProjectionChanged != null)
                    ProjectionChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public float AspectRatio
        {
            get { return aspectRatio; }
            set
            {
                if (aspectRatio == value)
                    return;

                aspectRatio = value;

                if (AspectRatioChanged != null)
                    AspectRatioChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public float FieldOfView
        {
            get { return fieldOfView; }
            set
            {
                if (fieldOfView == value)
                    return;

                fieldOfView = Utils.Clamp(value, MathHelper.DegreesToRadians(FIELD_OF_VIEW_MIN), MathHelper.DegreesToRadians(FIELD_OF_VIEW_MAX));

                if(FieldOfViewChanged != null)
                    FieldOfViewChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public float NearPlaneDistance
        {
            get
            {
                return nearPlaneDistance;
            }
            set
            {
                if (nearPlaneDistance == value)
                    return;

                nearPlaneDistance = value;

                if(NearPlaneDistanceChanged != null)
                    NearPlaneDistanceChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public float FarPlaneDistance
        {
            get
            {
                return farPlaneDistance;
            }
            set
            {
                if (farPlaneDistance == value)
                    return;

                farPlaneDistance = value;

                if(FarPlaneDistanceChanged != null)
                    FarPlaneDistanceChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                if (position == value)
                    return;

                position = value;

                if (PositionChanged != null)
                    PositionChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public float Pitch
        {
            get { return pitch; }
            set
            {
                if (pitch == value)
                    return;

                pitch = Utils.Clamp(value, MathHelper.DegreesToRadians(PITCH_MIN), MathHelper.DegreesToRadians(PITCH_MAX));

                if(PitchChanged != null)
                    PitchChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public float Yaw
        {
            get { return yaw; }
            set
            {
                if (yaw == value)
                    return;

                yaw = value;

                if(YawChanged != null)
                    YawChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public CameraController Controller
        {
            get { return controller; }
            set
            {
                if (controller == value)
                    return;

                controller = value;

                if (ControllerChanged != null)
                    ControllerChanged.Invoke(this, EventArgs.Empty);
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
        public event EventHandler ControllerChanged;

        protected Camera()
        {
            FieldOfView = MathHelper.DegreesToRadians(FIELD_OF_VIEW_DEFAULT);
            NearPlaneDistance = NEAR_PLANE_DISTANCE_DEFAULT;
            FarPlaneDistance = FAR_PLANE_DISTANCE_DEFAULT;
        }

        public virtual void Update()
        {
            if (Controller != null)
                Controller.Update();

            Projection = Matrix4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearPlaneDistance, FarPlaneDistance);
            
            Matrix4 rotation = Matrix4.CreateRotationX(Pitch) * Matrix4.CreateRotationY(Yaw);
            Vector3 forward = Vector3.Transform(Vector3.UnitZ, rotation);

            View = Matrix4.LookAt(Position, Position + forward, Vector3.UnitY);
        }
    }
}