using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventLibrary
{
    public static class StringExtensions
    {
        private static readonly Regex whiteSpaceRegex = new Regex(@"\s+");

        public static string ReverseString(this string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static List<int> GetIndexesOfSubstring(this string str, string substring)
        {
            var result = new List<int>();

            for (var i = 0; i <= str.Length - substring.Length; i++)
            {
                if (str[i] == substring[0])
                {
                    var valid = true;
                    for (var j = 1; j < substring.Length; j++)
                    {
                        if (str[j+i] != substring[j])
                        {
                            valid = false;
                        }
                    }
                    if (valid)
                    {
                        result.Add(i);
                    }
                }
            }

            return result;
        }

        public static string ReplaceFirstInstanceOf(this string str, string find, string replacement)
        {
            var index = str.IndexOf(find);

            if (index != -1 && !str.Equals(string.Empty))
            {
                return str.Remove(index, find.Length).Insert(index, replacement);
            }

            return str;
        }

        public static int CountPairs_NonOverlapping(this string str)
        {
            return CountGroups_NonOverlapping(str, 2);
        }

        public static int CountPairs_NonOverlapping_Unique(this string str)
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
        public static string RemoveWhitespace(this string input)
        {
            return whiteSpaceRegex.Replace(input, string.Empty);
        }

        public static string ReplaceWhitespace(string input, string replacement)
        {
            return whiteSpaceRegex.Replace(input, replacement);
        }

        public static bool IsEmpty(this string input)
        {
            return input.Equals(string.Empty);
        }
    }
}