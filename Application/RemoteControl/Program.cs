using Library.DropBox;
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteControl
{
	static class Program
	{
		public static Library.Configuration Configuration { get; set; }
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static async Task Main()
		{
			Configuration = new Library.Configuration
			{
				AppsRootPath = ConfigurationManager.AppSettings[nameof(Library.Configuration.AppsRootPath)],
				DropBoxAccessToken = ConfigurationManager.AppSettings[nameof(Library.Configuration.DropBoxAccessToken)],
				ScreenShootPath = ConfigurationManager.AppSettings[nameof(Library.Configuration.ScreenShootPath)],
				TempRoot = ConfigurationManager.AppSettings[nameof(Library.Configuration.TempRoot)],
				WaitForAppToStartMS = int.Parse(ConfigurationManager.AppSettings[nameof(Library.Configuration.WaitForAppToStartMS)]),
				LogPath = ConfigurationManager.AppSettings[nameof(Library.Configuration.LogPath)],
			};
			$"Configuration loaded!<br/>Apps Root Path : {Configuration.AppsRootPath}".LogSuccess();
			DropBoxHelper.DropBoxAccessToken = Configuration.DropBoxAccessToken;


			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
