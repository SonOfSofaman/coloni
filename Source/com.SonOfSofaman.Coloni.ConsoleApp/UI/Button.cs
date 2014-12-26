using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.UI
{
	internal class Button : Control
	{
		public Scenes.SceneEvent OnClick { get; set; }

		public Button(string text, Rectangle bounds) : base(text, bounds)
		{
			Bitmap bitmap = this.MakeBitmap(this.Text, bounds, false, true);
			Program.TextureManager.RegisterTexture(this.TextureTag, bitmap);
		}

		public override void Render()
		{
			TextureInfo textureInfo = Program.TextureManager[this.TextureTag];
			GL.BindTexture(TextureTarget.Texture2D, textureInfo.ID);
			GL.Begin(PrimitiveType.Quads); // LL, UL, UR, LR
			GL.TexCoord2(0, 0); GL.Vertex2(this.Bounds.Left, this.Bounds.Bottom);
			GL.TexCoord2(0, 1); GL.Vertex2(this.Bounds.Left, this.Bounds.Top);
			GL.TexCoord2(1, 1); GL.Vertex2(this.Bounds.Right, this.Bounds.Top);
			GL.TexCoord2(1, 0); GL.Vertex2(this.Bounds.Right, this.Bounds.Bottom);
			GL.End();
		}

		private Bitmap MakeBitmap(string text, Rectangle bounds, bool highlighted, bool enabled)
		{
			Font font = new Font(FontFamily.GenericSansSerif, 14.0F);
			SolidBrush brush = new SolidBrush(enabled ? Color.FromArgb(0xFF, 0xFF, 0xCC) : Color.FromArgb(0x66, 0x66, 0x66));
			StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

			Bitmap result = new Bitmap(bounds.Width, bounds.Height);
			Graphics graphics = Graphics.FromImage(result);

			graphics.Clear(Color.FromArgb(0x33, 0x33, 0x33));
			graphics.DrawString(text, font, brush, new RectangleF(0.0F, 0.0F, bounds.Width, bounds.Height), format);

			return result;
		}
	}
}
