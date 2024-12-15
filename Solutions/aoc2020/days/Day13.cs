using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2020
{
    public class Day13: ISolver
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
            var lines = input.Lines;
			var numbers = input.Longs;
            var time = numbers[0];
            var buses = numbers.GetWithout(0);

            var best = long.MaxValue;
            var bestBus = long.MaxValue;

            foreach (var bus in buses)
            {
                var blah = time / bus;
                var real = (blah + 1) * bus;
                if (real - time < best)
                {
                    bestBus = bus;
                    best = real - time;
                }
            }
            return best*bestBus;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var tokens = lines[1].Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            var buses = new List<int>();
            var rems = new List<int>();

            var index = 0;
            var last = 0;
            foreach (var tok in tokens)
            {
                long res;
                if (long.TryParse(tok, out res))
                {
                    buses.Add((int)res);
                    rems.Add(index);
                    last = index;
                }
                index++;
            }

            var blah = MathHelper.ChineseRemainderTheorem(new List<long>() { 3,4,5}, new List<long>() { 2,3,1});
            var blah2 = MathHelper.ChineseRemainderTheorem(new List<long>() { 2, 1, 7 }, new List<long>() { 3, 4, 11 });
            var blah3 = MathHelper.Evaluate(new List<int>() { 10, 13, 7, 11 }, new List<int>() { 4,6,4,2 }, 4);
            return MathHelper.Evaluate(buses, rems, rems.Count);
        }
    }
}