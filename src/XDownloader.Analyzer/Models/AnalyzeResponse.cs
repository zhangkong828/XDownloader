using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDownloader.Analyzer
{
    public class AnalyzeResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public VideoDetail Data { get; set; }



        public static AnalyzeResponse Fail(string message = null)
        {
            return new AnalyzeResponse() { Success = false, Message = string.IsNullOrWhiteSpace(message) ? "解析失败" : message };
        }

        public static AnalyzeResponse NotSupported(string site = null)
        {
            return new AnalyzeResponse() { Success = false, Message = $"{site}暂不支持该链接" };
        }
    }
}
