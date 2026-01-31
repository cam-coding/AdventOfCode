using AdventLibrary;
using AdventLibrary.Helpers.Grids;
using AdventLibrary.PathFinding;

namespace aoc2022
{
    public class Day12 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1(bool isTest = false)
        {
            var charGrid = ParseInput.ParseFileAsCharGrid(_filePath);
            var grid = new GridObject<char>(charGrid);
            var startLocation = grid.GetFirstLocationWhereCellEqualsValue('S');
            var endLocation = grid.GetFirstLocationWhereCellEqualsValue('E');
            grid.Set(startLocation, 'a');
            grid.Set(endLocation, 'z');

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in grid.GetOrthogonalNeighbours(node))
                {
                    // remove any edges where the height difference is too great
                    if (grid.Get(edge) - 1 <= grid.Get(node))
                    {
                        neighbours.Add(edge);
                    }
                }
                return neighbours;
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
            var charGrid = ParseInput.ParseFileAsCharGrid(_filePath);
            var grid = new GridObject<char>(charGrid);
            var startLocation = grid.GetFirstLocationWhereCellEqualsValue('S');
            var endLocation = grid.GetFirstLocationWhereCellEqualsValue('E');
            grid.Set(startLocation, 'a');
            grid.Set(endLocation, 'z');

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in grid.GetOrthogonalNeighbours(node))
                {
                    // remove any edges where the height difference is too great
                    if (grid.Get(edge) - 1 <= grid.Get(node))
                    {
                        neighbours.Add(edge);
                    }
                }
                return neighbours;
            };
            Func<GridLocation<int>, GridLocation<int>, int> WeightFunc = (current, neigh) =>
            {
                return 1;
            };

            Func<GridLocation<int>, bool> GoalFunc = (current) =>
            {
                return current == endLocation;
            };

            var best = int.MaxValue;
            foreach (var start in grid.GetAllLocationWhereCellEqualsValue('a'))
            {
                var res = Dijkstra<GridLocation<int>>.SearchEverywhere(start, NeighboursFunc, WeightFunc, GoalFunc);
                if (res.ContainsKey(endLocation))
                {
                    best = Math.Min(best, res[endLocation].Distance);
                }
            }
            return best;
        }
    }
}