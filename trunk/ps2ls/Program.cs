using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ps2ls.Forms;
using ps2ls.Graphics;
using ps2ls.Graphics.Materials;
using System.Threading;
using System.Globalization;
using ps2ls.Assets.Pack;

namespace ps2ls
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            PS2LS.CreateInstance();
            PackManager.CreateInstance();

            AboutBox.CreateInstance();
            MainForm.CreateInstance();

            MaterialDefinitionManager.CreateInstance();

            Application.Run(MainForm.Instance);
        }
    }
}
