using ps2ls.Assets.Pack;
using ps2ls.Forms;
using ps2ls.Graphics.Effects;
using ps2ls.Graphics.Materials;
using System;
using System.Windows.Forms;

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
            MaterialDefinitionLibrary.CreateInstance();
            EffectDefinitionLibrary.CreateInstance();

            Application.Run(new MainForm());
        }
    }
}
