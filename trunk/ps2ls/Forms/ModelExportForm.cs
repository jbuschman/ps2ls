using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.Dme;
using System.IO;

namespace ps2ls.Forms
{
    public partial class ModelExportForm : Form
    {
        #region Singleton
        private static ModelExportForm instance = null;

        public static void CreateInstance()
        {
            instance = new ModelExportForm();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static ModelExportForm Instance { get { return instance; } }
        #endregion

        public ModelExportForm()
        {
            InitializeComponent();

            exportFolderBrowserDialog.SelectedPath = Application.StartupPath;

            formatComboBox.SelectedIndex = 0;
            upAxisComboBox.SelectedIndex = 1;
            leftAxisComboBox.SelectedIndex = 0;
        }

        private void openExportFolderBrowserDialogButton_Click(object sender, EventArgs e)
        {
            if (exportFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                exportDirectoryTextBox.Text = exportFolderBrowserDialog.SelectedPath;
            }
        }

        private void ModelExportForm_Load(object sender, EventArgs e)
        {
            exportDirectoryTextBox.Text = exportFolderBrowserDialog.SelectedPath;
        }

        private void xScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (scaleLinkAxesCheckBox.Checked)
            {
                yScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
                zScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
            }
        }

        private void scaleLinkAxesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            yScaleNumericUpDown.Enabled = !scaleLinkAxesCheckBox.Checked;
            zScaleNumericUpDown.Enabled = !scaleLinkAxesCheckBox.Checked;

            if (scaleLinkAxesCheckBox.Checked)
            {
                yScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
                zScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            Model.ExportOptions exportOptions = createExportOptions();

            foreach (String fileName in FileNames)
            {
                MemoryStream memoryStream = PackBrowser.Instance.CreateMemoryStreamByName(fileName);

                if (memoryStream == null)
                {
                    continue;
                }

                Model model = Model.LoadFromStream(fileName, memoryStream);

                if (model == null)
                {
                    continue;
                }

                model.ExportToDirectory(exportFolderBrowserDialog.SelectedPath, exportOptions);
            }

            return;
        }

        private Model.ExportOptions createExportOptions()
        {
            Model.ExportOptions exportOptions = new Model.ExportOptions();

            exportOptions.Format = (Model.ExportFormat)formatComboBox.SelectedIndex;
            exportOptions.FlipX = flipXCheckBox.Checked;
            exportOptions.FlipY = flipYCheckBox.Checked;
            exportOptions.FlipZ = flipZCheckBox.Checked;
            exportOptions.Normals = normalsCheckBox.Checked;
            exportOptions.TextureCoordinates = textureCoordinatesCheckBox.Checked;
            exportOptions.Scale.X = (Single)xScaleNumericUpDown.Value;
            exportOptions.Scale.Y = (Single)yScaleNumericUpDown.Value;
            exportOptions.Scale.Z = (Single)zScaleNumericUpDown.Value;
            exportOptions.UpAxis = Model.Axes.Y;
            exportOptions.LeftAxis = Model.Axes.X;

            return exportOptions;
        }

        private void upAxisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void leftAxisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public List<String> FileNames { get; set; }
    }
}
