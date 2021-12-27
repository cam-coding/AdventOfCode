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
    }
}

