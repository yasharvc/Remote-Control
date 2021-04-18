using Library;
using Library.CMD;
using Library.DropBox;
using Library.Process;
using Library.Screen;
using Library.Window;
using Script;
using Script.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteControl
{
	class ScriptRunner
	{
		delegate void CommandRunner(Method m);
		static Dictionary<string, CommandRunner> ScriptToFunction { get; set; } = new Dictionary<string, CommandRunner>();
		static Configuration AppConfig => Program.Configuration;
		static string AppDir => CMDHelper.AppDir;
		public static void Run(string script) => Run(script.Parse());
		public static void Run(IEnumerable<Method> methods)
		{
			foreach (var method in methods)
			{
				try
				{
					RunMethod(method);
				}
				catch(Exception e)
				{
					e.Message.LogError();
				}
			}
		}

		private static void RunMethod(Method method)
		{
			ScriptToFunction["loginfo"] = LogInfo;
			ScriptToFunction["rebuild"] = Rebuild;
			ScriptToFunction["shutdown"] = Shutdown;
			ScriptToFunction["restart"] = Restart;
			ScriptToFunction["anydeskpicture"] = VoidAnydeskTakePictureAsync;
			if (ScriptToFunction.ContainsKey(method.Name.ToLower()))
				ScriptToFunction[method.Name.ToLower()](method);
		}
		private static async void VoidAnydeskTakePictureAsync(Method m) => await AnydeskTakePictureAsync(m);
		private static async Task AnydeskTakePictureAsync(Method m)
		{
			var appName = "AnyDesk";
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
						await AnydeskTakePictureAsync(m);
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

		private static void WaitForAppToStart() => Thread.Sleep(AppConfig.WaitForAppToStartMS);

		private static void ExecuteApp(string appName) => ProcessHelper.RunApp(Path.Combine(AppConfig.AppsRootPath, $"{appName}.exe"));


		private static void Shutdown(Method method) => Process.Start("Shutdown", "-s -t 1");
		private static void Restart(Method method) => Process.Start("Shutdown", "-r -t 1");

		private static void Rebuild(Method method)
		{
			string destBatFileName = Path.Combine(AppConfig.TempRoot, AppConfig.BuildScriptFileName);
			$"Dest bat FileName:{destBatFileName}".LogWarning();
			string sourceFileName = Path.Combine(AppDir, AppConfig.BuildScriptFileName);
			$"Source bat FileName:{sourceFileName}".LogWarning();
			File.Copy(sourceFileName, destBatFileName, true);
			CMDHelper.RunCMdAndDontWait($"{destBatFileName} {Environment.ProcessId}");
			Environment.Exit(0);
		}

		private static void LogInfo(Method method)
		{
			foreach (var item in method.Parameters)
				item.LogInfo();
		}
	}
}
