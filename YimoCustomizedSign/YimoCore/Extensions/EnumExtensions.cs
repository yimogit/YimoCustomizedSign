using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace YimoCore
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(int value, T defaultT) where T : struct
        {
            string enumName = Enum.GetName(typeof(T), value);

            return ToEnum<T>(enumName, defaultT);
        }

        public static T ToEnum<T>(string enumName, T defaultT) where T : struct
        {
            if (string.IsNullOrWhiteSpace(enumName))
            {
                return defaultT;
            }

            T result;

            if (!Enum.TryParse<T>(enumName.Trim(), out result))
            {
                return defaultT;
            }

            if (Enum.IsDefined(typeof(T), result))
            {
                return result;
            }

            return defaultT;
        }

        public static bool Contains(this Enum parent, Enum ele)
        {
            return ((int)(object)parent | (int)(object)ele) == (int)(object)parent;
        }

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            object[] attributes = fi.GetCustomAttributes(true);

            if (attributes != null &&
               attributes.Length > 0)
                return ((DescriptionAttribute)attributes[0]).Description;
            else
                return value.ToString();
        }
    }
}
