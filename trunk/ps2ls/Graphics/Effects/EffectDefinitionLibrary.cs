using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using System.Xml.Serialization;

namespace ps2ls.Graphics.Effects
{
    public class EffectDefinitionLibrary
    {
        #region Singleton
        private static EffectDefinitionLibrary instance = null;

        public static void CreateInstance()
        {
            instance = new EffectDefinitionLibrary();
            instance.load();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static EffectDefinitionLibrary Instance { get { return instance; } }
        #endregion

        [XmlArray("EffectDefinitions"), XmlArrayItem(typeof(string), ElementName = "EffectDefinition")]
        public List<EffectDefinition> EffectsDefinitions { get; private set; }

        EffectDefinitionLibrary()
        {
            EffectsDefinitions = new List<EffectDefinition>();
        }

        private void load()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(EffectDefinitionLibrary));

            EffectDefinition a = new EffectDefinition();
            EffectDefinition b = new EffectDefinition();
            a.Name = "BumpRigid.fxo";
            b.Name = "FoliagePick.fxo";
            EffectsDefinitions.Add(a);
            EffectsDefinitions.Add(b);

            FileStream fileStream = new FileStream("test.xml", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
            xmlSerializer.Serialize(fileStream, this);
            fileStream.Close();
        }
    }
}
