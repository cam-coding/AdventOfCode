using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day09: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
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
                var listy = new Dictionary<int, List<int>>();
                var numbers = StringParsing.GetNumbersWithNegativesFromString(line);
                var county = 0;
                listy.Add(0, numbers);
                county++;

                var diffs = GetDiffs(numbers);

                while (diffs.Any(x => x != 0))
                {
                    listy.Add(county, diffs);
                    diffs = GetDiffs(diffs);
                    county++;
                }

                for (var j = listy.Count-2; j >= 0; j--)
                {
                    var last = listy[j].Last();
                    listy[j].Add(last + listy[j + 1].Last());
                }

                counter += listy[0].Last();
            }
            return counter;
        }
        private List<int> GetDiffs(List<int> nums)
        {
            var listy = new List<int>();
            for (var j = 1; j < nums.Count; j++)
            {
                listy.Add(nums[j] - nums[j - 1]);
            }
            return listy;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var counter = 0;

            foreach (var line in lines)
            {
                var listy = new Dictionary<int, List<int>>();
                var numbers = StringParsing.GetNumbersWithNegativesFromString(line);
                var county = 0;
                listy.Add(0, numbers);
                county++;

                var diffs = GetDiffs(numbers);

                while (diffs.Any(x => x != 0))
                {
                    listy.Add(county, diffs);
                    diffs = GetDiffs(diffs);
                    county++;
                }
                diffs.Add(0);
                listy.Add(county, diffs);

                for (var j = listy.Count - 2; j >= 0; j--)
                {
                    var first = listy[j].First();
                    listy[j].Insert(0, first - listy[j + 1].First());
                }

                counter += listy[0].First();
            }
            return counter;
        }
    }
}