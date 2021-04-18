using Library;
using Library.DropBox;
using Library.Process;
using Library.Screen;
using Library.Services;
using Library.Window;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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


		private static Task RunAppAndTakeScreenShoot(string appName)
		{
			ScriptRunner.Run("anydeskpicture:");
			return Task.CompletedTask;
		}

		

		private async void TakeScreenShootButton_Click(object sender, EventArgs e)
		{
			await RunAppAndTakeScreenShoot("AnyDesk");
		}

		private void MainForm_Load(object sender, EventArgs e) => new TimedFileReader(AppConfig.ScriptFilePath, 10000, RunScriptAsync).Start();

		private async Task RunScriptAsync(string script)
		{
			if (!string.IsNullOrEmpty(script) && LastScript != script)
			{
				await DropBoxHelper.UploadFileAsync(new MemoryStream(), "", AppConfig.ScriptFilePath);
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
			ScriptRunner.Run("Rebuild:");
		}

		private void ScheduleTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				var scheduleFile = File.ReadAllText(AppConfig.ScheduleFilePath);
				foreach (var line in scheduleFile.Split('\r'))
				{
					if(line.StartsWith("Shutdown", StringComparison.OrdinalIgnoreCase))
					{
						var time = DateTime.Parse(line.Split('>')[1]).TimeOfDay;
						if(DateTime.Now.TimeOfDay.Subtract(time).TotalMilliseconds > 0)
							ScriptRunner.Run("shutdown:");
					}
				}
			}
			catch
			{

			}
		}
	}
}
