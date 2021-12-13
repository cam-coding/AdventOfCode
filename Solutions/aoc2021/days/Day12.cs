using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day12: ISolver
    {
        private string _filePath;
        private Dictionary<string, List<string>> _nodes;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            _nodes = AdventLibrary.ParseInput.ParseFileAsGraph(_filePath);
            return FindPaths(new List<string>(), "start", 1, false);
        }
        
        private object Part2()
        {
            _nodes = AdventLibrary.ParseInput.ParseFileAsGraph(_filePath);
            return FindPaths(new List<string>(), "start", 2, false);
        }

        private int FindPaths(List<string> path, string currentNode, int max, bool maxedOut)
        {
            if (path.Contains("start") && currentNode.Equals("start"))
            {
                return 0;
            }
            if (currentNode.All(x => Char.IsLower(x)) && path.Contains(currentNode) && maxedOut )
            {
                return 0;
            }
            if (currentNode.Equals("end"))
            {
                return 1;
            }

            var newPath = path.ToList();
            newPath.Add(currentNode);
            if (currentNode.All(x => Char.IsLower(x)))
            {
                maxedOut = maxedOut || (newPath.Count(x => x.Equals(currentNode)) == max);
            }
            var total = 0;
            foreach (var node in _nodes[currentNode])
            {
                total = total + FindPaths(newPath, node, max, maxedOut);
            }
            return total;
        }
    }
}