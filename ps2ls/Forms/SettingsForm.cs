using ps2ls.Assets.Pack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            if (assetsDirectory == string.Empty)
            {
                GenericDialog.ShowGenericDialog(
                    "Error",
                    "ps2ls was unable to detect your PlanetSide 2 assets directory." +
                    Environment.NewLine + Environment.NewLine +
                    "This could mean that a PlanetSide 2 installation does not exist or that PlanetSide 2 is installed in a non-standard way.",
                    GenericDialog.Types.Warning);
            }
            else
            {
                assetDirectoryTextBox.Text = assetsDirectory;
            }
        }

        private void assetDirectoryBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = assetDirectoryTextBox.Text;

            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            switch (dialogResult)
            {
                case System.Windows.Forms.DialogResult.OK:
                    {
                        assetDirectoryTextBox.Text = folderBrowserDialog.SelectedPath;
                    }
                    break;
                default:
                    break;
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            assetDirectoryTextBox.Text = Properties.Settings.Default.AssetDirectory;
            loadAssetsOnStartCheckBox.Checked = Properties.Settings.Default.ShouldLoadAssetsOnStart;
            promptLoadAssetsOnStart.Checked = Properties.Settings.Default.PromptLoadAssetsOnStart;
        }

        private void save()
        {
            Properties.Settings.Default.AssetDirectory = assetDirectoryTextBox.Text;
            Properties.Settings.Default.ShouldLoadAssetsOnStart = loadAssetsOnStartCheckBox.Checked;
            Properties.Settings.Default.PromptLoadAssetsOnStart = promptLoadAssetsOnStart.Checked;
            Properties.Settings.Default.Save();
        }

        private void assetDirectoryTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(assetDirectoryTextBox.Text))
            {
                assetsDirectoryIconPanel.BackgroundImage = Properties.Resources.tick_circle;
            }
            else
            {
                assetsDirectoryIconPanel.BackgroundImage = Properties.Resources.cross_circle;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            save();
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            save();

            Hide();
        }

        private void loadAssetsOnStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            promptLoadAssetsOnStart.Enabled = !loadAssetsOnStartCheckBox.Checked;

            if (loadAssetsOnStartCheckBox.Checked)
            {
                promptLoadAssetsOnStart.Checked = false;
            }
        }
    }
}
