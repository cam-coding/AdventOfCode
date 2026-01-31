using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;

namespace aoc2022
{
    public class Day08 : ISolver
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
            var grid = input.GridInt;

            var visibleTrees = new HashSet<GridLocation<int>>();

            var columns = grid.GetColumns();
            var rows = grid.GetRows();

            for (var y = 0; y < rows.Count; y++)
            {
                var treeHeight = -1;
                for (var x = 0; x < rows[y].Count; x++)
                {
                    if (grid.Get(x, y) > treeHeight)
                    {
                        treeHeight = grid.Get(x, y);
                        visibleTrees.Add(new GridLocation<int>(x, y));
                    }
                }
                treeHeight = -1;
                for (var x = rows[y].Count - 1; x >= 0; x--)
                {
                    if (grid.Get(x, y) > treeHeight)
                    {
                        treeHeight = grid.Get(x, y);
                        visibleTrees.Add(new GridLocation<int>(x, y));
                    }
                }
            }
            for (var x = 0; x < columns.Count; x++)
            {
                var treeHeight = -1;
                for (var y = 0; y < columns[x].Count; y++)
                {
                    if (grid.Get(x, y) > treeHeight)
                    {
                        treeHeight = grid.Get(x, y);
                        visibleTrees.Add(new GridLocation<int>(x, y));
                    }
                }
                treeHeight = -1;
                for (var y = columns[x].Count - 1; y >= 0; y--)
                {
                    if (grid.Get(x, y) > treeHeight)
                    {
                        treeHeight = grid.Get(x, y);
                        visibleTrees.Add(new GridLocation<int>(x, y));
                    }
                }
            }
            return visibleTrees.Count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.GridInt;

            var best = 0;
            var locations = grid.GetAllLocations();
            foreach (var loc in locations)
            {
                var views = new List<int>();
                var currentHeight = grid.Get(loc);
                foreach (var dir in Directions.OrthogonalDirections)
                {
                    var visible = 0;
                    var currentLoc = loc;
                    currentLoc += dir;
                    while (grid.WithinGrid(currentLoc))
                    {
                        visible++;
                        if (grid.Get(currentLoc) >= currentHeight)
                        {
                            break;
                        }
                        currentLoc += dir;
                    }
                    views.Add(visible);
                }
                best = Math.Max(best, views.Product());
            }
            return best;
        }
    }
}