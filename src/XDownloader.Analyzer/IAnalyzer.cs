using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDownloader.Analyzer
{
    public interface IAnalyzer
    {
        AnalyzeResponse Analyze(string url, Dictionary<string, object> parameters);
    }
}
