using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.Assets.Pack;
using System.IO;
using DevIL;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using ps2ls.Forms.Controls;

namespace ps2ls.Forms
{
    public partial class TextureBrowser : UserControl
    {
        private bool assetsDirty = false;

        public TextureBrowser()
        {
            InitializeComponent();

            textureListbox.Items.Clear();

            Dock = DockStyle.Fill;

            AssetManager.Instance.AssetsChanged += new EventHandler(onAssetsChanged);
        }

        private void onAssetsChanged(object sender, EventArgs e)
        {
            assetsDirty = true;
        }

        private void refreshTexturesListBox()
        {
            Cursor.Current = Cursors.WaitCursor;
            textureListbox.BeginUpdate();

            textureListbox.Items.Clear();

            List<Asset> images = null;
            AssetManager.Instance.AssetsByType.TryGetValue(Asset.Types.DDS, out images);

            int imageCount = images != null ? images.Count : 0;

            List<Asset> assets = new List<Asset>(imageCount);

            if (images != null)
                assets.AddRange(images);

            assets.Sort(new Asset.NameComparer());

            if (assets != null)
            {
                Regex regex = null;

                try
                {
                    regex = new Regex(searchText.Text, RegexOptions.Compiled);
                }
                catch (Exception) { /* invalid regex */ }

                foreach (Asset asset in assets)
                {
                    switch (searchTextTypeToolStripDrownDownButton1.SearchTextType)
                    {
                        case SearchTextTypeToolStripDrownDownButton.SearchTextTypes.Textual:
                            {
                                if (asset.Name.IndexOf(searchText.Text, 0, StringComparison.OrdinalIgnoreCase) < 0)
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

                    textureListbox.Items.Add(asset);
                }
            }

            int textureCount = textureListbox.Items.Count;
            int texturesMax = assets != null ? assets.Count : 0;

            texturesCountLabel.Text = textureCount + "/" + texturesMax;

            textureListbox.EndUpdate();
            Cursor.Current = Cursors.WaitCursor;
        }

        private void textureListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Asset asset = null;

            try
            {
                asset = (Asset)textureListbox.SelectedItem;
            }
            catch (InvalidCastException) { return; }

            System.IO.MemoryStream memoryStream = asset.Pack.CreateAssetMemoryStreamByName(asset.Name);

            System.Drawing.Image image = LoadImageFromStream(memoryStream);

            pictureWindow.BackgroundImage = image;
            pictureWindow.Show();
        }

        public override void Refresh()
        {
            base.Refresh();

            if (assetsDirty)
            {
                refreshTexturesListBox();
                assetsDirty = false;
            }
        }

        private void searchText_CustomTextChanged(object sender, EventArgs e)
        {
            refreshTexturesListBox();

            clearSearchTextButton.Enabled = searchText.Text.Length > 0;
        }

        private void clearSearchTextButton_Click(object sender, EventArgs e)
        {
            searchText.Clear();
        }

        public static System.Drawing.Image LoadImageFromStream(Stream stream)
        {
            ImageImporter importer = new ImageImporter();
            DevIL.Image img = importer.LoadImageFromStream(stream);

            DevIL.Unmanaged.ImageInfo imageInfo = img.GetImageInfo();
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imageInfo.Width, imageInfo.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, 0, imageInfo.Width, imageInfo.Height);
            System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(rectangle, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            DevIL.Unmanaged.IL.CopyPixels(0, 0, 0, imageInfo.Width, imageInfo.Height, 1, DataFormat.BGRA, DevIL.DataType.UnsignedByte, bitmapData.Scan0);

            bitmap.UnlockBits(bitmapData);

            return (System.Drawing.Image)bitmap;
        }

        private void searchTextTypeToolStripDrownDownButton1_SearchTextTypeChanged(object sender, EventArgs e)
        {
            if(searchText.Text.Length > 0)
                refreshTexturesListBox();
        }

        private void texturesMaxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshTexturesListBox();
        }
    }
}