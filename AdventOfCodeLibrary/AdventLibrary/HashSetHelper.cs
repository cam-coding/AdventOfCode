using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventLibrary
{
    public static class HashSetHelper
    {
        public static string Stringify<T>(this HashSet<T> hashSet)
        {
            var str = string.Empty;
            foreach (var item in hashSet)
            {
                str += item.ToString() + ":";
            }
            return str;
        }

        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                hashSet.Add(item);
            }
        }
    }
}
