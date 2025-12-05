using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2019
{
    public class Day24 : ISolver
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
            var grid = input.GridChar;
            long count = 0;

            var memory = new HashSet<string>();

            while (true)
            {
                var hashy = grid.Stringify();
                if (!memory.TryAdd(hashy))
                {
                    return CountGrid(grid);
                }

                var newGrid = grid.Clone();
                foreach (var loc in grid.GetAllLocations())
                {
                    var neighbourBugs = grid.GetOrthogonalNeighbours(loc).Count(x => grid.Get(x) == '#');
                    var val = grid.Get(loc);
                    if (val == '#')
                    {
                        if (neighbourBugs != 1)
                        {
                            newGrid.Set(loc, '.');
                        }
                    }
                    else if (val == '.')
                    {
                        if (neighbourBugs == 1 || neighbourBugs == 2)
                        {
                            newGrid.Set(loc, '#');
                        }
                    }
                }
                grid = newGrid;
            }
            return count;
        }

        private int CountGrid(GridObject<char> grid)
        {
            var count = 0;

            var rows = grid.Grid.Count;
            var columns = grid.Grid[0].Count;

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    if (grid.Get(column, row) == '#')
                    {
                        count += (int)Math.Pow(2, row * columns + column);
                    }
                }
                Console.Write("\n");
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            return 0;
        }
    }
}