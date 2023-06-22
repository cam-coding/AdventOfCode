using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventLibrary
{
    public static class StringHelper
    {
        public static string ReverseString(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static int CountPairs_NonOverlapping(string str)
        {
            return CountGroups_NonOverlapping(str, 2);
        }

        public static int CountPairs_NonOverlapping_Unique(string str)
        {
            return CountGroups_NonOverlapping(str, 2, true);
        }

        public static int CountGroups_NonOverlapping(string str, int size, bool unique = false)
        {
            var dict = new Dictionary<char, List<int>>();
            for (var i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (i <= str.Length - size)
                {
                    var valid = true;
                    for (var j = i + 1; j < i + size; j++)
                    {
                        if (str[j] != c)
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid)
                    {
                        if (!dict.ContainsKey(c))
                        {
                            dict.Add(c, new List<int>() {  i });
                        }
                        else
                        {
                            if (!unique)
                            {
                                // checking for non overlapping
                                if (dict[c].All(x => x + size <= i))
                                {
                                    dict[c].Add(i);
                                }
                            }
                        }
                    }
                }
            }

            return dict.Values.Sum(x => x.Count);
        }
    }
}