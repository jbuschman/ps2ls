using System;
using System.IO;
using System.Windows.Forms;
using ps2ls.Assets.Pack;
using ps2ls.Forms;
using ps2ls.Graphics.Materials;

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

            // Setup the asset location
            if (Properties.Settings.Default.AssetLocation == String.Empty)
            {
                // No valid location, try to work it out from the registry
                Properties.Settings.Default.AssetLocation = getDefaultAssetLocation();
                Properties.Settings.Default.Save();
            }
            else
            {
                // Make sure the saved asset location still exists
                if (!Directory.Exists(Properties.Settings.Default.AssetLocation))
                {
                    // Dir doesn't exist, wipe the setting.
                    Properties.Settings.Default.AssetLocation = "";
                    Properties.Settings.Default.Save();
                }
            }

            AssetManager.CreateInstance();

            AboutBox.CreateInstance();
            MainForm.CreateInstance();

            MaterialDefinitionManager.CreateInstance();

            Application.Run(MainForm.Instance);
        }

        /// <summary>
        /// Try to extract the location of the Pack files by looking in the registry at the 2 different kinds of installations.
        /// </summary>
        static private string getDefaultAssetLocation()
        {
            Microsoft.Win32.RegistryKey key = null;

            // non-steam install
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\LaunchPad.exe");

            if (key != null && key.GetValue("") != null)
            {
                string defaultDir;
                defaultDir = key.GetValue("").ToString();
                defaultDir = Path.GetDirectoryName(defaultDir) + @"\Resources\Assets";

                if (Directory.Exists(defaultDir))
                {
                    return defaultDir;
                }
            }

            // steam install
            key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 218230");

            if (key != null && key.GetValue("InstallLocation") != null)
            {
                string defaultDir;
                defaultDir = key.GetValue("InstallLocation").ToString();
                defaultDir += @"\Resources\Assets";

                if (Directory.Exists(defaultDir))
                {
                    return defaultDir;
                }
            }

            // Unable to guess a default dir
            return String.Empty;
        }
    }
}
