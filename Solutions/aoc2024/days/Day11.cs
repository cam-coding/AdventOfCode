using System.Collections.Generic;
using AdventLibrary;

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
            var str = item.ToString();
            if (level == _blinks)
            {
                return 1;
            }
            long cachedValue;
            if (_dictCache3.TryGetValue((item, diff), out cachedValue))
            {
                return cachedValue;
            }
            if (item == 0)
            {
                newCount += Recursion(1, level + 1);
            }
            else if (str.Length % 2 == 0)
            {
                var index = str.Length / 2;
                var first = str.Substring(0, index);
                var second = str.Substring(index);

                var firstNum = long.Parse(first);
                var firstNums = Recursion(firstNum, level + 1);
                var secondNum = long.Parse(second);
                var secondNums = Recursion(secondNum, level + 1);

                newCount = newCount + firstNums + secondNums;
            }
            else
            {
                newCount += Recursion(item * 2024, level + 1);
            }

            _dictCache3.TryAdd((item, diff), newCount);
            return newCount;
        }
    }
}