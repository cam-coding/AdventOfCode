using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using AdventLibrary.PathFinding;
using AStarSharp;

namespace aoc2024
{
    public class Day20 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var minCheatImprovement = isTest ? 2 : 100;
            var maxCheatLength = 2;
            return GoTime(maxCheatLength, minCheatImprovement);
        }

        private object Part2(bool isTest = false)
        {
            var minCheatImprovement2 = isTest ? 50 : 100;
            var maxCheatLength2 = 20;
            return GoTime(maxCheatLength2, minCheatImprovement2);
        }

        private int GoTime(int maxCheatLength, int minCheatImprovement)
        {
            var input = new InputObjectCollection(_filePath);
            var count = 0;
            var grid = input.GridChar;

            var startLocation = grid.GetFirstLocationWhereCellEqualsValue('S');
            var endLocation = grid.GetFirstLocationWhereCellEqualsValue('E');

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in grid.GetOrthogonalNeighbours(node))
                {
                    if (grid.Get(edge) != '#')
                    {
                        neighbours.Add(edge);
                    }
                }
                return neighbours;
            };
            Func<GridLocation<int>, GridLocation<int>, int> WeightFunc = (current, neigh) =>
            {
                return 1;
            };

            Func<GridLocation<int>, bool> GoalFunc = (current) =>
            {
                return current == endLocation;
            };
            var res = Dijkstra<GridLocation<int>>.SearchEverywhere(startLocation, NeighboursFunc, WeightFunc, GoalFunc);
            var firstRun = res[endLocation].Distance;
            var nonCheatPath = res[endLocation].Path;
            nonCheatPath.Insert(0, startLocation);

            var realCount = 0;

            for (var i = 0; i < nonCheatPath.Count; i++)
            {
                // we only need to look at points that are further ahead and are at least the min cheat improvement
                for (var j = i + minCheatImprovement; j < nonCheatPath.Count; j++)
                {
                    var cheatStart = nonCheatPath[i];
                    var cheatEnd = nonCheatPath[j];
                    var cheatLength = GridHelper.Distances.TaxicabDistance(cheatStart, cheatEnd);

                    // only care if we can get to it in our cheat length
                    if (cheatLength <= maxCheatLength)
                    {
                        // improvement is the index difference - how long the cheat was.
                        // aka we skipped ahead X steps in the path but it took cheatLength seconds to do it
                        var improve = j - i - cheatLength;
                        if (improve >= minCheatImprovement)
                        {
                            realCount++;
                        }
                    }
                }
            }

            return realCount;
        }
    }
}