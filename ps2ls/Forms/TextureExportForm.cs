using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.IO;

namespace ps2ls.Forms
{
    public partial class TextureExportForm : Form
    {
        public TextureExportForm()
        {
            InitializeComponent();
        }

        void loadTextureFormatComboxBox()
        {
            foreach (TextureExporter.TextureFormatInfo textureFormatInfo in TextureExporter.TextureFormats)
                textureFormatComboBox.Items.Add(textureFormatInfo);
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
    }
}
