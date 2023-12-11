using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    public static class GridHelper
    {
        /* Grid's are in the form of List<List<T>> aka grid[y][x] where  and 
         * x is horizontal axis
         * y is vertical
         * Always reversed from common math notation of "x,y" coordinates
         * 0,0 is the top left corner and y increases as you go down.
         * 
         * in a 3 row, 4 column grid
         * grid.Count == 3 number of rows.
         * grid[0].Count == 4 number of columns
         * */
        /* The other form of Grids are in 2d Arrays aka arr[y,x]
         * x is horizontal axis
         * y is vertical
         * in a 3 row, 4 column grid
         * GetLength(0) would be 3. for the 3 rows
         * GetLength(1) is the 2nd dimension for the 4 columns.
         * */
        public static List<List<int>> GenerateSquareGrid(int n)
        {
            return GenerateSquareGrid<int>(n, 0);
        }
        public static List<List<T>> GenerateSquareGrid<T>(int n, T value)
        {
            var grid = new List<List<T>>();

            for (var i = 0; i < n; i++)
            {
                var listy = new List<T>();
                for (var j = 0; j < n; j++)
                {
                    listy.Add(value);
                }
                grid.Add(listy);
            }

            return grid;
        }

        public static List<List<T>> GenerateCustomGrid<T>(int width, int height, T value)
        {
            var grid = new List<List<T>>();

            for (var i = 0; i < height; i++)
            {
                var listy = new List<T>();
                for (var j = 0; j < width; j++)
                {
                    listy.Add(value);
                }
                grid.Add(listy);
            }

            return grid;
        }

        public static List<Tuple<int,int>> GetPointsBetweenStartAndEndInclusive(List<int> nums)
        {
            return GetPointsBetweenStartAndEndInclusive(nums[0], nums[1], nums[2], nums[3]);
        }
        
        public static List<Tuple<int,int>> GetPointsBetweenStartAndEndInclusive(Tuple<int,int> start, Tuple<int,int> end)
        {
            return GetPointsBetweenStartAndEndInclusive(start.Item1, start.Item2, end.Item1, end.Item2);
        }

        public static List<Tuple<int,int>> GetPointsBetweenStartAndEndInclusive(int startX, int startY, int endX, int endY)
        {
            List<Tuple<int, int>> points = new List<Tuple<int, int>>();
            if (startX == endX)
            {
                if (startY > endY)
                {
                    for (int i = startY; i >= endY; i--)
                    {
                        points.Add(new Tuple<int, int>(startX, i));
                    }
                }
                else
                {
                    for (int i = startY; i <= endY; i++)
                    {
                        points.Add(new Tuple<int, int>(startX, i));
                    }
                }
            }
            else if (startY == endY)
            {
                var min = Math.Min(endX, startX);
                var max = Math.Max(endX, startX);
                for (int i = min; i <= max; i++)
                {
                    points.Add(new Tuple<int, int>(i, startY));
                }
            }
            else
            {
                int x = startX;
                int y = startY;
                int xIncrement = 1;
                int yIncrement = 1;
                if (startX > endX)
                {
                    xIncrement = -1;
                }
                if (startY > endY)
                {
                    yIncrement = -1;
                }
                while (x != endX || y != endY)
                {
                    points.Add(new Tuple<int, int>(x, y));
                    x += xIncrement;
                    y += yIncrement;
                }
                points.Add(new Tuple<int, int>(endX, endY));
            }
            return points;
        }

        public static List<Tuple<int,int>> GetPointsBetweenStartAndEndExclusive(int startX, int startY, int endX, int endY)
        {
            List<Tuple<int, int>> points = new List<Tuple<int, int>>();
            if (startX == endX)
            {
                if (startY > endY)
                {
                    for (int i = startY-1; i > endY; i--)
                    {
                        points.Add(new Tuple<int, int>(startX, i));
                    }
                }
                else
                {
                    for (int i = startY+1; i < endY; i++)
                    {
                        points.Add(new Tuple<int, int>(startX, i));
                    }
                }
            }
            else if (startY == endY)
            {
                if (startX > endX)
                {
                    for (int i = startX-1; i > endX; i--)
                    {
                        points.Add(new Tuple<int, int>(i, startY));
                    }
                }
                else
                {
                    for (int i = startX+1; i < endX; i++)
                    {
                        points.Add(new Tuple<int, int>(i, startY));
                    }
                }
            }
            else
            {
                int x = startX+1;
                int y = startY+1;
                int xIncrement = 1;
                int yIncrement = 1;
                if (startX > endX)
                {
                    xIncrement = -1;
                    x = x-2;
                }
                if (startY > endY)
                {
                    yIncrement = -1;
                    y = y-2;
                }
                while (x != endX || y != endY)
                {
                    points.Add(new Tuple<int, int>(x, y));
                    x += xIncrement;
                    y += yIncrement;
                }
            }
            return points;
        }

        public static List<Tuple<int,int>> GetAdjacentNeighbours<T>(List<List<T>> grid, int x, int y) 
        {
            var adj = new List<Tuple<int,int>>()
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

        public static List<Tuple<int, int>> GetOrthoginalNeighbours<T>(List<List<T>> grid, int x, int y)
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
        public static Tuple<int, int> MoveByOffset(int x, int y, int width, int height, bool wrap)
        {
            return MoveByOffset(x, y, 0, 0, width, height, wrap);
        }

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

        public static Dictionary<Tuple<int,int>, List<Tuple<int,int>>> GridToAdjList<T>(List<List<T>> grid)
        {
            var dict = new Dictionary<Tuple<int,int>, List<Tuple<int,int>>>();
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    var cord = new Tuple<int,int>(x,y);
                    dict.Add(cord, GetAdjacentNeighbours(grid, x, y));
                }
            }

            return dict;
        }

        #region RotateGrids

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
            var temp = grid[length-1, column];
            for (var j = length-1; j > 0; j--)
            {
                grid[j, column] = grid[j - 1, column];
            }
            grid[0, column] = temp;
            return grid;
        }

        public static List<List<T>> RotateColumnDownWithWrap<T>(List<List<T>> grid, int column)
        {
            var length = grid.Count;
            var temp = grid[length - 1][column];
            for (var j = length - 1; j > 0; j--)
            {
                grid[j][column] = grid[j - 1][column];
            }
            grid[0][column] = temp;
            return grid;
        }

        public static T[,] RotateColumnUpWithWrap<T>(T[,] grid, int column)
        {
            var length = grid.GetLength(0);
            var temp = grid[0, column];
            for (var j = 0; j < length-1; j++)
            {
                grid[j, column] = grid[j + 1, column];
            }
            grid[length-1, column] = temp;
            return grid;
        }
        public static List<List<T>> RotateColumnUpWithWrap<T>(List<List<T>> grid, int column)
        {
            var length = grid.Count;
            var temp = grid[0][column];
            for (var j = 0; j < length - 1; j++)
            {
                grid[j][column] = grid[j + 1][column];
            }
            grid[length - 1][column] = temp;
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

        public static List<List<T>> RotateGridDownWithWrap<T>(List<List<T>> grid)
        {
            for (var i = 0; i < grid[0].Count; i++)
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

        public static List<List<T>> RotateGridUpWithWrap<T>(List<List<T>> grid)
        {
            for (var i = 0; i < grid[0].Count; i++)
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
            var temp = grid[row, length-1];
            for (var j = length-1; j > 0; j--)
            {
                grid[row, j] = grid[row, j-1];
            }
            grid[row, 0] = temp;
            return grid;
        }
        public static List<List<T>> RotateRowRightWithWrap<T>(List<List<T>> grid, int row)
        {
            var length = grid[0].Count;
            var temp = grid[row][length - 1];
            for (var j = length - 1; j > 0; j--)
            {
                grid[row][j] = grid[row][j - 1];
            }
            grid[row][0] = temp;
            return grid;
        }

        public static T[,] RotateRowLeftWithWrap<T>(T[,] grid, int row)
        {
            var length = grid.GetLength(1);
            var temp = grid[row, 0];
            for (var j = 0; j < length-1; j++)
            {
                grid[row, j] = grid[row, j+1];
            }
            grid[row, length - 1] = temp;
            return grid;
        }

        public static List<List<T>> RotateRowLeftWithWrap<T>(List<List<T>> grid, int row)
        {
            var length = grid[0].Count;
            var temp = grid[row][0];
            for (var j = 0; j < length - 1; j++)
            {
                grid[row][j] = grid[row][j + 1];
            }
            grid[row][length - 1] = temp;
            return grid;
        }

        #endregion RotateGrids

        public static void PrintGrid<T>(T[,] grid)
        {
            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    Console.Write(grid[i,j]);
                }
                Console.WriteLine();
            }
        }
        public static void PrintGrid<T>(List<List<T>> grid)
        {
            var rows = grid.Count;
            var columns = grid[0].Count;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    Console.Write(grid[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}