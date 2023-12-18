using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AdventLibrary;
using AdventLibrary.CustomObjects;
using AdventLibrary.Helpers;
using AdventLibrary.PathFinding;

namespace aoc2023
{
    public class Day18: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var dict = new Dictionary<string, LocationTuple<int>>()
            {
                { "R", GridWalker.Right},
                { "L", GridWalker.Left},
                { "D", GridWalker.Down},
                { "U", GridWalker.Up},
            };
            var maxY = 0;
            var maxX = 0;
            var minY = 0;
            var minX = 0;
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var walker = new GridWalker((0, 0), GridWalker.Right);
            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars).ToList().OnlyRealStrings(delimiterChars);
                walker.Direction = dict[tokens[0]];
                var speed = int.Parse(tokens[1]);
                for(var i =0; i < speed; i++)
                {
                    walker.Walk();
                    maxY = Math.Max(maxY, walker.Current.Item1);
                    maxX = Math.Max(maxX, walker.Current.Item2);

                    minY = Math.Min(minY, walker.Current.Item1);
                    minX = Math.Min(minX, walker.Current.Item2);
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
                grid[item.Item1.Item1 + shiftY][item.Item1.Item2 + shiftX] = '#';
                numGrid[item.Item1.Item1 + shiftY][item.Item1.Item2 + shiftX] = 10000;
            }
            numGrid.Add(Enumerable.Repeat(0, numGrid[0].Count).ToList());
            numGrid.Insert(0, Enumerable.Repeat(0, numGrid[0].Count).ToList());
            foreach (var item in numGrid)
            {
                item.Insert(0, 0);
                item.Add(0);
            }
            var distances = Dijkstra.Search(numGrid, Tuple.Create(0, 0)).ToImmutableSortedDictionary();
            for (var i = 0; i < maxY + shiftY; i++)
            {
                for (var j = 0; j < maxX + shiftX; j++)
                {
                    if (grid[i][j] != '#')
                    {
                        /*
                        var bools = new List<bool> { false, false, false ,false };
                        for (var k = i; k >= 0; k--)
                        {
                            if (grid[k][j] == '#')
                            {
                                bools[0] = true;
                                break;
                            }
                        }
                        for (var k = i; k < grid.Count; k++)
                        {
                            if (grid[k][j] == '#')
                            {
                                bools[1] = true;
                                break;
                            }
                        }
                        for (var k = j; k >= 0; k--)
                        {
                            if (grid[i][k] == '#')
                            {
                                bools[2] = true;
                                break;
                            }
                        }
                        for (var k = j; k < grid[0].Count; k++)
                        {
                            if (grid[i][k] == '#')
                            {
                                bools[3] = true;
                                break;
                            }
                        }
                        if (bools.All(x => x))
                        {
                            grid[i][j] = '@';
                        }*/
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
            PrintGrid2(grid);
            return count;
        }
        
        private object Part2()
        {
            return 0;
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