using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day05: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        List<Dictionary<(long, long), (long, long)>> _dicts = new List<Dictionary<(long, long), (long, long)>>();
        public Solution Solve(string filePath)
        {
            _filePath = filePath;

            var timer = new Stopwatch();
            timer.Start();
            var solution = new Solution(
                Part1(),
                timer.Elapsed,
                Part2(),
                timer.Elapsed);
            timer.Stop();
            return solution;
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var seeds = new List<long>();
            var dicts = new List<Dictionary<(long, long), (long, long)>>();

            var current = -1;
			foreach (var line in lines)
			{
                if (line.Equals(string.Empty))
                {
                    continue;
                }
                else if (line.Contains("seeds:"))
                {
                    seeds = AdventLibrary.StringParsing.GetLongNumbersFromString(line);
                }
                else
                {
                    if (line.Contains("map:"))
                    {
                        current++;
                        dicts.Add(new Dictionary<(long, long), (long,long)>());
                    }
                    else
                    {
                        var nums = AdventLibrary.StringParsing.GetLongNumbersFromString(line);
                        var i = nums[1];
                        dicts[current].Add((i, i + nums[2]-1), (nums[0], nums[0] + nums[2]-1));
                    }
                }
			}
            long best = int.MaxValue;
            foreach (var seed in seeds)
            {
                var i = 0;
                long currentVal = seed;
                while (i <= current)
                {
                    foreach (var item in dicts[i])
                    {
                        if (currentVal >= item.Key.Item1 && currentVal <= item.Key.Item2)
                        {
                            var diff = currentVal - item.Key.Item1;
                            currentVal = item.Value.Item1 + diff;
                            break;
                        }
                    }
                    i++;
                }
                if (currentVal < best)
                {
                    best = currentVal;
                }
            }
            return best;
        }

        private object Part2()
        {
            var timer = new Stopwatch();
            timer.Start();
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var seeds = new List<(long, long)>();

            var current = -1;
            foreach (var line in lines)
            {
                if (line.Equals(string.Empty))
                {
                    continue;
                }
                else if (line.Contains("seeds:"))
                {
                    var nums = AdventLibrary.StringParsing.GetLongNumbersFromString(line);
                    for (var i = 0; i < nums.Count; i+=2)
                    {
                        seeds.Add((nums[i], nums[i + 1]));
                    }
                }
                else
                {
                    if (line.Contains("map:"))
                    {
                        current++;
                        _dicts.Add(new Dictionary<(long, long), (long, long)>());
                    }
                    else
                    {
                        var nums = AdventLibrary.StringParsing.GetLongNumbersFromString(line);
                        var i = nums[1];
                        _dicts[current].Add((i, i + nums[2] - 1), (nums[0], nums[0] + nums[2] - 1));
                        /*
                        for (var j = 0; j < nums[2]; j++)
                        {
                        }*/
                    }
                }
            }
            var ranges = new List<(long, long)>();
            foreach (var pair in seeds)
            {
                ranges.Add((pair.Item1, pair.Item1 + pair.Item2-1));
            }
            var best = Recursive(ranges.ToHashSet(), 0);
            timer.Stop();
            Console.WriteLine(timer.ElapsedMilliseconds);
            return best;
        }

        private long Recursive(HashSet<(long, long)> ranges, int current)
        {
            if (current >= _dicts.Count)
            {
                var blah = ranges.Min(x => x.Item1);
                var better = ranges.Where(x => x.Item1 != 0);
                var blah2 = better.Min(x => x.Item1);
                var dicty = better.ToImmutableSortedSet();
                var dicty2 = ranges.ToImmutableSortedSet();
                return ranges.Min(x => x.Item1);
            }
            var newRanges = new HashSet<(long, long)>();
            var que = new Queue<(long, long)>(ranges);
            while (que.Count > 0)
            {
                var range = que.Dequeue();
                var remainderList = new HashSet<(long, long)>();
                var min = range.Item1;
                var max = range.Item2;
                var used = false;
                foreach (var item in _dicts[current])
                {
                    var itemMin = item.Key.Item1;
                    var itemMax = item.Key.Item2;
                    long miny = -2;
                    long maxy = -2;
                    if (min < itemMin && (max >= itemMin && max <= itemMax))
                    {
                        miny = itemMin;
                        maxy = max;
                    }
                    if (min >= itemMin && min <= itemMax)
                    {
                        miny = min;
                        if (max <= itemMax)
                        {
                            maxy = max;
                        }
                        else
                        {
                            maxy = itemMax;
                        }
                    }
                    if (miny != -2 && maxy != -2)
                    {
                        var diff = item.Key.Item1 - item.Value.Item1;
                        newRanges.Add((miny - diff, maxy - diff));
                        if (min != miny)
                        {
                            long val1 = Math.Min(min, miny);
                            long val2 = Math.Max(min, miny) - 1;
                            que.Enqueue((val1, val2));
                        }
                        if (max != maxy)
                        {
                            var val1 = Math.Min(max, maxy) + 1;
                            var val2 = Math.Max(max, maxy);
                            que.Enqueue((val1, val2));
                        }
                        used = true;
                        break;
                    }
                    else
                    {
                        if (miny == -2 && maxy == -2)
                        {
                            if (!remainderList.Contains((min,max)))
                            {
                                remainderList.Add((min,max));
                            }
                        }
                    }
                }
                if (!used)
                {
                    foreach (var item in remainderList)
                    {
                        newRanges.Add((item.Item1, item.Item2));
                    }
                }
            }
            return Recursive(newRanges, current + 1);
        }
    }
}