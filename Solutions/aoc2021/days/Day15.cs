using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using AdventLibrary;

namespace aoc2021
{
    public class Day15: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
        private string _filePath;
        private List<List<int>> _grid;
        private Dictionary<Tuple<int,int>, List<Tuple<int,int>>> graph;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var grid = AdventLibrary.ParseInput.ParseFileAsGrid(_filePath);
            var dist = AdventLibrary.PathFinding.DijkstraTuple.Search(grid, new Tuple<int, int>(0,0));
            var blah = dist[new Tuple<int, int>(grid.Count-1,grid[0].Count-1)];
            var betterGrid = MakeMyGrid(grid);
            var pather = new AStarSharp.Astar(betterGrid);
            var path = pather.FindPath(new Vector2(0,0), new Vector2(grid.Count-1,grid[0].Count-1));
            return path.Sum(x => x.Weight);
        }
        
        private object Part2()
        {
            var grid = AdventLibrary.ParseInput.ParseFileAsGrid(_filePath);
            var grid2 = CreateLargerGrid(grid);
            var dist = AdventLibrary.PathFinding.DijkstraTuple.Search(grid2, new Tuple<int, int>(0,0));
            var blah2 = dist[new Tuple<int, int>(grid2.Count-1,grid2[0].Count-1)];
            return blah2;
            /*
            This used the Astar stuff that didn't really work...
            var betterGrid = MakeMyGrid(grid2);
            var pather = new AStarSharp.Astar(betterGrid);
            var path = pather.FindPath(new Vector2(grid2.Count-1,grid2[0].Count-1), new Vector2(0,0));
            var pathArray = path.ToArray();
            var blah = pathArray[pathArray.Count() - 1];
            return path.Sum(x => x.Weight); */
        }

        private List<List<int>> CreateLargerGrid(List<List<int>> grid)
        {
            var giantGrid = new List<List<int>>();
            var width = grid[0].Count;
            var height = grid.Count;
            for (var i = 0; i < 5; i++)
            {
                for (var a = 0; a < height; a++)
                {
                    giantGrid.Add(new List<int>());
                }
                for (var j = 0; j < 5; j++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var currentY = i*height + y;
                        for (var x = 0; x < width; x++)
                        {
                            var value = grid[y][x] + (i+j);
                            if ( value > 9)
                            {
                                value = value - 9;
                            }
                            giantGrid[currentY].Add(value);
                        }
                    }
                }
            }

            return giantGrid;
        }

        private List<List<AStarSharp.Node>> MakeMyGrid(List<List<int>> grid)
        {
            List<List<AStarSharp.Node>> nodeGrid = new List<List<AStarSharp.Node>>();
            for (var i = 0; i  < grid.Count; i++)
            {
                nodeGrid.Add(new List<AStarSharp.Node>());
                for (var j = 0; j < grid[0].Count; j++)
                {
                    nodeGrid[i].Add(new AStarSharp.Node(new Vector2(i,j), grid[i][j]));
                }
            }
            return nodeGrid;
        }

        private Dictionary<Tuple<int,int>, List<Tuple<int,int>>> GetGraphFromGrid(List<List<int>> grid)
        {
            var dict = new Dictionary<Tuple<int,int>, List<Tuple<int,int>>>();
            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[0].Count; j++)
                {
                    var toople = new Tuple<int,int>(i, j);
                    dict.Add(toople, AdventLibrary.GridHelperWeirdTypes.GetOrthoginalNeighbours(grid, i, j));
                }
            }
            return dict;
        }
    }
}