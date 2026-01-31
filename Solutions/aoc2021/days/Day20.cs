using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2021
{
    public class Day20 : ISolver
    {
        /*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
        private string _filePath;
        private string _answerKey;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            _answerKey = lines[0];
            lines = lines.GetAllExceptFirstItem();
            var grid = ParseLinesAsGrid(lines);

            for (var i = 0; i < 50; i++)
            {
                var blah = i % 2 == 1;
                grid = ShiftGrid(grid, blah && _answerKey[0].Equals('#'));
                grid = ShiftGrid(grid, blah && _answerKey[0].Equals('#'));
                grid = ShiftGrid(grid, blah && _answerKey[0].Equals('#'));
                grid = EnhanceGrid(grid);
            }
            /*
            grid = ShiftGrid(grid, false);
            grid = ShiftGrid(grid, false);
            grid = ShiftGrid(grid, false);
            grid = EnhanceGrid(grid);
            // PrintGrid(grid);
            grid = ShiftGrid(grid, _answerKey[0].Equals('#'));
            grid = ShiftGrid(grid, _answerKey[0].Equals('#'));
            grid = ShiftGrid(grid, _answerKey[0].Equals('#'));
            grid = EnhanceGrid(grid);*/
            // PrintGrid(grid);
            return Count(grid);
        }

        private object Part2()
        {
            return 0;
        }

        public void PrintGrid(List<List<bool>> grid)
        {
            foreach (var line in grid)
            {
                foreach (var spot in line)
                {
                    if (spot)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        public int Count(List<List<bool>> grid)
        {
            var count = 0;
            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (grid[i][j])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        // This could probably be generic
        public List<List<bool>> ParseLinesAsGrid(List<string> lines)
        {
            var grid = new List<List<bool>>();
            for (var i = 0; i < lines.Count; i++)
            {
                grid.Add(new List<bool>());
                var line = lines[i];
                for (var j = 0; j < line.Length; j++)
                {
                    var mybool = line[j].Equals('#');
                    grid[i].Add(mybool);
                }
            }
            return grid;
        }

        public List<List<bool>> ShiftGrid(List<List<bool>> currentGrid, bool badWhy)
        {
            var newGrid = new List<List<bool>>();

            for (var i = 0; i <= currentGrid.Count + 1; i++)
            {
                newGrid.Add(new List<bool>());
                for (var j = 0; j <= currentGrid[0].Count + 1; j++)
                {
                    if (i == 0 || j == 0 || i == currentGrid.Count + 1 || j == currentGrid[0].Count + 1)
                    {
                        newGrid[i].Add(badWhy);
                    }
                    else
                    {
                        newGrid[i].Add(currentGrid[i - 1][j - 1]);
                    }
                }
            }

            return newGrid;
        }

        public List<List<bool>> EnhanceGrid(List<List<bool>> currentGrid)
        {
            var newGrid = new List<List<bool>>();

            for (var i = 0; i < currentGrid.Count; i++)
            {
                newGrid.Add(new List<bool>());
                for (var j = 0; j < currentGrid[0].Count; j++)
                {
                    if (i == 0 || j == 0 || i == currentGrid.Count - 1 || j == currentGrid[0].Count - 1)
                    {
                        var badNum = currentGrid[0][0] ? 511 : 0;
                        newGrid[i].Add(_answerKey[badNum].Equals('#'));
                    }
                    else
                    {
                        var num = FindKeyNum(GetNeighbours(currentGrid, i, j));
                        newGrid[i].Add(_answerKey[num].Equals('#'));
                    }
                }
            }
            return newGrid;
        }

        public List<List<bool>> GetNeighbours(List<List<bool>> grid, int x, int y)
        {
            var newGrid = new List<List<bool>>();

            for (var i = x - 1; i <= x + 1; i++)
            {
                var listy = new List<bool>();
                for (var j = y - 1; j <= y + 1; j++)
                {
                    listy.Add(grid[i][j]);
                }
                newGrid.Add(listy);
            }
            return newGrid;
        }

        public int FindKeyNum(List<List<bool>> grid)
        {
            var str = string.Empty;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (grid[i][j])
                    {
                        str = str + "1";
                    }
                    else
                    {
                        str = str + "0";
                    }
                }
            }
            return Convert.ToInt32(str, 2);
        }
    }
}