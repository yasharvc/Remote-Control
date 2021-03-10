namespace Core
{
	public class Configuration
	{
		public string AppsRootPath { get; set; }
		public string LogPath { get; set; }
		public string ScreenShootPath { get; set; }
		public string DropBoxAccessToken { get; set; }
		public string TempRoot { get; set; }
		public int WaitForAppToStartMS { get; set; }
	}
}