using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2018
{
    public class Day01 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var counter = 0;

            foreach (var line in lines)
            {
                var nums = AdventLibrary.StringParsing.GetNumbersWithNegativesFromString(line);

                foreach (var num in nums)
                {
                    counter += num;
                }
            }
            return counter;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var counter = 0;
            var tracker = new HashSet<int>();

            while (true)
            {
                foreach (var line in lines)
                {
                    var nums = AdventLibrary.StringParsing.GetNumbersWithNegativesFromString(line);

                    foreach (var num in nums)
                    {
                        counter += num;
                    }

                    if (tracker.Contains(counter))
                    {
                        return counter;
                    }
                    else
                    {
                        tracker.Add(counter);
                    }
                }
            }
        }
    }
}