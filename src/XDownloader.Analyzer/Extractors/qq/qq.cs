using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using XDownloader.Analyzer.Infrastructure;

namespace XDownloader.Analyzer.Extractors.qq
{
    [AnalyzerType(Type = SiteCode.qq)]
    public class qq : IAnalyzer
    {

        private Dictionary<string, string> _headers = new Dictionary<string, string>() { { "UserAgent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko)  QQLive/10275340/50192209 Chrome/43.0.2357.134 Safari/537.36 QBCore/3.43.561.202 QQBrowser/9.0.2524.400" } };//腾讯视频客户端下载1080P

        public AnalyzeResponse Analyze(string url, Dictionary<string, object> parameters)
        {

            var response = new AnalyzeResponse();
            try
            {
                if (Util.IsMatch(url, "https?://(m\\.)?egame.qq.com/"))//企鹅电竞
                {

                }
                else if (url.Contains("kg.qq.com") || url.Contains("kg2.qq.com"))//全民K歌
                {

                }
                else if (url.Contains("live.qq.com"))//企鹅直播
                {

                }
                else if (url.Contains("mp.weixin.qq.com/s"))//微信公众号
                {

                }
                else
                {
                    var content = "";
                    var title = "";
                    var vid = "";
                    if (url.Contains("kuaibao.qq.com") || Util.IsMatch(url, "http://daxue.qq.com/content/content/id/\\d+"))
                    {
                        //# http://daxue.qq.com/content/content/id/2321
                        content = HttpHelper.Get(url, _headers);
                        vid = Util.Match(content, "vid\\s*=\\s*\"\\s*([^\"]+)\"");
                        title = Util.Match(content, "title\">([^\"]+)</p>");
                        title = string.IsNullOrWhiteSpace(title) ? vid : title;
                    }
                    else if (url.Contains("iframe/player.html"))
                    {
                        vid = Util.Match(url, "\\bvid=(\\w+)");
                        title = vid;
                    }
                    else if (url.Contains("view.inews.qq.com"))
                    {
                        content = HttpHelper.Get(url, _headers);
                        vid = Util.Match(content, "\"vid\":\"(\\w+)\"");
                        title = Util.Match(content, "\"title\":\"(\\w +)\"");
                    }
                    else
                    {
                        content = HttpHelper.Get(url, _headers);
                        vid = Util.Match(content, "vid\"*\\s*:\\s*\"\\s*([^\"]+)\"");
                        if (string.IsNullOrWhiteSpace(vid))
                            vid = Util.Match(content, "id\"*\\s*:\\s*\"(.+?)\"");

                        title = Util.Match(content, $"<a.*?id\\s*=\\s*\"{vid}\".*?title\\s*=\\s*\"(.+?)\".*?>");
                        if (string.IsNullOrWhiteSpace(title))
                            title = Util.Match(content, "title\">([^\"]+)</p>");
                        if (string.IsNullOrWhiteSpace(title))
                            title = Util.Match(content, "\"title\":\"([^\"]+)\"");
                        title = string.IsNullOrWhiteSpace(title) ? vid : title;
                    }

                    var detail = GetInfos(vid, out string errorMsg);
                    if (detail != null)
                    {
                        response.Success = true;
                        response.Data = detail;
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = errorMsg;
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                return AnalyzeResponse.Fail(ex.Message);
            }
        }


        private List<string> GetVid(string url)
        {
            var result = new List<string>();

            //http://mp.weixin.qq.com/s/IuJfF7zidy9MU6OsHveu7w
            if (url.Contains("mp.weixin.qq.com/s"))
            {
                var content = HttpHelper.Get(url);
                var matchs = Regex.Matches(content, "\\?vid=(\\w+)");
                foreach (Match item in matchs)
                {
                    var vid = item.Groups[1].Value;
                    if (!string.IsNullOrEmpty(vid))
                        result.Add(vid);
                }
            }
            else if (url.Contains("v.qq.com"))
            {
                int tryCount = 2;
                GetVid:
                string content = HttpHelper.Get(url);
                var vid = Regex.Match(content, "&vid=(.+?)&").Groups[1].Value;
                if (string.IsNullOrEmpty(vid))
                {
                    if (tryCount <= 0)
                    {
                        return result;
                    }
                    tryCount--;
                    goto GetVid;
                }
                result.Add(vid);
            }

            return result;
        }


        private VideoDetail GetInfos(string vid, out string errorMsg)
        {
            errorMsg = string.Empty;

            var info_api = $"http://vv.video.qq.com/getinfo?otype=json&appver=3.2.19.333&platform=11&defnpayver=1&vid={vid}";
            var info = HttpHelper.Get(info_api);
            var infoText = Regex.Match(info, "QZOutputJson=(.*)").Groups[1].Value.TrimEnd(';');
            var infoJson = JsonConvert.DeserializeObject(infoText) as JObject;

            if (infoJson["msg"] != null)
            {
                errorMsg = (string)infoJson["msg"];
                return null;
            }

            var fn_pre = (string)infoJson["vl"]["vi"][0]["lnk"];
            //名称
            var title = (string)infoJson["vl"]["vi"][0]["ti"];

            //文件名
            var fn = (string)infoJson["vl"]["vi"][0]["fn"];
            //文件类型
            var type = Path.GetExtension(fn);

            var host = (string)infoJson["vl"]["vi"][0]["ul"]["ui"][0]["url"];

            var seg_cnt = (int)infoJson["vl"]["vi"][0]["cl"]["fc"];
            if (seg_cnt == 0)
                seg_cnt = 1;

            var streams = infoJson["fl"]["fi"];


            //取最后一个
            var stream = streams.LastOrDefault();

            var quality = (string)stream["name"];
            var definition = (string)stream["cname"];
            definition = definition.Replace(";", "");
            var part_format_id = (int)stream["id"];
            var fs = (long)stream["fs"];

            var partInfos = new List<VideoPart>();
            //先根据fc判断是否有分片
            var fc = (int)infoJson["vl"]["vi"][0]["cl"]["fc"];
            if (fc == 0)
            {
                var id = (string)infoJson["vl"]["vi"][0]["cl"]["keyid"];
                var format_id = id.Split('.')[1];
                double.TryParse((string)infoJson["vl"]["vi"][0]["td"], out double duration);
                partInfos.Add(new VideoPart()
                {
                    Index = 1,
                    Id = format_id,
                    Duration = duration,
                    Name=fn
                });
            }
            else
            {
                var ci = infoJson["vl"]["vi"][0]["cl"]["ci"];
                foreach (var item in ci)
                {
                    var index = (int)item["idx"];
                    double.TryParse((string)item["cd"], out double duration);
                    var id = (string)item["keyid"];
                    partInfos.Add(new VideoPart()
                    {
                        Index = index,
                        Id = id,
                        Duration = duration,
                        Name = $"{fn_pre}.p{part_format_id % 10000}.{index}.mp4"
                    });
                }
            }


            Parallel.ForEach(partInfos, part =>
            {
                int tryCount = 2;
                var filename = part.Name;
                GetKeyInfo:
                var key_api = $"http://vv.video.qq.com/getkey?otype=json&platform=11&format={part_format_id}&vid={vid}&filename={filename}&appver=3.2.19.333";
                var keyInfo = HttpHelper.Get(key_api);
                if (string.IsNullOrEmpty(keyInfo))
                {
                    if (tryCount <= 0)
                    {
                        part.Remark = "请求失败";
                        return;
                    }
                    tryCount--;
                    Task.Delay(200);
                    goto GetKeyInfo;
                }
                var keyText = Util.Match(keyInfo, "QZOutputJson=(.*)").TrimEnd(';');
                var keyJson = JsonConvert.DeserializeObject(keyText) as JObject;


                if (string.IsNullOrEmpty((string)keyJson["key"]))
                {
                    part.Remark = (string)keyJson["msg"];
                    return;
                }

                var vkey = (string)keyJson["key"];
                var url = $"{host}{filename}?vkey={vkey}";
                part.Url = url;

            });

            var detail = new VideoDetail()
            {
                Site=SiteCode.qq.GetDescription(),
                Title = title,
                Type = type,
                Parts = partInfos.OrderBy(x => x.Index).ToList(),
                Definition = definition,
                Size = fs
            };

            return detail;


        }
    }
}
