using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2022
{
    public class Day03 : ISolver
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
            var lines = input.Lines;
            long count = 0;

            foreach (var line in lines)
            {
                var bags = line.ToList().GetNSubLists(2);
                var letter = bags[0].First(x => bags[1].Contains(x));

                if (char.IsUpper(letter))
                {
                    count += 26 + (letter - '@');
                }
                else
                {
                    count += letter - '`';
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            long count = 0;

            for (var i = 0; i < lines.Count; i += 3)
            {
                var listy = new List<List<char>>() { lines[i].ToList(), lines[i + 1].ToList(), lines[i + 2].ToList() };
                var letter = listy.GetAllCommonItems().First();
                if (char.IsUpper(letter))
                {
                    count += 26 + (letter - '@');
                }
                else
                {
                    count += letter - '`';
                }
            }
            return count;
        }
    }
}