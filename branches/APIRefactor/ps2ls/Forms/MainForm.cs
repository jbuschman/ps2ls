using System;
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

namespace ps2ls.Forms
{
    public partial class MainForm : Form
    {
        #region Singleton
        private static MainForm instance = null;

        public static void CreateInstance()
        {
            instance = new MainForm();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static MainForm Instance { get { return instance; } }
        #endregion

        private MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox.Instance.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AssetBrowser.CreateInstance();
            ModelBrowser.CreateInstance();

            ImageList imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.box_small);
            imageList.Images.Add(Properties.Resources.tree_small);
            tabControl1.ImageList = imageList;

            TabPage assetBrowserTabPage = new TabPage("Asset Browser");
            assetBrowserTabPage.Controls.Add(AssetBrowser.Instance);
            assetBrowserTabPage.ImageIndex = 0;
            tabControl1.TabPages.Add(assetBrowserTabPage);

            TabPage modelBrowserTabPage = new TabPage("Model Browser");
            modelBrowserTabPage.Controls.Add(ModelBrowser.Instance);
            modelBrowserTabPage.ImageIndex = 1;
            tabControl1.TabPages.Add(modelBrowserTabPage);
        }

        private void reportIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.ProjectNewIssueURL);
        }
    }
}
