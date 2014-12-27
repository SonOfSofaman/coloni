using OpenTK.Input;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.Scenes
{
	internal abstract class Scene
	{
		protected Rectangle ClientSize;
		protected InputDeviceState PreviousInputDeviceState = null;

		internal abstract void Enter();
		internal abstract void Exit();
		internal abstract void Update(double deltaTime, InputDeviceState inputDeviceState);
		internal abstract void Render();
		internal virtual void Viewport(int x, int y, int width, int height)
		{
			this.ClientSize = new Rectangle(x, y, width, height);
		}
		protected virtual bool PreviousKeyState(Key key)
		{
			return this.PreviousInputDeviceState != null && this.PreviousInputDeviceState.KeyboardState[key];
		}
	}
}
