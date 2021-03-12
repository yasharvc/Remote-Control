using System;
using System.IO;

namespace Library
{
	public static class Logger
	{
		public static string LogRootPath { get; set; } = "";

		public static void LogInfo(this string info)
		{
			var template = "<p style='border-color: #2196F3;background-color:#ddffff;  padding:10px;  border-top-style: none;  border-right-style: solid;  border-bottom-style: none;  border-left-style: solid;'>{0}</p>";
			WriteToLogFile(string.Format(template, EmbraceWithDateTime(info)));
		}

		private static string EmbraceWithDateTime(string info) => $"{DateTime.Now:HH:MM:ss}:{info}";

		public static void LogError(this string info)
		{
			var template = "<p style='border-color: #f44336;background-color:#ffdddd;  padding:10px;  border-top-style: none;  border-right-style: solid;  border-bottom-style: none;  border-left-style: solid;'>{0}</p>";
			WriteToLogFile(string.Format(template, EmbraceWithDateTime(info)));
		}
		public static void LogWarning(this string info)
		{
			var template = "<p style='border-color: #ffeb3b;background-color:#ffffcc;  padding:10px;  border-top-style: none;  border-right-style: solid;  border-bottom-style: none;  border-left-style: solid;'>{0}</p>";
			WriteToLogFile(string.Format(template, EmbraceWithDateTime(info)));
		}
		public static void LogSuccess(this string info)
		{
			var template = "<p style='border-color: #4CAF50!important;background-color:#ddffdd;  padding:10px;  border-top-style: none;  border-right-style: solid;  border-bottom-style: none;  border-left-style: solid;'>{0}</p>";
			WriteToLogFile(string.Format(template, EmbraceWithDateTime(info)));
		}

		private static void WriteToLogFile(string v)
		{
			var pathToLogs = LogRootPath;
			if (!Directory.Exists(pathToLogs))
				Directory.CreateDirectory(pathToLogs);
			var pathToLog = Path.Combine(LogRootPath, $"{DateTime.Now:yyyy-MM-dd}-Log.html");
			File.AppendAllText(pathToLog, v);
		}
	}
}
