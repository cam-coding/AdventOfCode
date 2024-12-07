using AdventLibrary;
using AdventLibrary.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace aoc2024
{
    public class Day07: ISolver
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
            var longLines = input.LongLines;
            long count = 0;

            foreach (var longs in longLines)
            {
                var answer = longs.PopFirst();

                var possible = new List<long>() { longs.PopFirst() };

                while (longs.Count > 0)
                {
                    var next = longs.PopFirst();
                    var nextPossible = new List<long>();
                    foreach (var current in possible)
                    {
                        nextPossible.Add(current + next);
                        nextPossible.Add(current * next);
                    }
                    possible = nextPossible;
                }

                if (possible.Any(x => x == answer))
                {
                    count = count + answer;
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var longLines = input.LongLines;
            long count = 0;

            foreach (var longs in longLines)
            {
                var answer = longs.PopFirst();

                var possible = new List<long>() { longs.PopFirst() };

                while (longs.Count > 0)
                {
                    var next = longs.PopFirst();
                    var nextPossible = new List<long>();
                    foreach (var current in possible)
                    {
                        nextPossible.Add(current + next);
                        nextPossible.Add(current * next);
                        nextPossible.Add(Stringy(current, next));
                    }
                    possible = nextPossible;
                }

                if (possible.Any(x => x == answer))
                {
                    count = count + answer;
                }
            }
            return count;
        }

        private long Stringy(long a, long b)
        {
            return long.Parse($"{a}{b}");
        }
    }
}