using System.Collections.Generic;

namespace AdventLibrary.Extensions
{
    public static class DictionaryExtensions
    {
        public static string Stringify<T, U>(this Dictionary<T, U> list, char separator = ':')
        {
            var str = string.Empty;
            foreach (var item in list)
            {
                str += item.Key.ToString() + separator + item.Value.ToString() + '\n';
            }
            return str;
        }
    }
}
