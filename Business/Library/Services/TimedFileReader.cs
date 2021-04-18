using System;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
	public class TimedFileReader
	{

		System.Timers.Timer Ticker { get; set; }
		string Path { get; }
		Func<string, Task> ActionOnRead { get; set; }

		public TimedFileReader(string path, int delayMS, Func<string, Task> actionOnFileRead)
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
				await ActionOnRead(file);
			}
			catch
			{

			}
		}

		public void Start() => Ticker.Start();

		public void Stop() => Ticker.Stop();
	}
}