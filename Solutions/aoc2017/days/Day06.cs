using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2017
{
    public class Day06: ISolver
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
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            long total = 1000000;
			long count = 0;
            long number = input.Long;

            var dict = DictionaryHelper.CreateIndexDictionary(numbers);
            var history = new HashSet<string>();
            var currentHash = HashSetHelper.HashObjectSorted(dict);

            while (!history.Contains(currentHash))
            {
                history.Add(currentHash);

                var index = GetMax(dict);
                var rebalance = dict[index];
                dict[index] = 0;

                while (rebalance > 0)
                {
                    index = GetNext(index, dict);
                    dict[index]++;
                    rebalance--;
                }

                count++;
                currentHash = HashSetHelper.HashObjectSorted(dict);
            }

            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var dict = DictionaryHelper.CreateIndexDictionary(numbers);
            var history = new Dictionary<string, long>();
            var currentHash = HashSetHelper.HashObjectSorted(dict);

            while (!history.Keys.Contains(currentHash))
            {
                history.Add(currentHash, count);

                var index = GetMax(dict);
                var rebalance = dict[index];
                dict[index] = 0;

                while (rebalance > 0)
                {
                    index = GetNext(index, dict);
                    dict[index]++;
                    rebalance--;
                }

                count++;
                currentHash = HashSetHelper.HashObjectSorted(dict);
            }

            return count- history[currentHash];
        }

        private int GetNext(int current, Dictionary<int, long> dict)
        {
            if (current == dict.Count-1)
            {
                return 0;
            }
            return current + 1;
        }

        private int GetMax(Dictionary<int, long> dict)
        {
            (int Index, long Value) max = (int.MaxValue, long.MinValue);
            foreach (var item in dict)
            {
                if (item.Value >= max.Value)
                {
                    if (item.Value > max.Value || item.Key < max.Index)
                    {
                        max = (item.Key, item.Value);
                    }
                }
            }

            return max.Index;
        }
    }
}