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

        public List<String> FileNames { get; set; }
        private GenericLoadingForm loadingForm;
        private BackgroundWorker exportBackgroundWorker = new BackgroundWorker();

        public ModelExportForm()
        {
            InitializeComponent();

            exportFolderBrowserDialog.SelectedPath = Application.StartupPath;

            formatComboBox.SelectedIndex = 0;
            upAxisComboBox.SelectedIndex = 1;
            leftAxisComboBox.SelectedIndex = 0;

            exportBackgroundWorker.WorkerReportsProgress = true;
            exportBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(exportProgressChanged);
            exportBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(exportRunWorkerCompleted);
            exportBackgroundWorker.DoWork += new DoWorkEventHandler(exportDoWork);

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
            Model.ExportOptions exportOptions = (Model.ExportOptions)arguments[2];

            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;

            Int32 result = 0;

            for (Int32 i = 0; i < fileNames.Count; ++i)
            {
                String fileName = fileNames[i];

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

                model.ExportToDirectory(directory, exportOptions);

                Int32 percent = (Int32)(((Single)i / (Single)fileNames.Count) * 100);

                backgroundWorker.ReportProgress(percent, fileName);

                ++result;
            }

            return result;
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

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (exportFolderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.ExportOptions exportOptions = createExportOptions();

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
    }
}
