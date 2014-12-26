using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.SonOfSofaman.Coloni.ConsoleApp.UI
{
	internal abstract class Control
	{
		private static ulong NextID = 0UL;

		public ulong ID { get; private set; }
		public string TextureTag { get { return string.Format("Control.{0}", this.ID); } }
		public Rectangle Bounds { get; set; }
		public string Text { get; private set; }
		public bool Enabled { get; set; }

		public Control(string text, Rectangle bounds)
		{
			this.ID = NextID++;
			this.Text = text;
			this.Bounds = bounds;
		}

		public abstract void Render();
	}
}
