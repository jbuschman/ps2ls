using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ps2ls.Assets.Pack;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace ps2ls.Forms
{
    public partial class ZoneBrowser : UserControl
    {
        bool assetsDirty = false;

        public ZoneBrowser()
        {
            InitializeComponent();

            AssetManager.Instance.AssetsChanged += AssetManager_AssetsChanged;
        }

        private void AssetManager_AssetsChanged(object sender, EventArgs e)
        {
            assetsDirty = true;
        }
    }
}
