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
        private List<GridLocation<int>> _uniqueLocationsPart1;
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

            GridLocation<int> loc = grid.GetLocationWhereEqualsValue('^');

            var walker = new GridWalker(loc, Directions.Up);
            var currentLocation = new GridLocation<int>(walker.X, walker.Y);
            while (grid.WithinGrid(currentLocation))
            {
                var nextVal = walker.GetNextLocation();
                if (!grid.WithinGrid(nextVal))
                {
                    break;
                }
                if (grid.Get(nextVal) == '#')
                {
                    walker.Direction = Directions.TurnRightOrthogonal(walker.Direction);
                }
                walker.Walk();
                currentLocation = new GridLocation<int>(walker.X, walker.Y);
            }
            _uniqueLocationsPart1 = walker.UniqueLocationsVisited;
            return walker.UniqueLocationsVisited.Count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.CharGrid;
            long count = 0;

            GridLocation<int> startingLoc = grid.GetLocationWhereEqualsValue('^');
            var places = _uniqueLocationsPart1;

            foreach (var place in places)
            {
                var curGrid = new GridObject<char>(grid.Grid);
                if (startingLoc.Equals((GridLocation<int>)place))
                {
                    continue;
                }

                curGrid.Set(place, '#');

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

            return count;
        }
    }
}