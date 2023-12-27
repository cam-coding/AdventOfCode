using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventLibrary
{
    public static class StringParsing
    {
        public static string GetStringBetweenTwoCharacters(string input, char startChar, char endChar)
        {
            int startIndex = input.IndexOf(startChar);
            int endIndex = input.IndexOf(endChar);
            return input.Substring(startIndex + 1, endIndex - startIndex - 1);
        }

        public static string GetLettersFromString(string input)
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

        public static List<int> GetNumbersFromString(string input)
        {
            var numbers = Regex.Split(input, @"\D+").ToList().Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x));
            return numbers.ToList();
        }

        public static List<long> GetLongNumbersFromString(string input)
        {
            var numbers = Regex.Split(input, @"\D+").ToList().Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => long.Parse(x));
            return numbers.ToList();
        }

        public static List<int> GetNumbersWithNegativesFromString(string input)
        {
            var numbers = Regex.Matches(input, @"-?[0-9]*?[0-9]+").ToList().
                            Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => int.Parse(x.Value));
            return numbers.ToList();
        }

        public static List<long> GetLongNumbersWithNegativesFromString(string input)
        {
            var numbers = Regex.Matches(input, @"-?[0-9]*?[0-9]+").ToList().
                            Where(x => !string.IsNullOrWhiteSpace(x.Value)).Select(x => long.Parse(x.Value));
            return numbers.ToList();
        }
        public static List<string> GetNumbersFromStringAsStrings(string input)
        {
            var numbers = Regex.Split(input, @"\D+").Where(x => !string.IsNullOrWhiteSpace(x));
            return numbers.ToList();
        }

        public static List<int> GetDigitsFromString(string input)
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

        public static string RemoveLettersFromString(string str, string remove)
        {
            foreach (var c in remove)
            {
                str = str.Replace(c.ToString(), string.Empty);
            }
            return str;
        }

        public static bool LettersInsideString(string str, string letters)
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

        public static List<(int,int)> GetNumbersWithIndexesFromString(string input)
        {
            var result = new List<(int,int)>();
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

        public static List<(int, int)> GetDigitsWithIndexesFromString(string input)
        {
            List<(int, int)> output = new List<(int, int)>();
            for (var i = 0; i < input.Length; i++)
            {
                var c = input[i];
                if (char.IsDigit(c))
                {
                    output.Add((c-'0', i));
                }
            }
            return output;
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
    }
}