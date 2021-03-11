﻿using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.CMD
{
	public class CMDHelper
	{
		public static void RunBatchScript(params string[] commands)
		{
			
		}

        public static string CMD(string command,
                                      string workingDirectory = null)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);

                procStartInfo.RedirectStandardError = procStartInfo.RedirectStandardInput = procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                if (null != workingDirectory)
                    procStartInfo.WorkingDirectory = workingDirectory;
                else
                    procStartInfo.WorkingDirectory = Environment.CurrentDirectory;

				var proc = new System.Diagnostics.Process
                {
					StartInfo = procStartInfo
				};
				proc.Start();

                StringBuilder sb = new StringBuilder();
                proc.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
                {
                    sb.AppendLine(e.Data);
                };
                proc.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e)
                {
                    sb.AppendLine(e.Data);
                };

                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                proc.WaitForExit();
                return sb.ToString();
            }
            catch (Exception objException)
            {
                return $"Error in command: {command}, {objException.Message}";
            }
        }

  //      public Task<string> CMDAsync(string command,string workingDirectory = null)
		//{
  //          try
  //          {
		//		Thread objThread = new Thread(new ParameterizedThreadStart(CMD))
		//		{
		//			IsBackground = true,
		//			Priority = ThreadPriority.AboveNormal
		//		};
  //              objThread.Start(command, workingDirectory);
  //          }
  //          catch (ThreadStartException objException)
  //          {
  //              return Task.FromResult("");
  //          }
  //          catch (ThreadAbortException objException)
  //          {
  //              // Log the exception
  //          }
  //          catch (Exception objException)
  //          {
  //              // Log the exception
  //          }
  //      }
    }
}
