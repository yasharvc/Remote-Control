using Library;
using Library.CMD;
using Script;
using Script.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace RemoteControl
{
	class ScriptRunner
	{
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
			if(method.Name.Equals("loginfo", StringComparison.OrdinalIgnoreCase))
				LogInfo(method);
			else if (method.Name.Equals("rebuild", StringComparison.OrdinalIgnoreCase))
				Rebuild(method);
			else if (method.Name.Equals("shutdown", StringComparison.OrdinalIgnoreCase))
				Shutdown(method);
		}

		private static void Shutdown(Method method) => Process.Start("Shutdown", "-s -t 1");

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
