using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DevIL;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace ps2ls
{
    public class TextureManager
    {
        #region Singleton
        private static TextureManager instance = null;

        public static void CreateInstance()
        {
            instance = new TextureManager();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static TextureManager Instance { get { return instance; } }
        #endregion


        private static List<int> textures = new List<int>();

        public Int32 LoadFromStream(Stream stream)
        {
            if (stream == null)
                return 0;

            ImageImporter imageImporter = new ImageImporter();

            Image image = imageImporter.LoadImageFromStream(stream);

            GCHandle imageDataGCHandle = GCHandle.Alloc(image.GetImageData(0).Data, GCHandleType.Pinned);
            IntPtr imageDataIntPtr = imageDataGCHandle.AddrOfPinnedObject();

            Int32 glTextureHandle = GL.GenTexture();

            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, glTextureHandle);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (Int32)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (Int32)All.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, imageDataIntPtr);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Disable(EnableCap.Texture2D);

            imageDataGCHandle.Free();

            textures.Add(glTextureHandle);

            return glTextureHandle;
        }

        public void Clear()
        {
            foreach (Int32 texture in textures)
            {
                GL.DeleteTexture(texture);
            }
        }
    }
}
