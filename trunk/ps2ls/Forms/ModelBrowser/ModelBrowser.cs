using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using ps2ls.Assets.Dme;
using ps2ls.Assets.Pack;
using ps2ls.Graphics.Materials;
using System.IO;
using ps2ls.Forms.Controls;
using System.Text.RegularExpressions;
using ps2ls.Assets.Dma;

namespace ps2ls.Forms
{
    public partial class ModelBrowser : UserControl
    {
        private Model model = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

            if (!MaterialDefinitionLibrary.Instance.IsLoaded)
            {
                MaterialDefinitionLibrary.Instance.Load();
            }

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

            int modelsMax = Int32.MaxValue;

            Int32.TryParse(modelsMaxComboBox.Items[modelsMaxComboBox.SelectedIndex].ToString(), out modelsMax);

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

            int count = modelsListBox.Items.Count;
            int max = assets != null ? assets.Count : 0;

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
            List<string> fileNames = new List<string>();

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

        /// <summary>
        /// This is debugging code used to extact valid parameter names out of
        /// shader files in a wildly inefficient and brute force manner. :)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //List<Asset> assets;
            //AssetManager.Instance.AssetsByType.TryGetValue(Asset.Types.DME, out assets);

            //Dictionary<string, List<string>> effectParameterNameLists = new Dictionary<string, List<String>>();
            //FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + @"\fxo_dump.txt", FileMode.OpenOrCreate);
            //StreamWriter streamWriter = new StreamWriter(fileStream);

            //foreach (Asset asset in assets)
            //{
            //    MemoryStream memoryStream = AssetManager.Instance.CreateAssetMemoryStreamByName(asset.Name);
            //    Model model = Model.LoadFromStream(asset.Name, memoryStream);

            //    if (model == null)
            //        continue;

            //    foreach (Material material in model.Materials)
            //    {
            //        MaterialDefinition materialDefinition = MaterialDefinitionLibrary.Instance.GetMaterialDefinitionFromHash(material.MaterialDefinitionHash);

            //        foreach (DrawStyle drawStyle in materialDefinition.DrawStyles)
            //        {
            //            if (!effectParameterNameLists.ContainsKey(drawStyle.Effect))
            //            {
            //                List<string> parameterNames = new List<string>();
            //                effectParameterNameLists.Add(drawStyle.Effect, parameterNames);
            //            }

            //            MemoryStream effectMemoryStream = AssetManager.Instance.CreateAssetMemoryStreamByName(drawStyle.Effect);

            //            if (effectMemoryStream == null)
            //                continue;

            //            BinaryReader binaryReader = new BinaryReader(effectMemoryStream, Encoding.ASCII);

            //            StringBuilder stringBuilder = new StringBuilder();
            //            List<string> words = new List<string>();

            //            const int MINIMUM_WORD_LENGTH = 3;
            //            const int MAXUMUM_WORD_LENGTH = 32;

            //            List<Material.Parameter> materialParametersNotFound = new List<Material.Parameter>();
            //            materialParametersNotFound.AddRange(material.Parameters);

            //            for (int wordLength = MINIMUM_WORD_LENGTH; wordLength <= MAXUMUM_WORD_LENGTH; ++wordLength)
            //            {
            //                effectMemoryStream.Position = 0;

            //                while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length - wordLength)
            //                {
            //                    String word = new String(binaryReader.ReadChars(wordLength));
            //                    uint wordHash = Cryptography.JenkinsOneAtATime(word);
            //                    uint wordHash2 = Cryptography.JenkinsOneAtATime(word.ToLower());

            //                    for (int i = materialParametersNotFound.Count - 1; i >= 0; --i)
            //                    {
            //                        if ((materialParametersNotFound[i].Type == Material.Parameter.D3DXParameterType.Texture) &&
            //                            wordHash == materialParametersNotFound[i].NameHash ||
            //                            wordHash2 == materialParametersNotFound[i].NameHash)
            //                        {
            //                            if (!effectParameterNameLists[drawStyle.Effect].Contains(word))
            //                                effectParameterNameLists[drawStyle.Effect].Add(word);

            //                            materialParametersNotFound.RemoveAt(i);
            //                            break;
            //                        }
            //                    }

            //                    binaryReader.BaseStream.Position -= (wordLength - 1);

            //                    if (materialParametersNotFound.Count == 0)
            //                        break;
            //                }

            //                if (materialParametersNotFound.Count == 0)
            //                    break;
            //            }

            //            effectMemoryStream.Close();
            //        }
            //    }
            //}

            //foreach(KeyValuePair<string, List<string>> effectParameterNameList in effectParameterNameLists)
            //{
            //    streamWriter.WriteLine(string.Format("[{0}]", effectParameterNameList.Key));

            //    foreach (string parameterName in effectParameterNameList.Value)
            //    {
            //        streamWriter.WriteLine(parameterName);
            //    }

            //    streamWriter.WriteLine();
            //}

            //streamWriter.Close();
        }
    }
}
