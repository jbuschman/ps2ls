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
        [XmlElement]
        public List<string> DiffuseMaps { get; set; }
        [XmlElement]
        public List<string> BumpMaps { get; set; }

        public EffectDefinition()
        {
            DiffuseMaps = new List<string>();
            DiffuseMaps.Add("BaseDiffuse");

            BumpMaps = new List<string>();
            DiffuseMaps.Add("Bump");
        }
    }
}
