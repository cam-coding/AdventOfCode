using System.Collections.Generic;
using AdventLibrary.Extensions;
using System.Linq;

namespace AdventLibrary.Helpers.Grids
{
    public static class GridObjectExtensions
    {
        public static List<GridLocation<int>> GetAllCorners<T>(this GridObject<T> grid)
        {
            return new List<GridLocation<int>>()
            {
                grid.GetTopLeftCorner(),
                grid.GetTopRightCorner(),
                grid.GetBottomLeftCorner(),
                grid.GetBottomRightCorner(),
            };
        }

        public static GridLocation<int> GetTopLeftCorner<T>(this GridObject<T> grid)
        {
            return new GridLocation<int>(0, 0);
        }

        public static GridLocation<int> GetTopRightCorner<T>(this GridObject<T> grid)
        {
            return new GridLocation<int>(0, grid.MaxY);
        }

        public static GridLocation<int> GetBottomLeftCorner<T>(this GridObject<T> grid)
        {
            return new GridLocation<int>(grid.MaxX, 0);
        }

        public static GridLocation<int> GetBottomRightCorner<T>(this GridObject<T> grid)
        {
            return new GridLocation<int>(grid.MaxX, grid.MaxY);
        }

        public static GridObject<T> GetSubGrid<T>(this GridObject<T> grid, GridLocation<int> topLeftCorner, GridLocation<int> bottomRightCorner)
        {
            var newGrid = new List<List<T>>();
            for (var y = topLeftCorner.Y; y <= bottomRightCorner.Y; y++)
            {
                var length = 1 + bottomRightCorner.X - topLeftCorner.X;
                newGrid.Add(grid.Grid[y].SubList(topLeftCorner.X, length));
            }

            return new GridObject<T>(newGrid);
        }

        public static List<List<T>> GetColumns<T>(this GridObject<T> grid)
        {
            var columns = new List<List<T>>();
            for (var x = 0; x < grid.Width; x++)
            {
                columns.Add(new List<T>());
                for (var y = 0; y < grid.Height; y++)
                {
                    columns[x].Add(grid.Get(x, y));
                }
            }
            return columns;
        }

        // use BFS to find regions of same value
        public static List<List<GridLocation<int>>> GetRegions<T>(this GridObject<T> grid)
        {
            var locations = new List<List<GridLocation<int>>>();
            var pointsToLocationGroup = new Dictionary<GridLocation<int>, List<GridLocation<int>>>();

            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var loc = new GridLocation<int>(x, y);
                    var val = grid.Get(loc);

                    // BFS to find region
                    if (!pointsToLocationGroup.ContainsKey(loc))
                    {
                        Queue<GridLocation<int>> q = new Queue<GridLocation<int>>();
                        var history = new HashSet<GridLocation<int>>();
                        var region = new List<GridLocation<int>>();

                        q.Enqueue(loc);
                        while (q.Count > 0)
                        {
                            var currentLocation = q.Dequeue();
                            region.Add(currentLocation);
                            history.Add(currentLocation);

                            //Get the next nodes/grids/etc to visit next
                            foreach (var neighbour in grid.GetOrthogonalNeighbours(currentLocation))
                            {
                                var neighVal = grid.Get(neighbour);
                                if (!neighVal.Equals(val) || history.Contains(neighbour))
                                {
                                    continue;
                                }
                                history.Add(neighbour);
                                q.Enqueue(neighbour);
                            }
                        }
                        if (region.Count > 0)
                        {
                            foreach (var item in region)
                            {
                                pointsToLocationGroup.Add(item, region);
                            }
                            locations.Add(region);
                        }
                    }
                }
            }

            return locations;
        }
    }
}