using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDownloader.Analyzer.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //腾讯视频 vip  https://v.qq.com/x/cover/kds9l8b75jvb6y6.html
            //https://v.qq.com/x/page/f3237khxrtt.html

            var url = "https://v.qq.com/x/page/f3237khxrtt.html";

            var response = AnalyzerFactory.Analyze(url);

            var json = JsonConvert.SerializeObject(response);
            Console.WriteLine(json);

            Console.ReadKey();
        }
    }
}
