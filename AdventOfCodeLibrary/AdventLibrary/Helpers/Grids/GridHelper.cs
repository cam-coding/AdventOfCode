using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary.CustomObjects;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;
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

        /* Assume offset already applied*/

        public static GridLocation<int> MoveByOffset(int y, int x, int width, int height, bool wrap)
        {
            return MoveByOffset(y, x, 0, 0, width, height, wrap);
        }

        public static GridLocation<int> MoveByOffset(int y, int x, int yOffset, int xOffset, int width, int height, bool wrap)
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
            return new GridLocation<int>(newX, newY);
        }

        public static Dictionary<GridLocation<int>, List<GridLocation<int>>> GridToAdjList<T>(List<List<T>> grid)
        {
            var gridObject = new GridObject<T>(grid);
            var dict = new Dictionary<GridLocation<int>, List<GridLocation<int>>>();
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    var loc = new GridLocation<int>(x, y);
                    dict.Add(loc, gridObject.GetOrthogonalNeighbours(loc));
                }
            }

            return dict;
        }

        public static class Rotations
        {
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
                Console.Write("\n");
            }
        }

        public static class Distances
        {
            /* Chebyshev distance aka king's move aka 8 directions aka all directions = 1 movement
             * Euclidian distance aka direct line distance ignoring grid movement rules.
             *  Uses Pythagoras' theorem
             * Taxicab distance aka manhattan distance aka 4 directions aka up, down, left, right = 1 movement
             * https://en.wikipedia.org/wiki/Chebyshev_distance#Properties
             * */

            // Get distance using Pythagoras's theorem
            //0,0 to 2,2 would be sqrt(8)/~2.82
            public static double EuclidianDistance((int, int) a, (int, int) b)
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

            public static double EuclidianDistance(GridLocation<int> a, GridLocation<int> b)
            {
                return EuclidianDistance((a.X, a.Y), (b.X, b.Y));
            }

            //Manhattan/taxicab distance aka routing through the grid system
            //distance between 0,0 and 2,2 would be 4.
            public static int TaxicabDistance((int, int) a, (int, int) b)
            {
                var x = Math.Abs(a.Item1 - b.Item1);
                var y = Math.Abs(a.Item2 - b.Item2);
                return x + y;
            }

            public static int TaxicabDistance((int, int, int) a, (int, int, int) b)
            {
                var x = Math.Abs(a.Item1 - b.Item1);
                var y = Math.Abs(a.Item2 - b.Item2);
                var z = Math.Abs(a.Item3 - b.Item3);
                return x + y + z;
            }

            public static double TaxicabDistance(GridLocation<int> a, GridLocation<int> b)
            {
                return TaxicabDistance((a.X, a.Y), (b.X, b.Y));
            }

            // Getting distance allowing diagonal moves
            // distance 0,0 to 2,2 is 2.
            public static int ChebyshevDistance((int, int) a, (int, int) b)
            {
                var x = Math.Abs(a.Item1 - b.Item1);
                var y = Math.Abs(a.Item2 - b.Item2);
                return Math.Max(x, y);
            }

            public static int ChebyshevDistance((int, int, int) a, (int, int, int) b)
            {
                var x = Math.Abs(a.Item1 - b.Item1);
                var y = Math.Abs(a.Item2 - b.Item2);
                var z = Math.Abs(a.Item3 - b.Item3);
                return Math.Min(x, Math.Max(y, z));
            }

            public static double ChebyshevDistance(GridLocation<int> a, GridLocation<int> b)
            {
                return ChebyshevDistance((a.X, a.Y), (b.X, b.Y));
            }
        }

        public static class GetPointsIn
        {
            // assumes a straight line between the two points
            public static List<GridLocation<int>> GetPointsBetween(GridLocation<int> start, GridLocation<int> end, bool inclusive = false)
            {
                return GetPointsBetween(start.X, start.Y, end.X, end.Y, inclusive);
            }

            public static List<GridLocation<int>> GetPointsBetween(int startX, int startY, int endX, int endY, bool inclusive = false)
            {
                if (!PointsFormStraightLine(startX, startY, endX, endY))
                {
                    throw new ArgumentException("Points do not form a straight line");
                }

                if (inclusive)
                    return GetPointsBetweenStartAndEndInclusive(startX, startY, endX, endY);
                else
                    return GetPointsBetweenStartAndEndExclusive(startX, startY, endX, endY);
            }

            public static List<GridLocation<int>> GetPointsWithinRectangle(List<GridLocation<int>> corners)
            {
                if (!PointsFormRectangle(corners))
                {
                    throw new ArgumentException("Points do not form a rectangle");
                }

                var xMax = corners.Max(x => x.X);
                var xMin = corners.Min(x => x.X);
                var yMax = corners.Max(y => y.Y);
                var yMin = corners.Min(y => y.Y);

                var points = new List<GridLocation<int>>();
                for (var y = yMin; y <= yMax; y++)
                {
                    for (var x = xMin; x <= xMax; x++)
                    {
                        points.Add(new GridLocation<int>(x, y));
                    }
                }
                return points;
            }

            private static List<GridLocation<int>> GetPointsBetweenStartAndEndInclusive(GridLocation<int> start, GridLocation<int> end)
            {
                return GetPointsBetweenStartAndEndInclusive(start.X, start.Y, end.X, end.Y);
            }

            private static List<GridLocation<int>> GetPointsBetweenStartAndEndInclusive(int startX, int startY, int endX, int endY)
            {
                List<GridLocation<int>> points = new List<GridLocation<int>>();
                if (startX == endX)
                {
                    if (startY > endY)
                    {
                        for (int i = startY; i >= endY; i--)
                        {
                            points.Add(new GridLocation<int>(startX, i));
                        }
                    }
                    else
                    {
                        for (int i = startY; i <= endY; i++)
                        {
                            points.Add(new GridLocation<int>(startX, i));
                        }
                    }
                }
                else if (startY == endY)
                {
                    var min = Math.Min(endX, startX);
                    var max = Math.Max(endX, startX);
                    for (int i = min; i <= max; i++)
                    {
                        points.Add(new GridLocation<int>(i, startY));
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
                        points.Add(new GridLocation<int>(x, y));
                        x += xIncrement;
                        y += yIncrement;
                    }
                    points.Add(new GridLocation<int>(endX, endY));
                }
                return points;
            }

            private static List<GridLocation<int>> GetPointsBetweenStartAndEndExclusive(GridLocation<int> start, GridLocation<int> end)
            {
                return GetPointsBetweenStartAndEndExclusive(start.X, start.Y, end.X, end.Y);
            }

            private static List<GridLocation<int>> GetPointsBetweenStartAndEndExclusive(int startX, int startY, int endX, int endY)
            {
                List<GridLocation<int>> points = new List<GridLocation<int>>();
                if (startX == endX)
                {
                    if (startY > endY)
                    {
                        for (int i = startY - 1; i > endY; i--)
                        {
                            points.Add(new GridLocation<int>(startX, i));
                        }
                    }
                    else
                    {
                        for (int i = startY + 1; i < endY; i++)
                        {
                            points.Add(new GridLocation<int>(startX, i));
                        }
                    }
                }
                else if (startY == endY)
                {
                    if (startX > endX)
                    {
                        for (int i = startX - 1; i > endX; i--)
                        {
                            points.Add(new GridLocation<int>(i, startY));
                        }
                    }
                    else
                    {
                        for (int i = startX + 1; i < endX; i++)
                        {
                            points.Add(new GridLocation<int>(i, startY));
                        }
                    }
                }
                else
                {
                    int x = startX + 1;
                    int y = startY + 1;
                    int xIncrement = 1;
                    int yIncrement = 1;
                    if (startX > endX)
                    {
                        xIncrement = -1;
                        x = x - 2;
                    }
                    if (startY > endY)
                    {
                        yIncrement = -1;
                        y = y - 2;
                    }
                    while (x != endX || y != endY)
                    {
                        points.Add(new GridLocation<int>(x, y));
                        x += xIncrement;
                        y += yIncrement;
                    }
                }
                return points;
            }
        }

        public static bool PointsFormStraightLine(int startX, int startY, int endX, int endY)
        {
            if (startX == endX || startY == endY)
            {
                return true;
            }
            return Math.Abs(startX - endX) == Math.Abs(startY - endY);
        }

        public static bool PointsFormRectangle(List<GridLocation<int>> corners)
        {
            if (corners.Count != 4 || corners.Distinct().Count() == 4)
            {
                return false;
            }
            var xValues = corners.Select(x => x.X).Distinct().ToList();
            var yValues = corners.Select(x => x.Y).Distinct().ToList();
            return xValues.Count == 2 && yValues.Count == 2;
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

        public static bool WithinGrid<T>(List<List<T>> grid, (int, int) coords)
        {
            return WithinGrid<T>(grid, coords.Item1, coords.Item2);
        }

        public static bool WithinGrid<T>(List<List<T>> grid, GridLocation<int> coords)
        {
            return WithinGrid<T>(grid, coords.Y, coords.X);
        }

        public static bool WithinGrid<T>(List<List<T>> grid, int y, int x)
        {
            return y >= 0 &&
            y < grid.Count &&
            x >= 0 &&
            x < grid[y].Count;
        }

        public static int GetWidth<T>(List<List<T>> grid)
        {
            return grid[0].Count;
        }

        public static int GetHeight<T>(List<List<T>> grid)
        {
            return grid.Count;
        }

        public static int GetMaxY<T>(List<List<T>> grid)
        {
            return grid.Count - 1;
        }
        public static int GetMaxX<T>(List<List<T>> grid)
        {
            return grid[0].Count - 1;
        }

        public static void RotateColumnDownWithWrap<T>(List<List<T>> grid, int column)
        {
            var temp = grid[GetMaxY(grid)][column];
            for (var currentRow = GetMaxY(grid); currentRow > 0; currentRow--)
            {
                var val = grid[currentRow - 1][column];
                grid[currentRow][column] = val;
            }
            grid[0][column] = temp;
        }

        public static void RotateColumnUpWithWrap<T>(List<List<T>> grid, int column)
        {
            var temp = grid[0][column];
            for (var currentRow = 0; currentRow < GetMaxY(grid); currentRow++)
            {
                var val = grid[currentRow + 1][column];
                grid[currentRow][column] = val;
            }
            grid[GetMaxY(grid)][column] = temp;
        }

        public static void RotateAllColumnsDownWithWrap<T>(List<List<T>> grid)
        {
            for (var i = 0; i < GetWidth(grid); i++)
            {
                RotateColumnDownWithWrap(grid, i);
            }
        }

        public static void RotateAllColumnsUpWithWrap<T>(List<List<T>> grid)
        {
            for (var i = 0; i < GetWidth(grid); i++)
            {
                RotateColumnUpWithWrap(grid, i);
            }
        }

        public static void RotateAllRowsLeftWithWrap<T>(List<List<T>> grid)
        {
            for (var i = 0; i < GetHeight(grid); i++)
            {
                RotateRowLeftWithWrap(grid, i);
            }
        }

        public static void RotateAllRowsRightWithWrap<T>(List<List<T>> grid)
        {
            for (var i = 0; i < GetHeight(grid); i++)
            {
                RotateRowRightWithWrap(grid, i);
            }
        }

        public static void RotateRowRightWithWrap<T>(List<List<T>> grid, int row)
        {
            var temp = grid[row][GetMaxX(grid)];
            for (var currentColumn = GetMaxY(grid); currentColumn > 0; currentColumn--)
            {
                var val = grid[row][currentColumn-1];
                grid[row][currentColumn] = val;
            }
            grid[row][0] = temp;
        }

        public static void RotateRowLeftWithWrap<T>(List<List<T>> grid, int row)
        {
            var temp = grid[row][0];
            for (var currentColumn = 0; currentColumn < GetMaxY(grid); currentColumn++)
            {
                var val = grid[row][currentColumn + 1];
                grid[row][currentColumn] = val;
            }
            grid[row][GetMaxX(grid)] = temp;
        }

        public static void FlipAboutHorizontal<T>(List<List<T>> grid)
        {
            ReverseGridColumns(grid);
        }

        public static void FlipAboutVertical<T>(List<List<T>> grid)
        {
            ReverseGridRows(grid);
        }

        public static void ReverseGridRows<T>(List<List<T>> grid)
        {
            foreach (var row in grid)
            {
                row.Reverse();
            }
        }

        public static void ReverseGridColumns<T>(List<List<T>> grid)
        {
            for (var currentColumn = 0; currentColumn <= GetMaxX(grid); currentColumn++)
            {
                ReverseColumn(grid, currentColumn);
            }
        }

        public static void ReverseColumn<T>(List <List<T>> grid, int column)
        {
            int start = 0;
            int end = GetMaxY(grid);

            while (start < end)
            {
                var temp = grid[start][column];
                grid[start][column] = grid[end][column];
                grid[end][column] = temp;

                start++;
                end--;
            }
        }

        /* X..      x.x
         * .X.      .x.
         * X..      ...
         **/
        public static List<List<T>> RotateGridRight<T>(List<List<T>> grid)
        {
            grid = TransposeGrid(grid);
            ReverseGridRows(grid);
            return grid;
        }

        public static List<List<T>> RotateGridLeft<T>(List<List<T>> grid)
        {
            ReverseGridRows(grid);
            grid = TransposeGrid(grid);
            return grid;
        }

        public static void RotateGrid180<T>(List<List<T>> grid)
        {
            ReverseGridRows(grid);
            ReverseGridColumns(grid);
        }

        public static List<List<T>> TransposeGrid<T>(List<List<T>> grid)
        {
            var newGrid = new List<List<T>>();
            var width = grid[0].Count;
            var height = grid.Count;
            for (var y = 0; y < width; y++)
            {
                var newRow = new List<T>();
                newRow.FillEmptyListWithValue(default(T), height);
                newGrid.Add(newRow);
            }
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[y].Count; x++)
                {
                    newGrid[x][y] = grid[y][x];
                }
            }

            return newGrid;
        }

        /* Pretty clunky method but it works fairly fast.
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