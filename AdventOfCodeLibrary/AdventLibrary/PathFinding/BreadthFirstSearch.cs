using System;
using System.Collections.Generic;

namespace AdventLibrary.PathFinding
{
    public class Graph<Location>
    {
        // NameValueCollection would be a reasonable alternative here, if
        // you're always using string location types
        public Dictionary<Location, Location[]> edges
            = new Dictionary<Location, Location[]>();

        public Location[] Neighbors(Location id)
        {
            return edges[id];
        }
    };


    public static class BreadthFirstSearch
    {
        public static void Search(Graph<string> graph, string start, string end = "")
        {
            var frontier = new Queue<string>();
            frontier.Enqueue(start);

            var reached = new HashSet<string>();
            reached.Add(start);

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current == end)
                {
                    return;
                }

                Console.WriteLine("Visiting {0}", current);
                foreach (var next in graph.Neighbors(current))
                {
                    if (!reached.Contains(next))
                    {
                        frontier.Enqueue(next);
                        reached.Add(next);
                    }
                }
            }
        }

        public static int FindShortestPath(
            int startX,
            int startY,
            int destX,
            int destY,
            Func<int,int, bool> condition = null,
            int maxX = int.MaxValue,
            int maxY = int.MaxValue)
        {
            var visited = new HashSet<(int, int)>();
            var solution = int.MaxValue;

            Queue<(int x, int y, int count)> q = new Queue<(int x, int y, int count)>();
            q.Enqueue((startX, startY, 0));
            while (q.Count > 0)
            {
                var current = q.Dequeue();

                if (visited.Contains((current.x, current.y)))
                {
                    continue;
                }

                if (current.count >= solution ||
                    current.x < 0 ||
                    current.x >= maxX ||
                    current.y < 0 ||
                    current.y >= maxY)
                {
                    continue;
                }

                if (condition == null || condition(current.x, current.y))
                {
                    visited.Add((current.x, current.y));

                    if (current.x == destX && current.y == destY)
                    {
                        if (current.count < solution)
                            solution = current.count;
                        continue;

                    }
                    else
                    {
                        q.Enqueue((current.x + 1, current.y, current.count + 1));
                        q.Enqueue((current.x - 1, current.y, current.count + 1));
                        q.Enqueue((current.x, current.y + 1, current.count + 1));
                        q.Enqueue((current.x, current.y - 1, current.count + 1));
                    }
                }
            }

            return solution;
        }
    }
}

