namespace com.SonOfSofaman.Coloni.ConsoleApp
{
	public class TextureInfo
	{
		public int ID { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public TextureInfo(int id, int width, int height)
		{
			this.ID = id;
			this.Width = width;
			this.Height = height;
		}
	}

	public enum TextureTag
	{
		Splash
	}
}
