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
                    grid.walls.Add(new Location(x, y));
                }
            }
            grid.forests = new HashSet<Location>
                {
                    new Location(3, 4), new Location(3, 5),
                    new Location(4, 1), new Location(4, 2),
                    new Location(4, 3), new Location(4, 4),
                    new Location(4, 5), new Location(4, 6),
                    new Location(4, 7), new Location(4, 8),
                    new Location(5, 1), new Location(5, 2),
                    new Location(5, 3), new Location(5, 4),
                    new Location(5, 5), new Location(5, 6),
                    new Location(5, 7), new Location(5, 8),
                    new Location(6, 2), new Location(6, 3),
                    new Location(6, 4), new Location(6, 5),
                    new Location(6, 6), new Location(6, 7),
                    new Location(7, 3), new Location(7, 4),
                    new Location(7, 5)
                };

            // Run A*
            var astar = new AStarSearch(grid, new Location(1, 4),
                                        new Location(3,4));

            DrawGrid(grid, astar);
        }

        private void DrawGrid(SquareGrid grid, AStarSearch astar) {
        // Print out the cameFrom array
        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                Location id = new Location(x, y);
                Location ptr = id;
                if (!astar.cameFrom.TryGetValue(id, out ptr))
                {
                    ptr = id;
                }
                if (grid.walls.Contains(id)) { Console.Write("##"); }
                else if (ptr.x == x+1) { Console.Write("\u2190 "); }
                else if (ptr.x == x-1) { Console.Write("\u2190 "); }
                else if (ptr.y == y+1) { Console.Write("\u2193 "); }
                else if (ptr.y == y-1) { Console.Write("\u2191 "); }
                else { Console.Write("* "); }
            }
            Console.WriteLine();
        }
    }
    }
}
