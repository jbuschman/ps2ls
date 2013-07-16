using System;
using System.IO;
using System.Windows.Forms;
using ps2ls.Assets.Pack;
using ps2ls.Forms;
using ps2ls.Graphics.Materials;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using lzhamNET;

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

            TextureManager.CreateInstance();
            AssetManager.CreateInstance();
            AboutBox.CreateInstance();
            MaterialDefinitionManager.CreateInstance();

            Application.Run(new MainForm());
        }
    }
}
