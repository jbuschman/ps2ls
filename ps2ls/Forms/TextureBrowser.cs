﻿using System;
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

namespace ps2ls.Forms
{
    public partial class TextureBrowser : UserControl
    {
        #region Singleton
        private static TextureBrowser instance = null;

        public static void CreateInstance()
        {
            instance = new TextureBrowser();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static TextureBrowser Instance { get { return instance; } }
        #endregion

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

        private void refreshImageListBox()
        {
            textureListbox.Items.Clear();

            List<Asset> images = null;
            AssetManager.Instance.AssetsByType.TryGetValue(Asset.Types.DDS, out images);

            int imageCount = images != null ? images.Count : 0;

            List<Asset> assets = new List<Asset>(imageCount);

            if (images != null)
            {
                assets.AddRange(images);
            }

            assets.Sort(new Asset.NameComparer());

            if (assets != null)
            {
                foreach (Asset asset in assets)
                {
                    if (asset.Name.IndexOf(searchText.Text, 0, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        textureListbox.Items.Add(asset);
                    }

                }
            }

            int count = textureListbox.Items.Count;
            int max = assets != null ? assets.Count : 0;

            imagesCountLabel.Text = count + "/" + max;

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

            System.Drawing.Image image = LoadDrawingImageFromStream(memoryStream);

            pictureWindow.BackgroundImage = image;
            pictureWindow.Show();
        }

        private void ImageBrowser_Load(object sender, EventArgs e)
        {
        }

        public override void Refresh()
        {
            base.Refresh();

            if (assetsDirty)
            {
                refreshImageListBox();
                assetsDirty = false;
            }
        }

        private void searchText_CustomTextChanged(object sender, EventArgs e)
        {
            refreshImageListBox();

            clearSearchTextButton.Enabled = searchText.Text.Length > 0;
        }

        private void clearSearchTextButton_Click(object sender, EventArgs e)
        {
            searchText.Clear();
        }

        public static System.Drawing.Image LoadDrawingImageFromStream(Stream stream)
        {
            ImageImporter importer = new ImageImporter();
            DevIL.Image img = importer.LoadImageFromStream(stream);

            DevIL.Unmanaged.ImageInfo data = img.GetImageInfo();
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(data.Width, data.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, data.Width, data.Height);
            System.Drawing.Imaging.BitmapData bdata = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            DevIL.Unmanaged.IL.CopyPixels(0, 0, 0, data.Width, data.Height, 1, DataFormat.BGRA, DevIL.DataType.UnsignedByte, bdata.Scan0);

            bitmap.UnlockBits(bdata);

            return (System.Drawing.Image)bitmap;
        }
    }
}