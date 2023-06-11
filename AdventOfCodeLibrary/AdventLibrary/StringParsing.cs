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

        public static string GetNumbersFromString2(string input)
        {
            string output = "";
            foreach (char c in input)
            {
                if (char.IsNumber(c))
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

        public static List<int> GetDigitsFromString(string input)
        {
            List<int> output = new List<int>();
            foreach (char c in input)
            {
                if (char.IsNumber(c))
                {
                    output.Add(int.Parse(c.ToString()));
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
    }
}