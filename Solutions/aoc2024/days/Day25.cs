using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2024
{
    public class Day25 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var keys = new List<List<int>>();
            var locks = new List<List<int>>();

            foreach (var group in input.LineGroupsSeperatedByWhiteSpace)
            {
                var line1 = group[0];
                var searchChar = group[group.Count - 1][0];
                var isLock = line1[0] == '#';

                var important = group.Skip(1).ToList();
                important.Remove(important.Last());
                var grid = important.Select(x => x.ToList()).ToList();
                var columns = GridHelper.GetColumns(grid);
                var counts = columns.Select(x => x.Count(y => y == searchChar)).ToList();
                if (isLock)
                {
                    locks.Add(counts);
                }
                else
                {
                    keys.Add(counts);
                }
            }

            return keys.Sum(key => locks.Count(lok =>
            key.Zip(lok, (toothHeight, gap) => (toothHeight, gap)).All(x => x.toothHeight <= x.gap)));
        }

        private object Part2(bool isTest = false)
        {
            return "50 Stars woohoo!";
        }
    }
}