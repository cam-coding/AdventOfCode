using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.CustomObjects;
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
            var results2 = Dijkstra<char>.SearchToEnd(nodeLookup[startTuple], nodeLookup[endTuple]);
            return results2.Distance;
        }

        private object Part2(bool isTest = false)
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var startTuple = GridHelper.GetPointWhere(grid, 'S');
            var endTuple = GridHelper.GetPointWhere(grid, 'E');
            grid[startTuple.y][startTuple.x] = 'a';
            grid[endTuple.y][endTuple.x] = 'z';

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
                    var results2 = Dijkstra<char>.SearchToEnd(nodeLookup[start.Key], nodeLookup[endTuple]);
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