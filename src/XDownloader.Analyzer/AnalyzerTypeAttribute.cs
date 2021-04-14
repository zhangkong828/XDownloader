using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDownloader.Analyzer
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class AnalyzerTypeAttribute: Attribute
    {
        public SiteCode Type { get; set; }
    }

    public enum SiteCode
    {
        [Description("qq.com")]
        qq,

        [Description("bilibili.com")]
        bilibili,

        [Description("youku.com")]
        youku,

        [Description("douyin.com")]
        douyin,

        [Description("tiktok.com")]
        tiktok,

        [Description("mgtv.com")]
        mgtv,
    }
}
