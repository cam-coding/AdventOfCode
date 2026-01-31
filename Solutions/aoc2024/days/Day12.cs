using AdventLibrary;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day12 : ISolver
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
            var grid = input.GridChar;
            long count = 0;

            var regions = grid.GetRegions();

            foreach (var list in regions)
            {
                var area = list.Count;
                var val = grid.Get(list.First());

                // Calculate Perimetre
                var perim = 0;
                foreach (var item in list)
                {
                    var neighs = new List<GridLocation<int>>();
                    foreach (var direction in Directions.OrthogonalDirections)
                    {
                        var tempLocation = item + direction;
                        neighs.Add(tempLocation);
                    }
                    foreach (var neigh in neighs)
                    {
                        if (!grid.WithinGrid(neigh) || grid.Get(neigh) != grid.Get(item))
                        {
                            perim++;
                        }
                    }
                }
                var cost = area * perim;
                count += cost;
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.GridChar;
            long count = 0;

            var regions = grid.GetRegions();

            foreach (var region in regions)
            {
                var area = region.Count;
                var sidesFromThisLocation = 0;
                var val = grid.Get(region.First());

                var dictLocationToOutsideEdges = new Dictionary<GridLocation<int>, List<GridLocation<int>>>();

                // find any "edge" that doesn't point to another cell in the region
                foreach (var item in region)
                {
                    var neighs = new List<GridLocation<int>>();
                    foreach (var direction in Directions.OrthogonalDirections)
                    {
                        var neigh = item + direction;
                        if (!grid.WithinGrid(neigh) || grid.Get(neigh) != grid.Get(item))
                        {
                            if (!dictLocationToOutsideEdges.TryAdd(item, new List<GridLocation<int>>() { direction }))
                            {
                                dictLocationToOutsideEdges[item].Add(direction);
                            }
                        }
                    }
                }

                var sideCount = 0;
                foreach (var item in region)
                {
                    if (!dictLocationToOutsideEdges.ContainsKey(item))
                    {
                        continue;
                    }

                    // get all the sides from any regionPoint that is above or to the right.
                    // those sides we consider "counted"
                    // points in the bottom left of a region don't usually count their side
                    // points in the top right count all their sides because no region square are above or to the right
                    var tempLocation = item + Directions.Up;
                    var currentSides = dictLocationToOutsideEdges[item];
                    var invalidSides = new List<GridLocation<int>>();
                    if (dictLocationToOutsideEdges.ContainsKey(tempLocation))
                    {
                        invalidSides.AddRange(dictLocationToOutsideEdges[tempLocation]);
                    }
                    tempLocation = item + Directions.Right;
                    if (dictLocationToOutsideEdges.ContainsKey(tempLocation))
                    {
                        invalidSides.AddRange(dictLocationToOutsideEdges[tempLocation]);
                    }

                    // only include sides that have not been counted
                    sideCount += currentSides.Where(x => !invalidSides.Contains(x)).Count();
                }
                var cost = area * sideCount;
                count += cost;
            }
            return count;
        }
    }
}