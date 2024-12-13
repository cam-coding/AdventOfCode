using System;
using Xunit;
using System.Collections.Generic;
using AdventLibrary.PathFinding;
using Xunit.Abstractions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using AdventLibrary;
using AdventLibrary.Helpers.Grids;

namespace AdventLibraryUnitTests
{
    public class ShortestPathUnitTests
    {
        private readonly ITestOutputHelper _output;

        public ShortestPathUnitTests(ITestOutputHelper output)
        {
            _output = output;
        }

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
        public void AstarTest_SmallInput()
        {
            var inputFilepath = UnitTestStaticValues.TestDataPath + "Pathfinding\\2021day15part1example.txt";
            var parser = new InputParser(inputFilepath);
            var grid = parser.GetLinesAsGrid<int>();
            var startLocation = grid.GetTopLeftCorner();
            var endLocation = grid.GetBottomRightCorner();

            // Run A*
            var aStarGrid = new AStar_GridObject<int>(grid, new List<int>(), grid.GetOrthogonalNeighbours);
            var astar = AStarFactory.CreateFromGrid(grid);
            astar.Search(startLocation, endLocation);

            Assert.Equal(40, astar.GetCost(endLocation));
            Assert.Equal(19, astar.GetPath(endLocation).Count);
        }

        [Fact]
        public void AstarTest_LargeInput()
        {
            var inputFilepath = UnitTestStaticValues.TestDataPath + "Pathfinding\\2021day15part2example.txt";
            var parser = new InputParser(inputFilepath);
            var grid = parser.GetLinesAsGrid<int>();
            var startLocation = grid.GetTopLeftCorner();
            var endLocation = grid.GetBottomRightCorner();

            // Run A*
            var aStarGrid = new AStar_GridObject<int>(grid, new List<int>(), grid.GetOrthogonalNeighbours);
            var astar = AStarFactory.CreateFromGrid(grid);
            astar.Search(startLocation, endLocation);

            Assert.Equal(315, astar.GetCost(endLocation));
            Assert.Equal(99, astar.GetPath(endLocation).Count);
        }
    }
}