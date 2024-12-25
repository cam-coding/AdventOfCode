using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day22 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            for (var i = 0; i < lines.Count; i++)
            {
            }

            foreach (var line in numbers)
            {
                var num = line;
                for (var i = 0; i < 2000; i++)
                {
                    num = AllSteps(num);
                }
                count += num;
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            long mostBananasPossible = 0;

            var tempLookup = new Dictionary<(long a, long b, long c, long d), long>();
            foreach (var myLong in input.Longs)
            {
                var hashy = new HashSet<(long a, long b, long c, long d)>();
                var num = myLong;
                long lastDigit = num % 10;
                var diffsList = new List<long>();
                for (var i = 0; i < 2000; i++)
                {
                    num = AllSteps(num);
                    var banana = num % 10;
                    diffsList.Add(banana - lastDigit);
                    lastDigit = banana;

                    if (diffsList.Count > 3)
                    {
                        var index = diffsList.Count - 1;
                        var tempTuple = (
                            diffsList[index - 3],
                            diffsList[index - 2],
                            diffsList[index - 1],
                            diffsList[index]);

                        if (!hashy.Contains(tempTuple))
                        {
                            if (tempLookup.ContainsKey(tempTuple))
                            {
                                tempLookup[tempTuple] += banana;
                            }
                            else
                            {
                                tempLookup.Add(tempTuple, banana);
                            }
                            mostBananasPossible = Math.Max(mostBananasPossible, tempLookup[tempTuple]);
                        }

                        hashy.Add(tempTuple);
                    }
                }
            }

            return mostBananasPossible;
        }

        private long GoTime(List<List<long>> diffs, List<List<long>> prices, List<long> seq)
        {
            long total = 0;
            for (var i = 0; i < diffs.Count; i++)
            {
                for (var j = 0; j < diffs[i].Count - 3; j++)
                {
                    if (diffs[i][j] == seq[0] &&
                        diffs[i][j + 1] == seq[1] &&
                        diffs[i][j + 2] == seq[2] &&
                        diffs[i][j + 3] == seq[3])
                    {
                        total += prices[i][j + 3];
                        break;
                    }
                }
            }

            if (total > 0)
            {
                return total;
            }

            return total;
        }

        private long AllSteps(long number)
        {
            var step1 = Step1(number);
            var step2 = Step2(step1);
            var step3 = Step3(step2);
            return step3;
        }

        private long Step1(long number)
        {
            var blah = number * 64;
            var next = Mix(number, blah);
            return Prune(next);
        }

        private long Step2(long number)
        {
            var blah = number / 32;
            var next = Mix(number, blah);
            return Prune(next);
        }

        private long Step3(long number)
        {
            var blah = number * 2048;
            var next = Mix(number, blah);
            return Prune(next);
        }

        private long Mix(long secret, long val)
        {
            return BitwiseHelper.XOR(secret, val);
        }

        private long Prune(long secret)
        {
            return secret % 16777216;
        }
    }
}