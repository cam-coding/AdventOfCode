using System;
using System.Collections.Generic;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2024
{
    public class Day11 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<(long, int), long> _dictCache3 = new Dictionary<(long, int), long>();
        private int _blinksPt2 = 75;
        private int _blinksPt1 = 25;
        private int _blinks = 0;

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
            return Part(_blinksPt1);
        }

        private object Part2(bool isTest = false)
        {
            return Part(_blinksPt2);
        }

        private object Part(int blinks)
        {
            var input = new InputObjectCollection(_filePath);
            var numbers = input.Longs;

            long count = 0;
            _blinks = blinks;
            foreach (var item in numbers)
            {
                count += Recursion(item, 0);
            }
            return count;
        }

        private long Recursion(long item, int level)
        {
            var diff = _blinks - level;
            long newCount = 0;
            if (level == _blinks)
            {
                return 1;
            }
            long cachedValue;

            // If we've already seen this number from this "height" we know
            // how many stones it creates and can just return that answer instead of doing the work.
            if (_dictCache3.TryGetValue((item, diff), out cachedValue))
            {
                return cachedValue;
            }

            var digitsLength = Math.Floor(Math.Log10(item)) + 1;
            if (item == 0)
            {
                newCount += Recursion(1, level + 1);
            }
            else if (digitsLength % 2 == 0)
            {
                var firstNum = Math.Floor(item / Math.Pow(10, digitsLength / 2));
                var firstNums = Recursion((long)firstNum, level + 1);
                var secondNum = item % Math.Pow(10, digitsLength / 2);
                var secondNums = Recursion((long)secondNum, level + 1);

                newCount = newCount + firstNums + secondNums;
            }
            else
            {
                newCount += Recursion(item * 2024, level + 1);
            }

            // save whatever work we've done for future lookups
            _dictCache3.TryAdd((item, diff), newCount);
            return newCount;
        }
    }
}