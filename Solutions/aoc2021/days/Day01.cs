using AdventLibrary;

namespace aoc2021
{
    public class Day01 : ISolver
    {
        public Solution Solve(string filePath, bool isTest = false)
        {
            return new Solution(Part1(filePath), Part2(filePath));
        }

        private object Part1(string filePath)
        {
            var increased = 0;
            var previous = int.MaxValue;
            var numbers = AdventLibrary.ParseInput.GetNumbersFromFile(filePath);
            foreach (var num in numbers)
            {
                if (num > previous)
                {
                    increased++;
                }
                previous = num;
            }

            return increased;
        }

        private int totalGroup(int start, int end, int[] numbers)
        {
            var total = 0;
            for (var i = start; i <= end; i++)
            {
                total += numbers[i];
            }
            return total;
        }

        private object Part2(string filePath)
        {
            var increased = 0;
            var numbers = AdventLibrary.ParseInput.GetNumbersFromFile(filePath);
            var groups = new int[numbers.Count];
            var i = 0;
            foreach (var num in numbers)
            {
                groups[i] = num;
                if (i > 2)
                {
                    if (totalGroup(i - 2, i, groups) > totalGroup(i - 3, i - 1, groups))
                    {
                        increased++;
                    }
                }
                i++;
            }

            return increased;
        }
    }
}
