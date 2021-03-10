using Core.WinAPI;
using System;
using System.Text;

namespace Core.Window
{
	public static class WindowHelper
	{
		public static void MaximizeWindow(this IntPtr hWnd) => User32.ShowWindow(hWnd, User32.SW_MAXIMIZE);

		public static string GetClassName(this IntPtr hWnd)
		{
            int nRet;
            // Pre-allocate 256 characters, since this is the maximum class name length.
            StringBuilder ClassName = new StringBuilder(256);
            //Get the window class name
            nRet = User32.GetClassName(hWnd, ClassName, ClassName.Capacity);
            if (nRet != 0)
            {
                return ClassName.ToString();
            }
            else
            {
                return "";
            }
        }
	}
}
