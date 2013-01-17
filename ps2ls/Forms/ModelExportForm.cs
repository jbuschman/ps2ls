using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ps2ls.Assets.Dme;
using System.IO;
using OpenTK;
using System.Globalization;
using ps2ls.Assets.Pack;

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

        public List<String> FileNames { get; set; }
        private GenericLoadingForm loadingForm;
        private BackgroundWorker exportBackgroundWorker = new BackgroundWorker();

        public ModelExportForm()
        {
            InitializeComponent();

            exportFolderBrowserDialog.SelectedPath = Application.StartupPath;

            upAxisComboBox.SelectedIndex = 1;
            leftAxisComboBox.SelectedIndex = 0;

            exportBackgroundWorker.WorkerReportsProgress = true;
            exportBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(exportProgressChanged);
            exportBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(exportRunWorkerCompleted);
            exportBackgroundWorker.DoWork += new DoWorkEventHandler(exportDoWork);

            foreach (ModelExporter.ExportFormats format in Enum.GetValues(typeof(ModelExporter.ExportFormats)))
            {
                ModelExporter.ExportFormatOptions options = ModelExporter.GetExportFormatOptionsByFormat(format);

                formatComboBox.Items.Add(options.Name);
            }

            formatComboBox.SelectedIndex = formatComboBox.Items.Count > 0 ? 0 : -1;
        }

        private void exportDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = export(sender, e.Argument);
        }

        private void exportRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingForm.Close();

            Close();

            MessageBox.Show("Successfully exported " + (Int32)e.Result + " models.");
        }

        private void exportProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (loadingForm != null)
            {
                loadingForm.SetLabelText((String)e.UserState);
                loadingForm.SetProgressBarPercent(e.ProgressPercentage);
            }
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

        private Int32 export(object sender, object argument)
        {
            List<object> arguments = (List<object>)argument;

            String directory = (String)arguments[0];
            List<String> fileNames = (List<String>)arguments[1];
            ModelExporter.ExportFormatOptions exportOptions = (ModelExporter.ExportFormatOptions)arguments[2];

            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;

            Int32 result = 0;

            for (Int32 i = 0; i < fileNames.Count; ++i)
            {
                String fileName = fileNames[i];

                MemoryStream memoryStream = PackManager.Instance.CreateAssetMemoryStreamByName(fileName);

                if (memoryStream == null)
                {
                    continue;
                }

                Model model = Model.LoadFromStream(fileName, memoryStream);

                if (model == null)
                {
                    continue;
                }

                ModelExporter.ExportToDirectory(model, directory, exportOptions);

                Int32 percent = (Int32)(((Single)i / (Single)fileNames.Count) * 100);

                backgroundWorker.ReportProgress(percent, fileName);

                ++result;
            }

            return result;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (exportFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ModelExporter.ExportFormatOptions exportOptions = ModelExporter.GetExportFormatOptionsByFormat((ModelExporter.ExportFormats)formatComboBox.SelectedIndex);

                List<object> argument = new List<object>()
                {
                    exportFolderBrowserDialog.SelectedPath,
                    FileNames,
                    exportOptions
                };

                loadingForm = new GenericLoadingForm();
                loadingForm.Show();

                exportBackgroundWorker.RunWorkerAsync(argument);
            }
        }

        private void formatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formatComboBox.SelectedIndex < 0)
                return;

            ModelExporter.ExportFormats format = (ModelExporter.ExportFormats)formatComboBox.SelectedIndex;

            setSelectedExportFormat(format);
        }

        private void setSelectedExportFormat(ModelExporter.ExportFormats format)
        {
            ModelExporter.ExportFormatOptions options = ModelExporter.GetExportFormatOptionsByFormat(format);

            applyExportCapabilities(options.Capabilities);
            applyExportOptions(options.Options);
        }

        private void applyExportCapabilities(ModelExporter.ExportCapabilities capabilities)
        {
            normalsCheckBox.Enabled = capabilities.Normals;
            textureCoordinatesCheckBox.Enabled = capabilities.TextureCoordinates;
        }

        private void applyExportOptions(ModelExporter.ExportOptions options)
        {
            normalsCheckBox.Checked = options.Normals;
            textureCoordinatesCheckBox.Checked = options.TextureCoordinates;
        }
    }
}
