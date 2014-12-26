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
		private MenuScene MenuScene;

		internal MainWindow()
		{
			this.SplashScene = new SplashScene();
			this.SplashScene.OnDone += new SceneEvent(() => { this.CurrentScene = this.MenuScene; });

			this.MenuScene = new MenuScene();
			this.MenuScene.OnExit += new SceneEvent(() => { this.Exit(); });

			this.CurrentScene = this.SplashScene;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.CurrentScene.Load();

			this.VSync = VSyncMode.On;
			this.WindowBorder = OpenTK.WindowBorder.Hidden;
			this.WindowState = OpenTK.WindowState.Fullscreen;

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
	}
}
