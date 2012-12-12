﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace ps2ls.Dme
{
    public class ArcBallCamera : Camera
    {
        public Single Distance { get; set; }
        public Vector3 Target { get; set; }

        public ArcBallCamera()
        {
            Yaw = MathHelper.DegreesToRadians(45.0f);
            Pitch = MathHelper.DegreesToRadians(45.0f);
            Distance = 10.0f;
        }

        public override void Update()
        {
            Matrix4 world = Matrix4.CreateRotationX(Pitch) * Matrix4.CreateRotationY(Yaw);
            Vector3 forward = Vector3.Transform(Vector3.UnitZ, world);

            Position = Target - (forward * Distance);

            base.Update();
        }
    }
}