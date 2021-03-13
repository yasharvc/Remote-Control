using Library;
using Library.DropBox;
using System;
using System.Configuration;
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
		static void Main()
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

			$"Temp root:{Configuration.TempRoot}".LogWarning();

			DropBoxHelper.DropBoxAccessToken = Configuration.DropBoxAccessToken;
			Logger.LogRootPath = Configuration.LogPath;

			$"Configuration loaded!<br/>Apps Root Path : {Configuration.AppsRootPath}".LogSuccess();

			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
