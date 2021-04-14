using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XDownloader.Analyzer
{
    public static class AnalyzerFactory
    {
        static List<Type> _allAnalyzerType = new List<Type>();

        static AnalyzerFactory()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var t in type.GetInterfaces())
                    {
                        if (t == typeof(IAnalyzer))
                        {
                            _allAnalyzerType.Add(type);
                            break;
                        }
                    }
                }
            }
        }

        public static AnalyzeResponse Analyze(string url)
        {
            var analyser = Create(url);
            if (analyser == null)
            {
                return new AnalyzeResponse()
                {
                    Success = false,
                    Message = "没有找到对应的解析器，无法解析"
                };
            }
            return analyser.Analyze(url, null);

        }

        public static IAnalyzer Create(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;

            foreach (var type in _allAnalyzerType)
            {
                var attrbutes = type.GetCustomAttributes(typeof(AnalyzerTypeAttribute), false);
                if (attrbutes.Length > 0)
                {
                    if (attrbutes[0] is AnalyzerTypeAttribute attrbute && Regex.IsMatch(url, attrbute.Type.GetDescription()))
                    {
                        return (IAnalyzer)Activator.CreateInstance(type);
                    }
                }
            }
            return null;

        }


        public static string GetDescription(this Enum value, bool nameInstend = true)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (attribute == null && nameInstend == true)
            {
                return name;
            }
            return attribute == null ? null : attribute.Description;
        }
    }
}
