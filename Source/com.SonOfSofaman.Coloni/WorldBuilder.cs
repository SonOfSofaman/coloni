using System.Threading;

namespace com.SonOfSofaman.Coloni
{
	public class WorldBuilder
	{
		public WorldBuilderProgressEvent OnProgress { get; set; }
		public WorldBuilderCompleteEvent OnComplete { get; set; }

		private Thread BuilderThread = null;

		public void Build()
		{
			this.BuilderThread = new Thread(new ThreadStart(Genesis));
			this.BuilderThread.Start();
		}

		private void Genesis()
		{
			for (double progress = 0.00; progress < 1.00; progress += 0.01)
			{
				Thread.Sleep(50);
				if (this.OnProgress != null) this.OnProgress(progress);
			}
			WorldState result = new WorldState("New World");

			if (this.OnComplete != null) this.OnComplete(result);
			this.BuilderThread = null;
		}
	}
}
