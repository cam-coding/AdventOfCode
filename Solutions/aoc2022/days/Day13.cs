using AdventLibrary;
using AdventLibrary.Helpers.Grids;

namespace aoc2022
{
    public class Day13 : ISolver
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
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            for (var i = 0; i < lines.Count; i++)
            {

            }

            foreach (var line in lines)
            {
                for (var i = 0; i < 0; i++)
                {
                    for (var j = 0; j < 0; j++)
                    {

                    }
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            return 0;
        }
    }
}