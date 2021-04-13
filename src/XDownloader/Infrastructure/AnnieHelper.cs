using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XDownloader.Models;

namespace XDownloader.Infrastructure
{
    public class AnnieHelper
    {
        public static string ExePath = "./annie.exe";

        private static string RegexStringSite = @"Site:\s+?(.+)";
        private static string RegexStringTitle = @"Title:\s+?(.+)";
        private static string RegexStringType = @"Type:\s+?(.+)";
        private static string RegexString1 = @"(.+?)\s{4}(.+?%)";
        private static string RegexString2 = @"(.+?)\s{2,4}(.+?%)\s(.+?/s)";
        private static string RegexString3 = @"(.+?)\s{2,4}(.+?%)\s(.+?/s)\s(.+)";

        public static DownloadInfo QueryInfo(string url)
        {
            var info = new DownloadInfo();
            info.Url = url;

            var sb = new StringBuilder();
            sb.Append($" -i");
            sb.Append($" {url}");
            var arguments = sb.ToString();
            var process = new ProcessHelper(ExePath, arguments, new Action<string>(log =>
            {
                if (!string.IsNullOrWhiteSpace(log))
                {
                    var text = log.TrimStart();
                    if (text.StartsWith("Site"))
                    {
                        if (Regex.IsMatch(text, RegexStringSite))
                        {
                            var match = Regex.Match(text, RegexStringSite);
                            info.Site = match.Groups[1].Value.Trim();
                        }
                    }
                    else if (text.StartsWith("Title"))
                    {
                        if (Regex.IsMatch(text, RegexStringTitle))
                        {
                            var match = Regex.Match(text, RegexStringTitle);
                            info.Title = match.Groups[1].Value.Trim();
                        }
                    }
                    else if (text.StartsWith("Type"))
                    {
                        if (Regex.IsMatch(text, RegexStringType))
                        {
                            var match = Regex.Match(text, RegexStringType);
                            info.Type = match.Groups[1].Value.Trim();
                        }
                    }
                }

            }));
            process.Start();
            return info;
        }

        public static DownloadInfo ParseOutput(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            Console.WriteLine(text);
            var info = new DownloadInfo();
            text = text.TrimEnd();
            if (text.StartsWith(" "))
            {
                text = text.TrimStart();
                if (text.StartsWith("Site"))
                {
                    if (Regex.IsMatch(text, RegexStringSite))
                    {
                        var match = Regex.Match(text, RegexStringSite);
                        info.Site = match.Groups[1].Value.Trim();
                    }
                }
                else if (text.StartsWith("Title"))
                {
                    if (Regex.IsMatch(text, RegexStringTitle))
                    {
                        var match = Regex.Match(text, RegexStringTitle);
                        info.Title = match.Groups[1].Value.Trim();
                    }
                }
                else if (text.StartsWith("Type"))
                {
                    if (Regex.IsMatch(text, RegexStringType))
                    {
                        var match = Regex.Match(text, RegexStringType);
                        info.Type = match.Groups[1].Value.Trim();
                    }
                }
                else if (text.EndsWith("%"))
                {
                    if (Regex.IsMatch(text, RegexString1))
                    {
                        var match = Regex.Match(text, RegexString1);
                        info.Size = match.Groups[1].Value;
                        info.Progress = match.Groups[2].Value;
                    }
                }
                else if (text.EndsWith("/s"))
                {
                    if (Regex.IsMatch(text, RegexString2))
                    {
                        var match = Regex.Match(text, RegexString2);
                        info.Size = match.Groups[1].Value;
                        info.Progress = match.Groups[2].Value;
                        info.Speed = match.Groups[3].Value;
                    }
                }
                else if (text.EndsWith("s"))
                {
                    if (Regex.IsMatch(text, RegexString3))
                    {
                        var match = Regex.Match(text, RegexString3);
                        info.Size = match.Groups[1].Value;
                        info.Progress = match.Groups[2].Value;
                        info.Speed = match.Groups[3].Value;
                        info.Time = match.Groups[4].Value;
                    }
                }
            }
            else
            {
                info.Message = text + Environment.NewLine;
            }
            return info;
        }
    }
}
