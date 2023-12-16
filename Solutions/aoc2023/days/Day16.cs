using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.CustomObjects;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day16: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private static Dictionary<(char, LocationTuple<int>), LocationTuple<int>> dict = new Dictionary<(char, LocationTuple<int>), LocationTuple<int>>()
            {
                { ('\\', GridWalker.Left), GridWalker.Up},
                { ('\\', GridWalker.Right), GridWalker.Down},
                { ('\\', GridWalker.Up), GridWalker.Left},
                { ('\\', GridWalker.Down), GridWalker.Right},
                { ('/', GridWalker.Left), GridWalker.Down},
                { ('/', GridWalker.Right), GridWalker.Up},
                { ('/', GridWalker.Up), GridWalker.Right},
                { ('/', GridWalker.Down), GridWalker.Left},
                { ('|', GridWalker.Left), GridWalker.Up},
                { ('|', GridWalker.Right), GridWalker.Up},
                { ('|', GridWalker.Up), GridWalker.Up},
                { ('|', GridWalker.Down), GridWalker.Down},
                { ('-', GridWalker.Left), GridWalker.Left},
                { ('-', GridWalker.Right), GridWalker.Right},
                { ('-', GridWalker.Up), GridWalker.Left},
                { ('-', GridWalker.Down), GridWalker.Left},
            };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            return PewPew(grid, new GridWalker((0, -1), GridWalker.Right));
        }
        
        private object Part2()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var max = 0;
            for (var j = 0; j < grid[0].Count; j++)
            {
                // go from every spot along the top and bottom
                max = Math.Max(PewPew(grid, new GridWalker((-1, j), GridWalker.Down)), max);
                max = Math.Max(PewPew(grid, new GridWalker((grid.Count, j), GridWalker.Up)), max);
            }
            for (var j = 0; j < grid.Count; j++)
            {
                max = Math.Max(PewPew(grid, new GridWalker((j, -1), GridWalker.Right)), max);
                max = Math.Max(PewPew(grid, new GridWalker((j, grid[j].Count), GridWalker.Left)), max);
            }
            return max;
        }

        private int PewPew(List<List<char>> grid, GridWalker starting)
        {
            var energize = new HashSet<LocationTuple<int>>();
            var beams = new List<GridWalker>();
            var max = 0;
            // walk once because we start outside the grid
            starting.Walk();
            beams.Add(starting);

            for (var j = 0; j < 1000; j++)
            {
                var newBeams = new List<GridWalker>();
                foreach (var item in beams)
                {
                    var y = item.Y;
                    var x = item.X;

                    // bounds check, I should just have a method to do this
                    if (y < 0 || y >= grid.Count || x < 0 || x >= grid[y].Count)
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
                            if (item.Direction == GridWalker.Left || item.Direction == GridWalker.Right)
                            {
                                item.Direction = dict[(grid[y][x], item.Direction)];
                                var newItem = new GridWalker(item);
                                newItem.Direction = GridWalker.Down;
                                newBeams.Add(newItem);
                            }
                        }
                        else if (grid[y][x] == '-')
                        {
                            if (item.Direction == GridWalker.Up || item.Direction == GridWalker.Down)
                            {
                                item.Direction = dict[(grid[y][x], item.Direction)];
                                var newItem = new GridWalker(item);
                                newItem.Direction = GridWalker.Right;
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