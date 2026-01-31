using AdventLibrary;

namespace aoc2017
{
    public class Day05 : ISolver
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
            var numbers = input.LongsWithNegatives;
            long count = 0;

            int index = 0;

            while (index >= 0 && index < numbers.Count)
            {
                int newIndex = index + (int)numbers[index];
                numbers[index]++;
                index = newIndex;
                count++;
            }

            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var numbers = input.LongsWithNegatives;
            long count = 0;

            int index = 0;

            while (index >= 0 && index < numbers.Count)
            {
                int newIndex = index + (int)numbers[index];

                if (numbers[index] >= 3)
                {
                    numbers[index]--;
                }
                else
                {
                    numbers[index]++;
                }
                index = newIndex;
                count++;
            }

            return count;
        }
    }
}