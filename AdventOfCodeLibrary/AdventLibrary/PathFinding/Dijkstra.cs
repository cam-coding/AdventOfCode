using System;
using System.Collections.Generic;
using AdventLibrary.CustomObjects;
using AdventLibrary.Extensions;

namespace AdventLibrary.PathFinding
{
    public static class Dijkstra<T>
    {
        public static Dictionary<CustomNode<T>, (int Distance, List<CustomNode<T>> Path)> SearchEverywhere(CustomNode<T> start)
        {
            return SearchEverywhere(start, null);
        }

        public static (int Distance, List<CustomNode<T>> Path) SearchToEnd(CustomNode<T> start, CustomNode<T> end)
        {
            var result = SearchEverywhere(start, end);
            if (result.ContainsKey(end))
            {
                return result[end];
            }
            return default;
        }

        // search from starting point to every point
        public static Dictionary<CustomNode<T>, (int Distance, List<CustomNode<T>> Path)> SearchEverywhere(CustomNode<T> start, CustomNode<T> end)
        {
            var distanceDictionary = new Dictionary<CustomNode<T>, (int Distance, List<CustomNode<T>> Path)>();
            // only visit each node once
            var processed = new HashSet<CustomNode<T>>();
            var queue = new PriorityQueue<(CustomNode<T> CurrentNode, List<CustomNode<T>> Path), int>();
            distanceDictionary.Add(start, (0,new List<CustomNode<T>>()));
            queue.Enqueue((start, new List<CustomNode<T>>()), 0);

            while(queue.Count != 0)
            {
                var current = queue.Dequeue();
                var currentNode = current.CurrentNode;
                var path = current.Path;

                if (currentNode.Equals(end))
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
                foreach (var edge in currentNode.EdgesOut)
                {
                    var neighbour = edge.GetOtherEnd(currentNode);
                    var newPath = path.Clone();
                    newPath.Add(neighbour);
                    if (!distanceDictionary.ContainsKey(neighbour))
                    {
                        distanceDictionary.Add(neighbour, (Int32.MaxValue, newPath));
                    }
                    var weight = edge.Weight;
                    if (distanceDictionary[currentNode].Distance + weight < distanceDictionary[neighbour].Distance)
                    {
                        var neighbourDistance = distanceDictionary[currentNode].Distance + weight;
                        distanceDictionary[neighbour] = (neighbourDistance,newPath);
                        queue.Enqueue((neighbour,newPath), neighbourDistance);
                    }
                }
            }

            return distanceDictionary;
        }

        // search from starting point to every point
        public static Dictionary<CustomNode<T>, (int Distance, List<CustomNode<T>> Path)> SearchEverywhereGeneric(
            CustomNode<T> start,
            Func<CustomNode<T>, List<CustomEdge<T>>> GetNeighboursFunc,
            CustomNode<T> goal = null,
            Func<CustomNode<T>, CustomNode<T>, int> Heuristic = null
            )
        {
            var distanceDictionary = new Dictionary<CustomNode<T>, (int Distance, List<CustomNode<T>> Path)>();
            // only visit each node once
            var processed = new HashSet<CustomNode<T>>();
            var queue = new PriorityQueue<(CustomNode<T> CurrentNode, List<CustomNode<T>> Path), int>();
            distanceDictionary.Add(start, (0, new List<CustomNode<T>>()));
            queue.Enqueue((start, new List<CustomNode<T>>()), 0);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                var currentNode = current.CurrentNode;
                var path = current.Path;

                if (goal != null && currentNode.Equals(goal))
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
                foreach (var edge in GetNeighboursFunc(currentNode))
                {
                    var neighbour = edge.GetOtherEnd(currentNode);
                    var newPath = path.Clone();
                    newPath.Add(neighbour);
                    if (!distanceDictionary.ContainsKey(neighbour))
                    {
                        distanceDictionary.Add(neighbour, (Int32.MaxValue, newPath));
                    }
                    var weight = edge.Weight;
                    if (distanceDictionary[currentNode].Distance + weight < distanceDictionary[neighbour].Distance)
                    {
                        var neighbourDistance = distanceDictionary[currentNode].Distance + weight;
                        // optional Heuristic
                        if (Heuristic != null)
                        {
                            neighbourDistance += Heuristic(neighbour, goal);
                        }
                        distanceDictionary[neighbour] = (neighbourDistance, newPath);
                        queue.Enqueue((neighbour, newPath), neighbourDistance);
                    }
                }
            }

            return distanceDictionary;
        }

        private static void ExampleNeighbourFunction()
        {
            Func<CustomNode<char>, List<CustomEdge<char>>> NeighboursFunct = (node) =>
            {
                var neighbours = new List<CustomEdge<char>>();
                foreach (var edge in node.EdgesOut)
                {
                    var otherNode = edge.GetOtherEnd(node);
                    neighbours.Add(edge);
                }
                return neighbours;
            };
        }

        //untested, need to work on maybe
        // can call this with things like (int y, int x) that I don't convert to my graph format
        public static Dictionary<T, (int Distance, List<T> Path)> Search(
            Dictionary<T, List<T>> adjacencyList,
            T start,
            T end)
        {
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

                if (currentNode.Equals(end))
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

                foreach (var neighbour in adjacencyList[currentNode])
                {
                    var newPath = path.Clone();
                    newPath.Add(neighbour);
                    if (!distanceDictionary.ContainsKey(neighbour))
                    {
                        distanceDictionary.Add(neighbour, (Int32.MaxValue, newPath));
                    }
                    var weight = 1;
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

