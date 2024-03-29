﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ps2ls.Properties;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using ps2ls.Assets;
using ps2ls.Assets.Pack;

namespace ps2ls.Forms
{
    public partial class MainForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AssetBrowser AssetBrowser { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ModelBrowser ModelBrowser { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextureBrowser TextureBrowser { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SoundBrowser SoundBrowser { get; private set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ZoneBrowser ZoneBrowser { get; private set; }

        public MainForm()
        {
            InitializeComponent();

            AssetBrowser = new AssetBrowser();
            ModelBrowser = new ModelBrowser();
            TextureBrowser = new TextureBrowser();
            SoundBrowser = new SoundBrowser();
            ZoneBrowser = new ZoneBrowser();
        }

        private void onAssetsChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AssetManager.Instance.AssetsChanged += new EventHandler(onAssetsChanged);

            ImageList imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.box_small);
            imageList.Images.Add(Properties.Resources.tree_small);
            imageList.Images.Add(Properties.Resources.image);
            imageList.Images.Add(Properties.Resources.music);
            imageList.Images.Add(Properties.Resources.map_medium);
            tabControl1.ImageList = imageList;

            TabPage assetBrowserTabPage = new TabPage("Asset Browser");
            assetBrowserTabPage.Controls.Add(AssetBrowser);
            assetBrowserTabPage.ImageIndex = 0;
            tabControl1.TabPages.Add(assetBrowserTabPage);

            TabPage modelBrowserTabPage = new TabPage("Model Browser");
            modelBrowserTabPage.Controls.Add(ModelBrowser);
            modelBrowserTabPage.ImageIndex = 1;
            tabControl1.TabPages.Add(modelBrowserTabPage);

            TabPage textureBrowserTagPage = new TabPage("Texture Browser");
            textureBrowserTagPage.Controls.Add(TextureBrowser);
            textureBrowserTagPage.ImageIndex = 2;
            tabControl1.TabPages.Add(textureBrowserTagPage);

            TabPage soundBrowserTabPage = new TabPage("Sound Browser");
            soundBrowserTabPage.Controls.Add(SoundBrowser);
            soundBrowserTabPage.ImageIndex = 3;
            tabControl1.TabPages.Add(soundBrowserTabPage);

            TabPage zoneBrowserTabPage = new TabPage("Zone Browser");
            zoneBrowserTabPage.Controls.Add(ZoneBrowser);
            zoneBrowserTabPage.ImageIndex = 4;
            tabControl1.TabPages.Add(zoneBrowserTabPage);
        }

        private void reportIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.ProjectNewIssueURL);
        }

        public override void Refresh()
        {
            base.Refresh();

            foreach (Control control in tabControl1.SelectedTab.Controls)
                control.Refresh();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }
    }
}
