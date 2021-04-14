using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDownloader.Analyzer.Infrastructure;

namespace XDownloader.Analyzer.Extractors.bilibili
{
    [AnalyzerType(Type = SiteCode.bilibili)]
    public class bilibili : IAnalyzer
    {
        private Dictionary<string, string> _headers = new Dictionary<string, string>() {
            { "UserAgent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.84 Safari/537.36" }
        };

        public AnalyzeResponse Analyze(string url, Dictionary<string, object> parameters)
        {
            var response = new AnalyzeResponse();
            try
            {
                _headers.Add("Referer", url);

                var html_content = HttpHelper.Get(url, _headers);
                var sort = "";
                if (Util.IsMatch(url, "https?://(www\\.)?bilibili\\.com/bangumi/play/ep(\\d+)"))
                    sort = "bangumi";
                else if (Util.IsMatch(url, "<meta property=\"og:url\" content=\"(https://www.bilibili.com/bangumi/play/[^\"]+)\""))
                    sort = "bangumi";
                else if (Util.IsMatch(url, "https?://(www\\.)?bilibili\\.com/bangumi/media/md(\\d+)") || Util.IsMatch(url, "https?://bangumi\\.bilibili\\.com/anime/(\\d+)"))
                    sort = "bangumi_md";
                else if (Util.IsMatch(url, "https?://(www\\.)?bilibili\\.com/video/(av(\\d+)|BV(\\S+))"))
                    sort = "video";
                else if (Util.IsMatch(url, "https?://space\\.?bilibili\\.com/(\\d+)/channel/detail\\?.*cid=(\\d+)"))
                    sort = "space_channel";
                else if (Util.IsMatch(url, "https?://space\\.?bilibili\\.com/(\\d+)/favlist\\?.*fid=(\\d+)"))
                    sort = "space_favlist";
                else if (Util.IsMatch(url, "https?://space\\.?bilibili\\.com/(\\d+)/video"))
                    sort = "space_video";
                else if (Util.IsMatch(url, "https?://(www\\.)?bilibili\\.com/audio/am(\\d+)"))
                    sort = "audio_menu";
                else
                    return AnalyzeResponse.NotSupported();

                return response;
            }
            catch (Exception ex)
            {
                return AnalyzeResponse.Fail(ex.Message);
            }
        }
    }
}
