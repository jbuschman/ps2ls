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

            Form1.CreateInstance();
            PS2LS.CreateInstance();
            AboutBox.CreateInstance();
            PackManager.CreateInstance();
            ModelManager.CreateInstance();

            Application.Run(Form1.Instance);
        }
    }
}
