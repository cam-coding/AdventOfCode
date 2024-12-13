using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;

namespace AdventLibrary.PathFinding
{
    /* Paste these example into the code
     * and rework them for the day's problem
     */

    public static class PathFindingExamples
    {
        /* Use BFS as your search algorithm if you want to:
         * Find all paths
         * Find multiple paths
         * Find any path
         * */

        public static void BFS_Example()
        {
            var exampleGrid = new GridObject<int>(new List<List<int>>());
            Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
            var fullPathHistory = new HashSet<string>();

            // (0,0) can be anything, just needs to be your starting point.
            var head = new GridLocation<int>(0, 0);
            q.Enqueue(new List<GridLocation<int>>() { head });
            var endsHash = new HashSet<GridLocation<int>>();
            while (q.Count > 0)
            {
                var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var currentLocation = fullPath.Last(); // this is just the most recent point
                var currentValue = exampleGrid.Get(currentLocation);
                var stringy = ListExtensions.Stringify(fullPath);

                if (
                    fullPath == null ||
                    fullPathHistory.Contains(stringy) ||
                    fullPath.Count > 10) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                    continue;

                // make sure to add current state to history
                fullPathHistory.Add(stringy);

                //Get the next nodes/grids/etc to visit next
                foreach (var neighbour in exampleGrid.GetOrthogonalNeighbours(currentLocation))
                {
                    var val = exampleGrid.Get(neighbour);

                    // this is very important. Always make sure the next value is valid in your search
                    // this might be checking total weight of path, or if the next value is accessible, etc.
                    if (
                        val != currentValue + 1 || // for example make sure the next value is valid
                        fullPath.Sum(x => exampleGrid.Get(x)) + val < 100 // or make sure the total weight hasn't gone overboard
                        )
                    {
                        continue;
                    }
                    var temp = fullPath.Clone(); // very important, do not miss this clone
                    temp.Add(neighbour);
                    q.Enqueue(temp);
                }

                // This is where you check your end goal
                if (fullPath.Count == 10 || //check path of a certain length is achieved
                    currentValue == 90) // or check you've reached a certain value
                {
                    endsHash.Add(currentLocation);
                }
            }
        }

        private static void DFS_Example()
        {
            var exampleGrid = new List<List<int>>();
            var exampleGridObject = new GridObject<int>(exampleGrid);

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in exampleGridObject.GetOrthogonalNeighbours(node))
                {
                    neighbours.Add(edge);
                }
                return neighbours;
            };

            Func<GridLocation<int>, bool> GoalFunc = (node) =>
            {
                return node.Y == 3 && node.X == 3;
            };

            Func<GridLocation<int>, int> WeightFunc = (node) =>
            {
                return exampleGrid[node.Y][node.X];
            };
            var start = (0, new List<GridLocation<int>>() { new GridLocation<int>(0, 0) });
            var DFS = new DepthFirstSearch<GridLocation<int>>();
            DFS.DFSgeneric(start, NeighboursFunc, GoalFunc, WeightFunc);
        }

        /* Use BFS as your search algorithm if you want to:
         * Find shortest path/most effecient path
         * Find paths from one location to all others
         * Best fast AF
         * */

        private static void AStar_Example()
        {
            var exampleGrid = new List<List<int>>();
            var exampleGridObject = new GridObject<int>(exampleGrid);
            var startLocation = exampleGridObject.GetTopLeftCorner();
            var endLocation = exampleGridObject.GetBottomRightCorner();

            // Create A* from a grid

            // any items in this list will be "walls" and those nodes will be ignored
            var wallValues = new List<int>();

            // how you want to calculate neighbours.
            var func = exampleGridObject.GetOrthogonalNeighbours;

            var aStarGrid = new AStar_GridObject<int>(exampleGridObject, wallValues, func);
            var astar = AStarFactory.CreateFromGrid(exampleGridObject);

            // Run A*
            astar.Search(startLocation, endLocation);

            // this will get you the cost of the shortest path going from the start to any location
            // note this doesn't include the value of the starting location
            var costToReachEnd = astar.GetCost(endLocation);

            // note this includes the first location
            var pathToReachEnd = astar.GetPath(endLocation);
        }
    }
}