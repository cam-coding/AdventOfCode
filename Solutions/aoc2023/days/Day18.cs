using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using AdventLibrary.PathFinding;
using System.Collections.Immutable;

namespace aoc2023
{
    public class Day18 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t', '(', ')' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var dict = new Dictionary<string, GridLocation<int>>()
            {
                { "R", Directions.Right},
                { "L", Directions.Left},
                { "D", Directions.Down},
                { "U", Directions.Up},
            };
            var maxY = 0;
            var maxX = 0;
            var minY = 0;
            var minX = 0;
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var walker = new GridWalker(new GridLocation<int>(0, 0), Directions.Right);
            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars).ToList().GetRealStrings(delimiterChars);
                walker.Direction = dict[tokens[0]];
                var speed = int.Parse(tokens[1]);
                for (var i = 0; i < speed; i++)
                {
                    walker.Walk();
                    maxY = Math.Max(maxY, walker.Current.Y);
                    maxX = Math.Max(maxX, walker.Current.X);

                    minY = Math.Min(minY, walker.Current.Y);
                    minX = Math.Min(minX, walker.Current.X);
                }
            }
            var grid = new List<List<char>>();
            var shiftX = 0 - minX;
            var shiftY = 0 - minY;
            var numGrid = new List<List<int>>();
            for (var i = 0; i <= maxY + shiftY; i++)
            {
                grid.Add(new List<char>());
                numGrid.Add(new List<int>());
                for (var j = 0; j <= maxX + shiftX; j++)
                {
                    grid[i].Add('.');
                    numGrid[i].Add(0);
                }
            }
            var start = walker.Path.First();
            var end = walker.Path.Last();
            foreach (var item in walker.Path[1..])
            {
                grid[item.Item1.Y + shiftY][item.Item1.X + shiftX] = '#';
                numGrid[item.Item1.Y + shiftY][item.Item1.X + shiftX] = 10000;
            }
            numGrid.Add(Enumerable.Repeat(0, numGrid[0].Count).ToList());
            numGrid.Insert(0, Enumerable.Repeat(0, numGrid[0].Count).ToList());
            foreach (var item in numGrid)
            {
                item.Insert(0, 0);
                item.Add(0);
            }
            var distances = DijkstraTuple.Search(numGrid, Tuple.Create(0, 0)).ToImmutableSortedDictionary();
            for (var i = 0; i < maxY + shiftY; i++)
            {
                for (var j = 0; j < maxX + shiftX; j++)
                {
                    if (grid[i][j] != '#')
                    {
                        if (distances[Tuple.Create(j + 1, i + 1)] >= 10000)
                        {
                            grid[i][j] = '@';
                        }
                    }
                }
            }
            var count = 0;
            for (var i = 0; i <= maxY + shiftY; i++)
            {
                for (var j = 0; j <= maxX + shiftX; j++)
                {
                    if (grid[i][j] == '#' || grid[i][j] == '@')
                    {
                        count++;
                    }
                }
            }
            //GridHelper.PrintGrid(grid);
            // PrintGrid2(grid);
            return count;
        }

        private object Part2()
        {
            var dict = new Dictionary<string, GridLocation<int>>()
            {
                { "0",Directions.Right},
                { "1",Directions.Down},
                { "2",Directions.Left},
                { "3",Directions.Up},
            };
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var listy = new List<GridLocation<int>>();
            var current = new GridLocation<int>(0, 0);
            var edgeLength = 1;

            // #70c710 into 461937 and Right
            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars).ToList().GetRealStrings(delimiterChars);
                var specialToken = tokens[^1];
                var hex = specialToken.Substring(0, specialToken.Length - 1);
                var hexNum = hex.ConvertToHex();
                var dirInt = dict[specialToken[^1].ToString()];
                var dir = new GridLocation<int>(dirInt.X, dirInt.Y);

                var next = current + (dir * hexNum);
                listy.Add(next);
                current = next;
                edgeLength += hexNum;
            }
            var area = MathHelper.ShoelaceArea(listy);
            // https://en.wikipedia.org/wiki/Pick%27s_theorem
            // area + "circumference" /2 + 1 = total area for this case
            return area + edgeLength / 2 + 1;
        }

        public static void PrintGrid2<T>(List<List<T>> grid)
        {
            var rows = grid.Count;
            var columns = grid[0].Count;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 150; j < columns; j++)
                {
                    Console.Write(grid[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}