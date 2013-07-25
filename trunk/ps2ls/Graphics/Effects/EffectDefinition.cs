using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.Graphics.Effects
{
    public class EffectDefinition
    {
        public string Name { get; private set; }
        public string[] DiffuseMaps { get; private set; }
        public string[] NormalMaps { get; private set; }
        public string[] BumpMaps { get; private set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
