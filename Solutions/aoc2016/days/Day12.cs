using AdventLibrary;

namespace aoc2016
{
    public class Day12 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var bunny = new AssemBunny(lines);
            bunny.RunInput();
            return bunny.Registers['a'];
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var bunny = new AssemBunny(lines);
            bunny.Registers['c'] = 1;
            bunny.RunInput();
            return bunny.Registers['a'];
        }
    }
}
