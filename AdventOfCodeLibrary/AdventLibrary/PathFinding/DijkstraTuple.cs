using System;
using System.Collections.Generic;

/*
* Implementation of the C++ version laid out in "Competitive Programmer’s Handbook" by Antti Laaksonen
*/

namespace AdventLibrary.PathFinding
{
    public class DijkstraTupleNode
    {
        public DijkstraTupleNode(int x, int y, int distance)
        {
            x = X;
            y = Y;
            distance = Distance;
        }
        public int X;
        public int Y;
        public int Distance;
    };


    public static class DijkstraTuple
    {
        // if missing item, distance is infinite
        public static Dictionary<Tuple<int,int>, int> DistanceDictionary;

        public static Dictionary<Tuple<int,int>, List<Tuple<int,int>>> Adj;

        public static Dictionary<Tuple<int, int>, int> Search(List<List<int>> grid, Tuple<int,int> start)
        {
            Adj = GridHelperWeirdTypes.GridToTupleAdjList(grid);
            DistanceDictionary = new Dictionary<Tuple<int, int>, int>();
            var processed = new Dictionary<Tuple<int, int>, bool>();
            var queue = new PriorityQueue<Tuple<int,int>, int>();
            DistanceDictionary.Add(start, 0);
            queue.Enqueue(start, 0);

            while(queue.Count != 0)
            {
                var current = queue.Dequeue();
                if (processed.ContainsKey(current))
                {
                    continue;
                }
                processed.Add(current, true);
                if (!DistanceDictionary.ContainsKey(current))
                {
                    DistanceDictionary.Add(current, Int32.MaxValue);
                }

                foreach (var neighbour in Adj[current])
                {
                    if (!DistanceDictionary.ContainsKey(neighbour))
                    {
                        DistanceDictionary.Add(neighbour, Int32.MaxValue);
                    }
                    var weight = grid[neighbour.Item2][neighbour.Item1];
                    if (DistanceDictionary[current] + weight < DistanceDictionary[neighbour])
                    {
                        var neighbourDistance = DistanceDictionary[current] + weight;
                        DistanceDictionary[neighbour] = neighbourDistance;
                        queue.Enqueue(neighbour, neighbourDistance);
                    }
                }

            }

            return DistanceDictionary;
        }
    }
}

