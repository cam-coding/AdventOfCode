using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            /*
            var grid = AdventLibrary.ParseInput.ParseFileAsGrid(_filePath);
            var dist = AdventLibrary.PathFinding.Dijkstra.Search(grid, new Tuple<int, int>(0, 0));
            var sorted = dist.ToImmutableSortedDictionary();
            var blah = dist[new Tuple<int, int>(grid.Count - 1, grid[0].Count - 1)];
            var betterGrid = MakeMyGrid(grid);
            var pather = new AStarSharp.Astar(betterGrid);
            var path = pather.FindPath(new Vector2(0, 0), new Vector2(grid.Count - 1, grid[0].Count - 1));
            return path.Sum(x => x.Weight); */
            /*
            Random r = new Random();
            int rInt = r.Next(0, 100);
            var grid2 = new List<List<int>>();
            for (var i = 0; i < 600; i++)
            {
                grid2.Add(new List<int>());
                for (var j = 0; j < 600; j++)
                {
                    grid2[i].Add(r.Next(0, 10));
                }
            }
            var dist = AdventLibrary.PathFinding.Dijkstra.Search(grid2, new Tuple<int, int>(grid2.Count-1, grid2[0].Count - 1));
            var blah = dist[new Tuple<int,int>(0,0)];
            blah = dist[new Tuple<int, int>(0, 1)];
            blah = dist[new Tuple<int, int>(0, 2)];
            blah = dist[new Tuple<int, int>(0, 3)];*/
            return 0;
            var grid = ParseInput.ParseFileAsGrid(_filePath);
            return BFS_Generic3(grid);
        }
        
        private object Part2()
        {
            var grid = ParseInput.ParseFileAsGrid(_filePath);
            return BFS_Generic3_Part2(grid);
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
            var visited = new Dictionary<((int, int),(int,int)), int>();
            // (0,0) can be anything, just needs to be your root item.
            q.Enqueue(new List<(int, int)>() { (0, 0) }, 0);
            var best = 150;
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
                    if (tot <= best)
                    {
                        var cur = current.Last(); // this is just the most recent point
                        if (current == null)
                            continue;
                        ((int, int), (int, int)) key;
                        if (current.Count > 1)
                        {
                            key = ((cur), (cur.Item1 - current[^2].Item1, cur.Item2 - current[^2].Item2));
                        }
                        else
                        {
                            key = ((cur), (0, 0));
                        }
                        if (visited.ContainsKey(key))
                        {
                            if (visited[key] < tot)
                            {
                                continue;
                            }
                            visited[key] = tot;
                        }
                        else
                        {
                            visited.Add(key, tot);
                        }

                        //Get the next nodes/grids/etc to visit next
                        var neighs = GridHelper.GetAdjacentNeighbours(grid, cur.Item1, cur.Item2);
                        foreach (var item in neighs)
                        {
                            var tup = (item.Item1, item.Item2);
                            if (current.Count < 4 || !FourInARow(current[^4..], tup))
                            {
                                var temp = new List<(int, int)>(current);
                                if (!temp.Contains(tup))
                                {
                                    temp.Add(tup);
                                    var priority = tot + Math.Abs(cur.Item1 - grid.Count - 1) + Math.Abs(cur.Item2 - grid[0].Count - 1);
                                    q.Enqueue(temp, priority);
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }

            return best;
        }

        public static int BFS_Generic2(List<List<int>> grid)
        {
            var proper = new List<LocationTuple<int>>()
            {
                { new LocationTuple<int>(0,0)},
                { new LocationTuple<int>(0,1)},
                { new LocationTuple<int>(0,2)},
                { new LocationTuple<int>(1,2)},
                { new LocationTuple<int>(1,3)},
                { new LocationTuple<int>(1,4)},
                { new LocationTuple<int>(1,5)},
                { new LocationTuple<int>(0,5)},
                { new LocationTuple<int>(0,6)},
                { new LocationTuple<int>(0,7)},
                { new LocationTuple<int>(0,8)},
                { new LocationTuple<int>(1,8)},
                { new LocationTuple<int>(2,8)},
                { new LocationTuple<int>(2,9)},
                { new LocationTuple<int>(2,10)},
                { new LocationTuple<int>(3,10)},
                { new LocationTuple<int>(4,10)},
                { new LocationTuple<int>(4,11)},
                { new LocationTuple<int>(5,11)},
                { new LocationTuple<int>(6,11)},
                { new LocationTuple<int>(7,11)},
                { new LocationTuple<int>(7,12)},
                { new LocationTuple<int>(8,12)},
                { new LocationTuple<int>(9,12)},
                { new LocationTuple<int>(10,12)},
                { new LocationTuple<int>(10,11)},
                { new LocationTuple<int>(11,11)},
                { new LocationTuple<int>(12,11)},
                { new LocationTuple<int>(12,12)},
            };
            PriorityQueue<(GridWalker, int), int> q = new PriorityQueue<(GridWalker, int), int>();
            var visited = new Dictionary<(LocationTuple<int>, LocationTuple<int>, int), int>();
            var distances = AdventLibrary.PathFinding.Dijkstra.Search(grid, new Tuple<int, int>(grid.Count - 1, grid[0].Count - 1));
            // (0,0) can be anything, just needs to be your root item.
            q.Enqueue((new GridWalker((0,0), GridWalker.Right), grid[0][0] * -1), 0);
            q.Enqueue((new GridWalker((0, 0), GridWalker.Down), grid[0][0] * -1), 0);
            var best = 1104;
            while (q.Count > 0)
            {
                var thing = q.Dequeue();
                var current = thing.Item1; // This will contain a list of all the points you visited on the way
                var distanceSoFar = thing.Item2 += grid[current.Current.Item1][current.Current.Item2];
                if (current == null)
                    continue;
                var cur = current.Current; // this is just the most recent point
                /*
                var valid = true;
                for (var i = 0; i < current.Path.Count; i++)
                {
                    if (i >= proper.Count)
                    {
                        valid = false; break;
                    }
                    if (current.Path[i].Item1 != proper[i])
                    {
                        valid = false;
                    }
                }
                if (valid)
                {
                    Console.WriteLine(current.Path.Count);
                }*/
                /*
                var distanceSoFar = grid[0][0]*-1;
                foreach (var item in current.Path)
                {
                    distanceSoFar += grid[item.Item1.Item1][item.Item1.Item2];
                }*/
                var stepsSinceTurn = 0;
                for (var i = current.Path.Count - 2; i >= 0; i--)
                {
                    if (current.Path[i].Item2 == current.Direction)
                    {
                        stepsSinceTurn++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (stepsSinceTurn >= 3)
                {
                    continue;
                }
                var key = (cur, current.Direction, stepsSinceTurn);
                if (!visited.ContainsKey(key))
                {
                    visited.Add(key, distanceSoFar);
                }
                else
                {
                    if (visited[key] < distanceSoFar)
                    {
                        continue;
                    }
                    else
                    {
                        visited[key] = distanceSoFar;
                    }
                }
                if (cur == new LocationTuple<int> (grid.Count-1, grid[0].Count - 1))
                {
                    if (distanceSoFar < best)
                    {
                        best = distanceSoFar;
                        continue;
                    }
                }

                if (distanceSoFar + distances[new Tuple<int,int>(cur.Item1, cur.Item2)] > best)
                {
                    continue;
                }
                foreach (var item in GridWalker.Directions)
                {
                    if (item != GridWalker.Opposites[current.Direction])
                    {
                        var temp = new GridWalker(current);
                        temp.Direction = item;
                        temp.Walk();
                        if (GridHelper.WithinGrid(grid, temp.Current)  && !temp.Looping)
                        {
                            var priority = GridHelper.TaxicabDistance((cur.Item1,cur.Item2), (grid.Count - 1, grid[0].Count - 1));
                            q.Enqueue((temp,distanceSoFar), priority);
                        }
                    }
                }

                //Get the next nodes/grids/etc to visit next

                // do something with the current node
            }
            return best;
        }

        public static int BFS_Generic3(List<List<int>> grid)
        {
            PriorityQueue<(GridWalker, int), int> q = new PriorityQueue<(GridWalker, int), int>();
            var visited = new HashSet<(int, int, int,int, int)>();
            var distances = AdventLibrary.PathFinding.Dijkstra.Search(grid, new Tuple<int, int>(grid.Count - 1, grid[0].Count - 1));
            // (0,0) can be anything, just needs to be your root item.
            q.Enqueue((new GridWalker((0, 0), GridWalker.Right), grid[0][0] * -1), 0);
            q.Enqueue((new GridWalker((0, 0), GridWalker.Down), grid[0][0] * -1), 0);
            var best = 1104;
            while (q.Count > 0)
            {
                var thing = q.Dequeue();
                var current = thing.Item1; // This will contain a list of all the points you visited on the way
                var distanceSoFar = thing.Item2 += grid[current.Current.Item1][current.Current.Item2];
                if (current == null)
                    continue;
                var cur = current.Current;
                var stepsSinceTurn = 0;
                for (var i = current.Path.Count - 2; i >= 0; i--)
                {
                    if (current.Path[i].Item2 == current.Direction)
                    {
                        stepsSinceTurn++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (stepsSinceTurn >= 3)
                {
                    continue;
                }
                var key = (cur.Item1, cur.Item2, current.Direction.Item1, current.Direction.Item2, stepsSinceTurn);
                if (!visited.Add(key))
                {
                    continue;
                }
                if (cur == new LocationTuple<int>(grid.Count - 1, grid[0].Count - 1))
                {
                    if (distanceSoFar < best)
                    {
                        best = distanceSoFar;
                        continue;
                    }
                }

                if (distanceSoFar + distances[new Tuple<int, int>(cur.Item1, cur.Item2)] > best)
                {
                    continue;
                }
                foreach (var item in GridWalker.Directions)
                {
                    if (item != GridWalker.Opposites[current.Direction])
                    {
                        var temp = new GridWalker(current);
                        temp.Direction = item;
                        temp.Walk();
                        if (GridHelper.WithinGrid(grid, temp.Current) && !temp.Looping)
                        {
                            var priority = distanceSoFar;
                            q.Enqueue((temp, distanceSoFar), priority);
                        }
                    }
                }
            }
            return best;
        }

        public static int BFS_Generic3_Part2(List<List<int>> grid)
        {
            PriorityQueue<(GridWalker, int), int> q = new PriorityQueue<(GridWalker, int), int>();
            var visited = new HashSet<(int, int, int, int, int)>();
            var distances = AdventLibrary.PathFinding.Dijkstra.Search(grid, new Tuple<int, int>(grid[0].Count - 1, grid.Count - 1));
            // (0,0) can be anything, just needs to be your root item.
            q.Enqueue((new GridWalker((0, 0), GridWalker.Right), grid[0][0] * -1), 0);
            q.Enqueue((new GridWalker((0, 0), GridWalker.Down), grid[0][0] * -1), 0);
            var best = int.MaxValue;
            while (q.Count > 0)
            {
                var thing = q.Dequeue();
                var current = thing.Item1; // This will contain a list of all the points you visited on the way
                var distanceSoFar = thing.Item2 += grid[current.Current.Item1][current.Current.Item2];
                if (current == null)
                    continue;
                var cur = current.Current;
                var stepsSinceTurn = 0;
                for (var i = current.Path.Count - 2; i >= 0; i--)
                {
                    if (current.Path[i].Item2 == current.Direction)
                    {
                        stepsSinceTurn++;
                    }
                    else
                    {
                        break;
                    }
                }
                /*
                if (stepsSinceTurn >= 3)
                {
                    continue;
                }*/
                var key = (cur.Item1, cur.Item2, current.Direction.Item1, current.Direction.Item2, stepsSinceTurn);
                if (!visited.Add(key))
                {
                    continue;
                }
                if (cur == new LocationTuple<int>(grid.Count - 1, grid[0].Count - 1) && stepsSinceTurn >= 3)
                {
                    if (distanceSoFar < best)
                    {
                        best = distanceSoFar;
                        continue;
                    }
                }

                if (distanceSoFar + distances[new Tuple<int, int>(cur.Item2, cur.Item1)] > best)
                {
                    continue;
                }
                if (stepsSinceTurn < 3)
                {
                    var temp = new GridWalker(current);
                    temp.Walk();
                    if (GridHelper.WithinGrid(grid, temp.Current) && !temp.Looping)
                    {
                        var priority = distanceSoFar;
                        q.Enqueue((temp, distanceSoFar), priority);
                    }
                }
                else
                {
                    foreach (var item in GridWalker.Directions)
                    {
                        if (item != GridWalker.Opposites[current.Direction])
                        {
                            if (item != current.Direction ||
                                (item == current.Direction && stepsSinceTurn < 9))
                            {
                                var temp = new GridWalker(current);
                                temp.Direction = item;
                                temp.Walk();
                                if (GridHelper.WithinGrid(grid, temp.Current) && !temp.Looping)
                                {
                                    var priority = distanceSoFar;
                                    q.Enqueue((temp, distanceSoFar), priority);
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            return best;
        }

        public class MaxHeapCompare : IComparer<int>
        {
            public int Compare(int x, int y) => y.CompareTo(x);
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