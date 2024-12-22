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
            var nodes = input.Graph;
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
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            for (var i = 0; i < lines.Count; i++)
            {
            }

            var myList = new List<List<long>>();
            var pricesList = new List<List<long>>();
            var myLookup = new List<Dictionary<(long a, long b, long c, long d), long>>();
            foreach (var line in numbers)
            {
                var num = line;
                long last = num % 10;
                long diff = 0;
                var currentList = new List<long>();
                var currentPrices = new List<long>();
                var tempLookup = new Dictionary<(long a, long b, long c, long d), long>();
                for (var i = 0; i < 2000; i++)
                {
                    num = AllSteps(num);
                    var banana = num % 10;
                    currentList.Add(banana - last);
                    currentPrices.Add(banana);
                    last = banana;

                    if (currentList.Count > 3)
                    {
                        var index = currentList.Count - 1;
                        var tempTuple = (
                            currentList[index - 3],
                            currentList[index - 2],
                            currentList[index - 1],
                            currentList[index]);
                        tempLookup.TryAdd(tempTuple, banana);
                    }
                }
                myList.Add(currentList);
                pricesList.Add(currentPrices);
                myLookup.Add(tempLookup);
                count += num;
            }

            var listy2 = new List<long>() { -2, 1, -1, 3 };
            var testy = GoTime(myList, pricesList, listy2);

            long best = 0;
            for (var i = -9; i < 10; i++)
            {
                for (var j = -9; j < 10; j++)
                {
                    for (var x = -9; x < 10; x++)
                    {
                        for (var y = -9; y < 10; y++)
                        {
                            long tot = 0;
                            foreach (var dict in myLookup)
                            {
                                var tempTuple = (i, j, x, y);
                                if (dict.ContainsKey(tempTuple))
                                {
                                    tot += dict[tempTuple];
                                }
                            }
                            best = Math.Max(best, tot);
                            /*
                            var listy = new List<long>() { i, j, x, y };
                            best = Math.Max(best, GoTime(myList, pricesList, listy));*/
                        }
                    }
                }
            }
            return best;
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