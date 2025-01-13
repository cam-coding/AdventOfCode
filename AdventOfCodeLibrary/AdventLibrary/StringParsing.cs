using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;

namespace AdventLibrary
{
    public static class StringParsing
    {
        private static char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public static string GetLettersFromString(this string input)
        {
            string output = "";
            foreach (char c in input)
            {
                if (char.IsLetter(c))
                {
                    output += c;
                }
            }
            return output;
        }

        public static List<int> GetDigitsFromString(this string input)
        {
            List<int> output = new List<int>();
            foreach (char c in input)
            {
                if (char.IsNumber(c))
                {
                    output.Add(c - '0');
                }
            }
            return output;
        }

        public static List<int> GetNumbersFromString(this string input)
        {
            var numbers = Regex.Split(input, @"\D+").ToList().Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x));
            return numbers.ToList();
        }

        public static List<int> GetNumbersWithNegativesFromString(this string input)
        {
            return Regex.Matches(input, @"-?[0-9]*?[0-9]+")
                            .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                            .Select(x => int.Parse(x.Value))
                            .ToList();
        }

        public static List<long> GetLongsFromString(this string input)
        {
            var numbers = Regex.Split(input, @"\D+").ToList().Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => long.Parse(x));
            return numbers.ToList();
        }

        public static List<long> GetLongsWithNegativesFromString(this string input)
        {
            var numbers = Regex.Matches(input, @"-?[0-9]*?[0-9]+").ToList().
                            Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => long.Parse(x.Value));
            return numbers.ToList();
        }

        public static List<string> GetNumbersFromStringAsStrings(this string input)
        {
            var numbers = Regex.Split(input, @"\D+").Where(x => !string.IsNullOrWhiteSpace(x));
            return numbers.ToList();
        }

        public static List<(int, int)> GetNumbersWithIndexesFromString(this string input)
        {
            var result = new List<(int, int)>();
            for (var i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    var currentNum = "";
                    var start = i;
                    while (i < input.Length && char.IsDigit(input[i]))
                    {
                        currentNum += (input[i]);
                        i++;
                    }
                    result.Add((int.Parse(currentNum), start));
                }
            }
            return result;
        }

        public static List<(int, int)> GetDigitsWithIndexesFromString(this string input)
        {
            List<(int, int)> output = new List<(int, int)>();
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsDigit(c))
                {
                    output.Add((c - '0', i));
                }
            }
            return output;
        }

        public static List<string> GetRealTokens(this string input, char delimiterChar)
        {
            return GetRealTokens(input, [delimiterChar]);
        }

        public static List<string> GetRealTokens(this string input, char[] delimiterChars)
        {
            var tokens = input.Split(delimiterChars).ToList();
            return tokens.GetRealStrings(delimiterChars);
        }

        public static GridLocation<int> GetCoordsFromString(this string input)
        {
            var nums = GetLongsFromString(input);
            return new GridLocation<int>((int)nums[0], (int)nums[1]);
        }
    }
}