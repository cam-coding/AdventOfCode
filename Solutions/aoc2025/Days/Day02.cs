using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2025
{
    public class Day02 : ISolver
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
            var numbers = input.Longs;
            long count = 0;

            for (var i = 0; i < numbers.Count; i = i + 2)
            {
                var start = numbers[i];
                var end = numbers[i + 1];
                for (var j = start; j <= end; j++)
                {
                    var numAsString = j.ToString();
                    if (numAsString.Length % 2 != 0)
                    {
                        continue;
                    }

                    var halves = numAsString.SplitInHalf();
                    if (halves[0].Equals(halves[1]))
                    {
                        count += j;
                    }
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var numbers = input.Longs;
            long count = 0;

            for (var i = 0; i < numbers.Count; i = i + 2)
            {
                var start = numbers[i];
                var end = numbers[i + 1];
                for (var j = start; j <= end; j++)
                {
                    var numAsString = j.ToString();
                    for (var k = 1; k < (numAsString.Length / 2) + 1; k++)
                    {
                        if (numAsString.Length % k != 0)
                        {
                            continue;
                        }
                        var substring = numAsString.Substring(0, k);
                        var indexes = numAsString.GetIndexesOfSubstringNonOverlapping(substring);
                        if (indexes.Count == numAsString.Length / k)
                        {
                            count += j;
                            break;
                        }
                    }
                }
            }
            return count;
        }
    }
}