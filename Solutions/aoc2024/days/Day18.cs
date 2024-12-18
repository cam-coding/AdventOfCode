using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using AdventLibrary.PathFinding;

namespace aoc2024
{
    public class Day18: ISolver
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
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
			var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            List<List<char>> tempGrid;
            if (isTest)
            {
                tempGrid = GridHelper.GenerateGrid(7, 7, '.');
            }
            else
            {
                tempGrid = GridHelper.GenerateGrid(71, 71, '.');
            }

            var grid = new GridObject<char>(tempGrid);

            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
			long count = 0;
            long number = input.Long;

            int magic;
            if (isTest)
            {
                magic = 12;
            }
            else
            {
                magic = 1024;
            }

            for (var i = 0; i < magic; i++)
            {
                var digits = StringParsing.GetLongsFromString(lines[i]);
                var location = new GridLocation<int>((int)digits[0], (int)digits[1]);
                grid.Set(location, '#');
            }


            var startLocation = new GridLocation<int>(0,0);
            var endLocation = new GridLocation<int>(grid.MaxX, grid.MaxY);

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in grid.GetOrthogonalNeighbours(node))
                {
                    // remove any edges where the height difference is too great
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
            return res[endLocation].Distance;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            List<List<char>> tempGrid;
            if (isTest)
            {
                tempGrid = GridHelper.GenerateGrid(7, 7, '.');
            }
            else
            {
                tempGrid = GridHelper.GenerateGrid(71, 71, '.');
            }

            var grid = new GridObject<char>(tempGrid);

            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            int magic;
            if (isTest)
            {
                magic = 12;
            }
            else
            {
                magic = 1024;
            }

            for (var i = 0; i < magic; i++)
            {
                var digits = StringParsing.GetLongsFromString(lines[i]);
                var location = new GridLocation<int>((int)digits[0], (int)digits[1]);
                grid.Set(location, '#');
            }


            var startLocation = new GridLocation<int>(0, 0);
            var endLocation = new GridLocation<int>(grid.MaxX, grid.MaxY);

            Func<GridLocation<int>, List<GridLocation<int>>> NeighboursFunc = (node) =>
            {
                var neighbours = new List<GridLocation<int>>();
                foreach (var edge in grid.GetOrthogonalNeighbours(node))
                {
                    // remove any edges where the height difference is too great
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
            var valid = true;
            var iter = 1024;
            if (isTest)
            {
                iter = 12;
            }

            var min = 1024;
            var max = lines.Count - 1;

            while (valid)
            {
                var digits = StringParsing.GetLongsFromString(lines[iter]);
                var location = new GridLocation<int>((int)digits[0], (int)digits[1]);
                grid.Set(location, '#');
                var res = Dijkstra<GridLocation<int>>.SearchEverywhere(startLocation, NeighboursFunc, WeightFunc, GoalFunc);
                valid = res.ContainsKey(endLocation);
                iter++;
            }
            return lines[iter-1];
        }
    }
}