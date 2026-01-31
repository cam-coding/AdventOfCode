using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2021
{
    public class Day06 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(BothParts(80), BothParts(256));
        }

        private object BothParts(int n)
        {
            var counts = new long[9].ToList();
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var nums = AdventLibrary.StringParsing.GetIntsFromString(lines.First());

            foreach (var num in nums)
            {
                counts[num]++;
            }

            for (var i = 0; i < n; i++)
            {
                counts = counts.RotateListLeft(1);
                counts[6] = counts[6] + counts[8];
            }
            return counts.Sum();
        }
    }
}