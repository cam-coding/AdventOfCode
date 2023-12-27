using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day11: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
        private string _filePath;
        private int _counter;
        private int _counter2;
        private List<List<int>> grid;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            grid = AdventLibrary.ParseInput.ParseFileAsGrid(_filePath);
			_counter = 0;
            var blah = AdventLibrary.GridHelperWeirdTypes.GetOrthoginalNeighbours(grid, 0, 0);

            for (var w = 0; w < 100; w++)
            {
                for (int i = 0; i < grid.Count; i++)
                {
                    for (int j = 0; j < grid[0].Count; j++)
                    {
                        grid[i][j]++;
                    }
                }
                while (FlashGrid());
            }
            return _counter;
        }

        private bool FlashGrid()
        {
            var flashed = false;
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Count; j++)
                {
                    if (grid[i][j] > 9)
                    {
                        Flash(i, j);
                        flashed = true;
                        _counter++;
                        _counter2++;
                    }
                }
            }
            return flashed;
        }

        private void Flash(int x, int y)
        {
            var adj = AdventLibrary.GridHelperWeirdTypes.GetOrthoginalNeighbours(grid, x, y);
            foreach (var item in adj)
            {
                if (grid[item.Item1][item.Item2] != 0)
                {
                    grid[item.Item1][item.Item2]++;
                }
            }
            grid[x][y] = 0;
        }


        
        private object Part2()
        {
            grid = AdventLibrary.ParseInput.ParseFileAsGrid(_filePath);
			_counter = 0;
            var blah = AdventLibrary.GridHelperWeirdTypes.GetOrthoginalNeighbours(grid, 0, 0);

            for (var w = 0; w < 10000; w++)
            {
                for (int i = 0; i < grid.Count; i++)
                {
                    for (int j = 0; j < grid[0].Count; j++)
                    {
                        grid[i][j]++;
                    }
                }
                _counter2 = 0;

                while (FlashGrid());
                if (_counter2 == 100)
                {
                    return w;
                }
            }
            return _counter;
        }
    }
}