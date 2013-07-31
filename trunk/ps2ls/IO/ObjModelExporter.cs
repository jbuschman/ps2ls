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
            get { return true; }
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

            NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.NumberDecimalSeparator = ".";

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

                //positions
                List<Vector3> positions;
                if (mesh.GetPositions(out positions, 0))
                {
                    foreach (Vector3 position in positions)
                    {
                        Vector3 scaledPosition = Vector3.Multiply(position, exportOptions.Scale);
                        streamWriter.WriteLine("v {0} {1} {2}", scaledPosition.X.ToString(numberFormatInfo), scaledPosition.Y.ToString(numberFormatInfo), scaledPosition.Z.ToString(numberFormatInfo));
                    }
                }

                //texture coordinates
                if (exportOptions.TextureCoordinates)
                {
                    Vector2[] texCoords;
                    if (mesh.GetTexCoords(out texCoords, 0))
                    {
                        foreach (Vector2 texcoord in texCoords)
                            streamWriter.WriteLine("vt {0} {1}", texcoord.X.ToString(numberFormatInfo), (-texcoord.Y).ToString(numberFormatInfo));
                    }
                }

                //normals
                if (exportOptions.Normals)
                {
                    Vector3[] normals;
                    if (mesh.GetNormals(out normals, 0))
                    {
                        foreach (Vector3 normal in normals)
                            streamWriter.WriteLine("vn {0} {1} {2}", normal.X.ToString(numberFormatInfo), normal.Y.ToString(numberFormatInfo), normal.Z.ToString(numberFormatInfo));
                    }
                }
            }

            //faces
            uint vertexCount = 0;

            for (int i = 0; i < model.Meshes.Length; ++i)
            {
                Mesh mesh = model.Meshes[i];

                streamWriter.WriteLine("g Mesh{0}", i);

                uint[] indices;
                mesh.GetIndices(out indices);

                for (int j = 0; j < mesh.IndexCount; j += 3)
                {
                    uint index0 = indices[j + 0];
                    uint index1 = indices[j + 1];
                    uint index2 = indices[j + 2];

                    if (exportOptions.TextureCoordinates && exportOptions.Normals)
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
