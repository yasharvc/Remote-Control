using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Library.DropBox
{
    public class DropBoxHelper
    {
        public static string DropBoxAccessToken { get; set; }
        public static async Task UploadFileAsync(Stream stream, string pathInDropBox,string fileName)
        {
            using (var dbx = new DropboxClient(DropBoxAccessToken))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                await UploadToDropBoxAsync(dbx, pathInDropBox, fileName, stream);
            }
        }
        public static async Task<byte[]> DownloadFileAsync(string fullPathToFile)
        {
            using (var dbx = new DropboxClient(DropBoxAccessToken))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                return await DownloadFromDropBoxAsync(dbx, fullPathToFile);
            }
        }
        static async Task UploadToDropBoxAsync(DropboxClient dbx, string folder, string file, Stream mem) => await dbx.Files.UploadAsync(
				folder + "/" + file,
				WriteMode.Overwrite.Instance,
				body: mem);
		static async Task<byte[]> DownloadFromDropBoxAsync(DropboxClient dbx, string pathToFile)
        {
            var file = await dbx.Files.DownloadAsync(pathToFile);
            return await file.GetContentAsByteArrayAsync();
        }
    }
}