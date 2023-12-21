using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day25: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
        private string _filePath;
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
			var counter = 0;
            var moved = true;

            while (moved)
            {
                moved = false;
                var newGrid = grid.Clone2dList();

                for (var i = 0; i < grid.Count; i++)
                {
                    for (var j = 0; j < grid[0].Count; j++)
                    {
                        if ((grid[i][j]) == '>')
                        {
                            var check = GridHelper.MoveByOffset(j, i, 1, 0, grid[0].Count, grid.Count, true);
                            if (grid[check.Item2][check.Item1] == '.')
                            {
                                newGrid[check.Item2][check.Item1] = '>';
                                newGrid[i][j] = '.';
                                moved = true;
                            }
                            else
                            {
                            }
                        }
                    }
                }
                
                grid = newGrid.Clone2dList();
                
                for (var i = 0; i < grid.Count; i++)
                {
                    for (var j = 0; j < grid[0].Count; j++)
                    {
                        if ((grid[i][j]) == 'v')
                        {
                            var check2 = GridHelper.MoveByOffset(j, i, 0, 1, grid[0].Count, grid.Count, true);
                            if (grid[check2.Item2][check2.Item1] == '.')
                            {
                                newGrid[check2.Item2][check2.Item1] = 'v';
                                newGrid[i][j] = '.';
                                moved = true;
                            }
                        }
                    }
                }

                grid = newGrid.Clone2dList();
                counter++;
            }
            return counter;
        }
        
        private object Part2()
        {
            return 0;
        }
    }
}