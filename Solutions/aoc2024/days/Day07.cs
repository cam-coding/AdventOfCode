using AdventLibrary;
using AdventLibrary.Extensions;
using System.Collections.Generic;
using System.Linq;

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
            var longLines = input.LongLines;
            long total = 1000000;
			long count = 0;
            long number = input.Long;

            foreach (var listy in longLines)
            {
                var answer = listy[0];
                var nums1 = listy.GetWithout(0);

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
            var rightValue = nums.Last();
            var remainingValues = nums.GetWithout(nums.Count-1);
            var combinedAnswers = new List<long>();
            if (remainingValues.Count == 1)
            {
                combinedAnswers = remainingValues;
            }
            else
            {
                var ans1 = GetPossible(true, remainingValues);
                var ans2 = GetPossible(false, remainingValues);
                combinedAnswers = ans1.Concat(ans2).ToList();
            }
            var ans = new List<long>();

            foreach (var leftValue in combinedAnswers)
            {
                if (add)
                {
                    ans.Add(rightValue + leftValue);
                }
                else
                {
                    ans.Add(rightValue * leftValue);
                }
            }

            return ans.Distinct().ToList();
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var longLines = input.LongLines;
            long count = 0;

            foreach (var listy in longLines)
            {
                var answer = listy[0];
                var values = listy.GetWithout(0);

                var possible = false;

                var ans = new List<long>();
                var combinedAnswers = new List<long>();
                for (var j = 0; j < 3; j++)
                {
                    var answers = GetPossible2(j, values);
                    combinedAnswers = combinedAnswers.Concat(answers).ToList();
                }

                if (combinedAnswers.Any(x => x == answer))
                {
                    count = count + answer;
                }
            }
            return count;
        }
        private List<long> GetPossible2(int operation, List<long> nums)
        {
            if (nums.Count == 1)
            {
                return nums;
            }
            var rightValue = nums.Last();
            var remainingNumbers = nums.GetWithout(nums.Count - 1);
            var combinedAnswers = new List<long>();
            if (remainingNumbers.Count == 1)
            {
                combinedAnswers = remainingNumbers;
            }
            else
            {
                for (var j = 0; j < 3; j++)
                {
                    var answers = GetPossible2(j, remainingNumbers);
                    combinedAnswers = combinedAnswers.Concat(answers).ToList();
                }
            }
            var ans = new List<long>();

            foreach (var leftValue in combinedAnswers)
            {
                if (operation == 0)
                {
                    ans.Add(rightValue + leftValue);
                }
                else if (operation == 1)
                {
                    ans.Add(rightValue * leftValue);
                }
                else
                {
                    ans.Add(Stringy(leftValue, rightValue));
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