using Core;
using Core.DropBox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		static async Task Main()
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

			var x = await DropBoxHelper.DownloadFileAsync("/test.txt");
			var str = Encoding.UTF8.GetString(x);
			MessageBox.Show(str);

			await DropBoxHelper.UploadFileAsync(new MemoryStream(Encoding.UTF8.GetBytes("File read!")), "", "test.txt");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
