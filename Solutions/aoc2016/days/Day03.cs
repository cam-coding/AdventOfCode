using AdventLibrary;

namespace aoc2016
{
    public class Day03 : ISolver
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
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var count = 0;

            var nums = lines.Select(x => AdventLibrary.StringParsing.GetIntsFromString(x)).ToList();

            for (var i = 0; i < nums.Count() - 2; i = i + 3)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (nums[i][j] + nums[i + 1][j] > nums[i + 2][j] &&
                    nums[i + 1][j] + nums[i + 2][j] > nums[i][j] &&
                    nums[i + 2][j] + nums[i][j] > nums[i + 1][j])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var count = 0;

            var nums = lines.Select(x => AdventLibrary.StringParsing.GetIntsFromString(x)).ToList();

            for (var i = 0; i < nums.Count() - 2; i = i + 3)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (nums[i][j] + nums[i + 1][j] > nums[i + 2][j] &&
                    nums[i + 1][j] + nums[i + 2][j] > nums[i][j] &&
                    nums[i + 2][j] + nums[i][j] > nums[i + 1][j])
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
