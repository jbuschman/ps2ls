using ps2ls.Assets.Pack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ps2ls.Forms.Controls
{
    public class AssetTreeNode : TreeNode
    {
        public String Name { get; set; }
        public Asset Asset { get; set; }

        public AssetTreeNode()
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
