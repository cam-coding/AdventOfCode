using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using AdventLibrary.PathFinding;

namespace aoc2024
{
    public class Day16 : ISolver
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
            return GoTime(out var i);
        }

        private object Part2(bool isTest = false)
        {
            int res = 0;
            GoTime(out res);
            return res;
        }

        public int Heuristic(GridLocation<int> a, GridLocation<int> b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public int GoTime(out int optimalPathSpots)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.GridChar;
            var start = grid.GetFirstLocationWhereCellEqualsValue('S');
            var end = grid.GetFirstLocationWhereCellEqualsValue('E');
            var pathsOnOptimalRoutes = new HashSet<GridLocation<int>>();
            var bestWeight = 300 * 1001;

            PriorityQueue<List<(GridLocation<int> node, GridLocation<int> directionEntered, int weight)>, int> q =
                new PriorityQueue<List<(GridLocation<int> node, GridLocation<int> directionEntered, int weight)>, int>();
            var visited = new Dictionary<(GridLocation<int> node, GridLocation<int> directionEntered), int>();
            q.Enqueue((new List<(GridLocation<int> node, GridLocation<int> directionEntered, int weight)>() { (start, Directions.Right, 0) }), 0);
            var counter = 0;
            while (q.Count > 0)
            {
                counter++;
                var historyToGetHere = q.Dequeue();
                var currentTuple = historyToGetHere.Last();
                var dictKey = (currentTuple.node, currentTuple.directionEntered);
                var currentLocation = currentTuple.node;
                var currentDirection = currentTuple.directionEntered;
                var weight = currentTuple.weight;

                if (currentLocation == end)
                {
                    if (weight > bestWeight)
                    {
                        continue;
                    }
                    var path = historyToGetHere.Select(x => x.node).ToList();
                    if (weight == bestWeight)
                    {
                        pathsOnOptimalRoutes.AddRange(path);
                    }
                    else if (weight < bestWeight)
                    {
                        bestWeight = Math.Min(bestWeight, weight);
                        pathsOnOptimalRoutes = path.ToHashSet();
                    }
                    continue;
                }

                if (historyToGetHere == null || weight > bestWeight)
                    continue;
                if (visited.ContainsKey(dictKey))
                {
                    if (visited[dictKey] < weight)
                    {
                        continue;
                    }
                }

                if (!visited.ContainsKey(dictKey) || weight < visited[dictKey])
                {
                    visited[dictKey] = weight;
                }

                //Get the next nodes/grids/etc to visit next
                foreach (var neighbour in grid.GetOrthogonalNeighbours(currentLocation))
                {
                    if (grid.Get(neighbour) == '#')
                    {
                        continue;
                    }
                    var dirToNeighbour = neighbour - currentLocation;
                    if (dirToNeighbour == Directions.Opposites[currentDirection])
                    {
                        continue;
                    }

                    var tempWeight = dirToNeighbour == currentDirection ? 1 : 1001;
                    var weightToNeighbour = weight + tempWeight;
                    var neighbourDictKey = (neighbour, dirToNeighbour);
                    if (visited.ContainsKey(neighbourDictKey))
                    {
                        if (visited[neighbourDictKey] < weightToNeighbour)
                        {
                            continue;
                        }
                    }

                    var temp = new List<(GridLocation<int> node, GridLocation<int> directionEntered, int weight)>(historyToGetHere); // very important, do not miss this clone
                    var neighbourTuple = (neighbour, dirToNeighbour, weightToNeighbour);
                    temp.Add(neighbourTuple);
                    var hur = Heuristic(neighbour, end);
                    visited[neighbourDictKey] = weightToNeighbour;
                    q.Enqueue(temp, weight);
                }
            }

            var sorted = pathsOnOptimalRoutes.OrderBy(x => x.X).ThenByDescending(x => x.Y).ToList();
            optimalPathSpots = pathsOnOptimalRoutes.Count;
            return bestWeight;
        }
    }
}