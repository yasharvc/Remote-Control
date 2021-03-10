using Core.DropBox;
using Core.Process;
using Core.Screen;
using Core.Window;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remote_Control
{
	public partial class Form1 : Form
	{
		Core.Configuration AppConfig => Program.Configuration;
		public Form1()
		{
			InitializeComponent();
		}

		private async void TakeScreenShootButton_Click(object sender, EventArgs e)
		{
			await RunAppAndTakeScreenShoot("AnyDesk");
		}

		private async Task RunAppAndTakeScreenShoot(string appName)
		{
			try
			{
				if (ProcessHelper.GetProcessesContainsName(appName).Any())
				{
					var image = TakeScreenShotOfWindowByProcessName(appName);
					ScreenPicture.Image = image;
					var uniquePath = Path.Combine(AppConfig.TempRoot, $"{Guid.NewGuid()}.jpg");
					image.Save(uniquePath);
					var mem = new MemoryStream(File.ReadAllBytes(uniquePath));
					File.Delete(uniquePath);
					await DropBoxHelper.UploadFileAsync(mem,
						AppConfig.ScreenShootPath,
						$"{DateTime.Now:yyyy-MM-dd h_mm tt}.jpg");
				}
				else
				{
					try
					{
						$"{appName} is not running".LogWarning();
						ExecuteApp(appName);
						WaitForAppToStart();
						await RunAppAndTakeScreenShoot(appName);
					}
					catch
					{
						"Window are not exists".LogError();
					}
				}
			}
			catch (Exception ex)
			{
				ex.Message.LogError();
			}
		}

		private void WaitForAppToStart() => Thread.Sleep(AppConfig.WaitForAppToStartMS);

		private void ExecuteApp(string appName) => ProcessHelper.RunApp(Path.Combine(AppConfig.AppsRootPath, $"{appName}.exe"));

		private void Log(string v)
		{
			Console.WriteLine(v);
		}

		private static Image TakeScreenShotOfWindowByProcessName(string processName)
		{
			try
			{
				ScreenCapture sc = new ScreenCapture();
				var hWnd = ProcessHelper.GetProcessesContainsName(processName).First().MainWindowHandle;
				hWnd.MaximizeWindow();
				Thread.Sleep(2000);
				return sc.CaptureWindow(hWnd);
			}
			catch
			{
				throw;
			}
		}

		private void TitleChaserTimer_Tick(object sender, EventArgs e)
		{
			WindowTitleLabel.Text = ScreenHelper.GetWindowTitleUnderMouse();
			label1.Text = ScreenHelper.GetWindowHandleUnderMouse().GetClassName();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
		}
	}
}
