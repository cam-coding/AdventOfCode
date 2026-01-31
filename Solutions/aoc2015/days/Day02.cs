using AdventLibrary;

namespace aoc2015
{
    public class Day02 : ISolver
    {
        private string _filePath;
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            long total = 0;

            foreach (var line in lines)
            {
                var nums = StringParsing.GetIntsFromString(line);
                nums.Sort();
                var min = nums[0] * nums[1];
                long current = 0;
                current = current + 2 * nums[0] * nums[1];
                current = current + 2 * nums[1] * nums[2];
                current = current + 2 * nums[0] * nums[2];
                current += min;

                total += current;
            }
            return total;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            long total = 0;

            foreach (var line in lines)
            {
                var nums = StringParsing.GetIntsFromString(line);
                nums.Sort();
                var perm = (nums[0] + nums[1]) * 2;
                var bow = nums[0] * nums[1] * nums[2];

                total += bow + perm;
            }
            return total;
        }
    }
}