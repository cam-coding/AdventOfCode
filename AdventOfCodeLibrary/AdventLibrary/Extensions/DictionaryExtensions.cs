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

        /// <summary>
        /// This functionality is already in Remove but I often forget that.
        /// </summary>
        public static TValue Pop<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            dict.Remove(key, out var value);
            return value;
        }
    }
}