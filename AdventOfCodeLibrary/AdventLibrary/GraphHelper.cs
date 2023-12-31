using System.Collections.Generic;
using System.Linq;
using AdventLibrary.CustomObjects;
using AdventLibrary.Helpers;

namespace AdventLibrary
{
    public static class GraphHelper
    {
        private static char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public static Dictionary<(int y, int x), CustomNode<T>> TransformGridToGraph<T>(List<List<T>> grid)
        {
            var nodeLookup = new Dictionary<(int y, int x), CustomNode<T>>();
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    nodeLookup[(y, x)] = new CustomNode<T>(grid[y][x], $"y:{y},x:{x}");
                }
            }
            GridToGraphAddConnections(nodeLookup, grid);
            return nodeLookup;
        }

        private static void GridToGraphAddConnections<T>(Dictionary<(int y, int x), CustomNode<T>> graph, List<List<T>> grid)
        {
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    var node = graph[(y, x)];
                    foreach (var neigh in GridHelper.GetAdjacentNeighbours(grid, y, x))
                    {
                        var otherNode = graph[neigh];
                        node.EdgesOut.Add(new CustomEdge<T>(node, otherNode, true));
                    }
                }
            }
        }

        private static Dictionary<T, CustomNode<T>> AdjacencyListToGraph<T>(Dictionary<T, List<T>> adjList)
        {
            var nodeLookup = new Dictionary<T, CustomNode<T>>();
            foreach (var pair in adjList)
            {
                var key = pair.Key;
                var cons = pair.Value;

                CustomNode<T> node = nodeLookup.GetOrCreate(key, new CustomNode<T>(key, key.ToString()));
                foreach (var item in cons)
                {
                    var otherNode = nodeLookup.GetOrCreate(item, new CustomNode<T>(item, item.ToString()));
                    node.EdgesOut.Add(new CustomEdge<T>(node, otherNode, true, 1));
                }
            }
            return nodeLookup;
        }

        // loop through the lines and look in the form of "key {seperator} key2, key3, key4"
        private static Dictionary<string, CustomNode<string>> InputToGraph(List<string> lines, string seperator)
        {
            var adjList = new Dictionary<string, List<string>>();

            foreach (var line in lines)
            {
                var tokens = line.Split(seperator).ToList().OnlyRealStrings(delimiterChars);
                var key = tokens[0];
                var connectedKeys = tokens[1].Split(delimiterChars).ToList().OnlyRealStrings(delimiterChars);
                adjList.Add(key, connectedKeys);
            }
            return AdjacencyListToGraph(adjList);
        }
    }
}
