using com.SonOfSofaman.Coloni.ConsoleApp.UI;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Collections.Generic;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.Scenes
{
	internal class MenuScene : Scene
	{
		internal SceneEvent OnCreateNewWorld { get; set; }
		internal SceneEvent OnPlay { get; set; }
		internal SceneEvent OnExit { get; set; }
		private List<Control> Controls = new List<Control>();

		internal MenuScene()
		{
			int index = 0;
			Button createNewWorldButton = new Button("create new world", new Rectangle(-80, (index-- * 40), 160, 32)) { Enabled = true, Visible = true };
			createNewWorldButton.OnClick += new ControlEvent(() => { if (this.OnCreateNewWorld != null) this.OnCreateNewWorld(); });

			Button playButton = new Button("play", new Rectangle(-80, (index-- * 40), 160, 32)) { Enabled = false, Visible = true };
			playButton.OnClick += new ControlEvent(() => { if (this.OnPlay != null) this.OnPlay(); });

			Button exitButton = new Button("exit", new Rectangle(-80, (index-- * 40), 160, 32)) { Enabled = true, Visible = true };
			exitButton.OnClick += new ControlEvent(() => { if (this.OnExit != null) this.OnExit(); });

			Controls.Add(createNewWorldButton);
			Controls.Add(playButton);
			Controls.Add(exitButton);
		}

		internal override void Enter()
		{
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-this.ClientSize.Width / 2.0, this.ClientSize.Width / 2.0, -this.ClientSize.Height / 2.0, this.ClientSize.Height / 2.0, -1.0, 1.0);
		}

		internal override void Exit()
		{
		}

		internal override void Update(double deltaTime, InputDeviceState inputDeviceState)
		{
			if (inputDeviceState.KeyboardState[Key.Escape])
			{
				if (this.OnExit != null) this.OnExit();
			}

			foreach (Control control in this.Controls.FindAll(c => c.Enabled))
			{
				if (control.Bounds.Contains(this.ClientSize.Width / 2 - inputDeviceState.MouseX, this.ClientSize.Height / 2 - inputDeviceState.MouseY))
				{
					control.Active = inputDeviceState.LeftButtonDown;
					if (inputDeviceState.LeftButtonDown)
					{
						if (control.OnClick != null) control.OnClick();
					}
				}
			}

			this.PreviousInputDeviceState = inputDeviceState;
		}

		internal override void Render()
		{
			foreach (Control control in Controls)
			{
				control.Render();
			}
		}
	}
}
