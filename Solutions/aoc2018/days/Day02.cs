using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2018
{
    public class Day02 : ISolver
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
            var lines = input.Lines;
            var two = 0;
            var three = 0;

            foreach (var line in lines)
            {
                var dict = new Dictionary<char, int>();
                foreach (var c in line)
                {
                    if (!dict.TryAdd(c, 1))
                    {
                        dict[c]++;
                    }
                }
                if (dict.Any(x => x.Value == 3))
                {
                    three++;
                }
                if (dict.Any(x => x.Value == 2))
                {
                    two++;
                }
            }
            return two * three;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;

            foreach (var line in lines)
            {
                for (var i = 0; i < lines.Count; i++)
                {
                    if (lines[i].Equals(line))
                    {
                        continue;
                    }

                    var diffs = 0;
                    var diffSpot = 0;

                    for (var j = 0; j < line.Length; j++)
                    {
                        if (lines[i][j] != line[j])
                        {
                            diffs++;
                            diffSpot = j;
                        }
                    }

                    if (diffs == 1)
                    {
                        return line.Remove(diffSpot, 1);
                    }
                }
            }
            return false;
        }
    }
}