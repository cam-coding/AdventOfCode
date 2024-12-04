using AdventLibrary;
using AdventLibrary.CustomObjects;
using System.Collections.Generic;

namespace aoc2024
{
    public class Day04: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public static LocationTuple<int> Up = new LocationTuple<int>(-1, 0);
        public static LocationTuple<int> UpRight = new LocationTuple<int>(-1, 1);
        public static LocationTuple<int> UpLeft = new LocationTuple<int>(-1, -1);
        public static LocationTuple<int> Down = new LocationTuple<int>(1, 0);
        public static LocationTuple<int> DownRight = new LocationTuple<int>(1, 1);
        public static LocationTuple<int> DownLeft = new LocationTuple<int>(1, -1);
        public static LocationTuple<int> Left = new LocationTuple<int>(0, -1);
        public static LocationTuple<int> Right = new LocationTuple<int>(0, 1);

        public static List<LocationTuple<int>> paths = new List<LocationTuple<int>>()
        {
            Up,
            UpRight,
            UpLeft,
            Down,
            DownRight,
            DownLeft,
            Left,
            Right,
        };

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
			var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            long total = 1000000;
			long count = 0;
            long number = input.Long;
            var str = "MAS";

            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    if (grid[y][x] == 'X')
                    {
                        foreach (var dir in paths)
                        {
                            var curX = x;
                            var curY = y;

                            var found = true;
                            for (var i = 0; i < 3; i++)
                            {
                                curX = curX + dir.X;
                                curY = curY + dir.Y;
                                
                                if (curX < 0 || curX == grid[0].Count || curY < 0 || curY == grid.Count || grid[curY][curX] != str[i])
                                {
                                    found = false;
                                    break;
                                }
                            }
                            if (found)
                            {
                                count++;
                            }
                        }
                        /*
                        var neighs = GridHelper.GetAllNeighbours(grid, x, y).Where(z => grid[z.y][z.x] == 'M');
                        var blah = neighs.Count();*/
                    }
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            long total = 1000000;
            long count = 0;
            long number = input.Long;
            var str = "MAS";

            for (var y = 1; y < grid.Count-1; y++)
            {
                for (var x = 1; x < grid[0].Count-1; x++)
                {
                    if (grid[y][x] == 'A')
                    {
                        var tl = grid[y + UpLeft.Y][x + UpLeft.X];
                        var tr = grid[y + UpRight.Y][x + UpRight.X];
                        var dl = grid[y + DownLeft.Y][x + DownLeft.X];
                        var dr = grid[y + DownRight.Y][x + DownRight.X];

                        var valid = false;

                        if ((tl == 'S' && tr == 'S'))
                        {
                            if (dl == 'M' && dr == 'M')
                            {
                                valid = true;
                            }
                        }
                        else if((tl == 'M' && tr == 'M'))
                            {
                                if (dl == 'S' && dr == 'S')
                                {
                                    valid = true;
                                }
                        }
                        else if ((tl == 'S' && dl == 'S'))
                        {
                            if (dr == 'M' && tr == 'M')
                            {
                                valid = true;
                            }
                        }
                        else if ((tl == 'M' && dl == 'M'))
                        {
                            if (dr == 'S' && tr == 'S')
                            {
                                valid = true;
                            }
                        }
                        if (valid)
                        {
                            count++;
                        }
                        /*
                        var neighs = GridHelper.GetAllNeighbours(grid, x, y).Where(z => grid[z.y][z.x] == 'M');
                        var blah = neighs.Count();*/
                    }
                }
            }
            return count;
        }
    }
}