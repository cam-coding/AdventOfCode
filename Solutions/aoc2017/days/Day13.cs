using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day13 : ISolver
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
            var longLines = input.LongLines;
            long count = 0;

            var dict = new Dictionary<long, long>();

            foreach (var line in longLines)
            {
                dict.Add(line[0], line[1]);
            }

            var max = dict.Keys.Max();

            for (var i = 0; i <= max; i++)
            {
                if (dict.ContainsKey(i))
                {
                    var pos = GetCurrentPosition(i, dict);
                    if (pos == 0)
                    {
                        count += i * dict[i];
                    }
                }
            }

            return count;
        }

        private long GetCurrentPosition(int i, Dictionary<long, long> dict, int offset = 0)
        {
            var depth = i;
            var range = dict[depth];
            var special = (range - 1) * 2;

            if ((i + offset) % special == 0)
            {
                return 0;
            }
            return 1;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var longLines = input.LongLines;
            long count = 0;

            var dict = new Dictionary<long, long>();

            foreach (var line in longLines)
            {
                dict.Add(line[0], line[1]);
            }

            var max = dict.Keys.Max();

            for (var j = 0; j < 10000000; j++)
            {
                var fail = false;
                for (var i = 0; i <= max; i++)
                {
                    if (dict.ContainsKey(i))
                    {
                        var pos = GetCurrentPosition(i, dict, j);
                        if (pos == 0)
                        {
                            fail = true;
                            i = (int)max + 1;
                        }
                    }
                }
                if (!fail)
                {
                    return j;
                }
            }

            return 0;
        }
    }
}