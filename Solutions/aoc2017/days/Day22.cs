using AdventLibrary;
using AdventLibrary.Helpers.Grids;
using System.Collections.Generic;

namespace aoc2017
{
    public class Day22: ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var origGrid = input.GridChar;
            var count = 0;

            var grid = new GridObject<char>(GridHelper.InsertGridIntoEmptyGrid(origGrid.Grid, '.', 1001, 1001));
            var centre = GridHelper.FindCentreOfGrid(grid.Grid);
            var currentLocation = new GridLocation<int>(centre.X, centre.Y);
            var walker = new GridWalker(currentLocation, Directions.Up);

            for (var i = 0; i < 10000; i++)
            {
                if (grid.Get(walker.Current) == '#')
                {
                    walker.Direction = Directions.TurnRightOrthogonal(walker.Direction);
                }
                else
                {
                    walker.Direction = Directions.TurnleftOrthogonal(walker.Direction);
                }
                if (grid.Get(walker.Current) == '#')
                {
                    grid.Set(walker.Current, '.');
                }
                else
                {
                    grid.Set(walker.Current, '#');
                    count++;
                }
                walker.Walk();
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var origGrid = input.GridChar;
            var count = 0;

            var locations = origGrid.GetAllLocationWhereCellEqualsValue('#');
            var grid = new GridInfinite<int>(locations, 2, 0);

            var centre = GridHelper.FindCentreOfGrid(origGrid.Grid);
            var currentLocation = new GridLocation<int>(centre.X, centre.Y);
            var walker = new GridWalker(currentLocation, Directions.Up);

            for (var i = 0; i < 10000000; i++)
            {
                var currentValue = grid.Get(walker.Current);

                // change dir
                if (currentValue == 0)
                {
                    walker.Direction = Directions.TurnleftOrthogonal(walker.Direction);
                }
                else if (currentValue == 2)
                {
                    walker.Direction = Directions.TurnRightOrthogonal(walker.Direction);
                }
                else if (currentValue == 3)
                {
                    walker.Direction = Directions.Opposites[walker.Direction];
                }

                //change current node
                if (currentValue == 3)
                {
                    grid.Set(walker.Current, 0);
                }
                else
                {
                    if (currentValue == 1)
                    {
                        count++;
                    }
                    grid.Set(walker.Current, currentValue + 1);
                }
                walker.Walk();
            }
            return count;
        }
    }
}