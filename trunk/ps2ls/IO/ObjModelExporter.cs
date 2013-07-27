using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ps2ls.Assets.Dme;
using System.Globalization;
using System.IO;
using DevIL;
using ps2ls.Assets.Pack;
using ps2ls.Graphics.Materials;
using OpenTK;

namespace ps2ls.IO
{
    public class ObjModelExporter : IModelExporter
    {
        public string Name
        {
            get { return "Wavefront OBJ"; }
        }

        public string Extension
        {
            get { return "obj"; }
        }

        public bool CanExportNormals
        {
            get { return false; }
        }

        public bool CanExportTextureCoordinates
        {
            get { return true; }
        }

        public override string ToString()
        {
            return string.Format("{0} (*.{1})", Name, Extension);
        }

        public void ExportModelToDirectoryWithExportOptions(Model model, string directory, ModelExportOptions exportOptions)
        {
            if (model == null || directory == null || exportOptions == null )
                return;

            NumberFormatInfo format = new NumberFormatInfo();
            format.NumberDecimalSeparator = ".";

            if (exportOptions.Package)
            {
                try
                {
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(directory + @"\" + Path.GetFileNameWithoutExtension(model.Name));
                    directory = directoryInfo.FullName;
                }
                catch (Exception) { }
            }

            if (exportOptions.Textures)
            {
                ImageImporter imageImporter = new ImageImporter();
                ImageExporter imageExporter = new ImageExporter();

                foreach(string textureString in model.TextureStrings)
                {
                    MemoryStream textureMemoryStream = AssetManager.Instance.CreateAssetMemoryStreamByName(textureString);

                    if(textureMemoryStream == null)
                        continue;

                    Image textureImage = imageImporter.LoadImageFromStream(textureMemoryStream);

                    if(textureImage == null)
                        continue;

                    imageExporter.SaveImage(textureImage, exportOptions.TextureFormat.ImageType, directory + @"\" + Path.GetFileNameWithoutExtension(textureString) + @"." + exportOptions.TextureFormat.Extension);
                }
            }

            string path = string.Format(@"{0}\{1}.{2}", directory, Path.GetFileNameWithoutExtension(model.Name), Extension);

            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            for (int i = 0; i < model.Meshes.Length; ++i)
            {
                Mesh mesh = model.Meshes[i];

                MaterialDefinition materialDefinition = MaterialDefinitionLibrary.Instance.MaterialDefinitions[model.Materials[(int)mesh.MaterialIndex].MaterialDefinitionHash];
                VertexLayout vertexLayout = MaterialDefinitionLibrary.Instance.VertexLayouts[materialDefinition.DrawStyles[0].VertexLayoutNameHash];

                //position
                VertexLayout.Entry.DataTypes positionDataType;
                int positionOffset;
                int positionStreamIndex;

                bool positionPresent = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Position, 0, out positionDataType, out positionStreamIndex, out positionOffset);

                if (positionPresent)
                {
                    Mesh.VertexStream positionStream = mesh.VertexStreams[positionStreamIndex];

                    for (int j = 0; j < mesh.VertexCount; ++j)
                    {
                        Vector3 position = Vector3.Zero;
                        position.X = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 0);
                        position.Y = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 4);
                        position.Z = BitConverter.ToSingle(positionStream.Data, (positionStream.BytesPerVertex * j) + positionOffset + 8);

                        //todo: convert x/y/z coordinates

                        position.Scale(exportOptions.Scale);

                        streamWriter.WriteLine("v {0} {1} {2}", position.X.ToString(format), position.Y.ToString(format), position.Z.ToString(format));
                    }
                }

                //texture coordinates
                if (exportOptions.TextureCoordinates)
                {
                    VertexLayout.Entry.DataTypes texCoord0DataType;
                    int texCoord0Offset = 0;
                    int texCoord0StreamIndex = 0;

                    bool texCoord0Present = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Texcoord, 0, out texCoord0DataType, out texCoord0StreamIndex, out texCoord0Offset);

                    if (texCoord0Present)
                    {
                        Mesh.VertexStream texCoord0Stream = mesh.VertexStreams[texCoord0StreamIndex];

                        for (int j = 0; j < mesh.VertexCount; ++j)
                        {
                            Vector2 texCoord = Vector2.Zero;

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
                            }

                            streamWriter.WriteLine("vt {0} {2}", texCoord.X.ToString(format), texCoord.Y.ToString(format));
                        }
                    }

