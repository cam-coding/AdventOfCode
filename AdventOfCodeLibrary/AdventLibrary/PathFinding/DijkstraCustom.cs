using System;
using System.Collections.Generic;
using AdventLibrary.CustomObjects;

namespace AdventLibrary.PathFinding
{
    public static class CustomDijkstra<T>
    {
        // search from starting point to every point
        public static Dictionary<CustomNode<T>, (int Distance, List<CustomNode<T>> Path)> Search(CustomNode<T> start)
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

                // only proces each node once
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
        public static (int Distance, List<CustomNode<T>> Path) Search(CustomNode<T> start, CustomNode<T> end)
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

                if (currentNode == end)
                {
                    return distanceDictionary[end];
                }

                // only proces each node once
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
                        distanceDictionary[neighbour] = (neighbourDistance, newPath);
                        queue.Enqueue((neighbour, newPath), neighbourDistance);
                    }
                }

            }

            return default;
        }
    }
}

