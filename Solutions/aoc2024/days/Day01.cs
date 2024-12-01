using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2024
{
    public class Day01: ISolver
    {
        private string _filePath;
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
			long count = 0;

            var list1 = input.LongLines.Select(x => x[0]).ToList();
            var list2 = input.LongLines.Select(x => x[1]).ToList();

            list1.Sort();
            list2.Sort();

            for (var i = 0; i < list1.Count; i++)
            {
                count += Math.Abs((int)list1[i] - (int)list2[i]);
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);

            var list1 = input.LongLines.Select(x => x[0]).ToList();
            var list2 = input.LongLines.Select(x => x[1]).ToList();

            return list1.Sum(x => list2.Count(z => z == x) * x);
        }
    }
}