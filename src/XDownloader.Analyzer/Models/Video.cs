using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDownloader.Analyzer
{
    public class VideoDetail
    {
        public string Site { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }


        public string Definition { get; set; }
        public long Size { get; set; }

        public List<VideoPart> Parts { get; set; }
    }

    public class VideoPart
    {
        public int Index { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public double Duration { get; set; }
        public long BytesTotal { get; set; }
        public string Remark { get; set; }
    }
}
