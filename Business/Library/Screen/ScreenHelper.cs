using Library.WinAPI;
using System;
using System.Drawing;
using System.Text;

namespace Library.Screen
{
	public class ScreenHelper
	{
		public static IntPtr FindWindow(string windowName)
		{
			var hWnd = User32.FindWindow(windowName, null);
			return hWnd;
		}

		public static IntPtr GetWindowHandleUnderMouse()
		{
			if (User32.GetCursorPos(out Point p))
			{
				IntPtr hWnd = User32.WindowFromPoint(p);
				return hWnd;
			}
			return IntPtr.Zero;
		}

		public static string GetWindowTitleUnderMouse()
		{
			var hWnd = GetWindowHandleUnderMouse();
			return hWnd != IntPtr.Zero ? GetWindowTextByHandle(hWnd) : "";
		}

		public static string GetWindowTextByHandle(IntPtr hWnd)
		{
			const int count = 512;
			var text = new StringBuilder(count);

			if (User32.GetWindowText(hWnd, text, count) > 0)
				return text.ToString();
			return "";
		}
	}
}