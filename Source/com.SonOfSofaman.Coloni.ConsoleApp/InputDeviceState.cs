using OpenTK.Input;

namespace com.SonOfSofaman.Coloni.ConsoleApp
{
	internal class InputDeviceState
	{
		public int MouseX { get; set; }
		public int MouseY { get; set; }
		public bool LeftButtonDown { get; set; }
		public bool RightButtonDown { get; set; }
		public KeyboardState KeyboardState { get; set; }
	}
}
