using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day11: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<long, List<long>> _dictCache = new Dictionary<long, List<long>>();
        private Dictionary<long, SortedDictionary<int,List<long>>> _dictCache2 = new Dictionary<long, SortedDictionary<int, List<long>>>();
        private Dictionary<long, SortedDictionary<int, long>> _dictCache3 = new Dictionary<long, SortedDictionary<int, long>>();
        private int _magic = 75;
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {/*
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
			var numbers = input.Longs;
            for (var i = 0; i < lines.Count; i++)
            {

            }

            var currentNumbers = numbers.Clone();

            for (var i = 0; i < 25; i++)
            {
                var newNumbers = new List<long>();
                foreach (var item in currentNumbers)
                {
                    var str = item.ToString();
                    if (item == 0)
                    {
                        newNumbers.Add(1);
                    }
                    else if (str.Length % 2 == 0)
                    {
                        var index = str.Length / 2;
                        var first = str.Substring(0, index);
                        var second = str.Substring(index);

                        newNumbers.Add(long.Parse(first));
                        newNumbers.Add(long.Parse(second));

                    }
                    else
                    {
                        newNumbers.Add(item * 2024);
                    }
                }
                currentNumbers = newNumbers.Clone();
            }
            return currentNumbers.Count;*/
            return 1;
        }

        private object Part2Old(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;

            var currentNumbers = numbers.Clone();


            for (var i = 0; i < 75; i++)
            {
                var newNumbers = new List<long>();
                foreach (var item in currentNumbers)
                {
                    var str = item.ToString();
                    if (item == 0)
                    {
                        newNumbers.Add(1);
                    }
                    else if (_dictCache.ContainsKey(item))
                    {
                        newNumbers.AddRange(_dictCache[item]);
                    }
                    else if (str.Length % 2 == 0)
                    {
                        var index = str.Length / 2;
                        var first = str.Substring(0, index);
                        var second = str.Substring(index);

                        var firstNum = long.Parse(first);
                        var secondNum = long.Parse(second);
                        newNumbers.Add(firstNum);
                        newNumbers.Add(secondNum);

                        _dictCache.TryAdd(item, new List<long>() { firstNum, secondNum });
                    }
                    else
                    {
                        newNumbers.Add(item * 2024);
                    }
                }
                currentNumbers = newNumbers.Clone();
            }
            return currentNumbers.Count;
        }
        private object Part2(bool isTest = false)
        {
            if (isTest)
            {
                return 1;
            }
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;


                var newNumbers = new List<long>();
            long count = 0;
                foreach (var item in numbers)
                {
                    count += Recursion2(item, 0);
                }
            return count;
        }

        private List<long> Recursion(long item, int level)
        {
            var diff = _magic - level;
            var newNumbers = new List<long>();
            var str = item.ToString();
            if (level == _magic)
            {
                newNumbers.Add(item);
                return newNumbers;
            }
            if (_dictCache2.ContainsKey(item))
            {
                if (_dictCache2[item].ContainsKey(diff))
                {
                    /*
                    var listy = _dictCache2[item][diff];
                    foreach (var thing in listy)
                    {
                        newNumbers.AddRange(Recursion(thing, level + 1));
                    }
                    if (_dictCache2.ContainsKey(item))
                    {
                        var dict = _dictCache2[item];
                        dict.TryAdd(diff, newNumbers);
                    }
                    else
                    {
                        var sDict = new SortedDictionary<int, List<long>>();
                        sDict.Add(diff, newNumbers);
                        _dictCache2.Add(item, sDict);
                    }
                    return newNumbers;*/
                    return _dictCache2[item][diff];
                }
                var derp = _dictCache2[item];
                var best = _dictCache2[item].Last().Key;
                if (best < diff)
                {
                    var newLevel = level + best;
                    var listy = _dictCache2[item][best];
                    foreach (var thing in listy)
                    {
                        newNumbers.AddRange(Recursion(thing, newLevel));
                    }

                    if (_dictCache2.ContainsKey(item))
                    {
                        var dict = _dictCache2[item];
                        dict.TryAdd(diff, newNumbers);
                    }
                    else
                    {
                        var sDict = new SortedDictionary<int, List<long>>();
                        sDict.Add(diff, newNumbers);
                        _dictCache2.Add(item, sDict);
                    }
                    return newNumbers;
                }
            }
            if (item == 0)
            {
                newNumbers.AddRange(Recursion(1, level + 1));
            }
            else if (str.Length % 2 == 0)
            {
                var index = str.Length / 2;
                var first = str.Substring(0, index);
                var second = str.Substring(index);

                var firstNum = long.Parse(first);
                var secondNum = long.Parse(second);
                var firstNums = Recursion(firstNum, level + 1);

                var secondNums = Recursion(secondNum, level + 1);
                newNumbers.AddRange(firstNums);
                newNumbers.AddRange(secondNums);
            }
            else
            {
                newNumbers.AddRange(Recursion(item * 2024, level + 1));
            }

            if (_dictCache2.ContainsKey(item))
            {
                var dict = _dictCache2[item];
                dict.TryAdd(diff, newNumbers);
            }
            else
            {
                var sDict = new SortedDictionary<int, List<long>>();
                sDict.Add(diff, newNumbers);
                _dictCache2.Add(item, sDict);
            }
            return newNumbers;
        }

        private long Recursion2(long item, int level)
        {
            var diff = _magic - level;
            long newCount = 0;
            var str = item.ToString();
            if (level == _magic)
            {
                return 1;
            }
            if (_dictCache3.ContainsKey(item))
            {
                if (_dictCache3[item].ContainsKey(diff))
                {
                    return _dictCache3[item][diff];
                }
            }
            if (item == 0)
            {
                newCount += Recursion2(1, level + 1);
            }
            else if (str.Length % 2 == 0)
            {
                var index = str.Length / 2;
                var first = str.Substring(0, index);
                var second = str.Substring(index);

                var firstNum = long.Parse(first);
                var secondNum = long.Parse(second);
                var firstNums = Recursion2(firstNum, level + 1);

                var secondNums = Recursion2(secondNum, level + 1);
                newCount = newCount + firstNums + secondNums;
            }
            else
            {
                newCount += Recursion2(item * 2024, level + 1);
            }

            if (_dictCache3.ContainsKey(item))
            {
                var dict2 = _dictCache3[item];
                dict2.TryAdd(diff, newCount);
            }
            else
            {
                var sDict2 = new SortedDictionary<int, long>();
                sDict2.Add(diff, newCount);
                _dictCache3.Add(item, sDict2);
            }
            return newCount;
        }
    }
}