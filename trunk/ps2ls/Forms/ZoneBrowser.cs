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
using System.IO;

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
                refreshZonesListBox();
            }
        }

        private void refreshZonesListBox()
        {
            Cursor.Current = Cursors.WaitCursor;
            zonesListBox.BeginUpdate();

            zonesListBox.Items.Clear();

            List<Asset> assets = null;
            AssetManager.Instance.AssetsByType.TryGetValue(Asset.Types.ZONE, out assets);

            if (assets != null)
            {
                foreach (Asset asset in assets)
                {
                    zonesListBox.Items.Add(asset);
                }
            }

            zonesListBox.EndUpdate();
            Cursor.Current = Cursors.Default;
        }

        private void zonesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (zonesListBox.SelectedIndex < 0)
                return;

            Zone zone = null;
            Asset asset = null;

            try
            {
                asset = (Asset)zonesListBox.SelectedItem;
            }
            catch(Exception) { return; }

            MemoryStream memoryStream = asset.Pack.CreateAssetMemoryStreamByName(asset.Name);

            string zoneName = Path.GetFileNameWithoutExtension(asset.Name);

            Zone.LoadError loadError = Zone.LoadFromStream(zoneName, memoryStream, out zone);

            switch (loadError)
            {
                case Assets.Zone.Zone.LoadError.None:
                    {

                    }
                    break;
                default:
                    break;
            }
        }
    }
}
