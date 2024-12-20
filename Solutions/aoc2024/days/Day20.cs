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
    public class Day20 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<GridLocation<int>, (int Distance, List<GridLocation<int>> Path)> _firstDict;

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            return "skipped";
            var input = new InputObjectCollection(_filePath);
            var count = 0;
            var grid = input.GridChar;

            var startLocation = grid.GetFirstLocationWhereCellEqualsValue('S');
            var endLocation = grid.GetFirstLocationWhereCellEqualsValue('E');
            grid.Set(startLocation, 'a');
            grid.Set(endLocation, 'z');

            GridLocation<int> magic = null;

            Func<GridLocation<int>, (int Distance, List<GridLocation<int>> Path), List<GridLocation<int>>> NeighboursFunc = (node, nodeHistory) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in grid.GetOrthogonalNeighbours(node))
                {
                    if (grid.Get(edge) != '#' || edge == magic)
                    {
                        neighbours.Add(edge);
                    }
                }
                return neighbours;
            };
            Func<GridLocation<int>, GridLocation<int>, int> WeightFunc = (current, neigh) =>
            {
                return 1;
            };

            Func<GridLocation<int>, bool> GoalFunc = (current) =>
            {
                return current == endLocation;
            };
            var validCheat = false;
            var blah = new Day20Dijkstra<GridLocation<int>>(null, 0);
            _firstDict = blah.SearchEverywhere(startLocation, NeighboursFunc, WeightFunc, GoalFunc, out validCheat);
            var firstRun = _firstDict[endLocation].Distance;

            Func<GridLocation<int>, bool> FindStart = (current) =>
            {
                return current == startLocation;
            };

            var distancesFromEnd = blah.SearchEverywhere(endLocation, NeighboursFunc, WeightFunc, FindStart, out validCheat);

            var firstPath = _firstDict[endLocation].Path;
            var totalMoves = firstPath.Count;

            var special = isTest ? 2 : 100;
            var trueSpecial = special;
            var realCount = 0;

            var tempDict = new Dictionary<int, int>();

            foreach (var node in firstPath)
            {
                var neighs = grid.GetOrthogonalNeighbours(node);
                var movesLeft = totalMoves - firstPath.IndexOf(node) + 1;
                foreach (var neigh in neighs)
                {
                    if (grid.Get(neigh) == '#')
                    {
                        var dif = neigh - node;
                        var nextLoc = neigh + dif;
                        if (distancesFromEnd.ContainsKey(nextLoc))
                        {
                            var improve = distancesFromEnd[nextLoc].Distance - movesLeft;
                            if (improve >= trueSpecial)
                            {
                                realCount++;
                                if (tempDict.ContainsKey(improve))
                                {
                                    tempDict[improve]++;
                                }
                                else
                                {
                                    tempDict.Add(improve, 1);
                                }
                            }
                        }
                    }
                }
            }

            return realCount;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var count = 0;
            var grid = input.GridChar;

            var startLocation = grid.GetFirstLocationWhereCellEqualsValue('S');
            var endLocation = grid.GetFirstLocationWhereCellEqualsValue('E');

            GridLocation<int> magic = null;

            Func<GridLocation<int>, (int Distance, List<GridLocation<int>> Path), List<GridLocation<int>>> NeighboursFunc = (node, nodeHistory) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in grid.GetOrthogonalNeighbours(node))
                {
                    if (grid.Get(edge) != '#' || edge == magic)
                    {
                        neighbours.Add(edge);
                    }
                }
                return neighbours;
            };
            Func<GridLocation<int>, GridLocation<int>, int> WeightFunc = (current, neigh) =>
            {
                return 1;
            };

            Func<GridLocation<int>, bool> GoalFunc = (current) =>
            {
                return current == endLocation;
            };
            var validCheat = false;
            var blah = new Day20Dijkstra<GridLocation<int>>(null, 0);
            _firstDict = blah.SearchEverywhere(startLocation, NeighboursFunc, WeightFunc, GoalFunc, out validCheat);
            var firstRun = _firstDict[endLocation].Distance;

            Func<GridLocation<int>, bool> FindStart = (current) =>
            {
                return current == startLocation;
            };

            var distancesFromEnd = blah.SearchEverywhere(endLocation, NeighboursFunc, WeightFunc, FindStart, out validCheat);

            var firstPath = _firstDict[endLocation].Path;
            firstPath.Insert(0, startLocation);
            var totalMoves = firstPath.Count;

            var special = isTest ? 50 : 100;
            var trueSpecial = special;
            var realCount = 0;

            var tempDict = new Dictionary<int, int>();

            foreach (var node in firstPath)
            {
                var movesLeft = totalMoves - (firstPath.IndexOf(node) + 1);
                Func<GridLocation<int>, (int Distance, List<GridLocation<int>> Path), List<GridLocation<int>>> nFunc = (node, nodeHistory) =>
                {
                    var neighbours = new List<GridLocation<int>>();
                    if (nodeHistory.Path.Count <= 20)
                    {
                        foreach (var edge in grid.GetOrthogonalNeighbours(node))
                        {
                            neighbours.Add(edge);
                        }
                    }
                    return neighbours;
                };
                Func<GridLocation<int>, GridLocation<int>, int> wFunc = (current, neigh) =>
                {
                    return 1;
                };

                Func<GridLocation<int>, bool> gFunc = (current) =>
                {
                    return current == endLocation;
                };
                var nodeDijk = new Day20Dijkstra<GridLocation<int>>(null, 0);
                var nodeDistances = nodeDijk.SearchEverywhere(node, nFunc, wFunc, gFunc, out validCheat);
                /*
                if (nodeDistances.ContainsKey(endLocation))
                {
                    realCount++;
                }
                else
                {*/
                foreach (var item in nodeDistances)
                {
                    if (distancesFromEnd.ContainsKey(item.Key))
                    {
                        var distancyFromEnd = distancesFromEnd[item.Key].Distance;
                        var cheatLength = item.Value.Distance;
                        var improve = movesLeft - distancyFromEnd - cheatLength;
                        if (improve >= trueSpecial)
                        {
                            if (improve == 70)
                            {
                            }
                            realCount++;
                            if (tempDict.ContainsKey(improve))
                            {
                                tempDict[improve]++;
                            }
                            else
                            {
                                tempDict.Add(improve, 1);
                            }
                        }
                    }
                }
            }

            return realCount;
        }

        public class Day20Dijkstra<T>
        {
            private int _turn;
            private Dictionary<T, (int Distance, List<T> Path)> _myDict;
            private int _magic;

            public Day20Dijkstra(Dictionary<T, (int Distance, List<T> Path)> dict, int magic)
            {
                _myDict = dict;
                _magic = magic;
            }

            public Dictionary<T, (int Distance, List<T> Path)> SearchEverywhere(
                T start,
                Func<T, (int Distance, List<T> Path), List<T>> getNeighboursFunc,
                Func<T, T, int> getWeightFunc,
                Func<T, bool> goalFunc,
                out bool validCheat)
            {
                validCheat = false;
                var distanceDictionary = new Dictionary<T, (int Distance, List<T> Path)>();
                // only visit each node once
                var processed = new HashSet<T>();
                var queue = new PriorityQueue<(T CurrentNode, List<T> Path), int>();
                distanceDictionary.Add(start, (0, new List<T>()));
                queue.Enqueue((start, new List<T>()), 0);

                while (queue.Count != 0)
                {
                    var current = queue.Dequeue();
                    var currentNode = current.CurrentNode;
                    var path = current.Path;

                    if (_myDict != null && _myDict.ContainsKey(currentNode) && distanceDictionary.ContainsKey(currentNode))
                    {
                        var weight = distanceDictionary[currentNode].Distance;
                        if (weight <= _myDict[currentNode].Distance - _magic)
                        {
                            validCheat = true;
                            return distanceDictionary;
                        }
                    }

                    if (goalFunc(currentNode))
                    {
                        return distanceDictionary;
                    }

                    // only process each node once
                    if (processed.Contains(currentNode))
                    {
                        continue;
                    }
                    processed.Add(currentNode);

                    // each node we see starts with an infinite distance
                    if (!distanceDictionary.ContainsKey(currentNode))
                    {
                        distanceDictionary.Add(currentNode, (Int32.MaxValue, path));
                    }

                    // Neighbours are figured out before objects are passed in, so no logic around that here.
                    foreach (var neighbour in getNeighboursFunc(currentNode, distanceDictionary[currentNode]))
                    {
                        var newPath = path.Clone();
                        newPath.Add(neighbour);
                        if (!distanceDictionary.ContainsKey(neighbour))
                        {
                            distanceDictionary.Add(neighbour, (Int32.MaxValue, newPath));
                        }
                        var weight = getWeightFunc(currentNode, neighbour);
                        if (distanceDictionary[currentNode].Distance + weight < distanceDictionary[neighbour].Distance)
                        {
                            var neighbourDistance = distanceDictionary[currentNode].Distance + weight;
                            distanceDictionary[neighbour] = (neighbourDistance, newPath);
                            queue.Enqueue((neighbour, newPath), neighbourDistance);
                        }
                    }
                }

                return distanceDictionary;
            }
        }

        public class Day20DijkstraPart2<T>
        {
            private int _turn;
            private Dictionary<T, (int Distance, List<T> Path)> _myDict;
            private int _magic;

            public Day20DijkstraPart2(Dictionary<T, (int Distance, List<T> Path)> dict, int magic)
            {
                _myDict = dict;
                _magic = magic;
            }

            public Dictionary<T, (int Distance, List<T> Path)> SearchEverywhere(
                T start,
                Func<T, (int Distance, List<T> Path), List<T>> getNeighboursFunc,
                Func<T, T, int> getWeightFunc,
                Func<T, bool> goalFunc,
                out bool validCheat)
            {
                validCheat = false;
                var distanceDictionary = new Dictionary<T, (int Distance, List<T> Path)>();
                // only visit each node once
                var processed = new HashSet<T>();
                var queue = new PriorityQueue<(T CurrentNode, List<T> Path), int>();
                distanceDictionary.Add(start, (0, new List<T>()));
                queue.Enqueue((start, new List<T>()), 0);

                while (queue.Count != 0)
                {
                    var current = queue.Dequeue();
                    var currentNode = current.CurrentNode;
                    var path = current.Path;

                    if (_myDict != null && _myDict.ContainsKey(currentNode) && distanceDictionary.ContainsKey(currentNode))
                    {
                        var weight = distanceDictionary[currentNode].Distance;
                        if (weight <= _myDict[currentNode].Distance - _magic)
                        {
                            validCheat = true;
                            return distanceDictionary;
                        }
                    }

                    if (goalFunc(currentNode))
                    {
                        return distanceDictionary;
                    }

                    // only process each node once
                    if (processed.Contains(currentNode))
                    {
                        continue;
                    }
                    processed.Add(currentNode);

                    // each node we see starts with an infinite distance
                    if (!distanceDictionary.ContainsKey(currentNode))
                    {
                        distanceDictionary.Add(currentNode, (Int32.MaxValue, path));
                    }

                    // Neighbours are figured out before objects are passed in, so no logic around that here.
                    foreach (var neighbour in getNeighboursFunc(currentNode, distanceDictionary[currentNode]))
                    {
                        var newPath = path.Clone();
                        newPath.Add(neighbour);
                        if (!distanceDictionary.ContainsKey(neighbour))
                        {
                            distanceDictionary.Add(neighbour, (Int32.MaxValue, newPath));
                        }
                        var weight = getWeightFunc(currentNode, neighbour);
                        if (distanceDictionary[currentNode].Distance + weight < distanceDictionary[neighbour].Distance)
                        {
                            var neighbourDistance = distanceDictionary[currentNode].Distance + weight;
                            distanceDictionary[neighbour] = (neighbourDistance, newPath);
                            queue.Enqueue((neighbour, newPath), neighbourDistance);
                        }
                    }
                }

                return distanceDictionary;
            }
        }
    }
}