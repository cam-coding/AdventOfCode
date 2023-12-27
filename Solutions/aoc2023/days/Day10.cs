using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using AdventLibrary;
using AdventLibrary.CustomObjects;
using AdventLibrary.Helpers;
using AdventLibrary.PathFinding;

namespace aoc2023
{
    public class Day10: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        private Dictionary<(char, int, int), (int, int)> characters = new Dictionary<(char, int, int), (int, int)>()
        {
            {('|', 0, -1), (0,1)},
            {('|', 0, 1), (0,-1)},
            {('-', -1, 0), (1,0)},
            {('-', 1, 0), (-1,0)},
            {('J', 0, -1), (-1,0)},
            {('J', -1, 0), (0,-1)},
            {('L', 0, -1), (1,0)},
            {('L', 1, 0), (0,-1)},
            {('7', 0, 1), (-1,0)},
            {('7', -1, 0), (0,1)},
            {('F', 0, 1), (1,0)},
            {('F', 1, 0), (0,1)},
        };

        private Dictionary<(char, LocationTuple<int>), LocationTuple<int>> characters2 = new Dictionary<(char, LocationTuple<int>), LocationTuple<int>>()
        {
            {('|', GridWalker.Up), GridWalker.Up},
            {('|', GridWalker.Down), GridWalker.Down},
            {('-', GridWalker.Left), GridWalker.Left},
            {('-', GridWalker.Right), GridWalker.Right },
            {('J', GridWalker.Right), GridWalker.Up },
            {('J', GridWalker.Down), GridWalker.Left },
            {('L', GridWalker.Down), GridWalker.Right },
            {('L', GridWalker.Left), GridWalker.Up },
            {('7', GridWalker.Right), GridWalker.Down },
            {('7', GridWalker.Up), GridWalker.Left },
            {('F', GridWalker.Left), GridWalker.Down },
            {('F', GridWalker.Up), GridWalker.Right },
        };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var startingX = 0;
            var startingY = 0;
            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (grid[i][j] == 'S')
                    {
                        startingX = j;
                        startingY = i;
                    }
                }
            }

            var maxLoop = 0;
            var tests = new List<GridWalker>()
            {
                new GridWalker((startingY, startingX), GridWalker.Up),
                new GridWalker((startingY, startingX), GridWalker.Down),
                new GridWalker((startingY, startingX), GridWalker.Left),
                new GridWalker((startingY, startingX), GridWalker.Right),
            };

            foreach (var item in tests)
            {
                item.Walk();
                item.OutOfBounds = !GridHelperWeirdTypes.WithinGrid(grid, item.Current);
                var currentLoop = 0;
                while (!item.Looping && !item.OutOfBounds)
                {
                    if (grid[item.Y][item.X] == '.')
                    {
                        break;
                    }
                    var cha = grid[item.Y][item.X];
                    if (!characters2.ContainsKey((cha, item.Direction)))
                    {
                        break;
                    }
                    item.Direction = characters2[(cha, item.Direction)];
                    item.Walk();
                    item.OutOfBounds = !GridHelperWeirdTypes.WithinGrid(grid, item.Current);
                }
                maxLoop = Math.Max(item.UniqueLocationsVisited, maxLoop);
            }
            return maxLoop/2;
        }

        private object Part2()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var total = 1000000;
            var counter = 0;
            var startingX = 0;
            var startingY = 0;
            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (grid[i][j] == 'S')
                    {
                        startingX = j;
                        startingY = i;
                    }
                }
            }

            var maxLoop = 0;

            var neighs = GridHelperWeirdTypes.GetAdjacentNeighboursTuple(grid, startingX, startingY);
            var bestX = 0;
            var bestY = 0;

            foreach (var item in neighs)
            {
                var previousX = startingX;
                var previousY = startingY;
                var currentX = item.Item1;
                var currentY = item.Item2;
                var currentLoop = 0;
                while (grid[currentY][currentX] != 'S')
                {
                    if (grid[currentY][currentX] == '.')
                    {
                        break;
                    }
                    var cha = grid[currentY][currentX];
                    if (!characters.ContainsKey((cha, previousX - currentX, previousY - currentY)))
                    {
                        break;
                    }
                    var nextDest = characters[(cha, previousX - currentX, previousY - currentY)];
                    previousX = currentX;
                    previousY = currentY;
                    currentX = previousX + nextDest.Item1;
                    currentY = previousY + nextDest.Item2;
                    currentLoop++;
                }
                if (grid[currentY][currentX] == 'S')
                {
                    currentLoop++;
                }
                if (currentLoop > maxLoop)
                {
                    bestX = item.Item1;
                    bestY = item.Item2;
                    maxLoop = Math.Max(currentLoop, maxLoop);
                }
            }
            var previousX2 = startingX;
            var previousY2 = startingY;
            var currentX2 = bestX;
            var currentY2 = bestY;
            var theLoop = new HashSet<(int, int)>();
            while (grid[currentY2][currentX2] != 'S')
            {
                if (grid[currentY2][currentX2] == '.')
                {
                    break;
                }
                theLoop.Add((currentX2, currentY2));
                var cha = grid[currentY2][currentX2];
                if (!characters.ContainsKey((cha, previousX2 - currentX2, previousY2 - currentY2)))
                {
                    break;
                }
                var nextDest = characters[(cha, previousX2 - currentX2, previousY2 - currentY2)];
                previousX2 = currentX2;
                previousY2 = currentY2;
                currentX2 = previousX2 + nextDest.Item1;
                currentY2 = previousY2 + nextDest.Item2;
            }
            theLoop.Add((currentX2, currentY2));
            var grid2 = grid.Clone2dList();

            //Make a new grid that is the size of the current grid + the in betweens
            // aka 4x4 becomes a 7x7
            var grid3 = new List<List<int>>();
            for (var i = 0; i < 2*grid2.Count-1; i++)
            {
                var blah = Enumerable.Repeat(0, 2*grid[0].Count-1).ToList();
                grid3.Add(blah);
            }

            for (var i = 0; i < grid2.Count; i++)
            {
                for (var j = 0; j < grid2[0].Count; j++)
                {
                    if (theLoop.Contains((j, i)))
                    {
                        // if the current spot is part of the loop, mark it's spot and the pieces it connects as blocked
                        var cur = grid2[i][j];
                        if (cur == '-' || cur == 'L' || cur == 'F' || cur == 'J' || cur == '|' || cur == '7' || cur == 'F' || cur == 'S')
                        {
                            grid3[i*2][j*2] = 10000;
                        }
                        if (cur == '-')
                        {
                            grid3[i * 2][j * 2-1] = 10000;
                            grid3[i * 2][j * 2 + 1] = 10000;
                        }
                        if (cur == '|')
                        {
                            grid3[i * 2-1][j * 2] = 10000;
                            grid3[i * 2+1][j * 2] = 10000;
                        }
                        if (cur == 'L')
                        {
                            grid3[i * 2-1][j * 2] = 10000;
                            grid3[i * 2][j * 2+1] = 10000;
                        }
                        if (cur == 'J')
                        {
                            grid3[i * 2-1][j * 2] = 10000;
                            grid3[i * 2][j * 2-1] = 10000;
                        }
                        if (cur == '7')
                        {
                            grid3[i * 2+1][j * 2] = 10000;
                            grid3[i * 2][j * 2-1] = 10000;
                        }
                        if (cur == 'F')
                        {
                            grid3[i * 2+1][j * 2] = 10000;
                            grid3[i * 2][j * 2+1] = 10000;
                        }
                    }
                }
            }

            // Add some extra 0 spaces around the outside to help dijstras
            grid3.Add(Enumerable.Repeat(0, grid3[0].Count).ToList());
            grid3.Insert(0, Enumerable.Repeat(0, grid3[0].Count).ToList());
            foreach (var item in grid3)
            {
                item.Insert(0, 0);
                item.Add(0);
            }

            // just another way of visualizing the loop
            foreach (var item in theLoop)
            {
                grid2[item.Item2][item.Item1] = '#';
            }

            // PrintGrid3(grid3);
            var myCount = 0;
            // dijkstra's for every spot back to origin.
            var distances = DijkstraTuple.Search(grid3, Tuple.Create(0, 0));
            var myList = new List<(int, int)>();
            for (var i = 0; i < grid2.Count; i++)
            {
                for (var j = 0; j < grid2[0].Count; j++)
                {
                    if (grid2[i][j] != '#')
                    {
                        // If it was to cross a wall, it's clear. Else the value should be 0
                        // if it has to cross a wall that means it's surrounded
                        if (distances[Tuple.Create(j*2+1,i*2+1)] >= 10000)
                        {
                            myCount++;
                            myList.Add((i, j));
                        }
                    }
                }
            }
            return myCount;
        }

        // making it easier to visualize
        public static void PrintGrid3(List<List<int>> grid)
        {
            var rows = grid.Count;
            var columns = grid[0].Count;

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    if (grid[i][j] == 10000)
                    {
                        Console.Write(1);
                    }
                    else
                    {
                        Console.Write(grid[i][j]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}