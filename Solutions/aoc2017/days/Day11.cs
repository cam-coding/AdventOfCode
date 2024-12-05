using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day11: ISolver
    {
        private string _filePath;
        private Dictionary<string, (int x, int y, int z)> _dict = new Dictionary<string, (int x, int y, int z)>()
        {
            { "nw", (-1, 0, 1)},
            { "n", (0, -1, 1)},
            { "ne", (1, -1, 0)},
            { "se", (1, 0, -1)},
            { "s", (0, 1, -1)},
            { "sw", (-1, 1, 0)},

        };
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            (int x, int y, int z) current = (0,0,0);
            foreach (var tok in input.Tokens)
            {
                var val = _dict[tok];
                current = (current.x + val.x, current.y + val.y, current.z + val.z);
            }
            return (Math.Abs(current.x) + Math.Abs(current.y) + Math.Abs(current.z)) / 2;
            // return Math.Max(Math.Max(Math.Abs(current.x), Math.Abs(current.y)), Math.Abs(current.z));
        }

        private object Part2(bool isTest = false)
        {
            var max = 0;
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            (int x, int y, int z) current = (0, 0, 0);
            foreach (var tok in input.Tokens)
            {
                var val = _dict[tok];
                current = (current.x + val.x, current.y + val.y, current.z + val.z);
                max = Math.Max(max, Math.Max(Math.Max(Math.Abs(current.x), Math.Abs(current.y)), Math.Abs(current.z)));
            }

            return max;
        }
    }
}