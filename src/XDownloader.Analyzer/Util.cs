using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XDownloader.Analyzer
{
    public class Util
    {
        public static bool IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        public static string Match(string input, string pattern)
        {
            var match = Regex.Match(input, pattern);
            if (match.Success)
            {
                var value = match.Groups[1].Value;
                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();
            }
            return null;
        }
    }
}
