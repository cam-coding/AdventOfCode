using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day04: ISolver
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
			
			foreach (var line in lines)
            {

                for (var i = 0; i < 10000000; i++)
                {
                    var current = line + i;
                    var result = HashHelper.GetMd5HashAsHexString(current);

                    if (result.StartsWith("00000"))
                    {
                        return i;
                    }
                }
			}
            return 0;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            foreach (var line in lines)
            {

                for (var i = 0; i < 10000000; i++)
                {
                    var current = line + i;
                    var result = HashHelper.GetMd5HashAsHexString(current);

                    if (result.StartsWith("000000"))
                    {
                        return i;
                    }
                }
            }
            return 0;
        }
    }
}