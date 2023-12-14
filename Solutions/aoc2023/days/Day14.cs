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
            var dict2 = new HashSet<string>();
            var listy = new List<int>();
            var magic = 200;
            for (var i = 0; i < magic; i++)
            {
                grid = RollNorth(grid);
                grid = RollWest(grid);
                grid = RollSouth(grid);
                grid = RollEast(grid);

                // GridHelper.PrintGrid(grid);
                dict.Add(i, grid.Clone2dList());
                if (!dict2.Contains(grid.Stringify()))
                {
                    dict2.Add(grid.Stringify());
                    listy.Add(i);
                }
            }

            // this was the last time we had a unique grid configuration + 1
            var cycleStart = listy.Last() + 1;
            var slow = cycleStart;
            var fast = slow + 1;
            var cycleLength = 1;
            while (!dict[slow].Stringify().Equals(dict[fast].Stringify()))
            {
                cycleLength++;
                slow++;
                fast += 2;
            }

            var diff = (1000000000 - 1) - (cycleStart);
            var rem = diff % cycleLength;
            return Count(dict[cycleStart + rem]);
        }

            /*Notes:
             * No new states after 111th cycle
             * Cycle Length of 9
             *
             *
             * */
            private object Part2WIP()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var dict = new Dictionary<int, List<List<char>>>();
            var dict2 = new Dictionary<string, int>();
            var listy = new List<int>();
            var magic = 200;
            for (var i = 0; i < magic; i++)
            {
                grid = RollNorth(grid);
                grid = RollWest(grid);
                grid = RollSouth(grid);
                grid = RollEast(grid);

                // GridHelper.PrintGrid(grid);
                dict.Add(i, grid.Clone2dList());
                if (!dict2.ContainsKey(grid.Stringify()))
                {
                    dict2.TryAdd(grid.Stringify(), Count(grid));
                    listy.Add(i);
                }
            }

            var cycleStart = listy.Last() + 1;
            var slow = cycleStart;
            var fast = slow+1;
            var cycleLength = 1;
            while (!dict[slow].Stringify().Equals(dict[fast].Stringify()))
            {
                cycleLength++;
                slow++;
                fast += 2;
            }

            GridHelper.PrintGrid(dict[9]);
            Console.WriteLine();
            Console.WriteLine();
            GridHelper.PrintGrid(dict[10]);

            var diff = (1000000000-1) - (cycleStart);
            var rem = diff % cycleLength;
            // return Count(dict[cycleStart + rem]);

            var newGrid = new List<List<char>>();
            var stationaryCount = 0;
            var hashyList = new List<(int, int)>();
            for (var i = 0; i < grid.Count; i++)
            {
                newGrid.Add(new List<char>());
                for (var j = 0; j < grid[i].Count; j++)
                {
                    var same = true;

                    // start cycle hunting a few hundred in after things have settled down.
                    var start = (int)(magic * .9);
                    var cr = dict[start][i][j];
                    for (var k = start; k < magic; k++)
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
                        hashyList.Add((i, j));
                    }
                }
            }

            for (var k = cycleStart; k < cycleStart+cycleLength + 1; k++)
            {
                var gridUnique = dict[k];
                for (var i = 0; i < gridUnique.Count; i++)
                {
                    for (var j = 0; j < gridUnique[i].Count; j++)
                    {
                        if (!hashyList.Contains((i,j)))
                        {
                            Console.Write(' ');
                        }
                        else
                        {
                            Console.Write(grid[i][j]);
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            // for (var k = (int)(magic * .9); k < magic; k++)
            var results = new HashSet<int>();
            var cycleCount = 0;
            for (var k = 0; k < 1000; k++)
            {
                var answer = Count(dict[k]);
                if (results.Contains(answer))
                {
                    Console.WriteLine($"First Cycle at {k}: {Count(dict[k])}");
                    cycleCount++;
                }
                if (cycleCount > 30)
                {
                    break;
                }
                results.Add(answer);
                // Console.WriteLine($"Cycle {k} result is: {Count(dict[k])}");
            }
            Console.WriteLine("---------------");
            Console.WriteLine($"Stat cont is: {stationaryCount}");
            Console.WriteLine("---------------");
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