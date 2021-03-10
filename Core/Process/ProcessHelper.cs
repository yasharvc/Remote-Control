using Core.WinAPI;
using System;
using System.Collections.Generic;

namespace Core.Process
{
	public static class ProcessHelper
	{
		public static List<System.Diagnostics.Process> GetProcessesContainsName(string name)
		{
			var processes = System.Diagnostics.Process.GetProcesses();
			var res = new List<System.Diagnostics.Process>();
			foreach (var p in processes)
			{
				IntPtr windowHandle = p.MainWindowHandle;

				var temp = Screen.ScreenHelper.GetWindowTextByHandle(windowHandle);
				if (temp.Contains(name))
					res.Add(p);
			}
			return res;
		}

		public static List<string> ProcessIntoMainWindowTitle(this IEnumerable<System.Diagnostics.Process> processes)
		{
			var res = new List<string>();
			foreach (var p in processes)
				res.Add(Screen.ScreenHelper.GetWindowTextByHandle(p.MainWindowHandle));
			return res;
		}

		//public static IntPtr GetTrayHandle()
		//{
		//	return User32.FindWindowEx(User32.GetDesktopWindow(), IntPtr.Zero, "Shell_TrayWnd", null);
		//	//IntPtr hReBar = User32.FindWindowEx(hTray, IntPtr.Zero, "ReBarWindow32", null);
		//	//IntPtr hTask = User32.FindWindowEx(hReBar, IntPtr.Zero, "MSTaskSwWClass", null);
		//	//IntPtr hToolbar = User32.FindWindowEx(hTask, IntPtr.Zero, "ToolbarWindow32", null);
		//}

		public static IntPtr GetSystemTrayHandle()
		{
			IntPtr hWndTray = User32.FindWindow("Shell_TrayWnd", null);
			if (hWndTray != IntPtr.Zero)
			{
				hWndTray = User32.FindWindowEx(hWndTray, IntPtr.Zero, "TrayNotifyWnd", null);
				if (hWndTray != IntPtr.Zero)
				{
					hWndTray = User32.FindWindowEx(hWndTray, IntPtr.Zero, "SysPager", null);
					if (hWndTray != IntPtr.Zero)
					{
						hWndTray = User32.FindWindowEx(hWndTray, IntPtr.Zero, "ToolbarWindow32", null);
						return hWndTray;
					}
				}
			}

			return IntPtr.Zero;
		}

		public static void RunApp(string pathToExe) => System.Diagnostics.Process.Start(pathToExe);
	}
}