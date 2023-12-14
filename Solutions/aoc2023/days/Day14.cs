using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day14: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);

            grid = RollNorth(grid);

            // GridHelper.PrintGrid(grid);

            var count = Count(grid);
            return count;
        }

        private object Part2()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var dict = new Dictionary<int, List<List<char>>>();
            var magic = 100;
            for (var i = 0; i < magic; i++)
            {
                grid = RollNorth(grid);
                grid = RollWest(grid);
                grid = RollSouth(grid);
                grid = RollEast(grid);

                // GridHelper.PrintGrid(grid);
                dict.Add(i, grid.Clone2dList());
            }

            var newGrid = new List<List<char>>();
            var stationaryCount = 0;
            for (var i = 0; i < grid.Count; i++)
            {
                newGrid.Add(new List<char>());
                for (var j = 0; j < grid[i].Count; j++)
                {
                    var same = true;
                    var cr = dict[90][i][j];
                    for (var k = 90; k < magic; k++)
                    {
                        if (dict[k][i][j] != cr)
                        {
                            same = false;
                            break;
                        }
                    }
                    if (same)
                    {
                        newGrid[i].Add('@');
                        stationaryCount++;
                    }
                    else
                    {
                        newGrid[i].Add('.');
                    }
                }
            }
            Console.WriteLine("---------------");
            Console.WriteLine($"Stat cont is: {stationaryCount}");
            Console.WriteLine("---------------");
            // GridHelper.PrintGrid(newGrid);
            Console.WriteLine();
            for (var i = 10; i < 40; i++)
            {
                /*
                Console.WriteLine(dict[i][4].Stringify());
                Console.WriteLine(dict[i][5].Stringify());
                Console.WriteLine(dict[i][6].Stringify());
                Console.WriteLine(dict[i][7].Stringify());
                Console.WriteLine(dict[i][8].Stringify());
                Console.WriteLine(dict[i][9].Stringify());
                Console.WriteLine();*/
                // Console.WriteLine(dict[i][8].Stringify());
            }

            var count = Count(grid);
            return count;
        }

        private int Count(List<List<char>> grid)
        {
            var count = 0;
            var iter = 1;
            for (var i = grid.Count - 1; i >= 0; i--)
            {
                for (var j = 0; j < grid[i].Count; j++)
                {
                    if (grid[i][j] == 'O')
                    {
                        count += iter;
                    }
                }
                iter++;
            }
            return count;
        }

        private List<List<char>> RollNorth(List<List<char>> grid)
        {
            var diff = true;

            while (diff)
            {
                diff = false;
                for (var i = grid.Count - 1; i >= 0; i--)
                {
                    for (var j = 0; j < grid[i].Count; j++)
                    {
                        if (grid[i][j] == 'O')
                        {
                            if (i > 0 && grid[i - 1][j] == '.')
                            {
                                diff = true;
                                grid[i - 1][j] = 'O';
                                grid[i][j] = '.';
                            }
                        }
                    }
                }
            }
            return grid;
        }
        private List<List<char>> RollSouth(List<List<char>> grid)
        {
            var diff = true;

            while (diff)
            {
                diff = false;
                for (var i = 0; i < grid.Count; i++)
                {
                    for (var j = 0; j < grid[i].Count; j++)
                    {
                        if (grid[i][j] == 'O')
                        {
                            if (i < grid.Count-1 && grid[i + 1][j] == '.')
                            {
                                diff = true;
                                grid[i][j] = '.';
                                grid[i+1][j] = 'O';
                            }
                        }
                    }
                }
            }
            return grid;
        }

        private List<List<char>> RollEast(List<List<char>> grid)
        {
            var diff = true;

            while (diff)
            {
                diff = false;
                for (var i = 0; i < grid.Count; i++)
                {
                    for (var j = 0; j < grid[i].Count; j++)
                    {
                        if (grid[i][j] == 'O')
                        {
                            if (j < grid[i].Count - 1 && grid[i][j+1] == '.')
                            {
                                diff = true;
                                grid[i][j] = '.';
                                grid[i][j+1] = 'O';
                            }
                        }
                    }
                }
            }
            return grid;
        }

        private List<List<char>> RollWest(List<List<char>> grid)
        {
            var diff = true;

            while (diff)
            {
                diff = false;
                for (var i = 0; i < grid.Count; i++)
                {
                    for (var j = grid[i].Count-1; j >= 0; j--)
                    {
                        if (grid[i][j] == 'O')
                        {
                            if (j > 0 && grid[i][j - 1] == '.')
                            {
                                diff = true;
                                grid[i][j] = '.';
                                grid[i][j - 1] = 'O';
                            }
                        }
                    }
                }
            }
            return grid;
        }
    }
}