using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace ps2ls
{
    class PS2LS
    {
        #region Singleton
        private static PS2LS instance = null;

        public static void CreateInstance()
        {
            instance = new PS2LS();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static PS2LS Instance { get { return instance; } }
        #endregion

        private PS2LS()
        {
            getGameDirectory();
        }

        private void getGameDirectory()
        {
            RegistryKey key = null;

            // non-steam install
            key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\LaunchPad.exe");

            if (key != null && key.GetValue("") != null)
            {
                GameDirectory = key.GetValue("").ToString();
                GameDirectory = Path.GetDirectoryName(GameDirectory) + @"\Resources\Assets";

                return;
            }

            // steam install
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 218230");

            if (key != null && key.GetValue("InstallLocation") != null)
            {
                GameDirectory = key.GetValue("InstallLocation").ToString();
                GameDirectory += @"\Resources\Assets";

                return;
            }
        }
        public String GameDirectory { get; private set; }
    }
}
