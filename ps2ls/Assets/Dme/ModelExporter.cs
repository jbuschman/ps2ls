using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using OpenTK;
using System.IO;

namespace ps2ls.Assets.Dme
{
    public class ModelExporter
    {
        public enum ExportFormats
        {
            OBJ,
            STL
        }

        public enum Axes
        {
            X,
            Y,
            Z
        }

        public struct ExportCapabilities
        {
            public Boolean Normals;
            public Boolean TextureCoordinates;
        }

        public struct ExportFormatOptions
        {
            public ExportFormats Format;
            public String Name;
            public ExportCapabilities Capabilities;
            public ExportOptions Options;
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

        private static Dictionary<ModelExporter.ExportFormats, ModelExporter.ExportFormatOptions> exportFormatOptions;

        static ModelExporter()
        {
            exportFormatOptions = new Dictionary<ExportFormats, ExportFormatOptions>();

            createExportFormatOptions();
        }

        private static void createExportFormatOptions()
        {
            ModelExporter.ExportFormatOptions options = new ModelExporter.ExportFormatOptions();

            //obj
            options.Format = ModelExporter.ExportFormats.OBJ;
            options.Name = "Wavefront OBJ (*.obj)";
            options.Capabilities.Normals = true;
            options.Capabilities.TextureCoordinates = true;
            options.Options.Normals = true;
            options.Options.TextureCoordinates = true;
            exportFormatOptions.Add(ModelExporter.ExportFormats.OBJ, options);

            //stl
            options.Format = ModelExporter.ExportFormats.STL;
            options.Name = "Stereolithography (*.stl)";
            options.Capabilities.Normals = false;
            options.Capabilities.TextureCoordinates = false;
            options.Options.Normals = true;
            options.Options.TextureCoordinates = false;
            exportFormatOptions.Add(ModelExporter.ExportFormats.STL, options);
        }

        public static void ExportToDirectory(Model model, string directory, ExportFormatOptions formatOptions)
        {
            switch (formatOptions.Format)
            {
                case ExportFormats.OBJ:
                    exportAsOBJToDirectory(model, directory, formatOptions.Options);
                    break;
                case ExportFormats.STL:
                    exportAsSTLToDirectory(model, directory, formatOptions.Options);
                    break;
            }
        }

        private static void exportAsOBJToDirectory(Model model, string directory, ExportOptions options)
        {
            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            String path = directory + @"\" + Path.GetFileNameWithoutExtension(model.Name) + ".obj";

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

        private static void exportAsSTLToDirectory(Model model, string directory, ExportOptions options)
        {
            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            String path = directory + @"\" + Path.GetFileNameWithoutExtension(model.Name) + ".stl";

            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            for (Int32 i = 0; i < model.Meshes.Length; ++i)
            {
                Mesh mesh = model.Meshes[i];

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
            }

            streamWriter.Close();
        }

        public static ExportFormatOptions GetExportFormatOptionsByFormat(ExportFormats format)
        {
            return exportFormatOptions[format];
        }
    }
}
