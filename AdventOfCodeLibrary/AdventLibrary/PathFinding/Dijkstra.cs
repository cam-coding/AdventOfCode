using System;
using System.Collections.Generic;
using AdventLibrary.Extensions;

namespace AdventLibrary.PathFinding
{
    public static class Dijkstra<T>
    {
        public static Dictionary<T, (int Distance, List<T> Path)> SearchEverywhere(
            T start,
            Func<T, List<T>> getNeighboursFunc,
            Func<T, T, int> getWeightFunc,
            Func<T, bool> goalFunc)
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
                foreach (var neighbour in getNeighboursFunc(currentNode))
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