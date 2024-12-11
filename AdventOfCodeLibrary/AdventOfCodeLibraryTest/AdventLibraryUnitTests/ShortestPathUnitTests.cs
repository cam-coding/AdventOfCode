using System;
using Xunit;
using System.Collections.Generic;
using AdventLibrary.PathFinding;

namespace AdventLibraryUnitTests
{
    public class ShortestPathUnitTests
    {
        [Fact]
        public void BreadthFirst()
        {
            AdventLibrary.PathFinding.Graph<string> g = new AdventLibrary.PathFinding.Graph<string>();
            g.edges = new Dictionary<string, string[]>
                {
                { "A", new [] { "B", "C" } },
                { "B", new [] { "A", "X" } },
                { "C", new [] { "A", "D", "F" } },
                { "D", new [] { "C", "E" } },
                { "E", new [] { "D", "F" } },
                { "F", new [] { "E", "C" } },
                { "X", new [] { "B" } }
            };

            AdventLibrary.PathFinding.BreadthFirstSearch.Search(g, "A", "D");
        }

        [Fact]
        public void AstarTest()
        {
            // Make "diagram 4" from main article
            var grid = new SquareGrid(10, 10);
            for (var x = 1; x < 4; x++)
            {
                for (var y = 7; y < 9; y++)
                {
                    grid.walls.Add(new AStarLocation(x, y));
                }
            }
            grid.forests = new HashSet<AStarLocation>
                {
                    new AStarLocation(3, 4), new AStarLocation(3, 5),
                    new AStarLocation(4, 1), new AStarLocation(4, 2),
                    new AStarLocation(4, 3), new AStarLocation(4, 4),
                    new AStarLocation(4, 5), new AStarLocation(4, 6),
                    new AStarLocation(4, 7), new AStarLocation(4, 8),
                    new AStarLocation(5, 1), new AStarLocation(5, 2),
                    new AStarLocation(5, 3), new AStarLocation(5, 4),
                    new AStarLocation(5, 5), new AStarLocation(5, 6),
                    new AStarLocation(5, 7), new AStarLocation(5, 8),
                    new AStarLocation(6, 2), new AStarLocation(6, 3),
                    new AStarLocation(6, 4), new AStarLocation(6, 5),
                    new AStarLocation(6, 6), new AStarLocation(6, 7),
                    new AStarLocation(7, 3), new AStarLocation(7, 4),
                    new AStarLocation(7, 5)
                };

            // Run A*
            var astar = new AStarSearch<AStarLocation>(grid, new AStarLocation(1, 4),
                                        new AStarLocation(3, 4));

            DrawGrid(grid, astar);
        }

        private void DrawGrid(SquareGrid grid, AStarSearch<AStarLocation> astar)
        {
            // Print out the cameFrom array
            for (var y = 0; y < 10; y++)
            {
                for (var x = 0; x < 10; x++)
                {
                    AStarLocation id = new AStarLocation(x, y);
                    AStarLocation ptr = id;
                    if (!astar.cameFrom.TryGetValue(id, out ptr))
                    {
                        ptr = id;
                    }
                    if (grid.walls.Contains(id)) { Console.Write("##"); }
                    else if (ptr.X == x + 1) { Console.Write("\u2190 "); }
                    else if (ptr.X == x - 1) { Console.Write("\u2190 "); }
                    else if (ptr.Y == y + 1) { Console.Write("\u2193 "); }
                    else if (ptr.Y == y - 1) { Console.Write("\u2191 "); }
                    else { Console.Write("* "); }
                }
                Console.WriteLine();
            }
        }
    }
}