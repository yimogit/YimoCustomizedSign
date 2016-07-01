using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Web;

namespace YimoCore
{
    public static class StringExtensions
    {
        public static int ToInt(this string value)
        {
            return Int32.Parse(value);
        }

        public static int ToInt(this string value, int defaultValue)
        {
            var result = defaultValue;
            return int.TryParse(value, out result) ? result : defaultValue;
        }

        public static int? ToNullableInt(this string value)
        {
            int result;

            if (string.IsNullOrEmpty(value) || !int.TryParse(value, out result))
            {
                return null;
            }

            return result;
        }

        public static decimal ToDecimal(this string value)
        {
            return decimal.Parse(value);
        }

        public static decimal ToDecimal(this string value, decimal defaultValue)
        {
            var result = defaultValue;
            return decimal.TryParse(value, out result) ? result : defaultValue;
        }

        public static decimal? ToNullableDecimal(this string value)
        {
            decimal result;

            if (string.IsNullOrEmpty(value) || !decimal.TryParse(value, out result))
            {
                return null;
            }

            return result;
        }

        public static short? ToNullableShort(this string value)
        {
            short result;

            if (string.IsNullOrEmpty(value) || !short.TryParse(value, out result))
            {
                return null;
            }

            return result;
        }

        public static DateTime? ToNullableDateTime(this string value)
        {
            DateTime result;

            if (DateTime.TryParse(value, out result))
            {
                return result;
            }

            return null;
        }

        public static DateTime ToDateTime(this string value)
        {
            return DateTime.Parse(value);
        }

        public static byte? ToNullableByte(this string value)
        {
            byte result;

            if (string.IsNullOrEmpty(value) || !byte.TryParse(value, out result))
            {
                return null;
            }

            return result;
        }

        public static bool? ToNullableBool(this string value)
        {
            bool result;

            if (string.IsNullOrEmpty(value) || !bool.TryParse(value, out result))
            {
                return null;
            }

            return result;
        }

        public static bool ToBool(this string value)
        {
            return bool.Parse(value);
        }

        public static T ToEnum<T>(this string value) where T : struct
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            var result = (value ?? string.Empty).ToNullableEnum<T>();

            return result.HasValue ? result.Value : defaultValue;
        }

