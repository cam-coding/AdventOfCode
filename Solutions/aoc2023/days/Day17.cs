using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventLibrary;
using AdventLibrary.CustomObjects;
using AdventLibrary.Helpers;
using AdventLibrary.PathFinding;

namespace aoc2023
{
    public class Day17: ISolver
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
            /*
            var grid = AdventLibrary.ParseInput.ParseFileAsGrid(_filePath);
            var dist = AdventLibrary.PathFinding.Dijkstra.Search(grid, new Tuple<int, int>(0, 0));
            var blah = dist[new Tuple<int, int>(grid.Count - 1, grid[0].Count - 1)];
            var betterGrid = MakeMyGrid(grid);
            var pather = new AStarSharp.Astar(betterGrid);
            var path = pather.FindPath(new Vector2(0, 0), new Vector2(grid.Count - 1, grid[0].Count - 1));
            return path.Sum(x => x.Weight);*/

            var grid = ParseInput.ParseFileAsGrid(_filePath);
            return BFS_Generic(grid);
        }
        
        private object Part2()
        {
            return 0;
        }
        private List<List<AStarSharp.Node>> MakeMyGrid(List<List<int>> grid)
        {
            List<List<AStarSharp.Node>> nodeGrid = new List<List<AStarSharp.Node>>();
            for (var i = 0; i < grid.Count; i++)
            {
                nodeGrid.Add(new List<AStarSharp.Node>());
                for (var j = 0; j < grid[0].Count; j++)
                {
                    nodeGrid[i].Add(new AStarSharp.Node(new Vector2(i, j), grid[i][j]));
                }
            }
            return nodeGrid;
        }

        public static int BFS_Generic(List<List<int>> grid)
        {
            PriorityQueue<List<(int, int)>, int> q = new PriorityQueue<List<(int, int)>, int>();
            // (0,0) can be anything, just needs to be your root item.
            q.Enqueue(new List<(int, int)>() { (0, 0) }, 0);
            var best = 105;
            while (q.Count > 0)
            {
                var current = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var tot = Total(grid, current);
                var avg = tot / current.Count;
                if (current.Equals((grid.Count - 1, grid[0].Count - 1)))
                {
                    best = Math.Min(best, tot);
                }
                else
                {
                    if (tot <= best && avg < 5)
                    {
                        var cur = current.Last(); // this is just the most recent point
                        if (current == null)
                            continue;

                        //Get the next nodes/grids/etc to visit next
                        var neighs = GridHelper.GetAdjacentNeighbours(grid, cur.Item1, cur.Item2);
                        foreach (var item in neighs)
                        {
                            var tup = (item.Item1, item.Item2);
                            if (current.Count > 1)
                            {
                                var secondLast = current[^2];
                            }
                            if (current.Count < 2 || tup != current[^2])
                            {
                                if (current.Count < 4 || !FourInARow(current[^4..], tup))
                                {
                                    var temp = new List<(int, int)>(current);
                                    if (!temp.Contains(tup))
                                    {
                                        temp.Add(tup);
                                        var prior = tot + Math.Abs(cur.Item1 - grid.Count - 1) + Math.Abs(cur.Item2 - grid[0].Count - 1);
                                        q.Enqueue(temp, prior);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return best;
        }

        private static int Total(List<List<int>> grid, List<(int,int)> visited)
        {
            var total = 0;
            foreach(var item in visited)
            {
                total += grid[item.Item1][item.Item2];
            }
            return total;
        }

        private static bool FourInARow(List<(int, int)> lastTwo, (int, int) current)
        {
            var cur = new LocationTuple<int>(current.Item1, current.Item2);
            var last = lastTwo.Select(x => new LocationTuple<int>(x.Item1, x.Item2)).ToList();
            if (lastTwo.Count < 4)
            {
                return false;
            }
            if (last[3] == (cur - GridWalker.Left))
            {
                if (last[2] == last[3] - GridWalker.Left)
                {
                    if (last[1] == last[2] - GridWalker.Left)
                    {
                        if (last[0] == last[1] - GridWalker.Left)
                        {
                            return true;
                        }
                    }
                }
            }
            else if (last[3] == (cur - GridWalker.Right))
            {
                if (last[2] == last[3] - GridWalker.Right)
                {
                    if (last[1] == last[2] - GridWalker.Right)
                    {
                        if (last[0] == last[1] - GridWalker.Right)
                        {
                            return true;
                        }
                    }
                }
            }
            else if (last[3] == (cur - GridWalker.Up))
            {
                if (last[2] == last[3] - GridWalker.Up)
                {
                    if (last[1] == last[2] - GridWalker.Up)
                    {
                        if (last[0] == last[1] - GridWalker.Up)
                        {
                            return true;
                        }
                    }
                }
            }
            else if (last[3] == (cur - GridWalker.Down))
            {
                if (last[2] == last[3] - GridWalker.Down)
                {
                    if (last[1] == last[2] - GridWalker.Down)
                    {
                        if (last[0] == last[1] - GridWalker.Down)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}