using System;
using System.Text;

namespace Library.Services
{
	public class TimedFileReader
	{

		System.Timers.Timer Ticker { get; set; }
		string Path { get; }
		Action<string> ActionOnRead { get; set; }

		public TimedFileReader(string path, int delayMS, Action<string> actionOnFileRead)
		{
			Path = path;
			ActionOnRead = actionOnFileRead;
			Ticker = new System.Timers.Timer(delayMS);
			Stop();
			Ticker.Elapsed += Ticker_Elapsed;
		}

		private async void Ticker_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				var file = Encoding.UTF8.GetString(await DropBox.DropBoxHelper.DownloadFileAsync(Path));
				ActionOnRead(file);
			}
			catch
			{

			}
		}

		public void Start() => Ticker.Start();

		public void Stop() => Ticker.Stop();
	}
}