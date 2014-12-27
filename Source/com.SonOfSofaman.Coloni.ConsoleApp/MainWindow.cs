using com.SonOfSofaman.Coloni.ConsoleApp.Scenes;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;

namespace com.SonOfSofaman.Coloni.ConsoleApp
{
	internal class MainWindow : GameWindow
	{
		private Scene CurrentScene;
		private MenuScene MenuScene;
		private CreateNewWorldScene CreateNewWorldScene;
		private SplashScene SplashScene;

		internal MainWindow()
		{
			this.VSync = VSyncMode.On;
			this.WindowBorder = OpenTK.WindowBorder.Hidden;
			this.WindowState = OpenTK.WindowState.Fullscreen;
			GL.Enable(EnableCap.Texture2D);
			GL.Viewport(0, 0, this.Width, this.Height);

			this.MenuScene = new MenuScene();
			this.MenuScene.OnCreateNewWorld += new SceneEvent(() => { this.SetCurrentScene(this.CreateNewWorldScene); });
			this.MenuScene.OnExit += new SceneEvent(() => { this.Exit(); });
			this.MenuScene.Viewport(0, 0, this.Width, this.Height);

			this.CreateNewWorldScene = new CreateNewWorldScene();
			this.CreateNewWorldScene.OnDone += new SceneEvent(() => { this.SetCurrentScene(this.MenuScene); });
			this.CreateNewWorldScene.Viewport(0, 0, this.Width, this.Height);

			this.SplashScene = new SplashScene();
			this.SplashScene.OnDone += new SceneEvent(() => { this.SetCurrentScene(this.MenuScene); });
			this.SplashScene.Viewport(0, 0, this.Width, this.Height);

			this.SetCurrentScene(this.SplashScene);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			double deltaTime = e.Time;

			MouseState mouseState = OpenTK.Input.Mouse.GetState();
			InputDeviceState inputDeviceState = new InputDeviceState();
			inputDeviceState.MouseX = this.Mouse.X;
			inputDeviceState.MouseY = this.Mouse.Y;
			inputDeviceState.LeftButtonDown = mouseState.IsButtonDown(MouseButton.Left);
			inputDeviceState.RightButtonDown = mouseState.IsButtonDown(MouseButton.Right);
			inputDeviceState.KeyboardState = OpenTK.Input.Keyboard.GetState();

			this.CurrentScene.Update(deltaTime, inputDeviceState);
		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			this.CurrentScene.Render();
			GL.Flush();

			this.SwapBuffers();
		}

		private void SetCurrentScene(Scene scene)
		{
			if (this.CurrentScene != null) this.CurrentScene.Exit();
			this.CurrentScene = scene;
			if (this.CurrentScene != null) this.CurrentScene.Enter();
		}
	}
}
