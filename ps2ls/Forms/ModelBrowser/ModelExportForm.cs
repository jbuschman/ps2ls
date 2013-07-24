using ps2ls.Assets.Dme;
using ps2ls.Assets.Pack;
using ps2ls.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace ps2ls.Forms
{
    public partial class ModelExportForm : Form
    {
        public class ModelAxesPreset
        {
            public ModelAxesPreset(string name, Axes upAxis, Axes leftAxis)
            {
                Name = name;
                UpAxis = upAxis;
                LeftAxis = leftAxis;
            }

            public string Name;
            public Axes UpAxis;
            public Axes LeftAxis;

            public override string ToString()
            {
                return Name;
            }
        }

        public ModelExportForm()
        {
            InitializeComponent();
        }

        public List<string> FileNames { get; set; }
        public BackgroundWorker exportBackgroundWorker = new BackgroundWorker();
        private GenericLoadingForm loadingForm;
        private ModelExportOptions modelExportOptions = new ModelExportOptions();

        private static List<ModelAxesPreset> modelAxesPresets = new List<ModelAxesPreset>();

        static ModelExportForm()
        {
            modelAxesPresets.Add(new ModelAxesPreset("Default", Axes.Y, Axes.X));
            modelAxesPresets.Add(new ModelAxesPreset("Autodesk® 3ds Max", Axes.Z, Axes.Y));
        }

        private void exportDoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = exportModel(sender, e.Argument);
        }

        private void exportRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingForm.Close();

            Close();

            MessageBox.Show("Successfully exported " + (int)e.Result + " models.");
        }

        private void exportProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (loadingForm != null)
            {
                loadingForm.SetLabelText((String)e.UserState);
                loadingForm.SetProgressBarPercent(e.ProgressPercentage);
            }
        }

        private int exportModel(object sender, object argument)
        {
            List<object> arguments = (List<object>)argument;

            ModelExporter modelExporter = (ModelExporter)arguments[0];
            string directory = (String)arguments[1];
            List<string> fileNames = (List<string>)arguments[2];
            ModelExportOptions exportOptions = (ModelExportOptions)arguments[3];

            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;

            int result = 0;

            for (int i = 0; i < fileNames.Count; ++i)
            {
                string fileName = fileNames[i];

                MemoryStream memoryStream = AssetManager.Instance.CreateAssetMemoryStreamByName(fileName);

                if (memoryStream == null)
                    continue;

                Model model = Model.LoadFromStream(fileName, memoryStream);

                if (model == null)
                    continue;

                modelExporter.ExportModelToDirectoryWithExportOptions(model, directory, exportOptions);

                int percent = (int)(((Single)i / (Single)fileNames.Count) * 100);

                backgroundWorker.ReportProgress(percent, fileName);

                ++result;
            }

            return result;
        }

        private void applyModelExporterOptions(ModelExporter modelExporter)
        {
            normalsCheckBox.Checked = modelExporter.CanExportNormals;
            normalsCheckBox.Enabled = modelExporter.CanExportNormals;

            textureCoordinatesCheckBox.Checked = modelExporter.CanExportTextureCoordinates;
            textureCoordinatesCheckBox.Enabled = modelExporter.CanExportTextureCoordinates;
        }

        private void loadModelAxesPresetComboBox()
        {
            modelAxesPresetComboBox.Items.Clear();

            foreach (ModelAxesPreset modelAxesPreset in modelAxesPresets)
                modelAxesPresetComboBox.Items.Add(modelAxesPreset);

            modelAxesPresetComboBox.SelectedIndex = modelAxesPresetComboBox.Items.Count > 0 ? 0 : -1;
        }

        private void loadTextureFormatComboBox()
        {
            textureFormatComboBox.Items.Clear();

            foreach (TextureExporter.TextureFormatInfo textureFormat in TextureExporter.TextureFormats)
                textureFormatComboBox.Items.Add(textureFormat);

            textureFormatComboBox.SelectedIndex = textureFormatComboBox.Items.Count > 0 ? 0 : -1;
        }

        private void loadModelExportFormatComboBox()
        {
            modelFormatComboBox.Items.Clear();
            modelFormatComboBox.Items.Add(new ObjModelExporter());

            modelFormatComboBox.SelectedIndex = modelFormatComboBox.Items.Count > 0 ? 0 : -1;
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
            applyCurrentStateToExportOptions();

            if (exportFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                List<object> argument = new List<object>()
                {
                    modelFormatComboBox.SelectedItem,
                    exportFolderBrowserDialog.SelectedPath,
                    FileNames,
                    modelExportOptions
                };

                loadingForm = new GenericLoadingForm();
                loadingForm.Show();

                exportBackgroundWorker.RunWorkerAsync(argument);
            }
        }

        private void formatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modelFormatComboBox.SelectedItem == null)
                return;

            ModelExporter modelExporter = (ModelExporter)modelFormatComboBox.SelectedItem;

            applyModelExporterOptions(modelExporter);
        }

        private void ModelExportForm_Load(object sender, EventArgs e)
        {
            exportFolderBrowserDialog.SelectedPath = Application.StartupPath;

            exportBackgroundWorker.WorkerReportsProgress = true;
            exportBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(exportProgressChanged);
            exportBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(exportRunWorkerCompleted);
            exportBackgroundWorker.DoWork += new DoWorkEventHandler(exportDoWork);

            loadModelExportFormatComboBox();
            loadModelAxesPresetComboBox();
            loadTextureFormatComboBox();
            loadModelsListBox();
        }

        private void loadModelsListBox()
        {
            foreach (string fileName in FileNames)
                modelsListBox.Items.Add(fileName);
        }

        private void exportTexturesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            textureFormatComboBox.Enabled = exportTexturesCheckBox.Checked;
            modelExportOptions.Textures = exportTexturesCheckBox.Checked;
        }

        private void normalsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            modelExportOptions.Normals = normalsCheckBox.Checked;
        }

        private void textureCoordinatesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            modelExportOptions.TextureCoordinates = textureCoordinatesCheckBox.Checked;
        }

        private void upAxisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            modelExportOptions.UpAxis = (Axes)upAxisComboBox.SelectedIndex;
        }

        private void leftAxisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            modelExportOptions.LeftAxis = (Axes)leftAxisComboBox.SelectedIndex;
        }

        private void xScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (scaleLinkAxesCheckBox.Checked)
            {
                yScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
                zScaleNumericUpDown.Value = xScaleNumericUpDown.Value;
            }

            modelExportOptions.Scale.X = (Single)xScaleNumericUpDown.Value;
        }

        private void yScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            modelExportOptions.Scale.Y = (Single)yScaleNumericUpDown.Value;
        }

        private void zScaleNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            modelExportOptions.Scale.Z = (Single)zScaleNumericUpDown.Value;
        }

        private void packageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            modelExportOptions.Package = packageCheckBox.Checked;
        }

        private void modelAxesPresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModelAxesPreset modelAxesPreset = (ModelAxesPreset)modelAxesPresetComboBox.SelectedItem;
            leftAxisComboBox.SelectedIndex = (int)modelAxesPreset.LeftAxis;
            upAxisComboBox.SelectedIndex = (int)modelAxesPreset.UpAxis;
        }

        private void applyCurrentStateToExportOptions()
        {
            modelExportOptions.LeftAxis = (Axes)leftAxisComboBox.SelectedIndex;
            modelExportOptions.Normals = normalsCheckBox.Checked;
            modelExportOptions.Package = packageCheckBox.Checked;
            modelExportOptions.Scale.X = (Single)xScaleNumericUpDown.Value;
            modelExportOptions.Scale.Y = (Single)yScaleNumericUpDown.Value;
            modelExportOptions.Scale.Z = (Single)zScaleNumericUpDown.Value;
            modelExportOptions.TextureCoordinates = textureCoordinatesCheckBox.Checked;
            modelExportOptions.Textures = exportTexturesCheckBox.Checked;
            modelExportOptions.UpAxis = (Axes)upAxisComboBox.SelectedIndex;
            modelExportOptions.TextureFormat = (TextureExporter.TextureFormatInfo)textureFormatComboBox.SelectedItem;
        }

        private void textureFormatComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            modelExportOptions.TextureFormat = (TextureExporter.TextureFormatInfo)textureFormatComboBox.SelectedItem;
        }

        private void exportTexturesCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            textureFormatComboBox.Enabled = exportTexturesCheckBox.Checked;
        }
    }
}
