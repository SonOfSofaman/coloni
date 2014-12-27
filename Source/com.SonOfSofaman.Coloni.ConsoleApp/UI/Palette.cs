using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.UI
{
	/*
	 * http://paletton.com/#uid=30z0u0kllcObRqkfVhMqN7MAW2X
	 * "chocolate milk"
	 *              light                  dark
	 * brown 664422 d2ab84 8e6a47 3e240a 170c00
	 * blue  1d2545 606a8e 363f60 0b122a 020510
	 * green 194c27 639d72 356a43 082e12 001105
	 */
	internal class Palette
	{
		private const int RGB_Primary = 0x00664422;
		private const int RGB_PrimaryTint = 0x00EEDDCC;
		private const int RGB_Primary0 = 0x00D2AB84;
		private const int RGB_Primary1 = 0x008E6A47;
		private const int RGB_Primary2 = 0x003E240A;
		private const int RGB_Primary3 = 0x00170C00;

		private const int RGB_Secondary = 0x001D2545;
		private const int RGB_SecondaryTint = 0x001D2545;
		private const int RGB_Secondary0 = 0x00606A8E;
		private const int RGB_Secondary1 = 0x00363F60;
		private const int RGB_Secondary2 = 0x000B122A;
		private const int RGB_Secondary3 = 0x00020510;

		private const int RGB_Tertiary = 0x00194C27;
		private const int RGB_TertiartTint = 0x00BDE0FF;
		private const int RGB_Tertiary0 = 0x00639D72;
		private const int RGB_Tertiary1 = 0x00356A43;
		private const int RGB_Tertiary2 = 0x00082E12;
		private const int RGB_Tertiary3 = 0x00001105;

		public static Color UIForeground { get { return Color.FromArgb(R(RGB_PrimaryTint), G(RGB_PrimaryTint), B(RGB_PrimaryTint)); } }
		public static Color UIForegroundDisabled { get { return Color.FromArgb(R(RGB_Primary1), G(RGB_Primary1), B(RGB_Primary1)); } }

		public static Color UIBackground { get { return Color.FromArgb(R(RGB_Primary), G(RGB_Primary), B(RGB_Primary)); } }
		public static Color UIBackgroundActive { get { return Color.FromArgb(R(RGB_Primary2), G(RGB_Primary2), B(RGB_Primary2)); } }

		private static int R(int argb)
		{
			return ((argb & 0x00FF0000) >> 16);
		}

		private static int G(int argb)
		{
			return ((argb & 0x0000FF00) >> 8);
		}

		private static int B(int argb)
		{
			return (argb & 0x000000FF);
		}
	}
}
