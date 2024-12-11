using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2017
{
    public class Day02: ISolver
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
            var nodes = input.Graph;
            var grid = input.GridChar;
            var total = 1000000;
			long count = 0;

            foreach (var ln in input.LongLines)
            {
                long min = long.MaxValue;
                long max = -1;
                foreach (var dig in ln)
                {
                    min = Math.Min(min, dig);
                    max = Math.Max(max, dig);
                }
                count += (max - min);
            }

            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var total = 1000000;
            long count = 0;

            foreach (var ln in input.LongLines)
            {
                count += Find(ln);
            }

            return count;
        }

        private long Find(List<long> ln)
        {
            var size = ln.Count;
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    if (j != i)
                    {
                        if (ln[i] % ln[j] == 0)
                        {
                            return (ln[i] / ln[j]);
                        }
                    }
                }
            }
            return -1;
        }
    }
}