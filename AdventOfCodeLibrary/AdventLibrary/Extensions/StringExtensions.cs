using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventLibrary.Extensions
{
    public static class StringExtensions
    {
        private static readonly Regex whiteSpaceRegex = new Regex(@"\s+");

        public static bool IsEmpty(this string input)
        {
            return input.Equals(string.Empty);
        }

        public static bool IsNumeric(this string input)
        {
            return long.TryParse(input, out _);
        }

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
                        if (str[j + i] != substring[j])
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

        public static string RemoveWhitespace(this string input)
        {
            return whiteSpaceRegex.Replace(input, string.Empty);
        }

        public static string RemoveLettersFromString(this string str, string remove)
        {
            foreach (var c in remove)
            {
                str = str.Replace(c.ToString(), string.Empty);
            }
            return str;
        }

        public static string RemoveDigitsFromString(this string input)
        {
            string output = string.Empty;
            foreach (char c in input)
            {
                if (!char.IsNumber(c))
                {
                    output += c;
                }
            }
            return output;
        }

        public static string ReplaceWhitespace(string input, string replacement)
        {
            return whiteSpaceRegex.Replace(input, replacement);
        }

        public static bool ContainsAllLetters(this string str, string letters)
        {
            return str.ContainsAllLetters(letters.ToCharArray());
        }

        public static bool ContainsAllLetters(this string str, char[] letters)
        {
            foreach (var c in letters)
            {
                if (!str.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsAnagram(this string str, string otherStr)
        {
            string strOrdered = String.Concat(str.OrderBy(c => c));
            string otherStrOrdered = String.Concat(otherStr.OrderBy(c => c));

            return strOrdered.Equals(otherStrOrdered);
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
                            dict.Add(c, new List<int>() { i });
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

        public static string ConcatListOfStrings(List<string> list, char? sep = null)
        {
            if (sep == null)
            {
                return string.Join(string.Empty, list.ToArray());
            }
            else
            {
                return string.Join(sep.Value, list.ToArray());
            }
        }

        public static string ConcatListOfStrings(List<string> list, string sep)
        {
            return ConcatListOfStrings(list, sep[0]);
        }

        public static string GetStringBetweenTwoCharacters(this string input, char startChar, char endChar)
        {
            int startIndex = input.IndexOf(startChar);
            int endIndex = input.IndexOf(endChar);
            return input.Substring(startIndex + 1, endIndex - startIndex - 1);
        }
    }
}