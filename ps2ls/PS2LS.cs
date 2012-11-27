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
        private static PS2LS _Instance = null;

        public static void CreateInstance()
        {
            _Instance = new PS2LS();
        }

        public static void DeleteInstance()
        {
            _Instance = null;
        }

        public static PS2LS Instance { get { return _Instance; } }
        #endregion

        private PS2LS()
        {
            ShowFullPath = false;

            PackOpenFileDialog = new OpenFileDialog();
            PackOpenFileDialog.Filter = "PACK files|*.pack|All files|*.*";
            PackOpenFileDialog.Multiselect = true;

            RegistryKey key = null;

            key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\LaunchPad.exe");

            if (key != null && key.GetValue("") != null)
            {
                GameDirectory = key.GetValue("").ToString();
                GameDirectory = Path.GetDirectoryName(GameDirectory) + @"\Resources\Assets";
            }

            PackOpenFileDialog.InitialDirectory = GameDirectory;
        }

        public Boolean ShowFullPath { get; set; }
        public OpenFileDialog PackOpenFileDialog { get; private set; }
        public String GameDirectory { get; private set; }
    }
}
