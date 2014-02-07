using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using ps2ls.Assets.Pack;

namespace ps2ls.Graphics.Materials
{
    public class MaterialDefinitionLibrary
    {
        #region Singleton
        private static MaterialDefinitionLibrary instance = null;

        public static void CreateInstance()
        {
            instance = new MaterialDefinitionLibrary();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static MaterialDefinitionLibrary Instance { get { return instance; } }
        #endregion

        public static string MaterialDefinitionFileName = "materials_3.xml";

        public bool IsLoaded { get; private set; }
        public Dictionary<uint, MaterialDefinition> MaterialDefinitions { get; private set; }
        public Dictionary<uint, VertexLayout> VertexLayouts { get; private set; }

        MaterialDefinitionLibrary()
        {
            IsLoaded = false;
            MaterialDefinitions = new Dictionary<uint, MaterialDefinition>();
            VertexLayouts = new Dictionary<uint, VertexLayout>();
        }

        public bool Load()
        {
            MemoryStream memoryStream = AssetManager.Instance.CreateAssetMemoryStreamByName(MaterialDefinitionFileName);

            if (memoryStream == null)
            {
                return false;
            }

            StreamReader streamReader = new StreamReader(memoryStream);
            StringReader stringReader = new StringReader(streamReader.ReadToEnd());

            loadFromStringReader(stringReader);

            IsLoaded = true;

            return true;
        }

        private void loadFromStringReader(StringReader stringReader)
        {
            if (stringReader == null)
                return;

            XPathDocument document = null;

            try
            {
                document = new XPathDocument(stringReader);
            }
            catch (Exception)
            {
                return;
            }

            XPathNavigator navigator = document.CreateNavigator();

            //vertex layouts
            loadVertexLayoutsByXPathNavigator(navigator.Clone());

            //TODO: parameter groups?

            //material definitions
            loadMaterialDefinitionsByXPathNavigator(navigator.Clone());
        }

        private void loadMaterialDefinitionsByXPathNavigator(XPathNavigator navigator)
        {
            XPathNodeIterator materialDefinitions = null;

            try
            {
                materialDefinitions = navigator.Select("/Object/Array[@Name='MaterialDefinitions']/Object[@Class='MaterialDefinition']");
            }
            catch (Exception)
            {
                return;
            }

            while (materialDefinitions.MoveNext())
            {
                MaterialDefinition materialDefinition = MaterialDefinition.LoadFromXPathNavigator(materialDefinitions.Current);

                if (materialDefinition != null && !MaterialDefinitions.ContainsKey(materialDefinition.NameHash))
                {
                    MaterialDefinitions.Add(materialDefinition.NameHash, materialDefinition);
                }
            }
        }

        private void loadVertexLayoutsByXPathNavigator(XPathNavigator navigator)
        {
            //material definitions
            XPathNodeIterator vertexLayouts = null;

            try
            {
                vertexLayouts = navigator.Select("/Object/Array[@Name='InputLayouts']/Object[@Class='InputLayout']");
            }
            catch (Exception)
            {
                return;
            }

            while (vertexLayouts.MoveNext())
            {
                VertexLayout vertexLayout = VertexLayout.LoadFromXPathNavigator(vertexLayouts.Current);

                if (vertexLayout != null && false == VertexLayouts.ContainsKey(vertexLayout.NameHash))
                {
                    VertexLayouts.Add(vertexLayout.NameHash, vertexLayout);
                }
            }
        }
    }
}
