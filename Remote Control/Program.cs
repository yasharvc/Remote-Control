using Core;
using Core.DropBox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace Remote_Control
{
	static class Program
	{
		public static Core.Configuration Configuration { get; set; }
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{

			Configuration = new Core.Configuration{
				AppsRootPath = ConfigurationManager.AppSettings[nameof(Core.Configuration.AppsRootPath)],
				DropBoxAccessToken = ConfigurationManager.AppSettings[nameof(Core.Configuration.DropBoxAccessToken)],
				ScreenShootPath = ConfigurationManager.AppSettings[nameof(Core.Configuration.ScreenShootPath)],
				TempRoot = ConfigurationManager.AppSettings[nameof(Core.Configuration.TempRoot)],
				WaitForAppToStartMS = int.Parse(ConfigurationManager.AppSettings[nameof(Core.Configuration.WaitForAppToStartMS)]),
				LogPath = ConfigurationManager.AppSettings[nameof(Core.Configuration.LogPath)],
			};
			"Configuration loaded!".LogSuccess();
			DropBoxHelper.DropBoxAccessToken = Configuration.DropBoxAccessToken;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
