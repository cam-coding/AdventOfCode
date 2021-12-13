using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day05: ISolver
    {
        private string _filePath;
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            /*
            var letters = AdventLibrary.StringParsing.GetStringBetweenTwoCharacters(input, "a", "b");
            var letters = AdventLibrary.StringParsing.GetLettersFromString(input);
            var letters = AdventLibrary.StringParsing.GetLettersFromString(input);
            var digits = AdventLibrary.StringParsing.GetDigitsFromString(input);
            var strings = AdventLibrary.ParseInput.GetLinesFromFile(filePath);
            var numbers = AdventLibrary.ParseInput.GetNumbersFromFile(filePath);
            var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
            var sub = item.Substring(0, 1);
            Console.WriteLine("Part 1: " + Part1.ToString());
            */
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            return BothParts(false);
        }

        private object Part2()
        {
            return BothParts(true);
        }

        private object BothParts(bool isPart2)
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var grid = new int[1000,1000];

            foreach (var line in lines)
            {
                var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);

                if (isPart2 || (nums[0] == nums[2] || nums[1] == nums[3]))
                {
                    var points = AdventLibrary.GridHelper.GetPointsBetweenStartAndEndInclusive(nums);
                    foreach (var point in points)
                    {
                        grid[point.Item1, point.Item2]++;
                    }
                }
            }
            return grid.Cast<int>().ToList().Count(x => x > 1);
        }
    }
}
