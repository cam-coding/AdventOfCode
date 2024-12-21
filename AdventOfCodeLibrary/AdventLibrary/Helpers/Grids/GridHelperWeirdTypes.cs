using System;
using System.Collections.Generic;
using AdventLibrary.Helpers.Grids;

namespace AdventLibrary
{
    public static class GridHelperWeirdTypes
    {
        public static List<Tuple<int, int>> GetOrthogonalNeighboursTuple<T>(List<List<T>> grid, int x, int y)
        {
            var adj = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(0, -1),
                new Tuple<int, int>(0, 1),
                new Tuple<int, int>(1, 0),
                new Tuple<int, int>(-1, 0),
            };
            List<Tuple<int, int>> neighbours = new List<Tuple<int, int>>();

            for (int i = 0; i < 4; i++)
            {
                var newNeighbour = MoveByOffset(x, y, adj[i].Item1, adj[i].Item2, grid[0].Count, grid.Count, false);

                if (!neighbours.Contains(newNeighbour) && (x != newNeighbour.Item1 || y != newNeighbour.Item2))
                {
                    neighbours.Add(newNeighbour);
                }
            }
            return neighbours;
        }

        public static List<Tuple<int, int>> GetAllNeighbours<T>(List<List<T>> grid, int x, int y)
        {
            List<Tuple<int, int>> neighbours = new List<Tuple<int, int>>();
            int rowMin = x - 1 > 0 ? x - 1 : 0;
            int rowMax = x + 1 < grid.Count ? x + 1 : grid.Count - 1;
            int colMin = y - 1 > 0 ? y - 1 : 0;
            int colMax = y + 1 < grid[0].Count ? y + 1 : grid[0].Count - 1;

            for (int i = rowMin; i <= rowMax; i++)
            {
                for (int j = colMin; j <= colMax; j++)
                {
                    if (i == x && j == y)
                    {
                        continue;
                    }
                    neighbours.Add(new Tuple<int, int>(i, j));
                }
            }
            return neighbours;
        }

        /* Assume offset already applied*/

        public static Tuple<int, int> MoveByOffset(int x, int y, int xOffset, int yOffset, int width, int height, bool wrap)
        {
            int newX = x + xOffset;
            int newY = y + yOffset;

            if (newX < 0)
            {
                if (wrap)
                {
                    newX = newX + width;
                }
                else
                {
                    newX = 0;
                }
            }
            else if (newX >= width)
            {
                if (wrap)
                {
                    newX = newX - width;
                }
                else
                {
                    newX = width - 1;
                }
            }

            if (newY < 0)
            {
                if (wrap)
                {
                    newY = newY + height;
                }
                else
                {
                    newY = 0;
                }
            }
            else if (newY >= height)
            {
                if (wrap)
                {
                    newY = newY - height;
                }
                else
                {
                    newY = height - 1;
                }
            }

            return new Tuple<int, int>(newX, newY);
        }

        public static Dictionary<Tuple<int, int>, List<Tuple<int, int>>> GridToTupleAdjList<T>(List<List<T>> grid)
        {
            var dict = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    var cord = new Tuple<int, int>(x, y);
                    dict.Add(cord, GetOrthogonalNeighboursTuple(grid, x, y));
                }
            }

            return dict;
        }

        /* assume we have an array like
         * 1  2  3  4
         * 5  6  7  8
         * 9 10 11 12
         * grid[row, column]
         * GetLength(0) would be 3. for the 3 rows
         * GetLength(1) is the 2nd dimension for the 4 columns.
         Should make a whole doc with notes on this for reference*/

        public static T[,] RotateColumnDownWithWrap<T>(T[,] grid, int column)
        {
            var length = grid.GetLength(0);
            var temp = grid[length - 1, column];
            for (var j = length - 1; j > 0; j--)
            {
                grid[j, column] = grid[j - 1, column];
            }
            grid[0, column] = temp;
            return grid;
        }

        public static T[,] RotateColumnUpWithWrap<T>(T[,] grid, int column)
        {
            var length = grid.GetLength(0);
            var temp = grid[0, column];
            for (var j = 0; j < length - 1; j++)
            {
                grid[j, column] = grid[j + 1, column];
            }
            grid[length - 1, column] = temp;
            return grid;
        }

        public static T[,] RotateGridDownWithWrap<T>(T[,] grid)
        {
            for (var i = 0; i < grid.GetLength(1); i++)
            {
                grid = RotateColumnDownWithWrap(grid, i);
            }
            return grid;
        }

        public static T[,] RotateGridUpWithWrap<T>(T[,] grid)
        {
            for (var i = 0; i < grid.GetLength(1); i++)
            {
                grid = RotateColumnUpWithWrap(grid, i);
            }
            return grid;
        }

        /* assume we have an array like
         * 1  2  3  4
         * 5  6  7  8
         * 9 10 11 12
         * grid[row, column]
         * GetLength(0) would be 3. for the 3 rows
         * GetLength(1) is the 2nd dimension for the 4 columns */

        public static T[,] RotateRowRightWithWrap<T>(T[,] grid, int row)
        {
            var length = grid.GetLength(1);
            var temp = grid[row, length - 1];
            for (var j = length - 1; j > 0; j--)
            {
                grid[row, j] = grid[row, j - 1];
            }
            grid[row, 0] = temp;
            return grid;
        }

        public static T[,] RotateRowLeftWithWrap<T>(T[,] grid, int row)
        {
            var length = grid.GetLength(1);
            var temp = grid[row, 0];
            for (var j = 0; j < length - 1; j++)
            {
                grid[row, j] = grid[row, j + 1];
            }
            grid[row, length - 1] = temp;
            return grid;
        }

        public static void PrintGrid<T>(T[,] grid)
        {
            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.Write("\n");
            }
        }

        public static bool WithinGrid<T>(List<List<T>> grid, GridLocation<int> coords)
        {
            return GridHelper.WithinGrid<T>(grid, coords.Y, coords.X);
        }
    }
}