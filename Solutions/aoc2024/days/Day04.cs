using AdventLibrary;
using AdventLibrary.Helpers.Grids;
using System.Collections.Generic;

namespace aoc2024
{
    public class Day04: ISolver
    {
        private string _filePath;

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
            var grid = input.CharGrid;
			long count = 0;
            var str = "MAS";

            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    if (grid.Get(x,y) == 'X')
                    {
                        foreach (var dir in Directions.AllDirections)
                        {
                            var current = new GridLocation<int>(x, y);

                            var found = true;
                            for (var i = 0; i < 3; i++)
                            {
                                current = current + dir;

                                if (!grid.WithinGrid(current) || grid.Get(current) != str[i])
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
                    }
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.CharGrid;
            long count = 0;

            for (var y = 1; y < grid.Height - 1; y++)
            {
                for (var x = 1; x < grid.Width - 1; x++)
                {
                    var current = new GridLocation<int>(x, y);
                    if (grid.Get(current) == 'A')
                    {
                        var tl = grid.Get(current + Directions.UpLeft);
                        var tr = grid.Get(current + Directions.UpRight);
                        var dl = grid.Get(current + Directions.DownLeft);
                        var dr = grid.Get(current + Directions.DownRight);

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
                    }
                }
            }
            return count;
        }
    }
}