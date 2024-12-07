using AdventLibrary;
using AdventLibrary.Helpers.Grids;
using System.Collections.Generic;

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

            GridLocation<int> loc = grid.GetLocationWhereCellEqualsValue('^');

            var walker = new GridWalker(loc, Directions.Up);
            while (grid.WithinGrid(walker.Current))
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
            }
            _uniqueLocationsPart1 = walker.UniqueLocationsVisited;
            return walker.UniqueLocationsVisited.Count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.CharGrid;
            long count = 0;

            GridLocation<int> startingLoc = grid.GetLocationWhereCellEqualsValue('^');
            var places = _uniqueLocationsPart1;

            foreach (var place in places)
            {
                var curGrid = new GridObject<char>(grid.Grid);
                if (startingLoc == place)
                {
                    continue;
                }

                curGrid.Set(place, '#');

                var walker = new GridWalker(startingLoc, Directions.Up);
                while (curGrid.WithinGrid(walker.Current))
                {
                    var nextVal = walker.Current + walker.Direction;
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
                        walker.Direction = Directions.TurnRightOrthogonal(walker.Direction);
                        nextVal = walker.GetNextLocation();
                    }
                    walker.Walk();
                }
            }

            return count;
        }
    }
}