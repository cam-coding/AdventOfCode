using System.Collections.Generic;
using System.Linq;
using AdventLibrary.CustomObjects;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;

namespace AdventLibrary.Helpers
{
    public static class GraphHelper
    {
        private static char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public static Dictionary<GridLocation<int>, CustomNode<T>> TransformGridToGraph<T>(List<List<T>> grid)
        {
            var nodeLookup = new Dictionary<GridLocation<int>, CustomNode<T>>();
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    nodeLookup[new GridLocation<int>(x, y)] = new CustomNode<T>(grid[y][x], $"y:{y},x:{x}");
                }
            }
            GridToGraphAddConnections(nodeLookup, grid);
            return nodeLookup;
        }

        private static void GridToGraphAddConnections<T>(Dictionary<GridLocation<int>, CustomNode<T>> graph, List<List<T>> grid)
        {
            var gridObject = new GridObject<T>(grid);
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    var loc = new GridLocation<int>(x, y);
                    var node = graph[loc];
                    foreach (var neigh in gridObject.GetOrthogonalNeighbours(loc))
                    {
                        var otherNode = graph[neigh];
                        node.EdgesOut.Add(new CustomEdge<T>(node, otherNode, true));
                    }
                }
            }
        }

        public static Dictionary<T, CustomNode<T>> AdjacencyListToGraph<T>(Dictionary<T, List<T>> adjList)
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
    }
}