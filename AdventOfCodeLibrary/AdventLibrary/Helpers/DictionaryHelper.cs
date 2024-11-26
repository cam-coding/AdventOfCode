using System.Collections.Generic;

namespace AdventLibrary.Helpers
{
    public static class DictionaryHelper
    {
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            return new Dictionary<TKey, TValue>(dict);
        }

        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (!dict.TryGetValue(key, out TValue val))
            {
                dict.Add(key, value);
            }

            return val;
        }

        // creates a dictionary from 0-size-1 with default value
        public static Dictionary<int, T> CreateIndexDictionary<T>(T defaultValue, int size)
        {
            var ret = new Dictionary<int, T>();
            for (var i  = 0; i < size; i++)
            {
                ret.Add(i, defaultValue);
            }
            return ret;
        }

        // creates a dictionary from 0-size-1 with default value
        public static Dictionary<int, T> CreateIndexDictionary<T>(List<T> defaultValue)
        {
            var ret = new Dictionary<int, T>();
            for (var i = 0; i < defaultValue.Count; i++)
            {
                ret.Add(i, defaultValue[i]);
            }
            return ret;
        }

        // creates a dictionary from 1-size with capital letters as keys
        public static Dictionary<char, T> CreateAlphaIndexDictionary<T>(T defaultValue, int size)
        {
            char currentChar = 'A';
            var ret = new Dictionary<char, T>();
            for (var i = 1; i <= size; i++)
            {
                ret.Add(currentChar, defaultValue);
                currentChar++;
            }
            return ret;
        }

        // creates a dictionary from 1-size with capital letters as keys
        public static Dictionary<string, T> CreateAlphaStringIndexDictionary<T>(T defaultValue, int size)
        {
            char currentChar = 'A';
            var ret = new Dictionary<string, T>();
            for (var i = 1; i <= size; i++)
            {
                ret.Add(currentChar.ToString(), defaultValue);
                currentChar++;
            }
            return ret;
        }
    }
}
