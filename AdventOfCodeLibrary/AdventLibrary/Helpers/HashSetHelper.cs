using AdventLibrary.Extensions;
using System.Collections.Immutable;

namespace AdventLibrary.Helpers
{
    public static class HashSetHelper
    {
        public static string Stringify<T>(this HashSet<T> hashSet, char separator = DefaultValues.DEFAULT_CHAR_SEPARATOR)
        {
            return string.Join(separator, hashSet);
        }

        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                hashSet.Add(item);
            }
        }

        public static bool TryAdd<T>(this HashSet<T> hashset, T value)
        {
            if (hashset.Contains(value))
            {
                return false;
            }
            hashset.Add(value);
            return true;
        }

        public static string HashObjectSorted<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            var str = string.Empty;
            var sorted = dict.Clone().ToImmutableSortedDictionary();
            foreach (var item in sorted)
            {
                str += "$" + item.Key.ToString() + "%" + item.Value.ToString();
            }
            return str;
        }

        public static string HashObjectSorted<T>(List<T> list)
        {
            var sorted = list.Clone();
            sorted.Sort();
            var str = string.Empty;
            foreach (var item in sorted)
            {
                str += "$" + item.ToString();
            }
            return str;
        }
    }
}