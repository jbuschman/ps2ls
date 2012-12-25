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
using ps2ls.Dme;
using ps2ls.Forms;

namespace ps2ls
{
    public partial class Form1 : Form
    {
        #region Singleton
        private static Form1 instance = null;

        public static void CreateInstance()
        {
            instance = new Form1();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static Form1 Instance { get { return instance; } }
        #endregion

        private Form1()
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
            PackBrowser.CreateInstance();
            ModelBrowser.CreateInstance();

            tabControl1.TabPages[0].Controls.Add(PackBrowser.Instance);
            tabControl1.TabPages[1].Controls.Add(ModelBrowser.Instance);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //HACK: Figure out a better way to do this.
            if (tabControl1.SelectedIndex == 1)
            {
                //ModelBrowser.Instance.Refresh();
            }
        }

        private void reportIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(PS2LS.Instance.ProjectNewIssueURL);
        }
    }
}
