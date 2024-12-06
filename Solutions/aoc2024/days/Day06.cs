using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day06: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
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
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
			long count = 0;
            long number = input.Long;

            GridLocation<int> loc = null;
            for (var i = 0; i < grid.Width; i++)
            {
                for (var j = 0; j < grid.Height; j++)
                {
                    if (grid.Get(i, j) == '^')
                    {
                        loc = new GridLocation<int>(i, j);
                    }
                }
            }
            var walker2 = new GridWalker(loc, Directions.Up);
            var currentLoc2 = new GridLocation<int>(walker2.X, walker2.Y);
            while (grid.WithinGrid(currentLoc2))
            {
                var nextVal = currentLoc2 + walker2.Direction;
                if (!grid.WithinGrid(nextVal))
                {
                    break;
                }
                if (grid.Get(nextVal) == '#')
                {
                    var cur = Directions.OrthogonalDirections.IndexOf(walker2.Direction);
                    if (cur == 3)
                    {
                        cur = 0;
                    }
                    else
                    {
                        cur++;
                    }
                    walker2.Direction = Directions.OrthogonalDirections[cur];
                }
                walker2.Walk();
                currentLoc2 = new GridLocation<int>(walker2.X, walker2.Y);
            }
            return walker2.Path.Select(x => x.Item1).Distinct().Count();
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            GridLocation<int> startingLoc = null;
            for (var i = 0; i < grid.Width; i++)
            {
                for (var j = 0; j < grid.Height; j++)
                {
                    if (grid.Get(i, j) == '^')
                    {
                        startingLoc = new GridLocation<int>(i, j);
                    }
                }
            }
            var walker2 = new GridWalker(startingLoc, Directions.Up);
            var currentLoc2 = new GridLocation<int>(walker2.X, walker2.Y);
            while (grid.WithinGrid(currentLoc2))
            {
                var nextVal = currentLoc2 + walker2.Direction;
                if (!grid.WithinGrid(nextVal))
                {
                    break;
                }
                if (grid.Get(nextVal) == '#')
                {
                    var cur = Directions.OrthogonalDirections.IndexOf(walker2.Direction);
                    if (cur == 3)
                    {
                        cur = 0;
                    }
                    else
                    {
                        cur++;
                    }
                    walker2.Direction = Directions.OrthogonalDirections[cur];
                }
                walker2.Walk();
                currentLoc2 = new GridLocation<int>(walker2.X, walker2.Y);
            }
            var places = walker2.Path.Select(x => x.Item1).Distinct();
            var countplc = places.Count();

            var listy = new List<GridLocation<int>>();

            foreach (var place in places)
            {
                var curGrid = new GridObject<char>(grid.Grid);
                var loc2 = place;
                if (startingLoc.Equals(loc2))
                {
                    continue;
                }
                if (curGrid.Get(place) == '#')
                {
                    continue;
                }

                curGrid.Grid[loc2.Y][loc2.X] = '#';
                // GridHelper.PrintGrid(curGrid.Grid);
                // Console.WriteLine();

                var walker = new GridWalker(startingLoc, Directions.Up);
                var currentLoc = new GridLocation<int>(walker.X, walker.Y);
                while (curGrid.WithinGrid(currentLoc))
                {
                    var nextVal = currentLoc + walker.Direction;
                    if (!curGrid.WithinGrid(nextVal))
                    {
                        break;
                    }
                    if (walker.Looping)
                    {
                        listy.Add(place);
                        count++;
                        break;
                    }
                    while (curGrid.Get(nextVal) == '#')
                    {
                        var cur = Directions.OrthogonalDirections.IndexOf(walker.Direction);
                        if (cur == 3)
                        {
                            cur = 0;
                        }
                        else
                        {
                            cur++;
                        }
                        walker.Direction = Directions.OrthogonalDirections[cur];
                        nextVal = currentLoc + walker.Direction;
                    }
                    walker.Walk();
                    currentLoc = new GridLocation<int>(walker.X, walker.Y);
                }
            }

            /*
            for (var j = 0; j < grid.Width; j++)
            {
                for (var i = 0; i < grid.Height; i++)
                {
                    var curGrid = new GridObject<char>(grid.Grid);
                    var loc2 = new GridLocation<int>(i, j);
                    if (startingLoc.Equals(loc2))
                    {
                        break;
                    }

                    curGrid.Grid[loc2.Y][loc2.X] = '#';
                    // GridHelper.PrintGrid(curGrid.Grid);
                    // Console.WriteLine();

                    var walker = new GridWalker(startingLoc, Directions.Up);
                    var currentLoc = new GridLocation<int>(walker.X, walker.Y);
                    while (curGrid.WithinGrid(currentLoc))
                    {
                        var nextVal = currentLoc + walker.Direction;
                        if (!curGrid.WithinGrid(nextVal))
                        {
                            break;
                        }
                        if (walker.Looping)
                        {
                            count++;
                            break;
                        }
                        if (curGrid.Get(nextVal) == '#')
                        {
                            var cur = Directions.OrthogonalDirections.IndexOf(walker.Direction);
                            if (cur == 3)
                            {
                                cur = 0;
                            }
                            else
                            {
                                cur++;
                            }
                            walker.Direction = Directions.OrthogonalDirections[cur];
                        }
                        walker.Walk();
                        currentLoc = new GridLocation<int>(walker.X, walker.Y);
                    }
                }
            }*/
            var cnt = listy.Count;
            var cnt2 = listy.Distinct().Count();
            return count;
        }
    }
}