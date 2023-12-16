using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day16: ISolver
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
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var energize = new HashSet<(int, int)>();
            var beams = new List<Beams>();
            var max = 0;
            beams.Add(new Beams((0, -1),(0,0)));

            for (var j = 0; j < 1000; j++)
            {
                var newBeams = new List<Beams>();
                foreach (var item in beams)
                {
                    var newCurrent = item.Next;
                    var y = newCurrent.Item1;
                    var x = newCurrent.Item2;
                    var oldY = item.Current.Item1;
                    var oldX = item.Current.Item2;

                    if (y < 0 || y >= grid.Count || x < 0 || x >= grid[y].Count)
                    {
                        item.Done = true;
                    }
                    else
                    {
                        energize.Add(newCurrent);
                        if (grid[y][x] == '\\')
                        {
                            // left
                            if (oldX < x)
                            {
                                // go down
                                item.NewCurrent((y + 1, x));
                            }
                            // right
                            else if (oldX > x)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                            }
                            // above
                            else if (oldY < y)
                            {
                                // go right
                                item.NewCurrent((y, x + 1));
                            }
                            // below
                            else if (oldY > y)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                            }
                        }
                        else if (grid[y][x] == '/')
                        {
                            // left
                            if (oldX < x)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                            }
                            // right
                            else if (oldX > x)
                            {
                                // go down
                                item.NewCurrent((y + 1, x));
                            }
                            // above
                            else if (oldY < y)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                            }
                            // below
                            else if (oldY > y)
                            {
                                // go right
                                item.NewCurrent((y, x + 1));
                            }
                        }
                        else if (grid[y][x] == '.')
                        {
                            // left
                            if (oldX < x)
                            {
                                // go right
                                item.NewCurrent((y, x + 1));
                            }
                            // right
                            else if (oldX > x)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                            }
                            // above
                            else if (oldY < y)
                            {
                                // go down
                                item.NewCurrent((y + 1, x));
                            }
                            // below
                            else if (oldY > y)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                            }
                        }
                        else if (grid[y][x] == '|')
                        {
                            // left
                            if (oldX < x)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                                // go down
                                newBeams.Add(new Beams((y, x), (y + 1, x), item.History));
                            }
                            // right
                            else if (oldX > x)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                                // go down
                                newBeams.Add(new Beams((y, x), (y + 1, x), item.History));
                            }
                            // above
                            else if (oldY < y)
                            {
                                // go down
                                item.NewCurrent((y + 1, x));
                            }
                            // below
                            else if (oldY > y)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                            }
                        }
                        else if (grid[y][x] == '-')
                        {
                            // left
                            if (oldX < x)
                            {
                                // go right
                                item.NewCurrent((y, x + 1));
                            }
                            // right
                            else if (oldX > x)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                            }
                            // above
                            else if (oldY < y)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                                // go right
                                newBeams.Add(new Beams((y, x), (y, x + 1), item.History));
                            }
                            // below
                            else if (oldY > y)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                                // go right
                                newBeams.Add(new Beams((y, x), (y, x + 1), item.History));
                            }
                        }
                    }
                }
                beams = beams.Where(x => !x.Done).ToList();
                beams.AddRange(newBeams);
                if (beams.Count == 0)
                {
                    j = 1000;
                }
            }
            return energize.Count;
        }

        private class Beams
        {
            public Beams((int, int) current, (int, int) next)
            {
                Current = current;
                History = new HashSet<((int, int), (int, int))>();
                History.Add((current, next));
                Next = next;
                Done = false;
            }
            public Beams((int, int) current, (int, int) next, HashSet<((int, int), (int, int))> history)
            {
                Current = current;
                History = history;
                Next = next;
                Done = false;
            }

            public (int, int) Current { get; set; }

            public (int, int) Next { get; set; }

            public HashSet<((int, int), (int, int))> History {get; set;}

            public bool Done { get; set; }

            public void NewCurrent((int,int) newNext)
            {
                if (History.Contains((Next, newNext)))
                {
                    Done = true;
                }
                else
                {
                    History.Add((Current, Next));
                    Current = Next;
                    Next = newNext;
                }
            }
        }
        
        private object Part2()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var max = 0;
            for (var j = 0; j < grid[0].Count; j++)
            {
                max = Math.Max(GoTime(grid, -1, j, 0, j),max);
            }
            for (var j = 0; j < grid[0].Count; j++)
            {
                max = Math.Max(GoTime(grid, grid.Count, j, grid.Count-1, j), max);
            }
            for (var j = 0; j < grid.Count; j++)
            {
                max = Math.Max(GoTime(grid, j, -1, j, 0), max);
            }
            for (var j = 0; j < grid.Count; j++)
            {
                max = Math.Max(GoTime(grid, j, grid[j].Count, j, grid[j].Count-1), max);
            }
            return max;
        }

        private int GoTime(List<List<char>> grid, int startY, int startX, int nextY, int nextX)
        {
            var energize = new HashSet<(int, int)>();
            var beams = new List<Beams>();
            var max = 0;
            beams.Add(new Beams((startY, startX), (nextY, nextX)));

            for (var j = 0; j < 1000; j++)
            {
                var newBeams = new List<Beams>();
                foreach (var item in beams)
                {
                    var newCurrent = item.Next;
                    var y = newCurrent.Item1;
                    var x = newCurrent.Item2;
                    var oldY = item.Current.Item1;
                    var oldX = item.Current.Item2;

                    if (y < 0 || y >= grid.Count || x < 0 || x >= grid[y].Count)
                    {
                        item.Done = true;
                    }
                    else
                    {
                        energize.Add(newCurrent);
                        if (grid[y][x] == '\\')
                        {
                            // left
                            if (oldX < x)
                            {
                                // go down
                                item.NewCurrent((y + 1, x));
                            }
                            // right
                            else if (oldX > x)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                            }
                            // above
                            else if (oldY < y)
                            {
                                // go right
                                item.NewCurrent((y, x + 1));
                            }
                            // below
                            else if (oldY > y)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                            }
                        }
                        else if (grid[y][x] == '/')
                        {
                            // left
                            if (oldX < x)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                            }
                            // right
                            else if (oldX > x)
                            {
                                // go down
                                item.NewCurrent((y + 1, x));
                            }
                            // above
                            else if (oldY < y)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                            }
                            // below
                            else if (oldY > y)
                            {
                                // go right
                                item.NewCurrent((y, x + 1));
                            }
                        }
                        else if (grid[y][x] == '.')
                        {
                            // left
                            if (oldX < x)
                            {
                                // go right
                                item.NewCurrent((y, x + 1));
                            }
                            // right
                            else if (oldX > x)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                            }
                            // above
                            else if (oldY < y)
                            {
                                // go down
                                item.NewCurrent((y + 1, x));
                            }
                            // below
                            else if (oldY > y)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                            }
                        }
                        else if (grid[y][x] == '|')
                        {
                            // left
                            if (oldX < x)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                                // go down
                                newBeams.Add(new Beams((y, x), (y + 1, x), item.History));
                            }
                            // right
                            else if (oldX > x)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                                // go down
                                newBeams.Add(new Beams((y, x), (y + 1, x), item.History));
                            }
                            // above
                            else if (oldY < y)
                            {
                                // go down
                                item.NewCurrent((y + 1, x));
                            }
                            // below
                            else if (oldY > y)
                            {
                                // go up
                                item.NewCurrent((y - 1, x));
                            }
                        }
                        else if (grid[y][x] == '-')
                        {
                            // left
                            if (oldX < x)
                            {
                                // go right
                                item.NewCurrent((y, x + 1));
                            }
                            // right
                            else if (oldX > x)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                            }
                            // above
                            else if (oldY < y)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                                // go right
                                newBeams.Add(new Beams((y, x), (y, x + 1), item.History));
                            }
                            // below
                            else if (oldY > y)
                            {
                                // go left
                                item.NewCurrent((y, x - 1));
                                // go right
                                newBeams.Add(new Beams((y, x), (y, x + 1), item.History));
                            }
                        }
                    }
                }
                beams = beams.Where(x => !x.Done).ToList();
                beams.AddRange(newBeams);
                if (beams.Count == 0)
                {
                    j = 1000;
                }
            }
            return energize.Count;
        }

        public static List<Tuple<int, int>> GetAdjacentNeighbours<T>(List<List<T>> grid, int x, int y)
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
                var newNeighbour = new Tuple<int,int>(y + adj[i].Item1, x + adj[i].Item2);

                if (neighbours.Contains(newNeighbour) && (x != newNeighbour.Item1 || y != newNeighbour.Item2))
                {
                    neighbours.Add(newNeighbour);
                }
            }
            return neighbours;
        }
    }
}