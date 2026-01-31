using AdventLibrary;

namespace aoc2021
{
    public class Day23 : ISolver
    {
        /*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
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
            var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var nodes = ParseInput.ParseFileAsGraph(_filePath);
            var grid = ParseInput.ParseFileAsGrid(_filePath);
            var total = 1000000;
            var counter = 0;

            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars);
                var nums = AdventLibrary.StringParsing.GetIntsFromString(line);

                foreach (var num in nums)
                {
                }

                for (var i = 0; i < 0; i++)
                {
                    for (var j = 0; j < 0; j++)
                    {

                    }
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