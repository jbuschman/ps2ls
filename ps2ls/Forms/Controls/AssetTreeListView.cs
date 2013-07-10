using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ps2ls.Assets.Pack;

namespace ps2ls.Forms.Controls
{
    public class AssetTreeListView : TreeView
    {
        private const char DELIMETER = '_';

        private class AssetNode
        {
            public String Name;
            public Asset Asset;
            public AssetNode Parent;
            public Dictionary<string, AssetNode> Children = new Dictionary<string, AssetNode>();

            public bool IsRoot
            {
                get { return Parent == null; }
            }
            public bool HasChildren
            {
                get { return Children.Count > 0; }
            }

            public void Collapse()
            {
                if (!HasChildren)
                    return;

                if (Children.Count == 1)
                {
                    AssetNode child = Children.First().Value;

                    child.Collapse();

                    //concatenate names
                    Name += DELIMETER + child.Name;
                    //give me your children, and dispose other object
                    Children = child.Children;
                }
            }
        }

        public AssetTreeListView()
        {
        }

        public void Load(IEnumerable<Asset> assets)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            AssetNode rootAssetNode = new AssetNode();
            rootAssetNode.Name = "root";

            foreach (Asset asset in assets)
            {
                AssetNode parentAssetNode = rootAssetNode;
                AssetNode assetNode = null;
                String assetNodeName = asset.Name;

                int delimeterIndex = assetNodeName.IndexOf(DELIMETER);
                while (delimeterIndex >= 0)
                {
                    string lhs = assetNodeName.Substring(0, delimeterIndex);

                    AssetNode newParentAssetNode;
                    parentAssetNode.Children.TryGetValue(lhs, out newParentAssetNode);


                    if (newParentAssetNode == null)
                    {
                        //parent does not have entry, let's make one and use it
                        assetNode = new AssetNode();
                        assetNode.Name = lhs;
                        assetNode.Parent = parentAssetNode;
                        assetNode.Parent.Children.Add(assetNode.Name, assetNode);
                    }
                    else
                    {
                        //parent has entry, let's use it
                        assetNode = newParentAssetNode;
                    }

                    assetNodeName = assetNodeName.Substring(delimeterIndex + 1, assetNodeName.Length - delimeterIndex - 1);

                    if ((delimeterIndex = assetNodeName.IndexOf(DELIMETER)) < 0)
                    {
                        AssetNode leafAssetNode = new AssetNode();
                        leafAssetNode.Name = assetNodeName;
                        leafAssetNode.Parent = assetNode;
                        leafAssetNode.Asset = asset;

                        assetNode.Children.Add(leafAssetNode.Name, leafAssetNode);
                        break;
                    }

                    parentAssetNode = assetNode;
                }
            }

            rootAssetNode.Collapse();

            stopwatch.Stop();

            foreach (AssetNode assetNode in rootAssetNode.Children.Values)
            {
                Nodes.Add(assetNode.Name);
            }

            Console.WriteLine("asd");
        }
    }
}
