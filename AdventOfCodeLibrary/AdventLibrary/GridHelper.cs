using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    public static class GridHelper
    {
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

        public static List<Tuple<int,int>> GetAdjacentNeighbours(List<List<int>> grid, int x, int y) 
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

        public static List<Tuple<int,int>> GetOrthoginalNeighbours(List<List<int>> grid, int x, int y) 
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

        public static List<Tuple<int,int>> GetOrthoginalNeighbours2(List<List<int>> grid, int x, int y) 
        {
            List<Tuple<int, int>> neighbours = new List<Tuple<int, int>>();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == x && j == y)
                    {
                        continue;
                    }

                    var newNeighbour = MoveByOffset(x, y, j, i, grid[0].Count, grid.Count, false);

                    if (!neighbours.Contains(newNeighbour) && (x != newNeighbour.Item1 || y != newNeighbour.Item2))
                    {
                        neighbours.Add(newNeighbour);
                    }
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

        // rotate grid down up, left right for row and column
        /*
        public static int[,] RotateColumnDownWithWrap(int[,] grid, int column)
        {
            int[,] newGrid = new int[,] {};
            Array.Copy(grid, newGrid, grid.Length);
            var temp  = grid[grid.GetLength(0)-1, column];
            for (int i = 1; i < grid.GetLength(0); i++)
            {
                newGrid[i, column] = grid[i-1, column];
            }
            newGrid[0, column] = temp;
            return newGrid;
        }
        */
    }
}