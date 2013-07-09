using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using ps2ls.Cameras;
using ps2ls.Assets.Dme;
using ps2ls.Assets.Pack;
using System.Diagnostics;
using ps2ls.Graphics.Materials;
using System.IO;
using System.Xml;
using System.Runtime.InteropServices;
using ps2ls.Forms.Controls;
using System.Text.RegularExpressions;

namespace ps2ls.Forms
{
    public partial class ModelBrowser : UserControl
    {
        private Model model = null;

        private bool assetsDirty = false;

        public ModelBrowser()
        {
            InitializeComponent();

            lodFilterComboBox.SelectedIndex = 1;    //LOD 0

            //HACK: Can't load ModelBrowser.cs in design mode unless we have at least one item for some reason.
            //Clear items after construction.
            modelsListBox.Items.Clear();

            Dock = DockStyle.Fill;

            AssetManager.Instance.AssetsChanged += new EventHandler(AssetManager_AssetsChanged);
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
                refreshModelsListBox();
                assetsDirty = false;
            }
        }

        private void refreshModelsListBox()
        {
            modelsListBox.Items.Clear();

            List<Asset> assets = new List<Asset>();
            List<Asset> dmes = null;

            AssetManager.Instance.AssetsByType.TryGetValue(Asset.Types.DME, out dmes);

            if (dmes != null)
                assets.AddRange(dmes);

            assets.Sort(new Asset.NameComparer());

            if (assets != null)
            {
                Regex regex = null;

                try
                {
                    regex = new Regex(searchModelsText.Text, RegexOptions.Compiled);
                }
                catch (Exception) { /* invalid regex */ }

                foreach (Asset asset in assets)
                {
                    if (showAutoLODModelsButton.Checked == false && asset.Name.EndsWith("Auto.dme"))
                        continue;

                    if (lodFilterComboBox.SelectedIndex > 0)
                    {
                        String lodString = "lod" + (lodFilterComboBox.SelectedIndex - 1).ToString();

                        if (asset.Name.IndexOf(lodString, 0, StringComparison.OrdinalIgnoreCase) == -1)
                            continue;
                    }

                    switch (searchTextTypeToolStripDrownDownButton.SearchTextType)
                    {
                        case SearchTextTypeToolStripDrownDownButton.SearchTextTypes.Textual:
                            {
                                if (searchModelsText.Text.Length > 0 && asset.Name.IndexOf(searchModelsText.Text, 0, StringComparison.OrdinalIgnoreCase) == -1)
                                    continue;
                            }
                            break;
                        case SearchTextTypeToolStripDrownDownButton.SearchTextTypes.RegularExpression:
                            {
                                if (regex == null || !regex.IsMatch(asset.Name))
                                    continue;
                            }
                            break;
                    }
                        
                    modelsListBox.Items.Add(asset);
                }
            }

            Int32 count = modelsListBox.Items.Count;
            Int32 max = assets != null ? assets.Count : 0;

            modelsCountToolStripStatusLabel.Text = count + "/" + max;
        }

        private void clearSearchModelsText_Click(object sender, EventArgs e)
        {
            searchModelsText.Clear();
        }

        private void modelsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Asset asset = null;

            try
            {
                asset = (Asset)modelsListBox.SelectedItem;
            }
            catch (InvalidCastException) { return; }

            MemoryStream memoryStream = asset.Pack.CreateAssetMemoryStreamByName(asset.Name);

            model = Model.LoadFromStream(asset.Name, memoryStream);
            modelBrowserGLControl.Model = model;

            ModelBrowserModelStats1.Model = model;

            materialSelectionComboBox.Items.Clear();

            if (model != null)
            {
                foreach (string textureName in model.TextureStrings)
                {
                    materialSelectionComboBox.Items.Add(textureName);
                }
            }

            materialSelectionComboBox.SelectedIndex = materialSelectionComboBox.Items.Count > 0 ? 0 : -1;

            snapCameraToModel();
        }

        private void snapCameraToModel()
        {
            if (model == null)
                return;

            Vector3 center = (model.Max + model.Min) / 2.0f;
            Vector3 extents = (model.Max - model.Min) / 2.0f;

            modelBrowserGLControl.Camera.DesiredTarget = center;
            modelBrowserGLControl.Camera.DesiredDistance = extents.Length * 1.75f;
        }

        private void showAutoLODModelsButton_CheckedChanged(object sender, EventArgs e)
        {
            refreshModelsListBox();
        }

        private void searchModelsText_CustomTextChanged(object sender, EventArgs e)
        {
            refreshModelsListBox();

            clearSearchModelsText.Enabled = searchModelsText.Text.Length > 0;
        }

        private void lodFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshModelsListBox();
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractSelectedModels();
        }

        private void extractSelectedModels()
        {
            List<String> fileNames = new List<string>();

            foreach (object selectedItem in modelsListBox.SelectedItems)
            {
                Asset asset = null;

                try
                {
                    asset = (Asset)selectedItem;
                }
                catch (InvalidCastException) { continue; }

                fileNames.Add(asset.Name);
            }

            ModelExportForm modelExportForm = new ModelExportForm();
            modelExportForm.FileNames = fileNames;
            modelExportForm.ShowDialog();
        }

        private void modelContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (modelsListBox.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }

            if (modelsListBox.SelectedItems.Count == 1)
            {
                extractToolStripMenuItem.Text = "Extract...";
            }
            else
            {
                extractToolStripMenuItem.Text = "Extract " + modelsListBox.SelectedItems.Count + "...";
            }
        }

        private void searchTextTypeToolStripDrownDownButton_SearchTextTypeChanged(object sender, EventArgs e)
        {
            if(searchModelsText.Text.Length != 0)
                refreshModelsListBox();
        }

        private void wireframeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modelBrowserGLControl.RenderMode = ModelBrowserGLControl.RenderModes.WireFrame;
        }

        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modelBrowserGLControl.RenderMode = ModelBrowserGLControl.RenderModes.Smooth;
        }

        private void showAxesButton_CheckedChanged(object sender, EventArgs e)
        {
            modelBrowserGLControl.DrawAxes = showAxesButton.Checked;
        }
    }
}
