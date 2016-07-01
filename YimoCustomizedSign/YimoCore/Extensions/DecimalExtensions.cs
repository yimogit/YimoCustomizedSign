using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YimoCore
{
    public static class DecimalExtensions
    {
        public static string FormatPrice(this decimal value)
        {
            if (value <= decimal.Zero)
                return value.ToString(CultureInfo.InvariantCulture);

            var v = decimal.Truncate(value).ToString(CultureInfo.InvariantCulture);
            var regex = new Regex(@"(-?\d+)(\d{3})");
            while (regex.IsMatch(v))
            {
                v = regex.Replace(v, "$1,$2");
            }

            return v;
        }
    }
}
