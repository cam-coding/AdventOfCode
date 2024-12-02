using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2024
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
            var grid = input.CharGrid;
            long total = 1000000;
			long count = 0;
            long number = input.Long;

            foreach (var line in input.LongLines)
            {
                var ascending = true;
                var descending = true;
                var valid = true;
                for (var i = 1; i < line.Count; i++)
                {
                    var diff = Math.Abs(line[i] - line[i - 1]);
                    if (diff < 1 || diff > 3)
                    {
                        valid = false;
                    }
                }
                if (!line.IsSorted())
                {
                    valid = false;
                }
                if (valid)
                {
                    count++;
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            foreach (var line in input.LongLines)
            {
                var valid = false;
                for (var i = 0; i < line.Count; i++)
                {
                    var except = line.Clone();
                    except.RemoveAt(i);
                    valid = valid || Method(except);
                }
                if (valid)
                {
                    count++;
                }
            }
            return count;
        }

        private bool Method(List<long> line)
        {
            var ascending = true;
            var descending = true;
            var valid = true;
            for (var i = 1; i < line.Count; i++)
            {
                var diff = Math.Abs(line[i] - line[i - 1]);
                if (diff < 1 || diff > 3)
                {
                    valid = false;
                }
                if (line[i] >= line[i - 1])
                {
                    descending = false;
                }
                if (line[i] <= line[i - 1])
                {
                    ascending = false;
                }
            }
            if (!ascending && !descending)
            {
                valid = false;
            }

            return valid;

        }

        private object Part3(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            foreach (var line in input.LongLines)
            {
                var ascending = 0;
                var descending = 0;
                var valid = true;
                var invalid = 0;
                for (var i = 1; i < line.Count; i++)
                {
                    var diff = Math.Abs(line[i] - line[i - 1]);
                    if (diff < 1 || diff > 3)
                    {
                        invalid++;
                    }
                    if (line[i] >= line[i - 1])
                    {
                        descending++;
                    }
                    if (line[i] <= line[i - 1])
                    {
                        ascending++;
                    }
                }
                if (invalid > 1)
                {
                    valid = false;
                }
                if (ascending < line.Count-1 && descending < line.Count - 1)
                {
                    valid = false;
                }
                if ((ascending == line.Count-1 || descending == line.Count-1) && invalid == 1)
                {
                    valid = false;
                }
                if (valid)
                {
                    count++;
                }
            }
            return count;
        }
    }
}