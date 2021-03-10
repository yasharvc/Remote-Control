using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.DropBox
{
    public class DropBoxHelper
    {
        public static string DropBoxAccessToken { get; set; }
        public static async Task UploadFileAsync(Stream stream, string pathInDropBox,string fileName)
        {
            using (var dbx = new DropboxClient(DropBoxAccessToken))
            {
                var full = await dbx.Users.GetCurrentAccountAsync();
                await Upload(dbx, pathInDropBox, fileName, stream);
            }
        }
        static async Task Upload(DropboxClient dbx, string folder, string file, Stream mem)
        {
            var updated = await dbx.Files.UploadAsync(
                folder + "/" + file,
                WriteMode.Overwrite.Instance,
                body: mem);

            Console.WriteLine("Saved {0}/{1} rev {2}", folder, file, updated.Rev);
        }
    }
}