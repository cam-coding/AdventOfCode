using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day04 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t', '[', ']' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var sum = 0;

            foreach (var line in lines)
            {
                var dict = new Dictionary<char, int>();

                var tokens = line.Split(delimiterChars);
                var letters = line.GetLettersFromString();

                foreach (var letter in letters)
                {
                    if (!dict.TryAdd(letter, 1))
                    {
                        dict[letter]++;
                    }
                }
                var checksum = string.Empty;
                var sortedDict = from entry in dict orderby entry.Value descending select entry;
                var newDict = dict.OrderByDescending(x => x.Value).ThenBy(x => x.Key);

                foreach (var entry in newDict)
                {
                    if (checksum.Length == 5)
                    {
                        break;
                    }
                    checksum = checksum + entry.Key;
                }

                if (checksum.Equals(tokens[^2]))
                {
                    var nums = AdventLibrary.StringParsing.GetIntsFromString(line);
                    sum = sum + nums[0];
                }
            }
            return sum;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var sum = 0;

            foreach (var line in lines)
            {
                var dict = new Dictionary<char, int>();

                var tokens = line.Split(delimiterChars);
                var letters = line.GetLettersFromString();

                foreach (var letter in letters)
                {
                    if (!dict.TryAdd(letter, 1))
                    {
                        dict[letter]++;
                    }
                }
                var checksum = string.Empty;
                var sortedDict = from entry in dict orderby entry.Value descending select entry;
                var newDict = dict.OrderByDescending(x => x.Value).ThenBy(x => x.Key);

                foreach (var entry in newDict)
                {
                    if (checksum.Length == 5)
                    {
                        break;
                    }
                    checksum = checksum + entry.Key;
                }

                if (checksum.Equals(tokens[^2]))
                {
                    var nums = AdventLibrary.StringParsing.GetIntsFromString(line);
                    var shift = nums[0] % 26;

                    var listy = line.ToArray();
                    for (var i = 0; i < listy.Length; i++)
                    {
                        if (Char.IsLetter(listy[i]))
                        {
                            listy[i] = listy[i] + shift > 122 ?
                                (char)(listy[i] - 25 + shift) :
                                (char)(listy[i] + shift);
                        }
                    }
                    Console.WriteLine(new string(listy));
                }
            }
            return sum;
        }
    }
}