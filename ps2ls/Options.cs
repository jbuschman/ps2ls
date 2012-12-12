using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ps2ls
{
    public partial class Options : Form
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

        private Options()
        {
            InitializeComponent();
        }
    }
}
