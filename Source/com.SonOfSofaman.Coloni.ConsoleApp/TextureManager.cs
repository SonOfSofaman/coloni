using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace com.SonOfSofaman.Coloni.ConsoleApp
{
	class TextureManager
	{
		private Dictionary<TextureTag, TextureInfo> TextureInfoList;

		public TextureManager()
		{
			this.TextureInfoList = new Dictionary<TextureTag, TextureInfo>();
		}

		public void AddTexture(TextureTag textureTag, Bitmap bitmap)
		{
			// Tell OpenGL about the texture
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			int textureID = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, textureID);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
			bitmap.UnlockBits(bitmapData);

			// Save the texture info for later
			this.TextureInfoList[textureTag] = new TextureInfo(textureID, bitmap.Width, bitmap.Height);
		}

		public TextureInfo this[TextureTag textureType]
		{
			get
			{
				return this.TextureInfoList[textureType];
			}
		}
	}
}
