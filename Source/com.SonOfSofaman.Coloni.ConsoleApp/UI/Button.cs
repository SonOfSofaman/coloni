using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.UI
{
	internal class Button : Control
	{
		public Button(string text, Rectangle bounds) : base(text, bounds)
		{
			Program.TextureManager.RegisterTexture(this.TextureTag + "_enabled_active", this.MakeBitmap(this.Text, bounds, true, true));
			Program.TextureManager.RegisterTexture(this.TextureTag + "_enabled_inactive", this.MakeBitmap(this.Text, bounds, true, false));
			Program.TextureManager.RegisterTexture(this.TextureTag + "_disabled_active", this.MakeBitmap(this.Text, bounds, false, true));
			Program.TextureManager.RegisterTexture(this.TextureTag + "_disabled_inactive", this.MakeBitmap(this.Text, bounds, false, false));
		}

		public override void Render()
		{
			if (this.Visible)
			{
				int[] viewport = new int[4]; // L, T, R, B
				GL.GetInteger(GetPName.Viewport, viewport);
				int width = viewport[2] - viewport[0];
				int height = viewport[3] - viewport[1];

				GL.PushMatrix();
				{
					GL.LoadIdentity();
					GL.Ortho(-width / 2.0, width / 2.0, -height / 2.0, height / 2.0, -1.0, 1.0); // L, R, B, T
					TextureInfo textureInfo = Program.TextureManager[this.TextureTag + "_" + (this.Enabled ? "enabled" : "disabled") + "_" + (this.Active ? "active" : "inactive")];
					GL.BindTexture(TextureTarget.Texture2D, textureInfo.ID);
					GL.Begin(PrimitiveType.Quads); // LL, UL, UR, LR
					GL.TexCoord2(0, 0); GL.Vertex2(this.Bounds.Left, this.Bounds.Bottom);
					GL.TexCoord2(0, 1); GL.Vertex2(this.Bounds.Left, this.Bounds.Top);
					GL.TexCoord2(1, 1); GL.Vertex2(this.Bounds.Right, this.Bounds.Top);
					GL.TexCoord2(1, 0); GL.Vertex2(this.Bounds.Right, this.Bounds.Bottom);
					GL.End();
				}
				GL.PopMatrix();
			}
		}

		private Bitmap MakeBitmap(string text, Rectangle bounds, bool enabled, bool active)
		{
			Font font = new Font(FontFamily.GenericSansSerif, 14.0F);
			SolidBrush foreground = new SolidBrush(enabled ? Palette.UIForeground : Palette.UIForegroundDisabled);
			SolidBrush background = new SolidBrush(active ? Palette.UIBackgroundActive : Palette.UIBackground);
			Pen backgroundPen = new Pen(active ? Palette.UIBackgroundActive : Palette.UIBackground);
			StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

			Bitmap result = new Bitmap(bounds.Width, bounds.Height);
			Graphics graphics = Graphics.FromImage(result);

			float radius = 24.0F;
			graphics.Clear(Palette.UIPanel);

			graphics.FillEllipse(background, 0.0F, 0.0F, radius, radius);
			graphics.DrawEllipse(backgroundPen, 0.0F, 0.0F, radius, radius);

			graphics.FillEllipse(background, bounds.Width - radius - 1.0F, 0.0F, radius, radius);
			graphics.DrawEllipse(backgroundPen, bounds.Width - radius - 1.0F, 0.0F, radius, radius);

			graphics.FillEllipse(background, 0.0F, bounds.Height - radius - 1.0F, radius, radius);
			graphics.DrawEllipse(backgroundPen, 0.0F, bounds.Height - radius - 1.0F, radius, radius);

			graphics.FillEllipse(background, bounds.Width - radius - 1.0F, bounds.Height - radius - 1.0F, radius, radius);
			graphics.DrawEllipse(backgroundPen, bounds.Width - radius - 1.0F, bounds.Height - radius - 1.0F, radius, radius);

			graphics.FillRectangle(background, 0.0F, radius / 2.0F, bounds.Width, bounds.Height - radius);
			graphics.FillRectangle(background, radius / 2.0F, 0.0F, bounds.Width - radius, bounds.Height);

			graphics.DrawString(text, font, foreground, new RectangleF(active ? 0.0F : -1.0F, active ? 0.0F : -1.0F, bounds.Width, bounds.Height), format);

			return result;
		}
	}
}
