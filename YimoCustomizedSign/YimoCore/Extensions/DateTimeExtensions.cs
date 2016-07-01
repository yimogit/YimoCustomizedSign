using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YimoCore
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUtc(this DateTime time)
        {
            if (time.Kind == DateTimeKind.Utc)
            {
                return time;
            }
            else if (time.Kind == DateTimeKind.Local)
            {
                return time.ToUniversalTime();
            }
            else
            {
                return time.ToLocalTime().ToUniversalTime();
            }
        }


        private static readonly DateTime s_maxDateTime = new DateTime(4000, 1, 1, 1, 1, 1, 1).ToUtc();
        public static DateTime MaxDateTime
        {
            get
            {
                return s_maxDateTime;
            }
        }

        public static bool IsMaxDateTime(this DateTime time)
        {
            return time.Year == MaxDateTime.Year;
        }

        public static DateTime? ToUtc(this DateTime? time)
        {
            return time.HasValue ? (DateTime?)time.Value.ToUtc() : null;
        }

        public static string ToChineseDate(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd");
        }

        public static string ToChineseLongDate(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换成时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static Int64 ToTimestamp(this DateTime time)
        {
            return (time.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }

        public static string ToOffsetString(this DateTime time)
        {
            DateTime now = DateTime.UtcNow;

            bool isBefore = now > time;

            TimeSpan offset = isBefore ? (now - time) : (time - now);

            if (offset < new TimeSpan(0, 1, 0))
            {
                return isBefore ? "刚刚" : "马上";
            }
            else if (offset < new TimeSpan(1, 0, 0))
            {
                return string.Format("{0}分钟{1}", offset.Minutes, isBefore ? "前" : "后");
            }
            else if (offset < new TimeSpan(1, 0, 0, 0))
            {
                return string.Format("{0}小时{1}", offset.Hours, isBefore ? "前" : "后");
            }
            else if (offset < new TimeSpan(7, 0, 0, 0))
            {
                return string.Format("{0}天{1}", offset.Days, isBefore ? "前" : "后");
            }
            else if (offset < new TimeSpan(30, 0, 0, 0))
            {
                return string.Format("{0}周{1}", offset.Days / 7, isBefore ? "前" : "后");
            }
            else if (offset < new TimeSpan(365, 0, 0, 0))
            {
                return string.Format("{0}个月{1}", offset.Days / 30, isBefore ? "前" : "后");
            }
            else
            {
                return string.Format("很久以{0}", isBefore ? "前" : "后");
            }
        }

        public static DateTime GetThisMonday(this DateTime value)
        {
            int dayofWeek = value.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)value.DayOfWeek;
            return value.Date.AddDays((int)DayOfWeek.Monday - dayofWeek);
        }

        public static DateTime GetTheMinTime(this DateTime time)
        {
            return DateTime.Parse(time.Date.ToShortDateString() + " 00:00:00");
        }

        public static DateTime? GetTheMinTime(this DateTime? time)
        {
            if (time.HasValue)
                return DateTime.Parse(time.Value.Date.ToShortDateString() + " 00:00:00");
            else
                return time;
        }

        public static DateTime GetTheMaxTime(this DateTime time)
        {
            return DateTime.Parse(time.Date.ToShortDateString() + " 23:59:59");
        }

        public static DateTime? GetTheMaxTime(this DateTime? time)
        {
            if (time.HasValue)
                return DateTime.Parse(time.Value.Date.ToShortDateString() + " 23:59:59");
            else
                return time;
        }

        public static string HtmlEncode(this DateTime value, string format)
        {
            return value.ToString(format).HtmlEncode();
        }

        public static string HtmlAttrEncode(this DateTime value, string format)
        {
            return value.ToString(format).HtmlAttrEncode();
        }

        public static string UrlEncode(this DateTime value, string format)
        {
            return value.ToString(format).UrlEncode();
        }
    }
}
