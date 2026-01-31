using AdventLibrary;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using AdventLibrary.PathFinding;

namespace aoc2024
{
    public class Day18 : ISolver
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
            var coords = input.Coords;
            List<List<char>> tempGrid;
            if (isTest)
            {
                tempGrid = GridHelper.GenerateGrid(7, 7, '.');
            }
            else
            {
                tempGrid = GridHelper.GenerateGrid(71, 71, '.');
            }

            var grid = new GridObject<char>(tempGrid);

            int magic;
            if (isTest)
            {
                magic = 12;
            }
            else
            {
                magic = 1024;
            }

            for (var i = 0; i < magic; i++)
            {
                grid.Set(coords[i], '#');
            }

            var startLocation = new GridLocation<int>(0, 0);
            var endLocation = new GridLocation<int>(grid.MaxX, grid.MaxY);

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                return grid.GetOrthogonalNeighbours(node).Where(x => grid.Get(x) != '#').ToList();
            };
            Func<GridLocation<int>, GridLocation<int>, int> WeightFunc = (current, neigh) =>
            {
                return 1;
            };

            Func<GridLocation<int>, bool> GoalFunc = (current) =>
            {
                return current == endLocation;
            };
            var res = Dijkstra<GridLocation<int>>.SearchEverywhere(startLocation, NeighboursFunc, WeightFunc, GoalFunc);
            return res[endLocation].Distance;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var coords = input.Coords;
            List<List<char>> tempGrid;
            if (isTest)
            {
                tempGrid = GridHelper.GenerateGrid(7, 7, '.');
            }
            else
            {
                tempGrid = GridHelper.GenerateGrid(71, 71, '.');
            }

            var grid = new GridObject<char>(tempGrid);

            int magic;
            if (isTest)
            {
                magic = 12;
            }
            else
            {
                magic = 1024;
            }

            for (var i = 0; i < magic; i++)
            {
                grid.Set(coords[i], '#');
            }

            var startLocation = new GridLocation<int>(0, 0);
            var endLocation = new GridLocation<int>(grid.MaxX, grid.MaxY);

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                return grid.GetOrthogonalNeighbours(node).Where(x => grid.Get(x) != '#').ToList();
            };
            Func<GridLocation<int>, GridLocation<int>, int> WeightFunc = (current, neigh) =>
            {
                return 1;
            };

            Func<GridLocation<int>, bool> GoalFunc = (current) =>
            {
                return current == endLocation;
            };
            var valid = true;
            var iter = 1024;
            if (isTest)
            {
                iter = 12;
            }

            var initialGrid = grid.Clone();
            var min = iter;
            var max = coords.Count - 1;
            while (min < max && max - min > 1)
            {
                grid = initialGrid.Clone();
                var mid = MathHelper.GetMiddle(min, max);

                for (var i = iter; i <= mid; i++)
                {
                    grid.Set(coords[i], '#');
                }
                var res = Dijkstra<GridLocation<int>>.SearchEverywhere(startLocation, NeighboursFunc, WeightFunc, GoalFunc);
                var midValue = res.ContainsKey(endLocation);
                if (midValue)
                {
                    min = mid;
                }
                else
                {
                    max = mid;
                }
            }
            return $"{coords[max].X},{coords[max].Y}";
        }
    }
}