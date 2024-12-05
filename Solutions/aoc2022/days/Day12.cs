using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.CustomObjects;
using AdventLibrary.Helpers.Grids;
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
            var startTuple2 = GridHelper.GetPointWhere(grid, 'S');
            var startTuple = new GridLocation<int>(startTuple2.x, startTuple2.y);
            var endTuple2 = GridHelper.GetPointWhere(grid, 'E');
            var endTuple = new GridLocation<int>(endTuple2.x, endTuple2.y);
            grid[startTuple.Y][startTuple.X] = 'a';
            grid[endTuple.Y][endTuple.X] = 'z';

            // get grid as graph
            var nodeLookup = GraphHelper.TransformGridToGraph(grid);

            Func<CustomNode<char>, List<CustomEdge<char>>> NeighboursFunct = (node) =>
            {
                var neighbours = new List<CustomEdge<char>>();
                foreach (var edge in node.EdgesOut)
                {
                    var otherNode = edge.GetOtherEnd(node);
                    // remove any edges where the height difference is too great
                    if (otherNode.Value - 1 <= node.Value)
                    {
                        neighbours.Add(edge);
                    }
                }
                return neighbours;
            };
            var results = Dijkstra<char>.SearchEverywhereGeneric(nodeLookup[startTuple], NeighboursFunct, nodeLookup[endTuple]);
            return results[nodeLookup[endTuple]].Distance;
        }

        private object Part2(bool isTest = false)
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var startTuple2 = GridHelper.GetPointWhere(grid, 'S');
            var startTuple = new GridLocation<int>(startTuple2.x, startTuple2.y);
            var endTuple2 = GridHelper.GetPointWhere(grid, 'E');
            var endTuple = new GridLocation<int>(endTuple2.x, endTuple2.y);
            grid[startTuple.Y][startTuple.X] = 'a';
            grid[endTuple.Y][endTuple.X] = 'z';

            // get grid as graph
            var nodeLookup = GraphHelper.TransformGridToGraph(grid);
            foreach (var pair in nodeLookup)
            {
                var node = pair.Value;
                var removable = new List<CustomEdge<char>>();
                foreach (var edge in node.EdgesOut)
                {
                    var otherNode = edge.GetOtherEnd(node);
                    // remove any edges where the height difference is too great
                    if (otherNode.Value - 1 > node.Value)
                    {
                        removable.Add(edge);
                    }
                }
                foreach (var edge in removable)
                {
                    node.EdgesOut.Remove(edge);
                }
            }

            var best = int.MaxValue;
            foreach (var start in nodeLookup)
            {
                if (start.Value.Value.Equals('a'))
                {
                    var results = Dijkstra<char>.SearchToEnd(nodeLookup[start.Key], nodeLookup[endTuple]);
                    if (results != default)
                    {
                        best = Math.Min(best, results.Distance);
                    }
                }
            }
            return best;
        }
    }
}