﻿using System;
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
                throw new ArgumentException("Canot resolve duplicate axes.");

            if ((axis1 == Axes.X || axis1 == Axes.Y) && (axis2 == Axes.X || axis2 == Axes.Y))
                return Axes.Z;
            else if ((axis1 == Axes.X || axis1 == Axes.Z) && (axis2 == Axes.X || axis2 == Axes.Z))
                return Axes.Z;
            else
                return Axes.Z;
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
                if (LeftAxis != Axes.X && UpAxis != Axes.X)
                    return Axes.X;
                else if (LeftAxis != Axes.Y && UpAxis != Axes.Y)
                    return Axes.Y;

                return Axes.Z;
            }
        }
        public Boolean Normals;
        public Boolean TextureCoordinates;
        public Vector3 Scale;
        public Boolean Textures;
        public Boolean Package;
        public TextureExporter.TextureFormatInfo TextureFormat;
    }

    public interface ModelExporter
    {
        string Name { get; }
        string Extension { get; }
        Boolean CanExportNormals { get; }
        Boolean CanExportTextureCoordinates { get; }

        void ExportModelToDirectoryWithExportOptions(Model model, string directory, ModelExportOptions exportOptions);
    }
}
