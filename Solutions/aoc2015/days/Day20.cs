using AdventLibrary;

namespace aoc2015
{
    public class Day20 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var input = numbers[0];

            for (var i = 1000000; i < input / 10; i++)
            {
                var total = 0;
                for (var j = 1; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        total += j * 10;
                    }
                }

                if (total >= input)
                {
                    return total;
                }
            }
            return 0;
        }

        private object Part2()
        {
            return 0;
        }
    }
}