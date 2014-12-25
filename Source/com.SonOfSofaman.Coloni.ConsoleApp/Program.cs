namespace com.SonOfSofaman.Coloni.ConsoleApp
{
	class Program
	{
		internal static TextureManager TextureManager = new TextureManager();

		static void Main(string[] args)
		{
			using (MainWindow window = new MainWindow())
			{
				window.Run(60.0);
			}
		}
	}
}
