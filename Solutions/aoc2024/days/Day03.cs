using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2024
{
    public class Day03: ISolver
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
            var longLines = input.LongLines;
			var numbers = input.Longs;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            long total = 1000000;
			long count = 0;
            long number = input.Long;
            
            var ln1 = input.Text;

            var indexes = ln1.GetIndexesOfSubstring("mul");
            var commaIndexes = ln1.GetIndexesOfSubstring(",");
            var rightIndexes = ln1.GetIndexesOfSubstring(")");

            foreach (var index in indexes)
            {
                var next = index + 3;
                var c = ln1[next];
                if (c != '(')
                {
                    continue;
                }
                var commaIndex = commaIndexes.First(x => x > next);
                var rightIndex = rightIndexes.First(x => x > commaIndex);
                var length = commaIndex - next - 1;
                var num1String = ln1.Substring(next + 1, length);
                int num1 = 0;
                if (!Int32.TryParse(num1String, out num1))
                { continue; }
                var length2 = rightIndex - commaIndex - 1;
                var num2String = ln1.Substring(commaIndex + 1, length2);
                int num2 = 0;
                if (!Int32.TryParse(num2String, out num2))
                { continue; }
                count += num1 * num2;
            }

            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var longLines = input.LongLines;
            var numbers = input.Longs;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var ln1 = input.Text;

            var indexes = ln1.GetIndexesOfSubstring("mul");
            var commaIndexes = ln1.GetIndexesOfSubstring(",");
            var rightIndexes = ln1.GetIndexesOfSubstring(")");
            var doIndexes = ln1.GetIndexesOfSubstring("do()");
            var dontIndexes = ln1.GetIndexesOfSubstring("don't()");

            var last = 0;
            var enabled = true;
            foreach (var index in indexes)
            {
                var doI = doIndexes.LastOrDefault(x => x < index);
                var dontI = dontIndexes.LastOrDefault(x => x < index);
                if (doI > last && doI < index)
                {
                    enabled = true;
                }
                if (dontI > last && dontI < index)
                {
                    if (doI > last && doI < index)
                    {
                        if (dontI > doI)
                        {
                            enabled = false;
                        }
                    }
                    else
                    {
                        enabled = false;
                    }
                }
                if (enabled)
                {
                    var next = index + 3;
                    var c = ln1[next];
                    if (c != '(')
                    {
                        continue;
                    }
                    var commaIndex = commaIndexes.First(x => x > next);
                    var rightIndex = rightIndexes.First(x => x > commaIndex);
                    var length = commaIndex - next - 1;
                    var num1String = ln1.Substring(next + 1, length);
                    int num1 = 0;
                    if (!Int32.TryParse(num1String, out num1))
                    { continue; }
                    var length2 = rightIndex - commaIndex - 1;
                    var num2String = ln1.Substring(commaIndex + 1, length2);
                    int num2 = 0;
                    if (!Int32.TryParse(num2String, out num2))
                    { continue; }
                    count += num1 * num2;
                }
                last = index;
            }
            return count;
        }
    }
}