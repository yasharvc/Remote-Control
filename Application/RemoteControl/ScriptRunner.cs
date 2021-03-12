using Library;
using Script;
using Script.Models;
using System;
using System.Collections.Generic;

namespace RemoteControl
{
	class ScriptRunner
	{
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
		}

		private static void LogInfo(Method method)
		{
			foreach (var item in method.Parameters)
				item.LogInfo();
		}
	}
}
