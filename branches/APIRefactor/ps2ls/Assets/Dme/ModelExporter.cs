using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using OpenTK;
using System.IO;
using ps2ls.Graphics.Materials;
using ps2ls.Assets.Pack;

namespace ps2ls.Assets.Dme
{
    public class ModelExporter
    {
        public enum ModelExportFormats
        {
            OBJ,
            //STL
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
            public ModelExportFormats Format;
            public String Name;
            public ExportCapabilities Capabilities;
            public ExportOptions Options;
        }

        public class ExportOptions
        {
            public Axes UpAxis;
            public Axes LeftAxis;
            public Boolean Normals;
            public Boolean TextureCoordinates;
            public Vector3 Scale;
            public Boolean Textures;
            public Boolean Package;
        }

        public struct ModelAxesPreset
        {
            public String Name;
            public ModelExporter.Axes UpAxis;
            public ModelExporter.Axes LeftAxis;

            public override string ToString()
            {
                return Name;
            }
        };

        private static Axes getForwardAxis(Axes leftAxis, Axes upAxis)
        {
            if (leftAxis != Axes.X && upAxis != Axes.X)
                return Axes.X;
            else if (leftAxis != Axes.Y && upAxis != Axes.Y)
                return Axes.Y;
            else
                return Axes.Z;
        }

        private static Dictionary<ModelExporter.ModelExportFormats, ModelExporter.ExportFormatOptions> exportFormatOptions;

        public static List<ModelAxesPreset> ModelAxesPresets { get; private set; }

        static ModelExporter()
        {
            exportFormatOptions = new Dictionary<ModelExportFormats, ExportFormatOptions>();

            createExportFormatOptions();
            createModelAxesPresets();
        }

        private static void createExportFormatOptions()
        {
            ModelExporter.ExportFormatOptions options = new ModelExporter.ExportFormatOptions();

            //obj
            options.Format = ModelExporter.ModelExportFormats.OBJ;
            options.Name = "Wavefront OBJ (*.obj)";
            options.Capabilities.Normals = false;
            options.Capabilities.TextureCoordinates = true;
            options.Options = new ExportOptions();
            options.Options.Scale = Vector3.One;
            options.Options.Normals = false;
            options.Options.TextureCoordinates = true;
            options.Options.Package = false;
            options.Options.Textures = false;
            exportFormatOptions.Add(ModelExporter.ModelExportFormats.OBJ, options);

            //TODO: Add STL support back once we can reliably get normals.
            ////stl
            //options.Format = ModelExporter.ModelExportFormats.STL;
            //options.Name = "Stereolithography (*.stl)";
            //options.Capabilities.Normals = false;
            //options.Capabilities.TextureCoordinates = false;
            //options.Options = new ExportOptions();
            //options.Options.Scale = Vector3.One;
            //options.Options.Normals = true;
            //options.Options.TextureCoordinates = false;
            //options.Options.Package = false;
            //options.Options.Textures = false;
            //exportFormatOptions.Add(ModelExporter.ModelExportFormats.STL, options);
        }

        private static void createModelAxesPresets()
        {
            ModelAxesPresets = new List<ModelAxesPreset>();

            ModelAxesPreset modelAxesPreset = new ModelAxesPreset();
            modelAxesPreset.Name = "Default";
            modelAxesPreset.UpAxis = Axes.Y;
            modelAxesPreset.LeftAxis = Axes.X;
            ModelAxesPresets.Add(modelAxesPreset);

            modelAxesPreset = new ModelAxesPreset();
            modelAxesPreset.Name = "Autodesk® 3ds Max";
            modelAxesPreset.UpAxis = Axes.Z;
            modelAxesPreset.LeftAxis = Axes.Y;
            ModelAxesPresets.Add(modelAxesPreset);
        }

        public static void ExportModelToDirectory(Model model, string directory, ExportFormatOptions formatOptions)
        {
            switch (formatOptions.Format)
            {
                case ModelExportFormats.OBJ:
                    exportModelAsOBJToDirectory(model, directory, formatOptions.Options);
                    break;
                //case ModelExportFormats.STL:
                //    exportModelAsSTLToDirectory(model, directory, formatOptions.Options);
                //    break;
            }
        }

        private static void exportModelAsOBJToDirectory(Model model, string directory, ExportOptions options)
        {
            //TODO: Figure out what to do with non-version 4 models.
            if (model != null && model.Version != 4)
            {
                return;
            }

            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            if (options.Package)
            {
                try
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(directory + @"\" + Path.GetFileNameWithoutExtension(model.Name));
                    directory = directoryInfo.FullName;
                }
                catch (Exception) { }
            }

            if (options.Textures)
            {
                // Build a list of assets from their names
                IEnumerable<Asset> textureAssets = Program.AssetManager.AssetsByType[Asset.Types.DDS].Where(a => model.Textures.Contains(a.Name));

                Dictionary<Pack.Pack, IList<Asset>> sortedTextureAssets = Pack.Pack.GetAssetListSortedByPack(textureAssets);

                // Now export all the textures
                foreach (Pack.Pack pack in sortedTextureAssets.Keys)
                {
                    pack.ExtractAssetsToDirectory(sortedTextureAssets[pack], directory);
                }
            }

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

                    position.X = BitConverter.ToSingle(positionStream.Data, positionOffset + (positionStream.BytesPerVertex * j) + 0);
                    position.Y = BitConverter.ToSingle(positionStream.Data, positionOffset + (positionStream.BytesPerVertex * j) + 4);
                    position.Z = BitConverter.ToSingle(positionStream.Data, positionOffset + (positionStream.BytesPerVertex * j) + 8);

                    switch (options.LeftAxis)
                    {
                        case Axes.X:
                            position.X = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 0);
                            break;
                        case Axes.Y:
                            position.Y = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 0);
                            break;
                        case Axes.Z:
                            position.Z = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 0);
                            break;
                    }

                    switch (options.UpAxis)
                    {
                        case Axes.X:
                            position.X = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 4);
                            break;
                        case Axes.Y:
                            position.Y = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 4);
                            break;
                        case Axes.Z:
                            position.Z = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 4);
                            break;
                    }

                    Axes forwardAxis = getForwardAxis(options.LeftAxis, options.UpAxis);

                    switch (forwardAxis)
                    {
                        case Axes.X:
                            position.X = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 8);
                            break;
                        case Axes.Y:
                            position.Y = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 8);
                            break;
                        case Axes.Z:
                            position.Z = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 8);
                            break;
                    }

                    position.X *= options.Scale.X;
                    position.Y *= options.Scale.Y;
                    position.Z *= options.Scale.Z;

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

        private static void exportModelAsSTLToDirectory(Model model, string directory, ExportOptions options)
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

        public static ExportFormatOptions GetExportFormatOptionsByFormat(ModelExportFormats format)
        {
            return exportFormatOptions[format];
        }
    }
}
