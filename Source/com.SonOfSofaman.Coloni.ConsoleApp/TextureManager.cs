using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace com.SonOfSofaman.Coloni.ConsoleApp
{
	class TextureManager
	{
		private Dictionary<string, TextureInfo> TextureInfoList;

		public TextureManager()
		{
			this.TextureInfoList = new Dictionary<string, TextureInfo>();
		}

		public void RegisterTexture(string textureTag, Bitmap bitmap)
		{
			TextureInfo textureInfo;
			if (this.TextureInfoList.TryGetValue(textureTag, out textureInfo))
			{
				this.TextureInfoList.Remove(textureTag);
			}
			else
			{
				textureInfo = new TextureInfo(GL.GenTexture(), bitmap.Width, bitmap.Height);
			}

			// Save the texture info for later
			this.TextureInfoList[textureTag] = textureInfo;

			// Tell OpenGL about the texture
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.BindTexture(TextureTarget.Texture2D, textureInfo.ID);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
			bitmap.UnlockBits(bitmapData);
		}

		public TextureInfo this[string textureTag]
		{
			get
			{
				return this.TextureInfoList[textureTag];
			}
		}
	}
}
