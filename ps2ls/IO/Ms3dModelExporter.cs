using ps2ls.Assets.Dma;
using ps2ls.Assets.Dme;
using ps2ls.Graphics.Materials;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps2ls.IO
{
    public class Ms3dModelExporter : ModelExporter
    {
        private const int MAX_VERTICES = 65534;
        private const int MAX_TRIANGLES = 65534;
        private const int MAX_GROUPS = 255;
        private const int MAX_MATERIALS = 128;
        private const int MAX_JOINTS = 128;

        enum Flags
        {
            Selected = 1,
            Hidden = 2,
            Selected2 = 4,
            Dirty = 8
        }

        private const string MAGIC = "MS3D000000";
        private const int VERSION = 4;

        public string Name
        {
            get { return "MilkShape 3D"; }
        }

        public string Extension
        {
            get { return "ms3d"; }
        }

        public bool CanExportNormals
        {
            get { return true; }
        }

        public bool CanExportTextureCoordinates
        {
            get { return true; }
        }

        public void ExportModelToDirectoryWithExportOptions(Assets.Dme.Model model, string directory, ModelExportOptions exportOptions)
        {
            if (model.VertexCount > MAX_VERTICES)
                throw new Exception(string.Format("Vertex count ({0}) exceeds allowable maximum ({1}).", model.VertexCount, MAX_VERTICES));

            if (model.TriangleCount / 3 > MAX_TRIANGLES)
                throw new Exception(string.Format("Triangle count ({0}) exceeds allowable maximum ({1}).", model.TriangleCount, MAX_VERTICES));

            String fileName = string.Format("{0}{1}.{2}", directory, model.Name, Extension);
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Write);

            BinaryWriter binaryWriter = new BinaryWriter(fileStream);

            binaryWriter.Write(MAGIC.ToCharArray());
            binaryWriter.Write(VERSION);

            binaryWriter.Write((ushort)model.VertexCount);

            foreach(Mesh mesh in model.Meshes)
            {
                Material material = model.Materials[(int)mesh.MaterialIndex];
                MaterialDefinition materialDefinition = MaterialDefinitionLibrary.Instance.GetMaterialDefinitionFromHash(material.MaterialDefinitionHash);
                VertexLayout vertexLayout = MaterialDefinitionLibrary.Instance.VertexLayouts[materialDefinition.DrawStyles[0].VertexLayoutNameHash];

                //position
                VertexLayout.Entry.DataTypes positionDataType;
                int positionOffset;
                int positionStreamIndex;

                vertexLayout.GetEntryInfoFromDataUsageAndUsageIndex(VertexLayout.Entry.DataUsages.Position, 0, out positionDataType, out positionStreamIndex, out positionOffset);
                
                Mesh.VertexStream positionStream = mesh.VertexStreams[positionStreamIndex];


                foreach (Mesh.VertexStream vertexStream in mesh.VertexStreams)
                {
                    materialDefinition.DrawStyles[0].VertexLayoutNameHash
                }
            }

            binaryWriter.Close();
        }
    }
}
