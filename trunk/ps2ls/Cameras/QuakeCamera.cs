using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ps2ls.Cameras
{
    public class QuakeCamera : Camera
    {
        public QuakeCamera()
            : base(Types.Quake)
        {

        }

        public override void Update()
        {
            base.Update();
        }
    }
}
