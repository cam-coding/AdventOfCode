using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.CustomObjects;
using AdventLibrary.Helpers;
using AdventLibrary.PathFinding;

namespace aoc2022
{
    public class Day12: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1(bool isTest = false)
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var startTuple = GridHelper.GetPointWhere(grid, 'S');
            var endTuple = GridHelper.GetPointWhere(grid, 'E');
            grid[startTuple.y][startTuple.x] = 'a';
            grid[endTuple.y][endTuple.x] = 'z';
            var nodeLookup = GraphHelper.TransformGridToGraph(grid);
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    var currentTuple = (y, x);
                    var node = nodeLookup[currentTuple];
                    var currentChar = grid[y][x];
                    var neighs = GridHelper.GetAdjacentNeighbours(grid, y, x);
                    foreach (var neigh in GridHelper.GetAdjacentNeighbours(grid, y, x))
                    {
                        var otherChar = grid[neigh.Item1][neigh.Item2];
                        var otherNode = nodeLookup[neigh];
                        if (otherChar - 1 <= currentChar)
                        {
                            if (!node.EdgesOut.Any(x => x.End.Equals(otherNode)))
                            {
                                node.EdgesOut.Add(new CustomEdge<char>(node, otherNode, true));
                            }
                        }
                        if (currentChar - 1 <= otherChar)
                        {
                            if (!otherNode.EdgesOut.Any(x => x.End.Equals(node)))
                            {
                                otherNode.EdgesOut.Add(new CustomEdge<char>(otherNode, node, true));
                            }
                        }
                    }

                }
            }
            var results2 = CustomDijkstra<char>.Search(nodeLookup[startTuple], nodeLookup[endTuple]);
            return results2.Distance;
        }

        private object Part2(bool isTest = false)
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var startTuple = GridHelper.GetPointWhere(grid, 'S');
            var endTuple = GridHelper.GetPointWhere(grid, 'E');
            grid[startTuple.y][startTuple.x] = 'a';
            grid[endTuple.y][endTuple.x] = 'z';
            var nodeLookup = GraphHelper.TransformGridToGraph(grid);
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[0].Count; x++)
                {
                    var currentTuple = (y, x);
                    var node = nodeLookup[currentTuple];
                    var currentChar = grid[y][x];
                    var neighs = GridHelper.GetAdjacentNeighbours(grid, y, x);
                    foreach (var neigh in GridHelper.GetAdjacentNeighbours(grid, y, x))
                    {
                        var otherChar = grid[neigh.Item1][neigh.Item2];
                        var otherNode = nodeLookup[neigh];
                        if (otherChar - 1 <= currentChar)
                        {
                            if (!node.EdgesOut.Any(x => x.End.Equals(otherNode)))
                            {
                                node.EdgesOut.Add(new CustomEdge<char>(node, otherNode, true));
                            }
                        }
                        if (currentChar - 1 <= otherChar)
                        {
                            if (!otherNode.EdgesOut.Any(x => x.End.Equals(node)))
                            {
                                otherNode.EdgesOut.Add(new CustomEdge<char>(otherNode, node, true));
                            }
                        }
                    }

                }
            }

            var best = int.MaxValue;
            foreach (var start in nodeLookup.Values)
            {
                if (start.Key.Equals('a'))
                {
                    var results2 = CustomDijkstra<char>.Search(nodeLookup[startTuple], nodeLookup[endTuple]);
                    if (results2 != default)
                    {
                        best = Math.Min(best, results2.Distance);
                    }
                }
            }
            return best;
        }
    }
}