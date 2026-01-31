using AdventLibrary;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day14 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var line = lines[0];
            var count = 0;

            var listGrid = GridHelper.GenerateGrid(128, 128, 0);
            var grid = new GridObject<int>(listGrid);

            for (var i = 0; i < 128; i++)
            {
                var hasher = new KnotHasher(line + $"-{i}");
                var hex = hasher.GenerateHash();
                var binary = string.Empty;
                for (var k = 0; k < hex.Length; k += 2)
                {
                    binary += ConversionHelper.ConvertBaseToBase(16, 2, hex.Substring(k, 2));
                }
                for (var j = 0; j < binary.Length; j++)
                {
                    if (binary[j] == '1')
                    {
                        grid.Set(new GridLocation<int>(j, i), 1);
                        count++;
                    }
                }
            }

            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var line = lines[0];
            var count = 0;

            var listGrid = GridHelper.GenerateGrid(128, 128, 0);
            var grid = new GridObject<int>(listGrid);

            for (var i = 0; i < 128; i++)
            {
                var hasher = new KnotHasher(line + $"-{i}");
                var hex = hasher.GenerateHash();
                var binary = string.Empty;
                for (var k = 0; k < hex.Length; k++)
                {
                    binary += ConversionHelper.ConvertBaseToBase(16, 2, hex.Substring(k, 1)).PadLeft(4, '0');
                }
                for (var j = 0; j < binary.Length; j++)
                {
                    if (binary[j] == '1')
                    {
                        grid.Set(new GridLocation<int>(j, i), 1);
                        count++;
                    }
                }
            }

            var regions = GridObjectExtensions.GetRegions(grid);
            var validRegions = regions.Where(x => grid.Get(x[0]) == 1);
            return validRegions.Count();
        }
    }
}