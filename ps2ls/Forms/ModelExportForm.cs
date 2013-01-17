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

        public enum ExportFormat
        {
            OBJ,
            STL
        }

        private static String[] exportFormatNames =
        {
            "Wavefront OBJ (*.obj)",
            "Stereolithography (*.stl)"
        };

        public struct ExportCapabilities
        {
            public Boolean Normals;
            public Boolean TextureCoordinates;
        }

        public struct ExportFormatOptions
        {
            public ExportFormat Format;
            public String Name;
            public ExportCapabilities Capabilities;
            public ExportOptions Options;
        }

        public enum Axes
        {
            X,
            Y,
            Z
        }

        public struct ExportOptions
        {
            public Axes UpAxis;
            public Axes LeftAxis;
            public Boolean FlipX;
            public Boolean FlipY;
            public Boolean FlipZ;
            public Boolean Normals;
            public Boolean TextureCoordinates;
            public Vector3 Scale;
        }

        public List<String> FileNames { get; set; }
        private GenericLoadingForm loadingForm;
        private BackgroundWorker exportBackgroundWorker = new BackgroundWorker();
        Dictionary<ExportFormat, ExportFormatOptions> exportFormatOptions = new Dictionary<ExportFormat, ExportFormatOptions>();

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

            createExportFormatOptions();

            for (Int32 i = 0; i < exportFormatOptions.Count; ++i)
            {
                formatComboBox.Items.Add(exportFormatOptions[(ExportFormat)i].Name);
            }

            formatComboBox.SelectedIndex = formatComboBox.Items.Count > 0 ? 0 : -1;
        }

        private void createExportFormatOptions()
        {
            ExportFormatOptions options = new ExportFormatOptions();

            //obj
            options.Format = ExportFormat.OBJ;
            options.Name = "Wavefront OBJ (*.obj)";
            options.Capabilities.Normals = true;
            options.Capabilities.TextureCoordinates = true;
            options.Options.Normals = true;
            options.Options.TextureCoordinates = true;
            exportFormatOptions.Add(ExportFormat.OBJ, options);

            //stl
            options.Format = ExportFormat.STL;
            options.Name = "Stereolithography (*.stl)";
            options.Capabilities.Normals = false;
            options.Capabilities.TextureCoordinates = false;
            options.Options.Normals = true;
            options.Options.TextureCoordinates = false;
            exportFormatOptions.Add(ExportFormat.STL, options);
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
            ExportFormatOptions exportOptions = (ExportFormatOptions)arguments[2];

            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;

            Int32 result = 0;

            for (Int32 i = 0; i < fileNames.Count; ++i)
            {
                String fileName = fileNames[i];

                MemoryStream memoryStream = PackBrowser.Instance.CreateAssetMemoryStreamByName(fileName);

                if (memoryStream == null)
                {
                    continue;
                }

                Model model = Model.LoadFromStream(fileName, memoryStream);

                if (model == null)
                {
                    continue;
                }

                exportToDirectory(model, directory, exportOptions);

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
                ExportFormatOptions exportOptions = exportFormatOptions[(ExportFormat)formatComboBox.SelectedIndex];

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

            ExportFormat format = (ExportFormat)formatComboBox.SelectedIndex;

            setSelectedExportFormat(format);
        }


        private void exportToDirectory(Model model, string directory, ExportFormatOptions formatOptions)
        {
            switch (formatOptions.Format)
            {
                case ExportFormat.OBJ:
                    exportAsOBJToDirectory(model, directory, formatOptions.Options);
                    break;
                case ExportFormat.STL:
                    exportAsSTLToDirectory(model, directory, formatOptions.Options);
                    break;
            }
        }

        public void exportAsOBJToDirectory(Model model, string directory, ExportOptions options)
        {
            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            String path = directory + @"\" + Path.GetFileNameWithoutExtension(Name) + ".obj";

            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            for (Int32 i = 0; i < model.Meshes.Length; ++i)
            {
                Mesh mesh = model.Meshes[i];

                for (Int32 j = 0; j < mesh.Vertices.Length; ++j)
                {
                    Vertex vertex = mesh.Vertices[j];
                    Vector3 position = vertex.Position;

                    position.X *= options.Scale.X;
                    position.Y *= options.Scale.Y;
                    position.Z *= options.Scale.Z;

                    if (options.FlipX)
                        position.X *= -1;

                    if (options.FlipY)
                        position.Y *= -1;

                    if (options.FlipZ)
                        position.Z *= -1;

                    streamWriter.WriteLine("v " + position.X.ToString(format) + " " + position.Y.ToString(format) + " " + position.Z.ToString(format));
                }

                if (options.Normals)
                {
                    for (Int32 j = 0; j < mesh.Vertices.Length; ++j)
                    {
                        Vertex vertex = mesh.Vertices[j];
                        Vector3 normal = vertex.Normal;

                        normal.X *= options.Scale.X;
                        normal.Y *= options.Scale.Y;
                        normal.Z *= options.Scale.Z;

                        if (options.FlipX)
                            normal.X *= -1;

                        if (options.FlipY)
                            normal.Y *= -1;

                        if (options.FlipZ)
                            normal.Z *= -1;

                        normal.Normalize();

                        streamWriter.WriteLine("vn " + normal.X.ToString(format) + " " + normal.Y.ToString(format) + " " + normal.Z.ToString(format));
                    }
                }
            }

            Int32 vertexCount = 0;

            for (Int32 i = 0; i < model.Meshes.Length; ++i)
            {
                Mesh mesh = model.Meshes[i];

                streamWriter.WriteLine("g Mesh" + i);

                for (Int32 j = 0; j < mesh.Indices.Length; j += 3)
                {
                    if (options.Normals)
                    {
                        streamWriter.WriteLine("f " + (vertexCount + mesh.Indices[j + 2] + 1) + "//" + (vertexCount + mesh.Indices[j + 2] + 1) + " " + (vertexCount + mesh.Indices[j + 1] + 1) + "//" + (vertexCount + mesh.Indices[j + 1] + 1) + " " + (vertexCount + mesh.Indices[j + 0] + 1) + "//" + (vertexCount + mesh.Indices[j + 0] + 1));
                    }
                    else
                    {
                        streamWriter.WriteLine("f " + (vertexCount + mesh.Indices[j + 2] + 1) + " " + (vertexCount + mesh.Indices[j + 1] + 1) + " " + (vertexCount + mesh.Indices[j + 0] + 1));
                    }
                }

                vertexCount += mesh.Vertices.Length;
            }

            streamWriter.Close();
        }

        public void exportAsSTLToDirectory(Model model, string directory, ExportOptions options)
        {
            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            String path = directory + @"\" + Path.GetFileNameWithoutExtension(Name) + ".stl";

            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            for (Int32 i = 0; i < model.Meshes.Length; ++i)
            {
                Mesh mesh = model.Meshes[i];

                //streamWriter.WriteLine("solid " + i);

                for (Int32 j = 0; j < mesh.Indices.Length; j += 3)
                {
                    Vector3 normal = Vector3.Zero;
                    normal += mesh.Vertices[mesh.Indices[j + 0]].Normal;
                    normal += mesh.Vertices[mesh.Indices[j + 1]].Normal;
                    normal += mesh.Vertices[mesh.Indices[j + 2]].Normal;
                    normal.Normalize();

                    streamWriter.WriteLine("facet normal " + normal.X.ToString("E", format) + " " + normal.Y.ToString("E", format) + " " + normal.Z.ToString("E", format));
                    streamWriter.WriteLine("outer loop");

                    for (Int32 k = 0; k < 3; ++k)
                    {
                        Vector3 vertex = mesh.Vertices[mesh.Indices[j + k]].Position;

                        streamWriter.WriteLine("vertex " + vertex.X.ToString("E", format) + " " + vertex.Y.ToString("E", format) + " " + vertex.Z.ToString("E", format));
                    }

                    streamWriter.WriteLine("endloop");
                    streamWriter.WriteLine("endfacet");
                }

                //streamWriter.WriteLine("endsolid");
            }

            streamWriter.Close();
        }

        private void setSelectedExportFormat(ExportFormat format)
        {
            ExportFormatOptions options = exportFormatOptions[format];

            applyExportCapabilities(options.Capabilities);
            applyExportOptions(options.Options);
        }

        private void applyExportCapabilities(ExportCapabilities capabilities)
        {
            normalsCheckBox.Enabled = capabilities.Normals;
            textureCoordinatesCheckBox.Enabled = capabilities.TextureCoordinates;
        }

        private void applyExportOptions(ExportOptions options)
        {
            normalsCheckBox.Checked = options.Normals;
            textureCoordinatesCheckBox.Checked = options.TextureCoordinates;
        }
    }
}
