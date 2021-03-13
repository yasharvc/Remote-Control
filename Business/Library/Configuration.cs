namespace Library
{
	public class Configuration
	{
		public string AppsRootPath { get; set; }
		public string LogPath { get; set; }
		public string ScriptFilePath { get; set; } = "Script.txt";
		public string BuildScriptFileName { get; set; } = "builder.bat";
		public string ScreenShootPath { get; set; }
		public string DropBoxAccessToken { get; set; }
		public string TempRoot { get; set; }
		public int WaitForAppToStartMS { get; set; }
	}
}