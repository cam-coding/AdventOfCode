using AdventLibrary;
using AdventLibrary.Extensions;
using System.Collections.Generic;

namespace aoc2024
{
    public class Day05: ISolver
    {
        private string _filePath;
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
			long count = 0;

            var rules = new List<(int a, int b)>();
            var groups = input.LineGroupsSeperatedByWhiteSpace;

            foreach (var line in groups[0])
            {
                var nums = StringParsing.GetIntsFromString(line);
                rules.Add((nums[0], nums[1]));
            }

            foreach (var line in groups[1])
            {
                var nums = StringParsing.GetIntsFromString(line);
                var valid = true;
                for (var j = 0; j < nums.Count; j++)
                {
                    foreach (var rule in rules)
                    {
                        if (nums[j] == rule.a)
                        {
                            var ind = nums.IndexOf(rule.b);
                            if (ind != -1 && ind < j)
                            {
                                valid = false;
                                break;
                            }
                        }
                    }
                }
                if (valid)
                {
                    count += nums.GetMiddleItem();
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            long count = 0;

            var rules = new List<(int a, int b)>();
            var groups = input.LineGroupsSeperatedByWhiteSpace;

            foreach (var line in groups[0])
            {
                var nums = StringParsing.GetIntsFromString(line);
                rules.Add((nums[0], nums[1]));
            }

            foreach (var line in groups[1])
            {
                {
                    var nums = StringParsing.GetIntsFromString(line);
                    var everInvalid = false;
                    while (true)
                    {
                        var valid = true;
                        for (var j = 0; j < nums.Count; j++)
                        {
                            foreach (var rule in rules)
                            {
                                if (nums[j] == rule.a)
                                {
                                    var ind = nums.IndexOf(rule.b);
                                    if (ind != -1 && ind < j)
                                    {
                                        valid = false;
                                        everInvalid = true;
                                        var tmp = nums[j];
                                        nums[j] = nums[ind];
                                        nums[ind] = tmp;
                                        break;
                                    }
                                }
                            }
                        }
                        if (valid)
                        {
                            break;
                        }
                    }
                    if (everInvalid)
                    {
                        count += nums.GetMiddleItem();
                    }
                }
            }
            return count;
        }
    }
}