using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using AdventLibrary.PathFinding;
using AStarSharp;

namespace aoc2024
{
    public class Day10: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.IntGrid;
			long count = 0;

            var heads = grid.GetAllLocationWhereCellEqualsValue(0);

            foreach (var head in heads)
            {
                Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
                var visited = new HashSet<string>();
                // (0,0) can be anything, just needs to be your root item.
                q.Enqueue(new List<GridLocation<int>>() { head });
                var ends = 0;
                var endsHash = new HashSet<GridLocation<int>>();
                while (q.Count > 0)
                {
                    var current = q.Dequeue(); // This will contain a list of all the points you visited on the way
                    var cur = current.Last(); // this is just the most recent point
                    var stringy = ListExtensions.Stringify(current);
                    if (current == null || current.Count > 10 || visited.Contains(stringy) || !EachStepAbove(grid,current))
                        continue;

                    //Get the next nodes/grids/etc to visit next
                    visited.Add(stringy);
                    var neighs = grid.GetOrthogonalNeighbours(cur);
                    foreach (var neigh in neighs)
                    {
                        var temp = current.Clone();
                        if (!temp.Any(x => x.X == neigh.X && x.Y == neigh.Y))
                        {
                            temp.Add(neigh);
                            q.Enqueue(temp);
                        }
                        else
                        {
                            var blah = 10;
                        }
                    }

                    if  (grid.Get(cur) == 9 && current.Count == 10 && EachStepAbove(grid, current) && !endsHash.Contains(cur))
                    {
                        ends++;
                        endsHash.Add(cur);
                    }
                    // do something with the current node
                }
                count += ends;
            }
            return count;
        }

        private bool EachStepAbove(GridObject<int> grid, List<GridLocation<int>> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (grid.Get(list[i]) != i)
                {
                    return false;
                }
            }
            return true;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.IntGrid;
            long count = 0;

            var heads = grid.GetAllLocationWhereCellEqualsValue(0);

            foreach (var head in heads)
            {
                Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
                var visited = new HashSet<string>();
                // (0,0) can be anything, just needs to be your root item.
                q.Enqueue(new List<GridLocation<int>>() { head });
                var ends = 0;
                while (q.Count > 0)
                {
                    var current = q.Dequeue(); // This will contain a list of all the points you visited on the way
                    var cur = current.Last(); // this is just the most recent point
                    var stringy = ListExtensions.Stringify(current);
                    if (current == null || current.Count > 10 || visited.Contains(stringy) || !EachStepAbove(grid, current))
                        continue;

                    //Get the next nodes/grids/etc to visit next
                    visited.Add(stringy);
                    var neighs = grid.GetOrthogonalNeighbours(cur);
                    foreach (var neigh in neighs)
                    {
                        var temp = current.Clone();
                        if (!temp.Contains(neigh))
                        {
                            temp.Add(neigh);
                            q.Enqueue(temp);
                        }
                        else
                        {
                            var blah = 10;
                        }
                    }

                    if (grid.Get(cur) == 9 && current.Count == 10 && EachStepAbove(grid, current))
                    {
                        ends++;
                    }
                    // do something with the current node
                }
                count += ends;
            }
            return count;
        }
    }
}