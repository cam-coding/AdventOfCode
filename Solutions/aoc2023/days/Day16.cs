using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2023
{
    public class Day16: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private static Dictionary<(char, GridLocation<int>), GridLocation<int>> dict = new Dictionary<(char, GridLocation<int>), GridLocation<int>>()
            {
                { ('\\', Directions.Left), Directions.Up},
                { ('\\', Directions.Right), Directions.Down},
                { ('\\', Directions.Up), Directions.Left},
                { ('\\', Directions.Down), Directions.Right},
                { ('/', Directions.Left), Directions.Down},
                { ('/', Directions.Right), Directions.Up},
                { ('/', Directions.Up), Directions.Right},
                { ('/', Directions.Down), Directions.Left},
                { ('|', Directions.Left), Directions.Up},
                { ('|', Directions.Right), Directions.Up},
                { ('|', Directions.Up), Directions.Up},
                { ('|', Directions.Down), Directions.Down},
                { ('-', Directions.Left), Directions.Left},
                { ('-', Directions.Right), Directions.Right},
                { ('-', Directions.Up), Directions.Left},
                { ('-', Directions.Down), Directions.Left},
            };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            return PewPew(grid, new GridWalker(new GridLocation<int>(0, 0), Directions.Right));
        }
        
        private object Part2()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var max = 0;
            for (var j = 0; j < grid[0].Count; j++)
            {
                // go from every spot along the top and bottom
                max = Math.Max(PewPew(grid, new GridWalker(new GridLocation<int>(0, j), Directions.Down)), max);
                max = Math.Max(PewPew(grid, new GridWalker(new GridLocation<int>(grid.Count-1, j), Directions.Up)), max);
            }
            for (var j = 0; j < grid.Count; j++)
            {
                // then left and right sides
                max = Math.Max(PewPew(grid, new GridWalker(new GridLocation<int>(j, 0), Directions.Right)), max);
                max = Math.Max(PewPew(grid, new GridWalker(new GridLocation<int>(j, grid[j].Count-1), Directions.Left)), max);
            }
            return max;
        }

        private int PewPew(List<List<char>> grid, GridWalker starting)
        {
            var energize = new HashSet<GridLocation<int>>();
            var beams = new List<GridWalker>();
            var max = 0;
            beams.Add(starting);

            for (var j = 0; j < 1000; j++)
            {
                var newBeams = new List<GridWalker>();
                foreach (var item in beams)
                {
                    var y = item.Y;
                    var x = item.X;

                    // bounds check, I should just have a method to do this
                    if (!GridHelperWeirdTypes.WithinGrid(grid, item.Current))
                    {
                        item.OutOfBounds = true;
                    }
                    else
                    {
                        energize.Add(item.Current);

                        // if we hit this we turn
                        if (grid[y][x] == '\\' || grid[y][x] == '/')
                        {
                            item.Direction = dict[(grid[y][x], item.Direction)];
                        }
                        // if we hit the next two we turn sometimes
                        else if (grid[y][x] == '|')
                        {
                            if (item.Direction == Directions.Left || item.Direction == Directions.Right)
                            {
                                item.Direction = dict[(grid[y][x], item.Direction)];
                                var newItem = new GridWalker(item);
                                newItem.Direction = Directions.Down;
                                newBeams.Add(newItem);
                            }
                        }
                        else if (grid[y][x] == '-')
                        {
                            if (item.Direction == Directions.Up || item.Direction == Directions.Down)
                            {
                                item.Direction = dict[(grid[y][x], item.Direction)];
                                var newItem = new GridWalker(item);
                                newItem.Direction = Directions.Right;
                                newBeams.Add(newItem);
                            }
                        }
                        // everything else (and after we've turned) we walk
                        item.Walk();
                    }
                }
                // Walk the new items once as well.
                newBeams.ForEach(x => x.Walk());
                // ignore the beams that are "finished"
                beams = beams.Where(x => !x.Looping && !x.OutOfBounds).ToList();
                beams.AddRange(newBeams);
                if (beams.Count == 0)
                {
                    j = 1000;
                }
            }
            return energize.Count;
        }
    }
}