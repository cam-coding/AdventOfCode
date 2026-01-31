using AdventLibrary;

namespace aoc2016
{
    public class Day23 : ISolver
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
            var bunny = new AssemBunny(lines);
            bunny.Registers['a'] = 7;
            bunny.RunInput();
            return bunny.Registers['a'];
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var bunny = new AssemBunny(lines);
            bunny.Registers['a'] = 12;
            bunny.RunInput();
            return bunny.Registers['a'];
        }
    }
}