                    if (exportOptions.Normals)
                    {
                        VertexLayout.Entry.DataTypes normalDataType;
                        int normalOffset = 0;
                        int normalStreamIndex = 0;

                        bool normalPresent = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Normal, 0, out normalDataType, out normalStreamIndex, out normalOffset);

                        if (normalPresent)
                        {
                            Mesh.VertexStream normalStream = mesh.VertexStreams[positionStreamIndex];

                            for(int j = 0; j < mesh.VertexCount; ++j)
                            {
                                Vector3 normal = Vector3.Zero;

                                switch (texCoord0DataType)
                                {
                                    case VertexLayout.Entry.DataTypes.Float3:
                                        normal.X = BitConverter.ToSingle(normalStream.Data, (j * normalStream.BytesPerVertex) + 0);
                                        normal.Y = BitConverter.ToSingle(normalStream.Data, (j * normalStream.BytesPerVertex) + 4);
                                        normal.Z = BitConverter.ToSingle(normalStream.Data, (j * normalStream.BytesPerVertex) + 8);
                                        break;
                                    case VertexLayout.Entry.DataTypes.ubyte4n:
                                        normal.X = (float)(normalStream.Data[(j * normalStream.BytesPerVertex) + 0] - 127) / 128.0f;
                                        normal.Y = (float)(normalStream.Data[(j * normalStream.BytesPerVertex) + 1] - 127) / 128.0f;
                                        normal.Z = (float)(normalStream.Data[(j * normalStream.BytesPerVertex) + 2] - 127) / 128.0f;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            VertexLayout.Entry.DataTypes binormalDataType;
                            int binormalOffset = 0;
                            int binormalStreamIndex = 0;

                            bool binormalPresent = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Binormal, 0, out binormalDataType, out binormalStreamIndex, out binormalOffset);

                            VertexLayout.Entry.DataTypes tangentDataType;
                            int tangentOffset = 0;
                            int tangentStreamIndex = 0;

                            bool tangentPresent = vertexLayout.GetEntryInfo(VertexLayout.Entry.DataUsages.Tangent, 0, out tangentDataType, out tangentOffset, out tangentStreamIndex);

                            if (binormalPresent && tangentPresent)
                            {

                            }
                        }
                    }
                }
            }

            //faces
            uint vertexCount = 0;

            for (int i = 0; i < model.Meshes.Length; ++i)
            {
                Mesh mesh = model.Meshes[i];

                streamWriter.WriteLine("g Mesh{0}", i);

                for (int j = 0; j < mesh.IndexCount; j += 3)
                {
                    uint index0 = 0;
                    uint index1 = 0;
                    uint index2 = 0;

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
                    }

                    if (exportOptions.Normals && exportOptions.TextureCoordinates)
                    {
                        streamWriter.WriteLine("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}", index2, index1, index0);
                    }
                    else if (exportOptions.Normals)
                    {
                        streamWriter.WriteLine("f {0}//{0} {1}//{1} {2}//{2}", index2, index1, index0);
                    }
                    else if (exportOptions.TextureCoordinates)
                    {
                        streamWriter.WriteLine("f {0}/{0} {1}/{1} {2}/{2}", index2, index1, index0);
                    }
                    else
                    {
                        streamWriter.WriteLine("f {0} {1} {2}", index2, index1, index0);
                    }
                }

                vertexCount += (uint)mesh.VertexCount;
            }

            streamWriter.Close();
        }
    }
}
