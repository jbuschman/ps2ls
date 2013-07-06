using System;
using System.IO;
using System.Windows.Forms;
using ps2ls.Assets.Pack;
using ps2ls.Forms;
using ps2ls.Graphics.Materials;
using Microsoft.Win32;
using System.Runtime.InteropServices;

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
            ps2ls.IO.LZHAM.DecompressParams decompressParams = new IO.LZHAM.DecompressParams();
            decompressParams.ComputeAdler32 = 0;
            decompressParams.DictSizeLog2 = ps2ls.IO.LZHAM.MinDictSizeLog2;
            decompressParams.NumSeedBytes = 0;
            decompressParams.OutputUnbuffered = 1;
            decompressParams.SeedBytes = IntPtr.Zero;
            decompressParams.StructSize = (uint)Marshal.SizeOf(decompressParams.GetType());

            IntPtr decompressState = ps2ls.IO.LZHAM.DecompressInitialize(decompressParams);
            Byte[] inputBuffer = new Byte[100];
            Byte[] outputBuffer = new Byte[200];
            ps2ls.IO.LZHAM.DecompressStatus decompressStatus = ps2ls.IO.LZHAM.Decompress(decompressState, inputBuffer, outputBuffer, true);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Setup the asset location
            if (Properties.Settings.Default.AssetDirectory == String.Empty)
            {
                // No valid location, try to work it out from the registry
                Properties.Settings.Default.AssetDirectory = getDefaultAssetDirectory();
                Properties.Settings.Default.Save();
            }
            else
            {
                // Make sure the saved asset location still exists
                if (!Directory.Exists(Properties.Settings.Default.AssetDirectory))
                {
                    // Directory doesn't exist, wipe the setting.
                    Properties.Settings.Default.AssetDirectory = "";
                    Properties.Settings.Default.Save();
                }
            }

            TextureManager.CreateInstance();
            AssetManager.CreateInstance();
            AboutBox.CreateInstance();
            MainForm.CreateInstance();
            MaterialDefinitionManager.CreateInstance();

            Application.Run(MainForm.Instance);
        }

        /// <summary>
        /// Try to get the Planetside 2 asset directory by looking in the registry for Planetside 2 installation directories.
        /// </summary>
        private static string getDefaultAssetDirectory()
        {
            RegistryKey key = null;

            // non-steam install
            key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\LaunchPad.exe");

            if (key != null && key.GetValue("") != null)
            {
                String defaultDirectory;
                defaultDirectory = key.GetValue("").ToString();
                defaultDirectory = Path.GetDirectoryName(defaultDirectory) + @"\Resources\Assets";

                if (Directory.Exists(defaultDirectory))
                {
                    return defaultDirectory;
                }
            }

            // steam install
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 218230");

            if (key != null && key.GetValue("InstallLocation") != null)
            {
                String defaultDirectory;
                defaultDirectory = key.GetValue("InstallLocation").ToString();
                defaultDirectory += @"\Resources\Assets";

                if (Directory.Exists(defaultDirectory))
                {
                    return defaultDirectory;
                }
            }

            return String.Empty;
        }
    }
}
