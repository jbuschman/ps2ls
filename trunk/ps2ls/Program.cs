using System;
using System.Collections.Generic;
using System.Linq;
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

            PS2LS.CreateInstance();
            AboutBox.CreateInstance();
            PackManager.CreateInstance();
            ModelBrowser.CreateInstance();

            Form1.CreateInstance();

            Application.Run(Form1.Instance);
        }
    }
}
