using OpenTK.Input;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.Scenes
{
	internal abstract class Scene
	{
		internal abstract void Load();
		internal abstract void Unload();
		internal abstract void Update(double deltaTime, KeyboardDevice keyboardDevice);
		internal abstract void Render(Size clientSize);
	}
}
