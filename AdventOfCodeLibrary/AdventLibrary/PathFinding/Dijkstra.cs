using System;
using System.Collections.Generic;
using AdventLibrary.CustomObjects;

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

        //untested, need to work on maybe
        public static Dictionary<(int y, int x), (int Distance, List<(int y, int x)> Path)> Search(
            Dictionary<(int y, int x), List<(int y, int x)>> adjacencyList,
            (int y, int x) start,
            (int y, int x) end)
        {
            var distanceDictionary = new Dictionary<(int y, int x), (int Distance, List<(int y, int x)> Path)>();
            // only visit each node once
            var processed = new HashSet<(int y, int x)>();
            var queue = new PriorityQueue<((int y, int x) CurrentNode, List<(int y, int x)> Path), int>();
            distanceDictionary.Add(start, (0, new List<(int y, int x)>()));
            queue.Enqueue((start, new List<(int y, int x)>()), 0);

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

