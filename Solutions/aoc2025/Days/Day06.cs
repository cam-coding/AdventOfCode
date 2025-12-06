using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2025
{
    public class Day06 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '=', '\t' };

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
            var lines = input.Lines;
            long count = 0;

            var nums2 = new List<List<long>>();

            var symbols = lines.PopLast().GetRealTokens(_delimiterChars).ToList();
            var cols = lines.Select(x => x.GetLongsFromString()).ToList().InvertList();

            for (var i = 0; i < symbols.Count; i++)
            {
                count += symbols[i].Contains("*") ?
                    cols[i].Multiply() :
                    cols[i].Sum();
            }

            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var grid = input.GridChar;
            long count = 0;

            // Visual Studio hates trailing spaces in this input
            var maxLength = lines.Max(x => x.Length);
            lines = lines.Select(x => x.PadRight(maxLength, ' ')).ToList();

            var cols = new GridObject<char>(lines.To2dList()).GetColumns();
            var colStrings = cols.Select(cols => cols.Stringify("")).ToList();
            var currentNums = new List<long>();

            for (var i = cols.Count - 1; i >= 0; i--)
            {
                var current = colStrings[i];
                currentNums.Add(current.GetLongsFromString()[0]);

                if (current.Contains("*") || current.Contains("+"))
                {
                    count += current.Contains("*") ?
                        currentNums.Multiply() :
                        currentNums.Sum();
                    currentNums.Clear();

                    // skip the blank column
                    i--;
                }
            }

            return count;
        }
    }
}