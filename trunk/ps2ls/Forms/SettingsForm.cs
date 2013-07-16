using ps2ls.Assets.Pack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ps2ls.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void assetDirectoryAutoDetectButton_Click(object sender, EventArgs e)
        {
            string assetsDirectory = AssetManager.DetectAssetDirectory();

            if (assetsDirectory != String.Empty)
            {
                GenericDialog genericDialog = new GenericDialog();
                genericDialog.Message = "ps2ls was unable to detect your PlanetSide 2 assets directory." +
                    Environment.NewLine + Environment.NewLine +
                    "This could mean that a PlanetSide 2 installation does not exist or that PlanetSide 2 is installed in a non-standard way.";
                genericDialog.Image = Properties.Resources.logo_48_warn;
                genericDialog.ShowDialog();
            }

            assetDirectoryTextBox.Text = assetsDirectory;
        }

        private void assetDirectoryBrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            assetDirectoryTextBox.Text = Properties.Settings.Default.AssetDirectory;

        }
    }
}
