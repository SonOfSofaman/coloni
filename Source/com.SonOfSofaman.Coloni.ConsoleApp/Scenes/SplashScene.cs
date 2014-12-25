using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.Scenes
{
	internal class SplashScene : Scene
	{
		internal SceneEvent OnExit { get; set; }
		internal SceneEvent OnDone { get; set; }

		private double Countdown = 10.0;

		internal override void Load()
		{
		}

		internal override void Unload()
		{
		}

		internal override void Update(double deltaTime, KeyboardDevice keyboardDevice)
		{
			if (keyboardDevice[Key.Escape])
			{
				if (this.OnExit != null) this.OnExit();
			}
			this.Countdown -= deltaTime;
			if (this.Countdown <= 0.0)
			{
				this.Countdown = 0.0;
				if (this.OnDone != null) this.OnDone();
			}
		}

		internal override void Render(Size clientSize)
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-clientSize.Width / 2.0, clientSize.Width / 2.0, -clientSize.Height / 2.0, clientSize.Height / 2.0, -1.0, 1.0);

			TextureInfo textureInfo = Program.TextureManager[TextureTag.Splash];
			GL.BindTexture(TextureTarget.Texture2D, textureInfo.ID);
			GL.Begin(PrimitiveType.Quads); // LL, UL, UR, LR
			GL.TexCoord2(0, 0); GL.Vertex2(-textureInfo.Width / 2, textureInfo.Height / 2);
			GL.TexCoord2(0, 1); GL.Vertex2(-textureInfo.Width / 2, -textureInfo.Height / 2);
			GL.TexCoord2(1, 1); GL.Vertex2(textureInfo.Width / 2, -textureInfo.Height / 2);
			GL.TexCoord2(1, 0); GL.Vertex2(textureInfo.Width / 2, textureInfo.Height / 2);
			GL.End();
		}

	}
}
