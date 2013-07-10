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
using ps2ls.Assets.Zone;

namespace ps2ls.Forms
{
    public partial class ZoneBrowser : UserControl
    {
        bool assetsDirty = false;

        private Zone zone;
        public Zone Zone
        {
            get { return zone; }
            set
            {
                if (zone == value)
                    return;

                zone = value;

                if (ZoneChanged != null)
                    ZoneChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler ZoneChanged;

        public ZoneBrowser()
        {
            InitializeComponent();

            AssetManager.Instance.AssetsChanged += AssetManager_AssetsChanged;
        }

        protected override void InitLayout()
        {
            base.InitLayout();

            Dock = DockStyle.Fill;
        }

        private void AssetManager_AssetsChanged(object sender, EventArgs e)
        {
            assetsDirty = true;
        }

        public override void Refresh()
        {
            base.Refresh();

            if (assetsDirty)
            {

            }
        }
    }
}
