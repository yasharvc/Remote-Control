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
			if (ScriptToFunction.ContainsKey(method.Name.ToLower()))
				ScriptToFunction[method.Name.ToLower()](method);
		}

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
