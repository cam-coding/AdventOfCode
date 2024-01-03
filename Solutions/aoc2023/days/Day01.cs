using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day01: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var counter = 0;
			foreach (var line in lines)
			{
				// var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
                var nums = AdventLibrary.StringParsing.GetDigitsFromString(line);
                var sum = nums[0] + nums[nums.Count-1];
                var blah = string.Empty + nums[0] + nums[nums.Count - 1];
                counter += int.Parse(blah);
			}
            return counter;
        }

        private object Part2()
        {
            var dict = new Dictionary<string, int>()
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 },
                { "1", 1 },
                { "2", 2 },
                { "3", 3 },
                { "4", 4 },
                { "5", 5 },
                { "6", 6 },
                { "7", 7 },
                { "8", 8 },
                { "9", 9 },
            };

            var lines = ParseInput.GetLinesFromFile(_filePath);
            var counter = 0;
            foreach (var line in lines)
            {
                var lowest = 10000;
                var highest = -1;
                var currentHigh = 0;
                var current = 0;

                foreach (var item in dict)
                {
                    var index = line.IndexOf($"{item.Key}");
                    if (index != -1 && index < lowest)
                    {
                        lowest = index;
                        current = item.Value;
                    }
                    var index2 = line.LastIndexOf($"{item.Key}");
                    if (index2 != -1 && index2 > highest)
                    {
                        highest = index2;
                        currentHigh = item.Value;
                    }
                }

                var combined = string.Empty + current + currentHigh;
                counter += int.Parse(combined);
            }
            return counter;
        }
    }
}