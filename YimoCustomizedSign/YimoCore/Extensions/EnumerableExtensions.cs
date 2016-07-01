using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace YimoCore
{
    public static class EnumerableExtensions
    {
        public static string Join<T>(this IEnumerable<T> values, string separator)
        {
            List<string> strList = new List<string>();

            foreach (T item in values)
            {
                strList.Add(item.ToString());
            }

            return String.Join(separator, strList.ToArray());
        }

        public static string Join<T>(this IEnumerable<T> values, string separator, string lastSeparator)
        {
            if (values.Count() > 1)
            {
                return values.Take(values.Count() - 1).Join(separator) + lastSeparator + values.Last().ToString();
            }

            return values.Join(separator);
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> values)
        {
            if (values == null)
            {
                return values;
            }

            var valueList = values.ToList<T>();

            if (valueList.Count <= 1)
            {
                // nothing to randomize...
                return values;
            }

            Random r = new Random(DateTime.Now.Millisecond);

            for (int currentIndex = 0; currentIndex < valueList.Count - 1; ++currentIndex)
            {
                int exchangeIndex = r.Next(currentIndex + 1, valueList.Count);

                // exchange
                var temp = valueList[currentIndex];
                valueList[currentIndex] = valueList[exchangeIndex];
                valueList[exchangeIndex] = temp;
            }

            return valueList;
        }

        public static int IndexOf<T>(this IEnumerable<T> values, T itemToFind)
        {
            int position = 0;
            foreach (T item in values)
            {
                if (item.Equals(itemToFind))
                {
                    return position;
                }

                position++;
            }

            return -1;
        }

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool IsAscending)
        {
            return IsAscending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool IsAscending)
        {
            return IsAscending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
        }

        public static void ForEach<T>(this IEnumerable<T> objs, Action<T> action)
        {
            foreach (var o in objs) action(o);
        }

        public static void ForEach<T>(this IEnumerable<T> objs, Action<T, int> action)
        {
            int i = 0;
            foreach (var o in objs)
            {
                action(o, i);
                i++;
            }
        }

    }
}
