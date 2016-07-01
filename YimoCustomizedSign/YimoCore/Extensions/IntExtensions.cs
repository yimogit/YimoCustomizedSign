using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YimoCore
{
    public static class IntExtensions
    {
        public static string ToFileSize(this int value)
        {
            string result = "0 B";
            if (value >= 1073741824)
                result = String.Format("{0:##.##}", value / 1073741824.0) + " GB";
            else if (value >= 1048576)
                result = String.Format("{0:##.##}", value / 1048576.0) + " MB";
            else if (value >= 1024)
                result = String.Format("{0:##.##}", value / 1024.0) + " KB";
            else if (value > 0 && value < 1024)
                result = value.ToString() + " B";

            return result;
        }
        /// <summary>
        /// 将时间戳转换为普通时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int value)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(value + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
    }
}
