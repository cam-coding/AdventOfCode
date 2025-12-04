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

        public static string SwapCharactersAtIndexes(this string str, int left, int right)
        {
            char[] charArray = str.ToCharArray();
            var tmp = charArray[left];
            charArray[left] = charArray[right];
            charArray[right] = tmp;
            return new string(charArray);
        }

        // gets middle char or left of middle if string length is even
        public static char GetMiddleCharacter(this string input)
        {
            return input.ToList().GetMiddleItem();
        }

        public static string GetStringBetweenTwoCharacters(this string input, char startChar, char endChar)
        {
            int startIndex = input.IndexOf(startChar);
            int endIndex = input.IndexOf(endChar);
            return input.Substring(startIndex + 1, endIndex - startIndex - 1);
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

        public static List<int> GetIndexesOfSubstringNonOverlapping(this string str, string substring)
        {
            var result = new List<int>();

            var index = 0;

            while (index <= str.Length - substring.Length)
            {
                var valid = true;
                for (var j = 0; j < substring.Length; j++)
                {
                    if (str[j + index] != substring[j])
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    result.Add(index);
                    index += substring.Length;
                }
                else
                {
                    index++;
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

        public static int CountMatchingCharacters(this string str, string characters)
        {
            return CountMatchingCharacters(str, characters.ToList());
        }

        // count how many characters in str appear in the list of characters
        // str = "abcdef" characters {a,b,c} return = 3
        public static int CountMatchingCharacters(this string str, List<char> characters)
        {
            return str.Count(x => characters.Contains(x));
        }

        public static int CountPairs_NonOverlapping(this string str)
        {
            return CountGroups_NonOverlapping(str, 2);
        }

        public static int CountPairs_NonOverlapping_Unique(this string str)
        {
            return CountGroups_NonOverlapping(str, 2, true);
        }

        /// <summary>
        /// Gets number of substrings of length N in a string. Can specific unique or not.
        /// </summary>
        /// ex: "aabbaa" n=2 would return 3.
        /// If unique, only count unique subgroups. The above example would return 2.
        public static int CountGroups_NonOverlapping(this string str, int size, bool unique = false)
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

        /// <summary>
        /// Get two string, first half and 2nd half. If odd length the 2nd string will be longer
        /// </summary>
        public static string[] SplitInHalf(this string str)
        {
            var mid = str.Length / 2;
            var firstString = str.Substring(0, mid);
            var secondString = str.Substring(mid);
            return new[] { firstString, secondString };
        }
    }
}