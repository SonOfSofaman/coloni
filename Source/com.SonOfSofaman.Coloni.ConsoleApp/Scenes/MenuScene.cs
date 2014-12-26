using com.SonOfSofaman.Coloni.ConsoleApp.UI;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.Scenes
{
	internal class MenuScene : Scene
	{
		internal SceneEvent OnExit { get; set; }
		private List<Control> Controls = new List<Control>();

		internal MenuScene()
		{
			int y = 0;

			Button createNewWorldButton = new Button("create new world", new Rectangle(-80, 16 + (y-- * 40), 160, 32)) { Enabled = true };
			Button playButton = new Button("play", new Rectangle(-80, 16 + (y-- * 40), 160, 32)) { Enabled = true };
			Button exitButton = new Button("exit", new Rectangle(-80, 16 + (y-- * 40), 160, 32)) { Enabled = true };

			Controls.Add(createNewWorldButton);
			Controls.Add(playButton);
			Controls.Add(exitButton);
		}

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
		}

		internal override void Render(Size clientSize)
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-clientSize.Width / 2.0, clientSize.Width / 2.0, -clientSize.Height / 2.0, clientSize.Height / 2.0, -1.0, 1.0);

			foreach (Control control in Controls)
			{
				control.Render();
			}
		}
	}
}
