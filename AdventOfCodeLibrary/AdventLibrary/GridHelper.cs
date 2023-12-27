using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary.CustomObjects;
using AdventLibrary.PathFinding;

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

        public static List<List<T>> GenerateGrid<T>(int width, int height, T value)
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

        public static List<(int, int)> GetPointsBetweenStartAndEndInclusive(List<int> nums)
        {
            return GetPointsBetweenStartAndEndInclusive(nums[0], nums[1], nums[2], nums[3]);
        }

        public static List<(int, int)> GetPointsBetweenStartAndEndInclusive(Tuple<int,int> start, Tuple<int,int> end)
        {
            return GetPointsBetweenStartAndEndInclusive(start.Item1, start.Item2, end.Item1, end.Item2);
        }

        // rework for y,x
        public static List<(int, int)> GetPointsBetweenStartAndEndInclusive(int startX, int startY, int endX, int endY)
        {
            List<(int, int)> points = new List<(int, int)>();
            if (startX == endX)
            {
                if (startY > endY)
                {
                    for (int i = startY; i >= endY; i--)
                    {
                        points.Add((startX, i));
                    }
                }
                else
                {
                    for (int i = startY; i <= endY; i++)
                    {
                        points.Add((startX, i));
                    }
                }
            }
            else if (startY == endY)
            {
                var min = Math.Min(endX, startX);
                var max = Math.Max(endX, startX);
                for (int i = min; i <= max; i++)
                {
                    points.Add((i, startY));
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
                    points.Add((x, y));
                    x += xIncrement;
                    y += yIncrement;
                }
                points.Add((endX, endY));
            }
            return points;
        }

        // rework for y,x
        public static List<(int, int)> GetPointsBetweenStartAndEndExclusive(int startX, int startY, int endX, int endY)
        {
            List<(int, int)> points = new List<(int, int)>();
            if (startX == endX)
            {
                if (startY > endY)
                {
                    for (int i = startY-1; i > endY; i--)
                    {
                        points.Add((startX, i));
                    }
                }
                else
                {
                    for (int i = startY+1; i < endY; i++)
                    {
                        points.Add((startX, i));
                    }
                }
            }
            else if (startY == endY)
            {
                if (startX > endX)
                {
                    for (int i = startX-1; i > endX; i--)
                    {
                        points.Add((i, startY));
                    }
                }
                else
                {
                    for (int i = startX+1; i < endX; i++)
                    {
                        points.Add((i, startY));
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
                    points.Add((x, y));
                    x += xIncrement;
                    y += yIncrement;
                }
            }
            return points;
        }

        public static List<(int y, int x)> GetAdjacentNeighbours<T>(List<List<T>> grid, int y, int x)
        {
            var adj = new List<(int xOffset, int yOffset)>()
            {
                (0, -1),
                (0, 1),
                (1, 0),
                (-1, 0),
            };
            List<(int, int)> neighbours = new List<(int, int)>();

            for (int i = 0; i < 4; i++)
            {
                var newNeighbour = MoveByOffset(y, x, adj[i].yOffset, adj[i].xOffset, grid[0].Count, grid.Count, false);

                if (!neighbours.Contains(newNeighbour) && (y != newNeighbour.y || x != newNeighbour.x))
                {
                    neighbours.Add(newNeighbour);
                }
            }
            return neighbours;
        }

        public static List<(int, int)> GetOrthoginalNeighbours<T>(List<List<T>> grid, int x, int y)
        {
            List<(int, int)> neighbours = new List<(int, int)>();
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
                    neighbours.Add((i, j));
                }
            }
            return neighbours;
        }

        /* Assume offset already applied*/
        public static (int y, int x) MoveByOffset(int y, int x, int width, int height, bool wrap)
        {
            return MoveByOffset(y, x, 0, 0, width, height, wrap);
        }

        public static (int y, int x) MoveByOffset(int y, int x, int yOffset, int xOffset, int width, int height, bool wrap)
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

            return (newY, newX);
        }

        // everything in the form of (y,x);
        public static Dictionary<(int, int), List<(int, int)>> GridToAdjList<T>(List<List<T>> grid)
        {
            var dict = new Dictionary<(int, int), List<(int, int)>>();
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    dict.Add((y, x), GetAdjacentNeighbours(grid, x, y));
                }
            }

            return dict;
        }

        #region RotateGrids
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

        public static List<List<T>> RotateGridDownWithWrap<T>(List<List<T>> grid)
        {
            for (var i = 0; i < grid[0].Count; i++)
            {
                grid = RotateColumnDownWithWrap(grid, i);
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

        // Get distance using Pythagoras's theorem
        //0,0 to 2,2 would be sqrt(8)/~2.82
        // https://en.wikipedia.org/wiki/Chebyshev_distance#Properties
        public static double EuclidianDistance((int,int) a, (int,int) b)
        {
            var x = Math.Pow(a.Item1 - b.Item1, 2);
            var y = Math.Pow(a.Item2 - b.Item2, 2);
            return Math.Pow(x + y, .5);
        }

        public static double EuclidianDistance((int, int, int) a, (int, int, int) b)
        {
            var x = Math.Pow(a.Item1 - b.Item1, 2);
            var y = Math.Pow(a.Item2 - b.Item2, 2);
            var z = Math.Pow(a.Item3 - b.Item3, 2);
            return Math.Pow(x + y + z, .5);
        }

        //Manhattan/taxicab distance aka routing through the grid system
        //distance between 0,0 and 2,2 would be 4.
        // https://en.wikipedia.org/wiki/Chebyshev_distance#Properties
        public static int TaxicabDistance((int, int) a, (int, int) b)
        {
            var x = Math.Abs(a.Item1 - b.Item1);
            var y = Math.Abs(a.Item2 - b.Item2);
            return x+y;
        }

        public static int TaxicabDistance((int, int, int) a, (int, int, int) b)
        {
            var x = Math.Abs(a.Item1 - b.Item1);
            var y = Math.Abs(a.Item2 - b.Item2);
            var z = Math.Abs(a.Item3 - b.Item3);
            return x + y + z;
        }

        // Getting distance allowing diagonal moves
        // distance 0,0 to 2,2 is 2.
        // https://en.wikipedia.org/wiki/Chebyshev_distance#Properties
        public static int ChebyshevDistance((int, int) a, (int, int) b)
        {
            var x = Math.Abs(a.Item1 - b.Item1);
            var y = Math.Abs(a.Item2 - b.Item2);
            return Math.Max(x,y);
        }

        public static int ChebyshevDistance((int, int, int) a, (int, int, int) b)
        {
            var x = Math.Abs(a.Item1 - b.Item1);
            var y = Math.Abs(a.Item2 - b.Item2);
            var z = Math.Abs(a.Item3 - b.Item3);
            return Math.Min(x, Math.Max(y, z));
        }

        public static (int y, int x) GetPointWhere<T>(List<List<T>> grid, T value)
        {
            return GetPointWhere(grid, x => x.Equals(value));
        }

        public static (int y, int x) GetPointWhere<T>(List<List<T>> grid, Predicate<T> pred)
        {
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[y].Count; x++)
                {
                    if (pred(grid[y][x]))
                    {
                        return (y, x);
                    }
                }
            }
            return (-1,-1);
        }

        public static List<(int, int)> GetPointsWhere<T>(List<List<T>> grid, T value)
        {
            return GetPointsWhere(grid, x => x.Equals(value));
        }

        public static List<(int,int)> GetPointsWhere<T>(List<List<T>> grid, Predicate<T> pred)
        {
            var values = new List<(int, int)>();
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[y].Count; x++)
                {
                    if (pred(grid[y][x]))
                    {
                        values.Add((y,x));
                    }
                }
            }
            return values;
        }

        public static int GetCountWhere<T>(List<List<T>> grid, T value)
        {
            return GetCountWhere(grid, x => x.Equals(value));
        }

        public static int GetCountWhere<T>(List<List<T>> grid, Predicate<T> pred)
        {
            var total = 0;
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[y].Count; x++)
                {
                    if (pred(grid[y][x]))
                    {
                        total++;
                    }
                }
            }
            return total;
        }
        public static List<string> GetColumns(this List<string> grid)
        {
            var result = GetColumns(grid.Select(x => x.ToList()).ToList());
            return CharGridToStringList(result);
        }

        public static List<List<T>> GetColumns<T>(this List<List<T>> grid)
        {
            var columns = new List<List<T>>();
            for (var i = 0; i < grid[0].Count; i++)
            {
                columns.Add(new List<T>());
            }
            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[i].Count; j++)
                {
                    columns[j].Add(grid[i][j]);
                }
            }
            return columns;
        }

        public static List<string> CharGridToStringList(List<List<char>> grid)
        {
            return grid.Select(x => x.Stringify()).ToList();
        }

        public static bool WithinGrid<T>(List<List<T>> grid, (int,int) coords)
        {
            return WithinGrid<T>(grid, coords.Item1, coords.Item2);
        }

        public static bool WithinGrid<T>(List<List<T>> grid, LocationTuple<int> coords)
        {
            return WithinGrid<T>(grid, coords.Item1, coords.Item2);
        }

        public static bool WithinGrid<T>(List<List<T>> grid, int y, int x)
        {
            return y >= 0 &&
            y < grid.Count &&
            x >= 0 &&
            x < grid[y].Count;
        }

        /* Pretty clunky method but it works faily fast.
         * 1. Assume you have a grid that has a loop/connected path going through it
         * 2. Convert to a numerical grid where any of the special chars are 10000 else 0
         *      This is basically putting "walls" in the grid
         * 3. Wrap the outside in an extra layer to handle cases where things connect to the wall
         * 4. Throw Dijkstra's at it.
         * 5. Analyze the results, anything that has a weight of 0 doesn't cross a wall and is outside the interior.
         *      Anything else must be in the interior.
         * NOTE: this assume the loop fully encompases everything and doesn't use the edge of the graph as a wall.
         *      This could probably be achieved by wrapping in another wall layer, and then wrapping in a 0 layer or something. */
        public static List<List<T>> FillInterior<T>(List<List<T>> grid, T specialCharacter)
        {
            var numGrid = new List<List<int>>();
            for (var i = 0; i < grid.Count; i++)
            {
                numGrid.Add(new List<int>());
                for (var j = 0; j < grid[i].Count; j++)
                {
                    if (grid[i][j].Equals(specialCharacter))
                    {
                        numGrid[i].Add(10000);
                    }
                    else
                    {
                        numGrid[i].Add(0);
                    }
                }
            }
            numGrid.Add(Enumerable.Repeat(0, numGrid[0].Count).ToList());
            numGrid.Insert(0, Enumerable.Repeat(0, numGrid[0].Count).ToList());
            foreach (var item in numGrid)
            {
                item.Insert(0, 0);
                item.Add(0);
            }
            var distances = DijkstraTuple.Search(numGrid, Tuple.Create(0, 0));
            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[i].Count; j++)
                {
                    if (!grid[i][j].Equals(specialCharacter))
                    {
                        if (distances[Tuple.Create(j + 1, i + 1)] >= 10000)
                        {
                            grid[i][j] = specialCharacter;
                        }
                    }
                }
            }
            return grid;
        }
    }
}