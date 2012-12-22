﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.Cameras;
using OpenTK;

namespace ps2ls.Forms
{
    public partial class ModelBrowserGLControl : CustomGLControl
    {
        private Point location;
        private bool rotating;
        private bool panning;

        public ArcBallCamera Camera { get; set; }

        public ModelBrowserGLControl()
        {
            Camera = new ArcBallCamera();

            InitializeComponent();
        }

        private void ModelBrowserGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    rotating = true;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    panning = true;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    break;
            }
        }

        private void ModelBrowserGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    rotating = false;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    panning = false;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    break;
            }
        }

        private void ModelBrowserGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (location.X == 0 && location.Y == 0)
            {
                location = e.Location;
            }

            Int32 deltaX = e.Location.X - location.X;
            Int32 deltaY = e.Location.Y - location.Y;

            if (panning)
            {
                Matrix4 world = Matrix4.CreateFromAxisAngle(Vector3.UnitX, Camera.Pitch) * Matrix4.CreateFromAxisAngle(Vector3.UnitY, Camera.Yaw);

                Vector3 forward = Vector3.Transform(Vector3.UnitZ, world);
                forward.Y = 0;
                forward.Normalize();

                Vector3 up = Vector3.UnitY;
                Vector3 left = Vector3.Cross(up, forward);

                Camera.DesiredTarget += (up * deltaY) * 0.06125f;
                Camera.DesiredTarget += (left * deltaX) * 0.06125f;
            }
            else if (rotating)
            {
                Camera.DesiredYaw -= MathHelper.DegreesToRadians(0.25f * deltaX);
                Camera.DesiredPitch += MathHelper.DegreesToRadians(0.25f * deltaY);
            }

            location = e.Location;
        }

        private void ModelBrowserGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    {
                        ResetCamera();
                    }
                    break;
            }
        }

        public void ResetCamera()
        {
            Camera.DesiredPitch = MathHelper.DegreesToRadians(45.0f);
            Camera.DesiredYaw = MathHelper.DegreesToRadians(45.0f);
            Camera.DesiredTarget = Vector3.Zero;
        }
    }
}
