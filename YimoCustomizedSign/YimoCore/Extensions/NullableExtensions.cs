using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YimoCore
{
    public static class NullableExtensions
    {
        public static T TryGetValue<T>(this Nullable<T> t, T defalutValue) where T : struct
        {
            return t.HasValue ?  t.Value : defalutValue;
        }

        public static string WhenHasValue<T>(this Nullable<T> t, Func<T, string> valueGetter) where T : struct
        {
            return WhenHasValue(t, valueGetter, string.Empty);
        }

        public static string WhenHasValue<T>(this Nullable<T> t, Func<T, string> valueGetter, string nullReturned) where T : struct
        {
            return t.HasValue ? valueGetter(t.Value) : nullReturned;
        }
    }
}
