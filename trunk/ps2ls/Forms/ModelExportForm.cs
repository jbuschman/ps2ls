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

        private ModelExportForm()
        {
            InitializeComponent();
        }

        public List<String> FileNames { get; set; }
        private GenericLoadingForm loadingForm;
        private BackgroundWorker exportBackgroundWorker = new BackgroundWorker();
        private ModelExporter.ExportOptions exportOptions;

        private void exportDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = exportModel(sender, e.Argument);
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

        private Int32 exportModel(object sender, object argument)
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

                MemoryStream memoryStream = AssetManager.Instance.CreateAssetMemoryStreamByName(fileName);

                if (memoryStream == null)
                {
                    continue;
                }

                Model model = Model.LoadFromStream(fileName, memoryStream);

                if (model == null)
                {
                    continue;
                }

                ModelExporter.ExportModelToDirectory(model, directory, exportOptions);

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
                ModelExporter.ExportFormatOptions exportOptions = ModelExporter.GetExportFormatOptionsByFormat((ModelExporter.ModelExportFormats)modelFormatComboBox.SelectedIndex);

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
            if (modelFormatComboBox.SelectedIndex < 0)
            {
                return;
            }

            ModelExporter.ModelExportFormats format = (ModelExporter.ModelExportFormats)modelFormatComboBox.SelectedIndex;

            setSelectedExportFormat(format);
        }

        private void setSelectedExportFormat(ModelExporter.ModelExportFormats format)
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

        private void applyExportOptions(ModelExporter.ExportOptions exportOptions)
        {
            this.exportOptions = exportOptions;

            normalsCheckBox.Checked = exportOptions.Normals;
            textureCoordinatesCheckBox.Checked = exportOptions.TextureCoordinates;
        }

        private void ModelExportForm_Load(object sender, EventArgs e)
        {
            exportFolderBrowserDialog.SelectedPath = Application.StartupPath;

            exportBackgroundWorker.WorkerReportsProgress = true;
            exportBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(exportProgressChanged);
            exportBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(exportRunWorkerCompleted);
            exportBackgroundWorker.DoWork += new DoWorkEventHandler(exportDoWork);

            loadModelFormatComboBox();
            loadModelAxesPresetComboBox();

            upAxisComboBox.SelectedIndex = 1;
            leftAxisComboBox.SelectedIndex = 0;
            textureFormatComboBox.SelectedIndex = 0;

            packageToolTip.SetToolTip(packageCheckBox, "When checked, all exported assets will be exported to their own directory.");
        }

        private void loadModelFormatComboBox()
        {
            modelFormatComboBox.Items.Clear();

            foreach (ModelExporter.ModelExportFormats format in Enum.GetValues(typeof(ModelExporter.ModelExportFormats)))
            {
                ModelExporter.ExportFormatOptions options = ModelExporter.GetExportFormatOptionsByFormat(format);

                modelFormatComboBox.Items.Add(options.Name);
            }

            modelFormatComboBox.SelectedIndex = modelFormatComboBox.Items.Count > 0 ? 0 : -1;
        }

        private void loadModelAxesPresetComboBox()
        {
            modelAxesPresetComboBox.Items.Clear();

            foreach (ModelExporter.ModelAxesPreset modelAxesPreset in ModelExporter.ModelAxesPresets)
            {
                modelAxesPresetComboBox.Items.Add(modelAxesPreset);
            }

            modelAxesPresetComboBox.SelectedIndex = modelAxesPresetComboBox.Items.Count > 0 ? 0 : -1;
        }

        private void exportTexturesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            textureFormatComboBox.Enabled = texturesCheckBox.Checked;
            exportOptions.Textures = texturesCheckBox.Checked;
        }

        private void normalsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            exportOptions.Normals = normalsCheckBox.Checked;
        }

        private void textureCoordinatesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            exportOptions.TextureCoordinates = textureCoordinatesCheckBox.Checked;
        }

        private void upAxisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            exportOptions.UpAxis = (ModelExporter.Axes)upAxisComboBox.SelectedIndex;
        }

        private void leftAxisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            exportOptions.LeftAxis = (ModelExporter.Axes)leftAxisComboBox.SelectedIndex;
        }

        private void xScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (scaleLinkAxesCheckBox.Checked)
            {
                yScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
                zScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
            }

            exportOptions.Scale.X = (Single)xScaleNumericUpDown.Value;
        }

        private void yScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            exportOptions.Scale.Y = (Single)yScaleNumericUpDown.Value;
        }

        private void zScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            exportOptions.Scale.Z = (Single)zScaleNumericUpDown.Value;
        }

        private void packageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            exportOptions.Package = packageCheckBox.Checked;
        }

        private void modelAxesPresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelExporter.ModelAxesPreset modelAxesPreset = ModelExporter.ModelAxesPresets[modelAxesPresetComboBox.SelectedIndex];

            applyModelAxesPreset(modelAxesPreset);
        }

        private void applyModelAxesPreset(ModelExporter.ModelAxesPreset modelAxesPreset)
        {
            leftAxisComboBox.SelectedIndex = (Int32)modelAxesPreset.LeftAxis;
            upAxisComboBox.SelectedIndex = (Int32)modelAxesPreset.UpAxis;
        }
    }
}
