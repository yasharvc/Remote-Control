using Library.DropBox;
using Library.Process;
using Library.Screen;
using Library.Services;
using Library.Window;
using Library;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Library.CMD;

namespace RemoteControl
{
	public partial class MainForm : Form
	{
		Configuration AppConfig => Program.Configuration;
		string LastScript { get; set; } = "";
		private delegate void SafeCallDelegate(string text);
		public MainForm()
		{
			InitializeComponent();
		}


		private async Task RunAppAndTakeScreenShoot(string appName)
		{
			try
			{
				if (ProcessHelper.GetProcessesContainsName(appName).Any())
				{
					var image = TakeScreenShotOfWindowByProcessName(appName);
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

		private async void TakeScreenShootButton_Click(object sender, EventArgs e)
		{
			await RunAppAndTakeScreenShoot("AnyDesk");
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			new TimedFileReader(AppConfig.ScriptFilePath, 10000, RunScript).Start();
		}

		private void RunScript(string script)
		{
			if (!string.IsNullOrEmpty(script) && LastScript != script)
			{
				//await DropBoxHelper.UploadFileAsync(new MemoryStream(), "", AppConfig.ScriptFilePath);
				if (textBox1.InvokeRequired)
				{
					var d = new SafeCallDelegate(WriteTextSafe);
					textBox1.Invoke(d, new object[] { script });
				}
				else
				{
					textBox1.Text = script;
				}
				LastScript = script;
				ScriptRunner.Run(script);
			}
		}

		private void WriteTextSafe(string text)
		{
			if (textBox1.InvokeRequired)
			{
				var d = new SafeCallDelegate(WriteTextSafe);
				textBox1.Invoke(d, new object[] { text });
			}
			else
			{
				textBox1.Text = text;
			}
		}

		private void RebuildButton_Click(object sender, EventArgs e)
		{
			string destBatFileName = Path.Combine(AppConfig.TempRoot, AppConfig.BuildScriptFileName);
			File.Copy(Path.Combine(Environment.CurrentDirectory, AppConfig.BuildScriptFileName),
				destBatFileName, true);
			CMDHelper.RunCMdAndDontWait($"{destBatFileName} {Environment.ProcessId}");
		}
	}
}
