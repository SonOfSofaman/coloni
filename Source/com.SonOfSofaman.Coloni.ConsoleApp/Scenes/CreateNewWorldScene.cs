using com.SonOfSofaman.Coloni.ConsoleApp.UI;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.Scenes
{
	internal class CreateNewWorldScene : Scene
	{
		internal SceneEvent OnDone { get; set; }

		private double Countdown;

		internal CreateNewWorldScene()
		{
			Program.TextureManager.RegisterTexture("CreateNewWorldScene.Message", MakeBitmap());
		}

		internal override void Enter()
		{
			this.Countdown = 10.0;

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-this.ClientSize.Width / 2.0, this.ClientSize.Width / 2.0, -this.ClientSize.Height / 2.0, this.ClientSize.Height / 2.0, -1.0, 1.0);
		}

		internal override void Exit()
		{
		}

		internal override void Update(double deltaTime, InputDeviceState inputDeviceState)
		{
			this.Countdown -= deltaTime;
			if (this.Countdown <= 0.0)
			{
				this.Countdown = 0.0;
				if (this.OnDone != null) this.OnDone();
			}

			if (this.PreviousKeyState(Key.Escape) && !inputDeviceState.KeyboardState[Key.Escape])
			{
				if (this.OnDone != null) this.OnDone();
			}

			this.PreviousInputDeviceState = inputDeviceState;
		}

		internal override void Render()
		{
			TextureInfo textureInfo = Program.TextureManager["CreateNewWorldScene.Message"];
			GL.BindTexture(TextureTarget.Texture2D, textureInfo.ID);
			GL.Begin(PrimitiveType.Quads); // LL, UL, UR, LR
			GL.TexCoord2(0, 0); GL.Vertex2(-textureInfo.Width / 2, textureInfo.Height / 2);
			GL.TexCoord2(0, 1); GL.Vertex2(-textureInfo.Width / 2, -textureInfo.Height / 2);
			GL.TexCoord2(1, 1); GL.Vertex2(textureInfo.Width / 2, -textureInfo.Height / 2);
			GL.TexCoord2(1, 0); GL.Vertex2(textureInfo.Width / 2, textureInfo.Height / 2);
			GL.End();
		}

		private static Bitmap MakeBitmap()
		{
			Size size = new Size(128, 48);
			Font font = new Font(FontFamily.GenericSansSerif, 14.0F);
			SolidBrush brush = new SolidBrush(Palette.UIForeground);
			StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

			Bitmap result = new Bitmap(size.Width, size.Height);
			Graphics graphics = Graphics.FromImage(result);

			graphics.Clear(Color.Black);
			graphics.DrawString("not implemented", font, brush, new RectangleF(0.0F, 0.0F, size.Width, size.Height), format);

			return result;
		}
	}
}
