using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventLibrary.Helpers
{
    public static class DictionaryHelper
    {
        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
            where TValue : new()
        {
            if (!dict.TryGetValue(key, out TValue val))
            {
                dict.Add(key, value);
            }

            return val;
        }
    }
}
