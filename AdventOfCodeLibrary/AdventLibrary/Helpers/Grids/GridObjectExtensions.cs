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

        public static void RotateColumnDownWithWrap<T>(this GridObject<T> grid, int column)
        {
            var temp = grid.Get(column, grid.MaxY);
            for (var currentRow = grid.MaxY; currentRow > 0; currentRow--)
            {
                var val = grid.Get(column, currentRow - 1);
                grid.Set(column, currentRow, val);
            }
            grid.Set(column, 0, temp);
        }

        public static void RotateColumnUpWithWrap<T>(this GridObject<T> grid, int column)
        {
            var temp = grid.Get(column, 0);
            for (var currentRow = 0; currentRow < grid.MaxY; currentRow++)
            {
                var val = grid.Get(column, currentRow + 1);
                grid.Set(column, currentRow, val);
            }
            grid.Set(column, grid.MaxY, temp);
        }

        public static void RotateGridDownWithWrap<T>(this GridObject<T> grid)
        {
            for (var i = 0; i < grid.Width; i++)
            {
                RotateColumnDownWithWrap(grid, i);
            }
        }

        public static void RotateGridUpWithWrap<T>(this GridObject<T> grid)
        {
            for (var i = 0; i < grid.Width; i++)
            {
                RotateColumnUpWithWrap(grid, i);
            }
        }

        public static void RotateGridLeftWithWrap<T>(this GridObject<T> grid)
        {
            for (var i = 0; i < grid.Height; i++)
            {
                RotateRowLeftWithWrap(grid, i);
            }
        }

        public static void RotateGridRightWithWrap<T>(this GridObject<T> grid)
        {
            for (var i = 0; i < grid.Height; i++)
            {
                RotateRowRightWithWrap(grid, i);
            }
        }

        public static void RotateRowRightWithWrap<T>(this GridObject<T> grid, int row)
        {
            var temp = grid.Get(grid.MaxX, row);
            for (var currentColumn = grid.MaxX; currentColumn > 0; currentColumn--)
            {
                var val = grid.Get(currentColumn - 1, row);
                grid.Set(currentColumn, row, val);
            }
            grid.Set(0, row, temp);
        }

        public static void RotateRowLeftWithWrap<T>(this GridObject<T> grid, int row)
        {
            var temp = grid.Get(0, row);
            for (var currentColumn = 0; currentColumn < grid.MaxX; currentColumn++)
            {
                var val = grid.Get(currentColumn + 1, row);
                grid.Set(currentColumn, row, val);
            }
            grid.Set(grid.MaxX, row, temp);
        }
    }
}