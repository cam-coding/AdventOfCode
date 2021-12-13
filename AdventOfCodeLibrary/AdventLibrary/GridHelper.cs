using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    public static class GridHelper
    {
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

        public static List<Tuple<int,int>> GetAdjacent(List<List<int>> grid, int x, int y) 
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