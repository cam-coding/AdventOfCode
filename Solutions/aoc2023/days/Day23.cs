using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.PathFinding;

namespace aoc2023
{
    public class Day23: ISolver
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
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            Adj = GridToAdjList(grid);
            var ans = BFS_Part1(grid.Count - 1);
            return ans - 1;
        }

        private object Part2()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            Adj2 = GridToAdjList2(grid);
            Adj2Weights = new Dictionary<Tuple<int, int>, int>();
            foreach (var item in Adj2.Keys)
            {
                Adj2Weights[item] = 1;
            }
            ReduceAdjGraph();
            var ans = BFS_Part2(grid.Count - 1);
            return ans - 1;
        }

        public static int BFS_Part1(int max)
        {
            PriorityQueue<List<(int, int)>, int> q = new PriorityQueue<List<(int, int)>, int>();
            var visited = new Dictionary<(int, int), int>();
            q.Enqueue(new List<(int, int)>() { (0, 1) }, 0);
            var lastPrint = 0;
            while (q.Count > 0)
            {
                var current = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var cur = current.Last(); // this is just the most recent point
                if (current == null)
                    continue;
                /*
                if (cur == (max, max - 1))
                {
                }*/
                if (visited.ContainsKey(cur))
                {
                    if (current.Count < visited[cur])
                    {
                        continue;
                    }
                    else
                    {
                        visited[cur] = current.Count;
                    }
                }
                else
                {
                    visited.Add(cur, current.Count);
                }
                var adj = Adj[new Tuple<int, int>(cur.Item1, cur.Item2)];
                foreach (var item in adj)
                {
                    var obj = (item.Item1, item.Item2);
                    if (!current.Contains(obj))
                    {
                        var newList = current.Clone();
                        newList.Add(obj);
                        q.Enqueue(newList, newList.Count * -1);
                    }
                }
            }
            return visited[(max, max - 1)];
        }

        public static int BFS_Part2(int max)
        {
            Queue<List<(int, int)>> q = new Queue<List<(int, int)>>();
            //var visited = new Dictionary<(int, int),List<(int,int)>>();
            q.Enqueue(new List<(int, int)>() { (0, 1) });
            var lastPrint = 0;
            var best = 0;
            while (q.Count > 0)
            {
                var current = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var cur = current.Last(); // this is just the most recent point
                if (current == null )
                    continue;
                if (cur == (max, max - 1))
                {
                    var count = CalcValue(current);
                    best = Math.Max(best, count);
                    /*
                    if (visited.ContainsKey(cur))
                    {
                        if (visited[cur].Count != lastPrint)
                        {
                            lastPrint = visited[cur].Count;
                            Console.WriteLine(visited[cur].Count);
                        }
                    }*/
                }
                /*
                if (visited.ContainsKey(cur))
                {
                    var counter = 0;
                    foreach (var item in current)
                    {
                        var tup = new Tuple<int, int>(item.Item1, item.Item2);
                        counter += Adj2Weights[tup];
                    }
                    var counter2 = 0;
                    foreach (var item in visited[cur])
                    {
                        var tup = new Tuple<int, int>(item.Item1, item.Item2);
                        counter += Adj2Weights[tup];
                    }
                    if (counter < counter2)
                    {
                        // continue;
                    }
                    else
                    {
                        visited[cur] = current;
                    }
                }
                else
                {
                    visited.Add(cur, current);
                }*/
                var adj = Adj2[new Tuple<int, int>(cur.Item1, cur.Item2)];
                foreach (var item in adj)
                {
                    var obj = (item.Item1, item.Item2);
                    if (!current.Contains(obj))
                    {
                        var newList = current.Clone();
                        newList.Add(obj);
                        q.Enqueue(newList);
                    }
                }
                //Get the next nodes/grids/etc to visit next

                // do something with the current node
            }
            //return visited[(max, max - 1)];
            return best;
        }

        private static int CalcValue(List<(int,int)> listy)
        {
            var counter = 0;
            foreach (var item in listy)
            {
                var tup = new Tuple<int, int>(item.Item1, item.Item2);
                counter += Adj2Weights[tup];
            }
            return counter;
        }

        // if missing item, distance is infinite
        public static Dictionary<Tuple<int, int>, int> DistanceDictionary;

        public static Dictionary<Tuple<int, int>, List<Tuple<int, int>>> Adj;

        public static Dictionary<Tuple<int, int>, List<Tuple<int, int>>> Adj2;

        public static Dictionary<Tuple<int, int>, int> Adj2Weights;

        public static Dictionary<Tuple<int, int>, List<Tuple<int, int>>> GridToAdjList(List<List<char>> grid)
        {
            var dict = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    var chr = grid[y][x];
                    if (chr != '#')
                    {
                        var cord = new Tuple<int, int>(y, x);
                        if (chr == '.')
                        {
                            var neigh = GridHelperWeirdTypes.GetAdjacentNeighboursTuple(grid, x, y);
                            var newNeigh = new List<Tuple<int, int>>();
                            foreach (var item in neigh)
                            {
                                if (grid[item.Item2][item.Item1] != '#')
                                {
                                    newNeigh.Add(new Tuple<int, int>(item.Item2, item.Item1));
                                }
                            }
                            if (newNeigh.Count > 0)
                            {
                                dict.Add(cord, newNeigh.ToList());
                            }
                        }
                        else if (chr == 'v')
                        {
                            var neigh = new List<Tuple<int, int>>() { new Tuple<int,int>(y+1,x)};
                            dict.Add(cord, neigh);
                        }
                        else if (chr == '^')
                        {
                            var neigh = new List<Tuple<int, int>>() { new Tuple<int, int>(y - 1, x) };
                            dict.Add(cord, neigh);
                        }
                        else if (chr == '<')
                        {
                            var neigh = new List<Tuple<int, int>>() { new Tuple<int, int>(y, x - 1) };
                            dict.Add(cord, neigh);
                        }
                        else if (chr == '>')
                        {
                            var neigh = new List<Tuple<int, int>>() { new Tuple<int, int>(y, x + 1) };
                            dict.Add(cord, neigh);
                        }
                    }
                }
            }

            return dict;
        }

        public static Dictionary<Tuple<int, int>, List<Tuple<int, int>>> GridToAdjList2(List<List<char>> grid)
        {
            var dict = new Dictionary<Tuple<int, int>, List<Tuple<int, int>>>();
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    var chr = grid[y][x];
                    if (chr != '#')
                    {
                        var cord = new Tuple<int, int>(y, x);
                        var neigh = GridHelperWeirdTypes.GetAdjacentNeighboursTuple(grid, x, y);
                        var newNeigh = new List<Tuple<int, int>>();
                        foreach (var item in neigh)
                        {
                            if (grid[item.Item2][item.Item1] != '#')
                            {
                                newNeigh.Add(new Tuple<int, int>(item.Item2, item.Item1));
                            }
                        }
                        if (newNeigh.Count > 0)
                        {
                            dict.Add(cord, newNeigh.ToList());
                        }
                    }
                }
            }

            return dict;
        }

        public static void ReduceAdjGraph()
        {
            var iter = 0;
            while (iter < Adj2.Keys.Count)
            {
                var current = Adj2.Values.ToList()[iter];
                if (current.Count == 2)
                {
                    current.Sort((a, b) => Adj2[a].Count.CompareTo(Adj2[b].Count));
                    var left = current[0];
                    var right = current[1];
                    if (Adj2[left].Count == 2 || Adj2[right].Count == 2)
                    {
                        var key = Adj2.Keys.ToList()[iter];

                        Adj2[left].Remove(key);
                        Adj2[left].Add(right);
                        Adj2[right].Remove(key);
                        Adj2[right].Add(left);
                        Adj2Weights[left] += Adj2Weights[key];
                        Adj2.Remove(key);
                        Adj2Weights.Remove(key);
                        iter = -1;
                    }
                }
                iter++;
            }
        }
    }
}