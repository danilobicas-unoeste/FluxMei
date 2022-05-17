using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace teste
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            teste();
            Application.Run(new Form1());
        }

        private static async Task teste()
        {
            int downloading = await DownloadDocsMainPageAsync();
            Console.WriteLine($"{nameof(Main)}: Launched downloading.");

            int bytesLoaded = downloading;
            Console.WriteLine($"{nameof(Main)}: Downloaded {bytesLoaded} bytes.");
        }

        private static async Task<int> DownloadDocsMainPageAsync()
        {
            Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: About to start downloading.");

            var client = new HttpClient();
            byte[] content = await client.GetByteArrayAsync("https://docs.microsoft.com/en-us/");

            Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: Finished downloading.");
            return content.Length;
        }
    }
}
