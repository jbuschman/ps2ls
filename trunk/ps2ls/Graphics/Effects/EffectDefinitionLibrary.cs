using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.Graphics.Effects
{
    public class EffectDefinitionLibrary
    {
        #region Singleton
        private static EffectDefinitionLibrary instance = null;

        public static void CreateInstance()
        {
            instance = new EffectDefinitionLibrary();

            StringReader stringReader = new StringReader(Properties.Resources.materials_3);
            instance.loadFromStringReader(stringReader);
        }

        private bool loadFromStringReader(StringReader stringReader)
        {
            throw new NotImplementedException();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static EffectDefinitionLibrary Instance { get { return instance; } }
        #endregion

        public Dictionary<string, EffectDefinition> EffectsDefinitions;
    }
}
