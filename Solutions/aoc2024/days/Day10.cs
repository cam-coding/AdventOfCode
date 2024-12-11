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
    public class Day10 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            Part1_DFS();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.GridInt;
            long count = 0;

            var trailHeads = grid.GetAllLocationWhereCellEqualsValue(0);

            foreach (var head in trailHeads)
            {
                Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
                var visited = new HashSet<string>();
                q.Enqueue(new List<GridLocation<int>>() { head });
                var ends = 0;
                var endsHash = new HashSet<GridLocation<int>>();
                while (q.Count > 0)
                {
                    var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                    var currentLocation = fullPath.Last(); // this is just the most recent point
                    var currentValue = grid.Get(currentLocation);
                    var stringy = ListExtensions.Stringify(fullPath);
                    if (fullPath == null || visited.Contains(stringy) || fullPath.Count > 10)
                        continue;

                    //Get the next nodes/grids/etc to visit next
                    visited.Add(stringy);
                    foreach (var neighbour in grid.GetOrthogonalNeighbours(currentLocation))
                    {
                        var val = grid.Get(neighbour);

                        // this is very important. Always make sure the next value is valid in your search
                        // this might be checking total weight of path, or if the next value is accessible, etc.
                        if (val != currentValue + 1)
                        {
                            continue;
                        }
                        var temp = fullPath.Clone(); // very important, do not miss this clone
                        temp.Add(neighbour);
                        q.Enqueue(temp);
                    }

                    // checking for your goal
                    if (fullPath.Count == 10)
                    {
                        endsHash.Add(currentLocation);
                    }
                }
                count += endsHash.Count();
            }
            return count;
        }

        private object Part1_DFS(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.GridInt;
            long count = 0;

            var trailHeads = grid.GetAllLocationWhereCellEqualsValue(0);

            foreach (var head in trailHeads)
            {
                Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
                {
                    var neighbours = new List<GridLocation<int>>();
                    foreach (var edge in grid.GetOrthogonalNeighbours(node))
                    {
                        if (grid.Get(edge) == grid.Get(node) + 1)
                        {
                            neighbours.Add(edge);
                        }
                    }
                    return neighbours;
                };

                Func<GridLocation<int>, bool> GoalFunc = (node) =>
                {
                    return grid.Get(node) == 9;
                };

                Func<GridLocation<int>, int> WeightFunc = (node) =>
                {
                    return grid.Get(node);
                };
                var start = (0, new List<GridLocation<int>>() { head });
                var DFS = new DepthFirstSearch<GridLocation<int>>();
                DFS.DFSgeneric(start, NeighboursFunc, GoalFunc, WeightFunc);
                var boo = DFS.GoalAchieved;
            }

            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.GridInt;
            long count = 0;

            var trailHeads = grid.GetAllLocationWhereCellEqualsValue(0);

            foreach (var head in trailHeads)
            {
                Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
                var visited = new HashSet<string>();
                q.Enqueue(new List<GridLocation<int>>() { head });
                var ends = 0;
                var endsLocation = new List<GridLocation<int>>();
                while (q.Count > 0)
                {
                    var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                    var currentLocation = fullPath.Last(); // this is just the most recent point
                    var currentValue = grid.Get(currentLocation);
                    var stringy = ListExtensions.Stringify(fullPath);
                    if (fullPath == null || visited.Contains(stringy) || fullPath.Count > 10)
                        continue;

                    //Get the next nodes/grids/etc to visit next
                    visited.Add(stringy);
                    foreach (var neighbour in grid.GetOrthogonalNeighbours(currentLocation))
                    {
                        var val = grid.Get(neighbour);

                        // this is very important. Always make sure the next value is valid in your search
                        // this might be checking total weight of path, or if the next value is accessible, etc.
                        if (val != currentValue + 1)
                        {
                            continue;
                        }
                        var temp = fullPath.Clone(); // very important, do not miss this clone
                        temp.Add(neighbour);
                        q.Enqueue(temp);
                    }

                    // checking for your goal
                    if (fullPath.Count == 10)
                    {
                        endsLocation.Add(currentLocation);
                    }
                }
                count += endsLocation.Count();
            }
            return count;
        }
    }
}