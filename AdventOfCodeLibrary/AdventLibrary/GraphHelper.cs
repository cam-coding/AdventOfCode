using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    var node = new CustomNode<T>(grid[y][x]);
                    nodeLookup[(y, x)] = node;
                }
            }
            return nodeLookup;
        }

        /*
        public static void ConnectGraph<T>(Dictionary<(int y, int x), CustomNode<T>> graph, Func<string, void>)
        {
        }*/
    }
}
