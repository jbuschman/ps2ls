using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.Assets.Dme;
using ps2ls.Graphics.Materials;
using ps2ls.Assets.Pack;
using System.IO;
using ps2ls.Assets.Dma;
using ps2ls.Cryptography;

namespace ps2ls.Forms
{
    public partial class ModelBrowserModelStats : UserControl
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
                    ModelChanged.Invoke(this, null);
            }
        }
        public event EventHandler ModelChanged;

        public ModelBrowserModelStats()
        {
            InitializeComponent();

            ModelChanged += ModelBrowserModelStats_ModelChanged;
        }

        void ModelBrowserModelStats_ModelChanged(object sender, EventArgs e)
        {
            nameLabel.Text = model != null ? model.Name : "";
            modelVertexCountLabel.Text = model != null ? model.VertexCount.ToString() : "0";
            modelTriangleCountLabel.Text = model != null ? (model.IndexCount / 3).ToString() : "0";

            //materials
            materialsListBox.Items.Clear();
            if (model != null)
            {
            }

            //meshes
            meshesComboBox.Items.Clear();
            if (model != null)
                for (int i = 0; i < model.Meshes.Length; ++i)
                    meshesComboBox.Items.Add("Mesh " + i);

            if (meshesComboBox.Items.Count > 0)
                meshesComboBox.SelectedIndex = 0;

            //textures
            texturesListBox.Items.Clear();
            if (model != null)
                foreach(string textureString in model.TextureStrings)
                    texturesListBox.Items.Add(textureString);
        }

        private void meshesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (model == null)
                return;
        }
    }
}