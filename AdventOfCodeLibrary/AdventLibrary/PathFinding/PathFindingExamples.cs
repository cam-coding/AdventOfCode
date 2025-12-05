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
                    fullPath.Count > 20) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
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

        /* Use this if you want to:
         * Find all connected nodes
         * Find a group in a grid/graph
         * Weights and paths don't matter
         * Find all transitive neighbours
         * */

        public static void BFS_AllConnectedNodes_Example()
        {
            // start with a dict and a startug node
            var dict = new Dictionary<string, List<string>>();
            var start = "0";

            Queue<string> q = new Queue<string>();
            var fullPathHistory = new HashSet<string>();
            var head = start;
            q.Enqueue(head);
            var endsHash = new HashSet<string>();
            while (q.Count > 0)
            {
                var current = q.Dequeue();

                if (current == null || fullPathHistory.Contains(current))
                    continue;

                fullPathHistory.Add(current);

                // can replace this with whatever is needed in this scenario
                var neighbours = dict[current].Where(x => !fullPathHistory.Contains(x));

                foreach (var neighbour in neighbours)
                {
                    q.Enqueue(neighbour);
                }
            }

            // now fullPathHistory has every interconnected node from the starting node
        }

        private static void BFS_AllBestPaths_Example()
        {
            // just examples for compiling
            var grid = new GridObject<int>(new List<List<int>>());
            GridLocation<int> start = new GridLocation<int>(0, 0);
            GridLocation<int> end = new GridLocation<int>(3, 3);

            Queue<List<GridLocation<int>>> q = new Queue<List<GridLocation<int>>>();
            var fullPathHistory = new HashSet<string>();

            var bestPaths = new List<List<GridLocation<int>>>();
            var bestPathLength = int.MaxValue;

            // (0,0) can be anything, just needs to be your starting point.
            q.Enqueue(new List<GridLocation<int>>() { start });
            while (q.Count > 0)
            {
                var fullPath = q.Dequeue(); // This will contain a list of all the points you visited on the way
                var currentLocation = fullPath.Last(); // this is just the most recent point
                var currentValue = grid.Get(currentLocation);
                var stringy = ListExtensions.Stringify(fullPath);

                if (
                    fullPathHistory.Contains(stringy) ||
                    fullPath.Count > 6 ||
                    fullPath.Count > bestPathLength) // Use this as a way to jump out of your current path. Length too long, total val too high, something.
                    continue;

                // make sure to add current state to history
                fullPathHistory.Add(stringy);

                // if we reach the end and it's as good as the best path, save it
                if (currentLocation == end)
                {
                    if (fullPath.Count <= bestPathLength)
                    {
                        if (fullPath.Count < bestPathLength)
                        {
                            bestPaths = new List<List<GridLocation<int>>>() { fullPath };
                            bestPathLength = fullPath.Count;
                        }
                        else
                        {
                            bestPaths.Add(fullPath);
                        }
                    }
                    continue;
                }

                //Get the next nodes/grids/etc to visit next
                foreach (var neighbour in grid.GetOrthogonalNeighbours(currentLocation))
                {
                    var val = grid.Get(neighbour);

                    if (val == 'X' || // can be any limiter
                        fullPath.Contains(neighbour)) // assumes we don't want to go to the same node twice
                    {
                        continue;
                    }
                    var temp = fullPath.Clone(); // very important, do not miss this clone
                    temp.Add(neighbour);
                    q.Enqueue(temp);
                }
            }

            var allPaths = bestPaths;
            var length = bestPathLength;
        }

        private static void DFS_Example()
        {
            var basicGrid = new List<List<int>>();
            var grid = new GridObject<int>(basicGrid);

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (current) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in grid.GetOrthogonalNeighbours(current))
                {
                    neighbours.Add(edge);
                }
                return neighbours;
            };

            Func<GridLocation<int>, bool> GoalFunc = (current) =>
            {
                return current.Y == 3 && current.X == 3;
            };

            Func<GridLocation<int>, int> WeightFunc = (current) =>
            {
                return basicGrid[current.Y][current.X];
            };
            var start = (0, new List<GridLocation<int>>() { new GridLocation<int>(0, 0) });
            var DFS = new DepthFirstSearch<GridLocation<int>>();
            DFS.DFSgeneric(start, NeighboursFunc, GoalFunc, WeightFunc);
        }

        private static void DFS_Weightless_Example()
        {
            var dict = new Dictionary<string, List<string>>();
            Func<string, List<string>> NeighboursFunc = (current) =>
            {
                if (dict.ContainsKey(current))
                {
                    return dict[current];
                }
                return new List<string>();
            };

            Func<string, bool> GoalFunc = (current) =>
            {
                return current.Equals("0");
            };

            var start = new List<string>() { "1" };
            var DFS = new DepthFirstSearch<string>();
            DFS.DFS_Weightless(start, NeighboursFunc, GoalFunc);
        }

        /* Use A-Star as your search algorithm if you want to:
         * Find shortest path/most effecient path
         * Find a SINGLE best path from one location to all others
         * Need a heuristic to guide the search
         * Be fast AF
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

            // use this to customize
            var aStarGrid = new AStar_GridObject<int>(exampleGridObject, wallValues, func);
            var aStartCustomized = new AStarSearcher<GridLocation<int>>(aStarGrid);

            // use this as the default
            var aStarDefault = AStarFactory.CreateFromGrid(exampleGridObject);

            // Run A*
            aStarDefault.Search(startLocation, endLocation);

            // this will get you the cost of the shortest path going from the start to any location
            // note this doesn't include the value of the starting location
            var costToReachEnd = aStarDefault.GetCost(endLocation);

            // note this includes the first location
            var pathToReachEnd = aStarDefault.GetPath(endLocation);
        }

        /* Use Dijkstra as your search algorithm if you want to:
         * Find shortest path/most effecient path
         * Find a SINGLE best path from one location to all others
         * */

        private static int Dijkstra_Example()
        {
            var charGrid = ParseInput.ParseFileAsCharGrid("");
            var grid = new GridObject<char>(charGrid);
            var startLocation = grid.GetFirstLocationWhereCellEqualsValue('S');
            var endLocation = grid.GetFirstLocationWhereCellEqualsValue('E');
            grid.Set(startLocation, 'a');
            grid.Set(endLocation, 'z');

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in grid.GetOrthogonalNeighbours(node))
                {
                    // remove any edges where the height difference is too great
                    if (grid.Get(edge) - 1 <= grid.Get(node))
                    {
                        neighbours.Add(edge);
                    }
                }
                return neighbours;
            };
            Func<GridLocation<int>, GridLocation<int>, int> WeightFunc = (current, neigh) =>
            {
                return grid.Get(neigh) - grid.Get(current);
            };

            Func<GridLocation<int>, bool> GoalFunc = (current) =>
            {
                return current == endLocation;
            };
            var res = Dijkstra<GridLocation<int>>.SearchEverywhere(startLocation, NeighboursFunc, WeightFunc, GoalFunc);
            return res[endLocation].Distance;
        }
    }
}