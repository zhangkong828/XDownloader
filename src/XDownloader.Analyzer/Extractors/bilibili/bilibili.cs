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
            { "Referer", "https://www.bilibili.com" }
        };

        private const string _bilibiliAPI = "https://api.bilibili.com/x/player/playurl?";
        private const string _bilibiliBangumiAPI = "https://api.bilibili.com/pgc/player/web/playurl?";
        private const string _bilibiliTokenAPI = "https://api.bilibili.com/x/player/playurl/token?";

        public AnalyzeResponse Analyze(string url, Dictionary<string, object> parameters)
        {
            try
            {

                var html_content = HttpHelper.Get(url, _headers);

                if (url.Contains("bangumi"))
                {
                    return ExtractBangumi(url, html_content);
                }

                return ExtractNormalVideo(url, html_content);
            }
            catch (Exception ex)
            {
                return AnalyzeResponse.Fail(ex.Message);
            }
        }


        public string GenerateApi(string aid, string cid, int quality, string bvid, bool bangumi, string cookie)
        {
            string baseAPIURL;
            string param;
            string utoken = "";

            if (!cookie.IsNullOrWhiteSpace() && utoken.IsNullOrWhiteSpace())
            {
                utoken = HttpHelper.Get($"{_bilibiliTokenAPI}aid={aid}&cid={cid}", _headers);
                if (!utoken.IsNullOrWhiteSpace())
                {
                    //todo
                }
            }

            if (bangumi)
            {
                param = $"cid={cid}&bvid={bvid}&qn={quality}&type=&otype=json&fourk=1&fnver=0&fnval=16";
                baseAPIURL = _bilibiliBangumiAPI;
            }
            else
            {
                param = $"avid={aid}&cid={cid}&bvid={bvid}&qn={quality}&type=&otype=json&fourk=1&fnver=0&fnval=16";
                baseAPIURL = _bilibiliAPI;
            }
            var api = string.Concat(baseAPIURL, param);
            // bangumi utoken also need to put in params to sign, but the ordinary video doesn't need
            if (!bangumi && !utoken.IsNullOrWhiteSpace())
            {
                api = $"{api}&utoken={utoken}";
            }
            return api;
        }


        public AnalyzeResponse ExtractBangumi(string url,string html)
        {
            return null;
        }

        public AnalyzeResponse ExtractNormalVideo(string url, string html)
        {
            return null;
        }
    }
}
