using Script.Models;
using System.Collections.Generic;

namespace Script
{
	public static class ScriptParser
	{
		public static IEnumerable<Method> Parse(this string script)
		{
			var lines = script.Split('\n');
			var res = new List<Method>();
			foreach (var line in lines)
			{
				if (line.Trim().Length > 0 && !line.StartsWith("//"))
					res.Add(ParseLine(line));
			}
			return res;
		}

		private static Method ParseLine(string line)
		{
			//CommandName:par1,par2,...\n
			var res = new Method();
			var cmd = line.Split(':')[0].Trim().ToLower();
			var parList = line[(line.IndexOf(':') + 1)..].Split(',');
			res.Name = cmd;
			res.Parameters = new List<string>();
			foreach (var par in parList)
			{
				if(!string.IsNullOrEmpty(par))
					res.Parameters.Add(par.Trim());
			}
			return res;
		}
	}
}
