using AdventLibrary;

namespace aoc2021
{
    public class Day07 : ISolver
    {
        /*
		var sub = item.Substring(0, 1);
		Console.WriteLine("Part 1: " + Part1.ToString());
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
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var total = 1000000;

            foreach (var line in lines)
            {
                var nums = AdventLibrary.StringParsing.GetIntsFromString(line);

                for (var i = 0; i < nums.Max(); i++)
                {
                    var tot = 0;
                    foreach (var num in nums)
                    {
                        tot = tot + Math.Abs(num - i);
                    }
                    if (tot < total)
                    {
                        total = tot;
                    }
                }
            }
            return total;
        }



        private object Part2()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var total = 1000000000;

            foreach (var line in lines)
            {
                var nums = AdventLibrary.StringParsing.GetIntsFromString(line);

                for (var i = nums.Min(); i < nums.Max(); i++)
                {
                    var tot = 0;
                    foreach (var num in nums)
                    {
                        var abs = Math.Abs(num - i);
                        tot = tot + (abs * (abs + 1)) / 2;
                    }
                    if (tot < total)
                    {
                        total = tot;
                    }
                }
            }
            return total;
        }
    }
}