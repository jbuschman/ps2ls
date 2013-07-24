using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.IO;
using DevIL;
using ps2ls.Assets.Pack;

namespace ps2ls.Forms
{
    public partial class TextureExportForm : Form
    {
        public class TextureExportFormat
        {
            public TextureExportFormat(ImageType imageType, string name, string extension)
            {
                ImageType = imageType;
                Name = name;
                Extension = extension;
            }

            public ImageType ImageType;
            public string Name;
            public string Extension;

            public override string ToString()
            {
                return Name + " (*." + Extension + ")";
            }
        }

        private static List<TextureExportFormat> textureExportFormats = new List<TextureExportFormat>();

        private List<Asset> assets = new List<Asset>();

        static TextureExportForm()
        {
            textureExportFormats.Add(new TextureExportFormat(ImageType.Bmp, "Bitmap", "bmp"));
            textureExportFormats.Add(new TextureExportFormat(ImageType.Png, "Portable Network Graphics", "png"));
            textureExportFormats.Add(new TextureExportFormat(ImageType.Tga, "Truevision Targa", "tga"));
        }

        public TextureExportForm(IEnumerable<Asset> assets)
        {
            InitializeComponent();

            foreach (Asset asset in assets)
                this.assets.Add(asset);
        }

        void loadTextureFormatComboxBox()
        {
            foreach (TextureExporter.TextureFormatInfo textureFormatInfo in TextureExporter.TextureFormats)
                formatComboBox.Items.Add(textureFormatInfo);
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            switch (folderBrowserDialog.ShowDialog())
            {
                case DialogResult.OK:
                    {
                        AssetManager.Instance.ExtractToDirectoryAsync(assets, folderBrowserDialog.SelectedPath, new EventHandler(exportCompleted));
                    }
                    break;
                default:
                    break;
            }
        }

        private void TextureExportForm_Load(object sender, EventArgs e)
        {
            loadTexturesListBox();
            loadFormatComboBox();
        }

        private void loadTexturesListBox()
        {
            foreach (Asset asset in assets)
                texturesListBox.Items.Add(asset);
        }

        private void loadFormatComboBox()
        {
            foreach (TextureExportFormat format in textureExportFormats)
                formatComboBox.Items.Add(format);
        }

        private void exportCompleted(object sender, EventArgs args)
        {
            bool optionChecked;

            GenericDialog.ShowGenericDialog(
                "Export Complete",
                "Successfully exported " + assets.Count + " textures.",
                GenericDialog.Types.Default,
                GenericDialog.Buttons.OK,
                "Open in Windows Explorer",
                false,
                out optionChecked);

            if (optionChecked)
                System.Diagnostics.Process.Start("explorer.exe", folderBrowserDialog.SelectedPath);

            Hide();
        }
    }
}
