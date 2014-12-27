using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.UI
{
	internal abstract class Control
	{
		private static ulong NextID = 0UL;

		public ulong ID { get; private set; }
		public string TextureTag { get { return string.Format("Control.{0}", this.ID); } }
		public Rectangle Bounds { get; set; }
		public string Text { get; private set; }
		public bool Visible { get; set; }
		public bool Enabled { get; set; }
		public bool Active { get; set; }

		public Control(string text, Rectangle bounds)
		{
			this.ID = NextID++;
			this.Text = text;
			this.Bounds = bounds;
		}

		public abstract void Render();
		public virtual ControlEvent OnClick { get; set; }
	}
}
