using System.Collections.Generic;
using AdventLibrary.CustomObjects;

namespace AdventLibrary
{
    public static class GraphHelper
    {
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
    }
}
