using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ps2ls.Graphics.Effects
{
    public class EffectDefinition
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string DiffuseMaps { get; set; }
        [XmlAttribute]
        public string BumpMaps { get; set; }

        public EffectDefinition()
        {
        }
    }
}
