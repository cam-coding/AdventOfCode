using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2025
{
    public class Day05 : ISolver
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

            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var listy = new List<(long, long)>();

            foreach (var line in groups[0])
            {
                var nums = line.GetLongsFromString();
                listy.Add((nums[0], nums[1]));
            }

            foreach (var line in groups[1])
            {
                var nums = line.GetLongsFromString();
                if (listy.Any(x => nums[0] >= x.Item1 && nums[0] <= x.Item2))
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

            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var listy = new List<(long, long)>();

            foreach (var line in groups[0])
            {
                var nums = line.GetLongsFromString();
                listy.Add((nums[0], nums[1]));
            }

            var last = 0;

            while (last != listy.Count)
            {
                last = listy.Count;
                for (var i = 0; i < listy.Count; i++)
                {
                    var nums = new List<long>() { listy[i].Item1, listy[i].Item2 };
                    var matching = listy.Where(x => nums[0] < x.Item1 && nums[1] >= x.Item1).ToList();
                    var matching2 = listy.Where(x => nums[0] <= x.Item2 && nums[1] > x.Item2).ToList();
                    if (matching.Count() > 0)
                    {
                        var temp = matching[0];
                        var index = listy.IndexOf(matching[0]);
                        temp.Item1 = nums[0];

                        if (nums[1] >= temp.Item2)
                        {
                            temp.Item2 = nums[1];
                        }
                        listy[index] = temp;
                        listy.RemoveAt(i);
                        i--;
                    }
                    else if (matching2.Count() > 0)
                    {
                        var temp = matching2[0];
                        var index = listy.IndexOf(matching2[0]);
                        temp.Item2 = nums[1];

                        if (nums[0] <= temp.Item1)
                        {
                            temp.Item1 = nums[0];
                        }
                        listy[index] = temp;
                        listy.RemoveAt(i);
                        i--;
                    }
                    /*
                    for (var j = 0; j < listy.Count; j++)
                    {
                        if (i == j) continue;
                    }*/
                }
            }
            /*
            foreach (var line in groups[0])
            {
                var nums = line.GetLongsFromString();
                var matching = listy.Where(x => nums[0] < x.Item1 && nums[1] >= x.Item1).ToList();
                var matching2 = listy.Where(x => nums[0] <= x.Item2 && nums[1] >= x.Item2).ToList();
                if (matching.Count() > 0)
                {
                    var temp = matching[0];
                    var index = listy.IndexOf(matching[0]);
                    temp.Item1 = nums[0];

                    if (nums[1] >= temp.Item2)
                    {
                        temp.Item2 = nums[1];
                    }
                    listy[index] = temp;
                }
                else if (matching2.Count() > 0)
                {
                    var temp = matching2[0];
                    var index = listy.IndexOf(matching2[0]);
                    temp.Item2 = nums[1];

                    if (nums[0] <= temp.Item1)
                    {
                        temp.Item1 = nums[0];
                    }
                    listy[index] = temp;
                }
                else
                {
                    listy.Add((nums[0], nums[1]));
                }
            }*/

            for (var i = 0; i < listy.Count; i++)
            {
                for (var j = i + 1; j < listy.Count; j++)
                {
                    if (i == j) continue;

                    var iNums = listy[i];
                    var jNums = listy[j];

                    if ((iNums.Item1 >= jNums.Item1 && iNums.Item1 <= jNums.Item2)
                        || iNums.Item2 >= jNums.Item1 && iNums.Item2 <= jNums.Item2)
                    {
                        Console.WriteLine("BAD");
                        listy.RemoveAt(j);
                        j--;
                    }
                }
            }

            foreach (var item in listy)
            {
                count += (item.Item2 - item.Item1) + 1;
            }
            return count;
        }
    }
}