        public static Nullable<T> ToNullableEnum<T>(this string value) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            try
            {
                return (Nullable<T>)Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                return null;
            }
        }

        public static HashSet<string> GetDistinctSubtrings(this string value, int minLength)
        {
            HashSet<string> set = new HashSet<string>();
            for (int i = 0; i < value.Length - minLength + 1; i++)
            {
                for (int len = minLength; len <= value.Length - i; len++)
                {
                    string subStr = value.Substring(i, len);
                    set.Add(subStr);
                }
            }

            return set;
        }

        public static string ToCommaSeparatedString(this string value)
        {
            return String.Join(", ", value.SplitByComma(true, true));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="distinct"></param>
        /// <param name="caseless">will be ignored if distinct is false</param>
        /// <returns></returns>
        public static string[] SplitByComma(this string value, bool distinct, bool caseless)
        {
            if (String.IsNullOrEmpty(value)) return new string[0];

            string[] parts = value.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> list = new List<string>(parts.Length);
            HashSet<string> set = distinct ? new HashSet<string>() : null;

            foreach (string item in parts)
            {
                string trimedItem = item.Trim();
                if (trimedItem == "") continue;

                if (distinct)
                {
                    string itemToCompare = caseless ? item : item.ToLower();
                    if (set.Contains(itemToCompare)) continue;
                    set.Add(itemToCompare);
                }

                list.Add(trimedItem);
            }

            return list.ToArray();
        }

        public static int[] SplitByCommaAnConvertToInt32(this string value, bool distinct, bool caseless)
        {
            return value.SplitByComma(distinct, caseless).Select(e => int.Parse(e)).ToArray();
        }

        public static Dictionary<char, char> DictReplace = new Dictionary<char, char>
        {
                {'ｑ','q'},
                {'ｗ','w'},
                {'ｅ','e'},
                {'ｒ','r'},
                {'ｔ','t'},
                {'ｙ','y'},
                {'ｕ', 'u'},
                {'ｉ','i'},
                {'ｏ','o'},
                {'ｐ','p'},
                {'ａ','a'},
                {'ｓ','s'},
                {'ｄ','d'},
                {'ｆ','f'},
                {'ｇ','g'},
                {'ｈ','h'},
                {'ｊ','j'},
                {'ｋ','k'},
                {'ｌ','l'},
                {'ｚ','z'},
                {'ｘ','x'},
                {'ｃ','c'},
                {'ｖ','v'},
                {'ｂ','b'},
                {'ｎ','n'},
                {'ｍ','m'},
                {'，',','}
        };

        //替换
        public static string ConvertSbcCase(this string value)
        {
            StringBuilder result = new StringBuilder();

            foreach (char s in value)
            {
                if (DictReplace.Keys.Contains(s))
                {
                    result.Append(DictReplace[s]);
                }
                else
                {
                    result.Append(s);
                }
            }

            return result.ToString();

        }


        private static Regex blankCharRegex = new Regex(@"\s+", RegexOptions.Singleline | RegexOptions.Compiled);
        private static Regex validBeginTagRegex = new Regex(@"< *(p|div|cite|blockquote|object|embed|param|strong|b|em|i|a|s|del|span|br|img|strike)\b([^>]*)>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex validEndTagRegex = new Regex(@"< */(p|div|cite|blockquote|object|embed|param|strong|b|em|i|a|s|del|span|strike) *>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex scriptTagRegex = new Regex(@"< *script([^>])*>.*(< *(/) *script *>)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex styleTagRegex = new Regex(@"< *style([^>])*>.*(< *(/) *style *>)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex invalidElementRegex = new Regex(@"<[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex eventRegex = new Regex(@"(<[^>]*)on[a-zA-Z]+\s*=([^>]*>)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex hrefJsRegex = new Regex(@"href*=*[\s\S]javascript*:", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static Regex srcJsRegex = new Regex(@"src*=*[\s\S]javascript*:", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static string HtmlFilter(this string value)
        {
            // remove all the blanks
            value = blankCharRegex.Replace(value, " ");
            // encode valid tags
            value = validBeginTagRegex.Replace(value, "\n$1$2\t");
            value = validEndTagRegex.Replace(value, "\n/$1\t");
            // remove script & style blocks;
            value = scriptTagRegex.Replace(value, "");
            value = styleTagRegex.Replace(value, "");
            // remove invalid tags and keep the text.
            value = invalidElementRegex.Replace(value, "");
            // encode the char "<" & ">"
            value = value.Replace("<", "&lt;").Replace(">", "&gt;");
            // decode valid tags
            value = value.Replace("\n", "<").Replace("\t", ">");
            // remove event definitions(e.g., onclick="...", href="javascript:..", src="javascript:...").
            value = eventRegex.Replace(value, "");
            value = hrefJsRegex.Replace(value, "");
            value = srcJsRegex.Replace(value, "");

            return value.Trim();
        }

        public static string RemoveHtmlTags(this string value)
        {
            value = Regex.Replace(value, "<[^>]*>", "");
            value = Regex.Replace(value, @"\s+", " ");

            return value.HtmlDecode();
        }

        public static void HtmlEncode(this char ch, TextWriter writer)
        {
            ch.HtmlEncode(writer, false, false);
        }

        public static void HtmlEncode(this char ch, TextWriter writer, bool convertLineBreaks, bool doNotEncodeSpace)
        {
            if (ch == '\n')
            {
                writer.Write(convertLineBreaks ? "<br />" : "\n");
            }
            else if (ch == '\r')
            {
                writer.Write(convertLineBreaks ? "" : "\r");
            }
            else if (ch == ' ')
            {
                if (doNotEncodeSpace)
                {
                    writer.Write(ch);
                }
                else
                {
                    writer.Write("&nbsp;");
                }
            }
            else if (ch == '<')
            {
                writer.Write("&lt;");
            }
            else if (ch == '>')
            {
                writer.Write("&gt;");
            }
            else if (ch == '&')
            {
                writer.Write("&amp;");
            }
            else if (ch.NeedEncode())
            {
                writer.Write("&#" + ((int)ch).ToString() + ";");
            }
            else
            {
                writer.Write(ch);
            }
        }

        public static string HtmlEncode(this string value)
        {
            return HtmlEncode(value, false);
        }

        public static string HtmlEncode(this string value, bool convertLinebreaks)
        {
            return Encode(value, convertLinebreaks, false);
        }

        private static string Encode(string value, bool convertLinebreaks, bool withoutEncodeSpace)
        {
            if (value == null)
            {
                return null;
            }

            if (value.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder(value.Length * 2);
            TextWriter writer = new StringWriter(builder);

            foreach (char ch in value)
            {
                ch.HtmlEncode(writer, convertLinebreaks, withoutEncodeSpace);
            }

            return builder.ToString();
        }

        private static bool NeedEncode(this char ch)
        {
            if ((ch > '`') && (ch < '{')) return false;
            if ((ch > '@') && (ch < '[')) return false;
            if ((ch > '/') && (ch < ':')) return false;
            if ((ch == '.') || (ch == ',') || (ch == '-') || (ch == '_') || (ch == '/')) return false;
            if (ch >= '一') return false;
            // if (ch == ' ') return true;
            return true;
        }

        public static string UrlEncode(this string value)
        {
            // return AntiXss.UrlEncode(value);
            return HttpUtility.UrlEncode(value);
        }

        public static string HtmlAttrEncode(this string value)
        {
            return Encode(value, false, true);
        }

        public static string HtmlDecode(this string value)
        {
            return System.Web.HttpUtility.HtmlDecode(value);
        }

        public static string ExcludedSection(this string value, string newValue)
        {
            List<string> listOldSectionArray = value.SplitByComma(true, true).ToList();
            List<string> listNewSectionArray = newValue.SplitByComma(true, true).ToList();

            foreach (var item in listNewSectionArray)
            {
                if (listOldSectionArray.Contains(item))
                {
                    listOldSectionArray.Remove(item);
                }
            }

            return String.Join(", ", listOldSectionArray.ToArray());
        }

        public static bool IsDigitalOrLetter(this char ch)
        {
            return ch.IsDigital() || ch.IsLetter();
        }

        public static bool IsDigital(this char ch)
        {
            return ch >= '0' && ch <= '9';
        }

        public static bool IsLetter(this char ch)
        {
            return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z');
        }

        public static string ConvertToDbc(this string input)
        {
            char[] charArray = input.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                if (charArray[i] == 12288)
                {
                    charArray[i] = (char)32;
                    continue;
                }
                if (charArray[i] > 65280 && charArray[i] < 65375)
                    charArray[i] = (char)(charArray[i] - 65248);
            }
            return new string(charArray);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string WhenNullOrEmpty(this string value, string defaultValue)
        {
            return value.IsNullOrEmpty() ? defaultValue : value;
        }

        public static string CutLeft(this string value, int length)
        {
            if (value.Length > length)
            {
                return value.Substring(0, length - 1) + "…";
            }

            return value;
        }

        public static string CutLeftAndHtmlEncode(this string value, int length)
        {
            if (value.Length > length)
            {
                return value.Substring(0, length - 1).HtmlEncode() + "…";
            }

            return value.HtmlEncode();
        }

        public static string TryParseDateTime(this string value)
        {
            DateTime dateTime;

            if (DateTime.TryParse(value, out dateTime))
            {
                return dateTime.ToChineseDate();
            }

            return value;
        }
        public static bool IsBase64Formatted(this string input)
        {
            try
            {
                Convert.FromBase64String(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
