using com.SonOfSofaman.Coloni.ConsoleApp.UI;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace com.SonOfSofaman.Coloni.ConsoleApp.Scenes
{
	internal class CreateNewWorldScene : Scene
	{
		internal SceneEvent OnDone { get; set; }

		private WorldBuilder WorldBuilder;

		private SceneState CurrentSceneState;
		private double CurrentProgress = 0.0;
		private double PreviousProgress = 0.0;

		private const string TextureTag_Working = "CreateNewWorldScene.Working";
		private const string TextureTag_Complete = "CreateNewWorldScene.Complete";

		internal CreateNewWorldScene()
		{
			Program.TextureManager.RegisterTexture(TextureTag_Complete, MakeMessageBitmap("complete"));
			this.WorldBuilder = new WorldBuilder();
			this.WorldBuilder.OnProgress += new WorldBuilderProgressEvent((progress) => { this.CurrentProgress = progress; });
			this.WorldBuilder.OnComplete += new WorldBuilderCompleteEvent((worldState) => { Program.CurrentWorldState = worldState; this.CurrentSceneState = SceneState.Complete; });
		}

		internal override void Enter()
		{
			this.CurrentSceneState = SceneState.None;
			this.CurrentProgress = 0.0;
			this.PreviousProgress = 0.0;
			Program.TextureManager.RegisterTexture(TextureTag_Working, MakeProgressBitmap(0.0));

			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-this.ClientSize.Width / 2.0, this.ClientSize.Width / 2.0, -this.ClientSize.Height / 2.0, this.ClientSize.Height / 2.0, -1.0, 1.0);
		}

		internal override void Exit()
		{
		}

		internal override void Update(double deltaTime, InputDeviceState inputDeviceState)
		{
			switch (this.CurrentSceneState)
			{
				case SceneState.None:
				{
					this.CurrentSceneState = SceneState.Working;
					WorldBuilder.Build();
					break;
				}
				case SceneState.Working:
				{
					if (this.CurrentProgress > this.PreviousProgress)
					{
						Program.TextureManager.RegisterTexture(TextureTag_Working, MakeProgressBitmap(this.CurrentProgress));
						this.PreviousProgress = this.CurrentProgress;
					}

					break;
				}
				case SceneState.Complete:
				{
					if (this.PreviousKeyState(Key.Escape) && !inputDeviceState.KeyboardState[Key.Escape])
					{
						if (this.OnDone != null) this.OnDone();
					}
					break;
				}
			}

			this.PreviousInputDeviceState = inputDeviceState;
		}

		internal override void Render()
		{
			TextureInfo textureInfo = this.CurrentSceneState == SceneState.Complete ? Program.TextureManager[TextureTag_Complete] : Program.TextureManager[TextureTag_Working];
			GL.BindTexture(TextureTarget.Texture2D, textureInfo.ID);
			GL.Begin(PrimitiveType.Quads); // LL, UL, UR, LR
			GL.TexCoord2(0, 0); GL.Vertex2(-textureInfo.Width / 2, textureInfo.Height / 2);
			GL.TexCoord2(0, 1); GL.Vertex2(-textureInfo.Width / 2, -textureInfo.Height / 2);
			GL.TexCoord2(1, 1); GL.Vertex2(textureInfo.Width / 2, -textureInfo.Height / 2);
			GL.TexCoord2(1, 0); GL.Vertex2(textureInfo.Width / 2, textureInfo.Height / 2);
			GL.End();
		}

		private static Bitmap MakeMessageBitmap(string message)
		{
			Size size = new Size(128, 48);
			Font font = new Font(FontFamily.GenericSansSerif, 14.0F);
			SolidBrush brush = new SolidBrush(Palette.UIForeground);
			StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

			Bitmap result = new Bitmap(size.Width, size.Height);
			Graphics graphics = Graphics.FromImage(result);

			graphics.Clear(Color.Black);
			graphics.DrawString(message, font, brush, new RectangleF(0.0F, 0.0F, size.Width, size.Height), format);

			return result;
		}

		private static Bitmap MakeProgressBitmap(double progress)
		{
			Size size = new Size(128, 48);
			Font font = new Font(FontFamily.GenericSansSerif, 14.0F);
			Pen penBackground = new Pen(Palette.UIBackground, 1.0F);
			SolidBrush brushBackground = new SolidBrush(Palette.UIPanel);
			SolidBrush brushForground = new SolidBrush(Palette.UIForeground);
			StringFormat format = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
			Rectangle rectangle = new Rectangle(0, 0, (int)(128.0 * progress), 48);
			string message = string.Format("{0:P0}", progress);

			Bitmap result = new Bitmap(size.Width, size.Height);
			Graphics graphics = Graphics.FromImage(result);

			graphics.Clear(Color.Black);
			graphics.FillRectangle(brushBackground, rectangle);
			graphics.DrawRectangle(penBackground, 0, 0, size.Width - 1, size.Height - 1);
			graphics.DrawString(message, font, brushForground, new RectangleF(0.0F, 0.0F, size.Width, size.Height), format);

			return result;
		}

		enum SceneState
		{
			None,
			Working,
			Complete,
		}
	}
}
