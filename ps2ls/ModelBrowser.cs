using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using ps2ls.Dme;

namespace ps2ls
{
    class ModelBrowser
    {
        #region Singleton
        private static ModelBrowser instance = null;

        public static void CreateInstance()
        {
            instance = new ModelBrowser();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static ModelBrowser Instance { get { return instance; } }
        #endregion

        public Camera Camera { get; set; }
        public Model CurrentModel { get; set; }
        public Color BackgroundColor { get; private set; }

        private ColorDialog colorDialog;

        private ModelBrowser()
        {
            Camera = new ArcBallCamera();
            BackgroundColor = Color.FromArgb(32, 32, 32);
            colorDialog = new ColorDialog();
        }

        public Color ShowBackgroundColorDialog()
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                BackgroundColor = colorDialog.Color;
            }

            return BackgroundColor;
        }
    }
}
