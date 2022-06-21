using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace BaseApplication.Helper
{
    public static class StringHelper
    {
        public static string RemoveMultiSpace(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return Regex.Replace(s, @"\s+", " ");
        }
        public static string RemoveAllSpace(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return Regex.Replace(s, @"\s+", "");
        }
        public static string ConvertToUnsign(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            return regex.Replace(s.Normalize(NormalizationForm.FormD),
                    String.Empty).Replace('\u0111', 'd')
                .Replace('\u0110', 'D');
        }
        public static string ConvertToFts(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return s.Trim()
                .ToLower()
                .RemoveMultiSpace()
                .ConvertToUnsign();
        }

        public static string ConvertFtsNotSpace(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return s.Trim()
                .ToLower()
                .RemoveAllSpace()
                .ConvertToUnsign();
        }

        public static string LikeTextSearch(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            return $"%{s.ConvertToFts()}%";
        }
        public static string GetMimeType(this string fileName)
        {
            var extension = System.IO.Path.GetExtension(fileName).ToLower();
            return FileExtensionMapping.Mappings.TryGetValue(extension, out var mimeType) ? mimeType : "application/octet-stream";
        }
        public static string TextCapitalize(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            str = str.RemoveMultiSpace().Trim();
            var lst = str.Split(" ")
                .Select(x => char.ToUpper(x[0]) + x.Substring(1).ToLower());
            return string.Join(" ", lst);
        }

        public static string GetWhereSoftDelete(List<string> listTbl)
        {
            var listWhere = new List<string>();
            foreach (var tbl in listTbl)
            {
                listWhere.Add($" ({tbl}.Id is null or {tbl}.IsDeleted = 0) ");
            }

            return $" ( {string.Join(" and ", listWhere)} ) ";
        }

        public static string ConvertToCapitalize(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            text = Regex.Replace(text.ToLower().Trim(), @"\s+", " ");
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(text);
        }

        public static string TrimIfNotNull(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            return text.Trim();
        }

        public static bool CheckBoolConfigurationString(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            var fts = text.ConvertFtsNotSpace();
            return fts == "1" || fts == "true";
        }
        public static string TruncateLongString(this string str, int maxLength)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > maxLength)
            {
                return str.Substring(0, maxLength);
            }
            return str;
        }
        /// <summary>
        /// Mặc định
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string timeSpan)
        {
            try
            {
                timeSpan = timeSpan.ConvertFtsNotSpace();
                var l = timeSpan.Length - 1;
                var value = timeSpan.Substring(0, l);
                var type = timeSpan.Substring(l, 1);

                return type switch
                {
                    "d" => TimeSpan.FromDays(double.Parse(value)),
                    "h" => TimeSpan.FromHours(double.Parse(value)),
                    "m" => TimeSpan.FromMinutes(double.Parse(value)),
                    "s" => TimeSpan.FromSeconds(double.Parse(value)),
                    _ => throw new FormatException($"{timeSpan} can't be converted to TimeSpan, unknown type {type}"),
                };
            }
            catch
            {
                return TimeSpan.FromMinutes(30);
            }
          
        }
    }
}
