using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;
using AdventLibrary.PathFinding;
using AStarSharp;

namespace aoc2023
{
    public class Day11: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(0, Part2());
        }

        private object Part1()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
			var counter = 0;
            var emptyRows = new HashSet<int>();
            var emptyColumns = new HashSet<int>();
            for (var j = 0; j < grid.Count; j++)
            {
                if (grid[j].All(x => x == '.'))
                {
                    var temp = Enumerable.Repeat('.', grid[0].Count).ToList();
                    grid.Insert(j + 1, temp);
                    emptyRows.Add(j);
                    j++;
                }
            }
            for (var j = 0; j < grid[0].Count; j++)
            {
                var boo = true;
                for (var i = 0; i < grid.Count; i++)
                {
                    if (grid[i][j] != '.')
                    {
                        boo = false;
                        break;
                    }
                }
                if (boo)
                {
                    for (var i = 0; i < grid.Count; i++)
                    {
                        grid[i].Insert(j, '.');
                    }
                    emptyColumns.Add(j);
                    j++;
                }
            }
            var listy = new List<(int, int)>();
            var grid2 = new List<List<int>>();

            for (var i = 0; i < grid.Count; i++)
            {
                grid2.Add(new List<int>());
                for (var j = 0; j < grid[0].Count; j++)
                {
                    grid2[i].Add(1);
                    if (grid[i][j] == '#')
                    {
                        listy.Add((i, j));
                    }
                }
            }
            /*
            foreach (var i in listy)
            {
                var distances = Dijkstra.Search(grid2, Tuple.Create(i.Item2, i.Item1)).ToImmutableSortedDictionary();
                foreach (var item in listy)
                {
                    if (i != item)
                    {
                        counter += distances[Tuple.Create(item.Item2, item.Item1)];
                    }
                }
            }*/
            for (var i = 0; i < listy.Count; i++)
            {
                var item = listy[i];
                var distances = Dijkstra.Search(grid2, Tuple.Create(item.Item2, item.Item1)).ToImmutableSortedDictionary();
                for (var j = i+1; j < listy.Count; j++)
                {
                    var item2 = listy[j];
                    counter += distances[Tuple.Create(item2.Item2, item2.Item1)];
                }
            }
            return counter;
        }

        private object Part2Attempt2()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var counter = 0;
            var emptyRows = new HashSet<int>();
            var emptyColumns = new HashSet<int>();
            var saver = 0;
            for (var j = 0; j < grid.Count; j++)
            {
                if (grid[j].All(x => x == '.'))
                {
                    var temp = Enumerable.Repeat('.', grid[0].Count).ToList();
                    grid.Insert(j + 1, temp);
                    emptyRows.Add(j - saver);
                    saver++;
                    j++;
                }
            }
            saver = 0;
            for (var j = 0; j < grid[0].Count; j++)
            {
                var boo = true;
                for (var i = 0; i < grid.Count; i++)
                {
                    if (grid[i][j] != '.')
                    {
                        boo = false;
                        break;
                    }
                }
                if (boo)
                {
                    for (var i = 0; i < grid.Count; i++)
                    {
                        grid[i].Insert(j, '.');
                    }
                    emptyColumns.Add(j - saver);
                    saver++;
                    j++;
                }
            }
            GridHelper.PrintGrid(grid);
            var listy = new List<(int, int)>();
            var grid2 = new List<List<int>>();

            for (var i = 0; i < grid.Count; i++)
            {
                grid2.Add(new List<int>());
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (emptyRows.Contains(i) || emptyColumns.Contains(j))
                    {
                        grid2[i].Add(100000);
                    }
                    else
                    {
                        grid2[i].Add(1);
                    }
                    if (grid[i][j] == '#')
                    {
                        listy.Add((i, j));
                    }
                }
            }
            for (var i = 0; i < listy.Count; i++)
            {
                var item = listy[i];
                var distances = Dijkstra.Search(grid2, Tuple.Create(item.Item2, item.Item1)).ToImmutableSortedDictionary();
                for (var j = i + 1; j < listy.Count; j++)
                {
                    var item2 = listy[j];
                    var rem = distances[Tuple.Create(item2.Item2, item2.Item1)] % 100000;
                    var mody = distances[Tuple.Create(item2.Item2, item2.Item1)] / 100000;
                    counter += mody * 10 + rem;
                }
            }
            return counter;
        }

        private object Part2Attemp3()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var counter = 0;
            var emptyRows = new HashSet<int>();
            var emptyColumns = new HashSet<int>();
            var saver = 0;
            for (var j = 0; j < grid.Count; j++)
            {
                if (grid[j].All(x => x == '.'))
                {
                    var temp = Enumerable.Repeat('.', grid[0].Count).ToList();
                    grid.Insert(j + 1, temp);
                    emptyRows.Add(j-saver);
                    saver++;
                    j++;
                }
            }
            saver = 0;
            for (var j = 0; j < grid[0].Count; j++)
            {
                var boo = true;
                for (var i = 0; i < grid.Count; i++)
                {
                    if (grid[i][j] != '.')
                    {
                        boo = false;
                        break;
                    }
                }
                if (boo)
                {
                    for (var i = 0; i < grid.Count; i++)
                    {
                        grid[i].Insert(j, '.');
                    }
                    emptyColumns.Add(j-saver);
                    saver++;
                    j++;
                }
            }
            GridHelper.PrintGrid(grid);
            var hashy = new HashSet<(int, int)>();
            var grid2 = new List<List<int>>();

            for (var i = 0; i < grid.Count; i++)
            {
                grid2.Add(new List<int>());
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (emptyRows.Contains(i) || emptyColumns.Contains(j))
                    {
                        grid2[i].Add(100000);
                    }
                    else
                    {
                        grid2[i].Add(1);
                    }
                    if (grid[i][j] == '#')
                    {
                        hashy.Add((i, j));
                    }
                }
            }
            var dicty = new Dictionary<((int, int), (int, int)), int>();
            var done = new HashSet<(int, int)>();
            foreach(var item in hashy)
            {
                Queue<List<(int, int)>> q = new Queue<List<(int, int)>>();
                var visited = new HashSet<(int, int)>();
                q.Enqueue(new List<(int, int)>() { item });
                while (q.Count > 0)
                {
                    var current = q.Dequeue();
                    var cur = current.Last();
                    if (current == null || visited.Contains(cur))
                        continue;
                    var neighbours = GridHelper.GetAdjacentNeighbours(grid, cur.Item2, cur.Item1);
                    visited.Add(cur);
                    /*
                    var neighs = new List<(int, int)>();
                    neighs.Add((cur.Item1 + 1, cur.Item2));
                    neighs.Add((cur.Item1, cur.Item2+1));*/
                    foreach (var neigh in neighbours)
                    {
                        var temp = new List<(int, int)>(current);
                        temp.Add((neigh.Item2,neigh.Item1));
                        q.Enqueue(temp);
                    }


                    if (hashy.Contains(cur) && !done.Contains(cur) && !cur.Equals(current.First()))
                    {
                        var county = -1;
                        foreach (var node in current)
                        {
                            if (emptyRows.Contains(node.Item2) || emptyColumns.Contains(node.Item1))
                            {
                                county += 10;
                            }
                            else
                            {
                                county++;
                            }
                        }
                        counter += county;
                        dicty.Add((item, cur), county);
                    }
                }
                done.Add(item);
            }
            return counter;
        }
        private object Part2()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            long counter = 0;
            var emptyRows = new HashSet<int>();
            var emptyColumns = new HashSet<int>();
            var saver = 0;
            for (var j = 0; j < grid.Count; j++)
            {
                if (grid[j].All(x => x == '.'))
                {
                    emptyRows.Add(j);
                }
            }
            saver = 0;
            for (var j = 0; j < grid[0].Count; j++)
            {
                var boo = true;
                for (var i = 0; i < grid.Count; i++)
                {
                    if (grid[i][j] != '.')
                    {
                        boo = false;
                        break;
                    }
                }
                if (boo)
                {
                    emptyColumns.Add(j);
                }
            }
            var listy = new List<(int, int)>();
            var grid2 = new List<List<int>>();

            long county = 0;

            for (var i = 0; i < grid.Count; i++)
            {
                grid2.Add(new List<int>());
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (grid[i][j] == '#')
                    {
                        listy.Add((i, j));
                    }
                }
            }
            for (var i = 0; i < listy.Count; i++)
            {
                for (var j = i+1; j < listy.Count; j++)
                {
                    var dist = Math.Abs(listy[i].Item1 - listy[j].Item1) + Math.Abs(listy[i].Item2 - listy[j].Item2);
                    var sortedCol = new List<int>() { listy[i].Item2 , listy[j].Item2};
                    sortedCol.Sort();
                    var emptiesCol = emptyColumns.Count(x => x > sortedCol[0] && x < sortedCol[1]);
                    var sortedRow = new List<int>() { listy[i].Item1, listy[j].Item1 };
                    sortedRow.Sort();
                    var emptiesRow = emptyRows.Count(x => x > sortedRow[0] && x < sortedRow[1]);
                    county += dist + (1000000-1) * emptiesCol + (1000000 - 1) * emptiesRow;
                }
            }
            return county;
        }
        private object Part2Final()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            long counter = 0;
            var emptyRows = new HashSet<int>();
            var emptyColumns = new HashSet<int>();
            var saver = 0;
            for (var j = 0; j < grid.Count; j++)
            {
                if (grid[j].All(x => x == '.'))
                {
                    emptyRows.Add(j);
                }
            }
            saver = 0;
            for (var j = 0; j < grid[0].Count; j++)
            {
                var boo = true;
                for (var i = 0; i < grid.Count; i++)
                {
                    if (grid[i][j] != '.')
                    {
                        boo = false;
                        break;
                    }
                }
                if (boo)
                {
                    emptyColumns.Add(j);
                }
            }
            var hashy = new HashSet<(int, int)>();
            var grid2 = new List<List<int>>();

            for (var i = 0; i < grid.Count; i++)
            {
                grid2.Add(new List<int>());
                for (var j = 0; j < grid[0].Count; j++)
                {
                    if (emptyRows.Contains(i) || emptyColumns.Contains(j))
                    {
                        grid2[i].Add(100000);
                    }
                    else
                    {
                        grid2[i].Add(1);
                    }
                    if (grid[i][j] == '#')
                    {
                        hashy.Add((i, j));
                    }
                }
            }
            var dicty = new Dictionary<((int, int), (int, int)), int>();
            var done = new HashSet<(int, int)>();
            foreach (var item in hashy)
            {
                Queue<List<(int, int)>> q = new Queue<List<(int, int)>>();
                var visited = new HashSet<(int, int)>();
                q.Enqueue(new List<(int, int)>() { item });
                while (q.Count > 0)
                {
                    var current = q.Dequeue();
                    var cur = current.Last();
                    if (current == null || visited.Contains(cur))
                        continue;
                    var neighbours = GridHelper.GetAdjacentNeighbours(grid, cur.Item2, cur.Item1);
                    visited.Add(cur);
                    /*
                    var neighs = new List<(int, int)>();
                    neighs.Add((cur.Item1 + 1, cur.Item2));
                    neighs.Add((cur.Item1, cur.Item2+1));*/
                    foreach (var neigh in neighbours)
                    {
                        var temp = new List<(int, int)>(current);
                        temp.Add((neigh.Item2, neigh.Item1));
                        q.Enqueue(temp);
                    }


                    if (hashy.Contains(cur) && !done.Contains(cur) && !cur.Equals(current.First()))
                    {
                        var county = -1;
                        foreach (var node in current)
                        {
                            if (emptyRows.Contains(node.Item1) || emptyColumns.Contains(node.Item2))
                            {
                                county += 1000000;
                            }
                            else
                            {
                                county++;
                            }
                        }
                        counter += county;
                        dicty.Add((item, cur), county);
                    }
                }
                done.Add(item);
            }
            return counter;
        }
    }
}