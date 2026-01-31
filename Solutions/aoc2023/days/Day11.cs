using AdventLibrary;

namespace aoc2023
{
    public class Day11 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            // every empty row/col is replaced with 2 aka add 1 row/col
            return CalculateDistancesTotal(2);
        }

        private object Part2()
        {
            // every empty row/col is replaced with 1000000 aka add 999999 row/col
            return CalculateDistancesTotal(1000000);
        }

        private long CalculateDistancesTotal(int galaxyExpansionFactor)
        {
            // went for repeating of loops instead of doing all the setup in 1 loop for
            // easier future understanding.
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var emptyRows = new HashSet<int>();
            var emptyColumns = new HashSet<int>();
            for (var j = 0; j < grid.Count; j++)
            {
                if (grid[j].All(x => x == '.'))
                {
                    emptyRows.Add(j);
                }
            }
            for (var j = 0; j < grid[0].Count; j++)
            {
                var boo = true;
                for (var i = 0; i < grid.Count; i++)
                {
                    if (grid[i][j] != '.')
                    {
                        boo = false;
                        break;
                    }
                }
                if (boo)
                {
                    emptyColumns.Add(j);
                }
            }
            var galaxies = new List<(int, int)>();

            long total = 0;
            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (grid[i][j] == '#')
                    {
                        galaxies.Add((i, j));
                    }
                }
            }
            for (var i = 0; i < galaxies.Count; i++)
            {
                for (var j = i + 1; j < galaxies.Count; j++)
                {
                    var dist = Math.Abs(galaxies[i].Item1 - galaxies[j].Item1) + Math.Abs(galaxies[i].Item2 - galaxies[j].Item2);
                    var sortedCol = new List<int>() { galaxies[i].Item2, galaxies[j].Item2 };
                    sortedCol.Sort();
                    var emptiesCol = emptyColumns.Count(x => x > sortedCol[0] && x < sortedCol[1]);
                    var sortedRow = new List<int>() { galaxies[i].Item1, galaxies[j].Item1 };
                    sortedRow.Sort();
                    var emptiesRow = emptyRows.Count(x => x > sortedRow[0] && x < sortedRow[1]);
                    total += dist + (galaxyExpansionFactor - 1) * emptiesCol + (galaxyExpansionFactor - 1) * emptiesRow;
                }
            }
            return total;
        }
    }
}