using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDownloader.Models;

namespace XDownloader.Infrastructure
{
    public class AnnieHelper
    {
        private static string _exePath = "./annie.exe";

        public static void Download(string url, string output, Action<string> logAction)
        {
            Task.Run(() =>
            {
                var sb = new StringBuilder();
                sb.Append($" -o {output}");
                //sb.Append($" -i");
                sb.Append($" {url}");

                var arguments = sb.ToString();
                var process = new ProcessHelper(_exePath, arguments, logAction);
                process.Start();
            });
        }

        public static DownloadInfo ParseDlOutput(string text)
        {
            var info = new DownloadInfo();

            return info;
        }
    }
}
