using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day07: ISolver
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
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
			long count = 0;
            long number = input.Long;

            var ln1 = lines != null && lines.Count > 0 ? lines[0] : string.Empty;
            var ln2 = lines != null && lines.Count > 1 ? lines[1] : string.Empty;
            for (var i = 0; i < lines.Count; i++)
            {

            }

            foreach (var listy in longLines)
            {
                var answer = listy[0];
                var nums1 = listy.GetWithout(0);

                var possible = false;

                var ans = new List<long>();
                var ans1 = GetPossible(true, nums1);
                var ans2 = GetPossible(false, nums1);

                var listy2 = ans1.Concat(ans2);

                if (listy2.Any(x => x == answer))
                {
                    count = count + answer;
                }
            }
            return count;
        }

        private List<long> GetPossible(bool add, List<long> nums)
        {
            if (nums.Count == 1)
            {
                return nums;
            }
            var num = nums.Last();
            var nums1 = nums.GetWithout(nums.Count-1);
            var ans1 = GetPossible(true, nums1);
            var ans2 = GetPossible(false, nums1);
            var ans = new List<long>();

            foreach (var i in ans1)
            {
                if (add)
                {
                    ans.Add(num + i);
                }
                else
                {
                    ans.Add(num * i);
                }
            }

            foreach (var i in ans2)
            {
                if (add)
                {
                    ans.Add(num + i);
                }
                else
                {
                    ans.Add(num * i);
                }
            }

            return ans.Distinct().ToList();
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var ln1 = lines != null && lines.Count > 0 ? lines[0] : string.Empty;
            var ln2 = lines != null && lines.Count > 1 ? lines[1] : string.Empty;
            for (var i = 0; i < lines.Count; i++)
            {

            }

            foreach (var listy in longLines)
            {
                var answer = listy[0];
                var nums1 = listy.GetWithout(0);

                var possible = false;

                var ans = new List<long>();
                var ans1 = GetPossible2(0, nums1);
                var ans2 = GetPossible2(1, nums1);
                var ans3 = GetPossible2(2, nums1);

                var listy2 = ans1.Concat(ans2).Concat(ans3);

                if (listy2.Any(x => x == answer))
                {
                    count = count + answer;
                }
            }
            return count;
        }
        private List<long> GetPossible2(int add, List<long> nums)
        {
            if (nums.Count == 1)
            {
                return nums;
            }
            var num = nums.Last();
            var nums1 = nums.GetWithout(nums.Count - 1);
            var ans1 = GetPossible2(0, nums1);
            var ans2 = GetPossible2(1, nums1);
            var ans3 = GetPossible2(2, nums1);
            var ans = new List<long>();

            foreach (var i in ans1)
            {
                if (add == 0)
                {
                    ans.Add(num + i);
                }
                else if (add == 1)
                {
                    ans.Add(num * i);
                }
                else
                {
                    ans.Add(Stringy(i, num));
                }
            }

            foreach (var i in ans2)
            {
                if (add == 0)
                {
                    ans.Add(num + i);
                }
                else if (add == 1)
                {
                    ans.Add(num * i);
                }
                else
                {
                    ans.Add(Stringy(i, num));
                }
            }

            foreach (var i in ans3)
            {
                if (add == 0)
                {
                    ans.Add(num + i);
                }
                else if (add == 1)
                {
                    ans.Add(num * i);
                }
                else
                {
                    ans.Add(Stringy(i, num));
                }
            }

            return ans.Distinct().ToList();
        }

        private long Stringy(long a, long b)
        {
            var str = a.ToString() + b.ToString();
            return long.Parse(str);
        }
    }
}