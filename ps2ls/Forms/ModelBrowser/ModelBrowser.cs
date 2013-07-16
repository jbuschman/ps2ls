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
        public Model Model
        {
            get { return model; }
            set
            {
                if (model == value)
                    return;

                model = value;

                if (ModelChanged != null)
                    ModelChanged.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler ModelChanged;

        private bool assetsDirty = false;

        public ModelBrowser()
        {
            InitializeComponent();

            //HACK: Can't load ModelBrowser.cs in design mode unless we have at least one item for some reason.
            //Clear items after construction.
            modelsListBox.Items.Clear();

            Dock = DockStyle.Fill;

            AssetManager.Instance.AssetsChanged += new EventHandler(AssetManager_AssetsChanged);

            ModelChanged += ModelBrowser_ModelChanged;

            modelBrowserGLControl.RenderModeChanged += modelBrowserGLControl_RenderModeChanged;
            modelBrowserGLControl.RenderMode = ModelBrowserGLControl.RenderModes.Wireframe;

            drawAxesButton.Checked = modelBrowserGLControl.DrawAxes;
            snapCameraToModelButton.Checked = modelBrowserGLControl.SnapCameraToModelOnModelChange;

            modelsMaxComboBox.SelectedIndex = 3;
        }

        void modelBrowserGLControl_RenderModeChanged(object sender, EventArgs e)
        {
            switch(modelBrowserGLControl.RenderMode)
            {
                case ModelBrowserGLControl.RenderModes.Smooth:
                    {
                        toolStripDropDownButton1.Text = "Smooth";
                        toolStripDropDownButton1.Image = Properties.Resources.smooth;
                    }
                    break;
                case ModelBrowserGLControl.RenderModes.Wireframe:
                    {
                        toolStripDropDownButton1.Text = "Wireframe";
                        toolStripDropDownButton1.Image = Properties.Resources.wireframe;
                    }
                    break;
            }
        }

        void ModelBrowser_ModelChanged(object sender, EventArgs e)
        {
            modelBrowserGLControl.Model = model;
            modelBrowserModelStats.Model = model;
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
            Cursor.Current = Cursors.WaitCursor;
            modelsListBox.BeginUpdate();

            Int32 modelsMax = 0;

            try
            {
                modelsMax = Int32.Parse(modelsMaxComboBox.Items[modelsMaxComboBox.SelectedIndex].ToString());
            }
            catch (FormatException)
            {
                modelsMax = Int32.MaxValue;
            }

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

                    if (modelsListBox.Items.Count >= modelsMax)
                        break;
                }
            }

            Int32 count = modelsListBox.Items.Count;
            Int32 max = assets != null ? assets.Count : 0;

            modelsCountToolStripStatusLabel.Text = count + "/" + max;

            modelsListBox.EndUpdate();
            Cursor.Current = Cursors.Default;
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

            Model = Model.LoadFromStream(asset.Name, memoryStream);
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
            modelBrowserGLControl.RenderMode = ModelBrowserGLControl.RenderModes.Wireframe;
        }

        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modelBrowserGLControl.RenderMode = ModelBrowserGLControl.RenderModes.Smooth;
        }

        private void showAxesButton_CheckedChanged(object sender, EventArgs e)
        {
            modelBrowserGLControl.DrawAxes = drawAxesButton.Checked;
        }

        private void snapCameraToModelButton_CheckedChanged(object sender, EventArgs e)
        {
            modelBrowserGLControl.SnapCameraToModelOnModelChange = snapCameraToModelButton.Checked;
        }

        private void modelsMaxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshModelsListBox();
        }
    }
}
