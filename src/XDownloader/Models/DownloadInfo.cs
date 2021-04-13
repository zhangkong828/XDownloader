using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDownloader.Models
{
    public class DownloadInfo
    {
        public string Url { get; set; }
        public string Site { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }


        public string Size { get; set; }
        public string Progress { get; set; }
        public string Speed { get; set; }
        public string Time { get; set; }

        public bool IsMessage { get { return !string.IsNullOrEmpty(Message); } }
        public string Message { get; set; }
    }
}
