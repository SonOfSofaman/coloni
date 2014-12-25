using com.SonOfSofaman.Coloni.ConsoleApp.Scenes;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp
{
	internal class MainWindow : GameWindow
	{
		private Scene CurrentScene;
		private SplashScene SplashScene;

		internal MainWindow()
		{
			this.SplashScene = new SplashScene();
			this.SplashScene.OnExit += new SceneEvent(() => { this.Exit(); });
			this.SplashScene.OnDone += new SceneEvent(() => { this.Exit(); });
			this.CurrentScene = this.SplashScene;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.CurrentScene.Load();

			this.VSync = VSyncMode.On;
			this.WindowBorder = OpenTK.WindowBorder.Hidden;
			this.WindowState = OpenTK.WindowState.Fullscreen;

			Program.TextureManager.AddTexture(TextureTag.Splash, MakeSplashBitmap());

			GL.Enable(EnableCap.Texture2D);
		}

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
			this.CurrentScene.Unload();
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			GL.Viewport(0, 0, this.Width, this.Height);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);
			double deltaTime = e.Time;
			this.CurrentScene.Update(deltaTime, this.Keyboard);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			this.CurrentScene.Render(this.ClientSize);
			GL.Flush();
			this.SwapBuffers();
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
