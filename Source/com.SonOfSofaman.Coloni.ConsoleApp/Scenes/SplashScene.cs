using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.Scenes
{
	internal class SplashScene : Scene
	{
		internal SceneEvent OnDone { get; set; }

		private double Countdown = 10.0;

		internal SplashScene()
		{
			Program.TextureManager.RegisterTexture("SplashScene.Logo", MakeSplashBitmap());
		}

		internal override void Load()
		{
		}

		internal override void Unload()
		{
		}

		internal override void Update(double deltaTime, KeyboardDevice keyboardDevice)
		{
			this.Countdown -= deltaTime;
			if (this.Countdown <= 0.0)
			{
				this.Countdown = 0.0;
				if (this.OnDone != null) this.OnDone();
			}
			if (keyboardDevice[Key.Space])
			{
				if (this.OnDone != null) this.OnDone();
			}
		}

		internal override void Render(Size clientSize)
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-clientSize.Width / 2.0, clientSize.Width / 2.0, -clientSize.Height / 2.0, clientSize.Height / 2.0, -1.0, 1.0);

			TextureInfo textureInfo = Program.TextureManager["SplashScene.Logo"];
			GL.BindTexture(TextureTarget.Texture2D, textureInfo.ID);
			GL.Begin(PrimitiveType.Quads); // LL, UL, UR, LR
			GL.TexCoord2(0, 0); GL.Vertex2(-textureInfo.Width / 2, textureInfo.Height / 2);
			GL.TexCoord2(0, 1); GL.Vertex2(-textureInfo.Width / 2, -textureInfo.Height / 2);
			GL.TexCoord2(1, 1); GL.Vertex2(textureInfo.Width / 2, -textureInfo.Height / 2);
			GL.TexCoord2(1, 0); GL.Vertex2(textureInfo.Width / 2, textureInfo.Height / 2);
			GL.End();
		}

		private static Bitmap MakeSplashBitmap()
		{
			Size size = new Size(128, 48);
			Font font = new Font(FontFamily.GenericSansSerif, 28.0F);
			SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 204));
			StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

			Bitmap result = new Bitmap(size.Width, size.Height);
			Graphics graphics = Graphics.FromImage(result);

			graphics.Clear(Color.Black);
			graphics.DrawString("coloni", font, brush, new RectangleF(0.0F, 0.0F, size.Width, size.Height), format);

			return result;
		}
	}
}
