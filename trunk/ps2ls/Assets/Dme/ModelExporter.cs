using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using OpenTK;
using System.IO;
using ps2ls.Graphics.Materials;

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
            options.Options.Scale = Vector3.One;
            options.Options.Normals = true;
            options.Options.TextureCoordinates = true;
            exportFormatOptions.Add(ModelExporter.ExportFormats.OBJ, options);

            //stl
            options.Format = ModelExporter.ExportFormats.STL;
            options.Name = "Stereolithography (*.stl)";
            options.Capabilities.Normals = false;
            options.Capabilities.TextureCoordinates = false;
            options.Options.Scale = Vector3.One;
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

                MaterialDefinition materialDefinition = MaterialDefinitionManager.Instance.MaterialDefinitions[model.Materials[(Int32)mesh.MaterialIndex].MaterialDefinitionHash];
                VertexLayout vertexLayout = MaterialDefinitionManager.Instance.VertexLayouts[materialDefinition.DrawStyles[0].VertexLayoutNameHash];

                //position
                VertexLayout.Entry.DataTypes positionDataType;
                Int32 positionOffset;
                Int32 positionStreamIndex;

                vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Position, 0, out positionDataType, out positionStreamIndex, out positionOffset);
                
                Mesh.VertexStream positionStream = mesh.VertexStreams[positionStreamIndex];

                for (Int32 j = 0; j < mesh.VertexCount; ++j)
                {
                    Vector3 position;

                    position.X = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 0);
                    position.Y = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 4);
                    position.Z = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 8);

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

                //texture coordinates
                if (options.TextureCoordinates)
                {
                    VertexLayout.Entry.DataTypes texCoord0DataType;
                    Int32 texCoord0Offset = 0;
                    Int32 texCoord0StreamIndex = 0;

                    Boolean texCoord0Present = vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Texcoord, 0, out texCoord0DataType, out texCoord0StreamIndex, out texCoord0Offset);

                    if (texCoord0Present)
                    {
                        Mesh.VertexStream texCoord0Stream = mesh.VertexStreams[texCoord0StreamIndex];

                        for (Int32 j = 0; j < mesh.VertexCount; ++j)
                        {
                            Vector2 texCoord;

                            switch (texCoord0DataType)
                            {
                                case VertexLayout.Entry.DataTypes.Float2:
                                    texCoord.X = BitConverter.ToSingle(texCoord0Stream.Data, (j * texCoord0Stream.BytesPerVertex) + 0);
                                    texCoord.Y = 1.0f - BitConverter.ToSingle(texCoord0Stream.Data, (j * texCoord0Stream.BytesPerVertex) + 4);
                                    break;
                                case VertexLayout.Entry.DataTypes.float16_2:
                                    texCoord.X = Half.FromBytes(texCoord0Stream.Data, (j * texCoord0Stream.BytesPerVertex) + texCoord0Offset + 0).ToSingle();
                                    texCoord.Y = 1.0f - Half.FromBytes(texCoord0Stream.Data, (j * texCoord0Stream.BytesPerVertex) + texCoord0Offset + 2).ToSingle();
                                    break;
                                default:
                                    texCoord.X = 0;
                                    texCoord.Y = 0;
                                    break;
                            }

                            streamWriter.WriteLine("vt " + texCoord.X.ToString(format) + " " + texCoord.Y.ToString(format));
                        }
                    }
                }
            }

            //faces
            UInt32 vertexCount = 0;

            for (Int32 i = 0; i < model.Meshes.Length; ++i)
            {
                Mesh mesh = model.Meshes[i];

                streamWriter.WriteLine("g Mesh" + i);

                for (Int32 j = 0; j < mesh.IndexCount; j += 3)
                {
                    UInt32 index0, index1, index2;

                    switch (mesh.IndexSize)
                    {
                        case 2:
                            index0 = vertexCount + BitConverter.ToUInt16(mesh.IndexData, (j * 2) + 0) + 1;
                            index1 = vertexCount + BitConverter.ToUInt16(mesh.IndexData, (j * 2) + 2) + 1;
                            index2 = vertexCount + BitConverter.ToUInt16(mesh.IndexData, (j * 2) + 4) + 1;
                            break;
                        case 4:
                            index0 = vertexCount + BitConverter.ToUInt32(mesh.IndexData, (j * 4) + 0) + 1;
                            index1 = vertexCount + BitConverter.ToUInt32(mesh.IndexData, (j * 4) + 4) + 1;
                            index2 = vertexCount + BitConverter.ToUInt32(mesh.IndexData, (j * 4) + 8) + 1;
                            break;
                        default:
                            index0 = 0;
                            index1 = 0;
                            index2 = 0;
                            break;
                    }

                    if (options.Normals && options.TextureCoordinates)
                    {
                        streamWriter.WriteLine("f " + index2 + "/" + index2 + "/" + index2 + " " + index1 + "/" + index1 + "/" + index1 + " " + index0 + "/" + index0 + "/" + index0);
                    }
                    else if (options.Normals)
                    {
                        streamWriter.WriteLine("f " + index2 + "//" + index2 + " " + index1 + "//" + index1 + " " + index0 + "//" + index0);
                    }
                    else if (options.TextureCoordinates)
                    {
                        streamWriter.WriteLine("f " + index2 + "/" + index2 + " " + index1 + "/" + index1 + " " + index0 + "/" + index0);
                    }
                    else
                    {
                        streamWriter.WriteLine("f " + index2 + " " + index1 + " " + index0);
                    }
                }

                vertexCount += (UInt32)mesh.VertexCount;
            }

            streamWriter.Close();
        }

        private static void exportAsSTLToDirectory(Model model, string directory, ExportOptions options)
        {
            //NumberFormatInfo format = new NumberFormatInfo();
            //format.NumberDecimalSeparator = ".";

            //String path = directory + @"\" + Path.GetFileNameWithoutExtension(model.Name) + ".stl";

            //FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
            //StreamWriter streamWriter = new StreamWriter(fileStream);

            //for (Int32 i = 0; i < model.Meshes.Length; ++i)
            //{
            //    Mesh mesh = model.Meshes[i];

            //    for (Int32 j = 0; j < mesh.Indices.Length; j += 3)
            //    {
            //        Vector3 normal = Vector3.Zero;
            //        normal += mesh.Vertices[mesh.Indices[j + 0]].Normal;
            //        normal += mesh.Vertices[mesh.Indices[j + 1]].Normal;
            //        normal += mesh.Vertices[mesh.Indices[j + 2]].Normal;
            //        normal.Normalize();

            //        streamWriter.WriteLine("facet normal " + normal.X.ToString("E", format) + " " + normal.Y.ToString("E", format) + " " + normal.Z.ToString("E", format));
            //        streamWriter.WriteLine("outer loop");

            //        for (Int32 k = 0; k < 3; ++k)
            //        {
            //            Vector3 vertex = mesh.Vertices[mesh.Indices[j + k]].Position;

            //            streamWriter.WriteLine("vertex " + vertex.X.ToString("E", format) + " " + vertex.Y.ToString("E", format) + " " + vertex.Z.ToString("E", format));
            //        }

            //        streamWriter.WriteLine("endloop");
            //        streamWriter.WriteLine("endfacet");
            //    }
            //}

            //streamWriter.Close();
        }

        public static ExportFormatOptions GetExportFormatOptionsByFormat(ExportFormats format)
        {
            return exportFormatOptions[format];
        }
    }
}
