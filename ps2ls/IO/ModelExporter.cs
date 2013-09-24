using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using ps2ls.Assets.Dme;

namespace ps2ls.IO
{
    public enum Axes
    {
        X,
        Y,
        Z
    }

    public static class AxisResolver
    {
        public static Axes Resolve(Axes axis1, Axes axis2)
        {
            if (axis1 == axis2)
                throw new ArgumentException("Cannot resolve duplicate axes.");

            if ((axis1 == Axes.X || axis1 == Axes.Y) && (axis2 == Axes.X || axis2 == Axes.Y))
                return Axes.Z;
            else if ((axis1 == Axes.X || axis1 == Axes.Z) && (axis2 == Axes.X || axis2 == Axes.Z))
                return Axes.Y;
            else
                return Axes.X;
        }
    }

    public class ModelExportOptions
    {
        public Axes UpAxis;
        public Axes LeftAxis;
        public Axes ForwardAxis
        {
            get
            {
                return AxisResolver.Resolve(UpAxis, LeftAxis);
            }
        }
        public bool Normals;
        public bool TextureCoordinates;
        public Vector3 Scale;
        public bool Textures;
        public bool Package;
        public TextureExporter.TextureFormatInfo TextureFormat;
    }

    public interface IModelExporter
    {
        string Name { get; }
        string Extension { get; }
        bool CanExportNormals { get; }
        bool CanExportTextureCoordinates { get; }

        void ExportModelToDirectoryWithExportOptions(Model model, string directory, ModelExportOptions exportOptions);
    }
}
