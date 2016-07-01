using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YimoCore
{
    public static class DictionaryExtensions
    {
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            return dict.ContainsKey(key) ? dict[key] : default(TValue);            
        }
    }
}
