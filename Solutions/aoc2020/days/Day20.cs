using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2020
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
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var dict = new Dictionary<long, GridObject<char>>();
            var edgesDict = new Dictionary<long, List<List<char>>>();
            var edges = new List<List<char>>();
            var cornerDict = new Dictionary<long, GridObject<char>>();
            var edgePieceDict = new Dictionary<long, GridObject<char>>();
            var centrePieceDict = new Dictionary<long, GridObject<char>>();

            foreach (var group in groups)
            {
                var key = StringParsing.GetLongsFromString(group[0])[0];
                var gridList = group.GetAllExceptFirstItem();
                var tempInput = InputParserFactory.CreateFromText(gridList);
                var tempGrid = tempInput.GetLinesAsGrid<char>();
                dict.Add(key, tempGrid);
                var tempEdges = GetEdges(tempGrid);
                edgesDict.Add(key, tempEdges);
                edges.AddRange(tempEdges);
            }

            foreach (var item in dict)
            {
                var tempGrid = item.Value;
                var tempEdges = GetEdges(tempGrid);
                var reverseEdges = tempEdges.Clone2dList();
                reverseEdges.ForEach(reverseEdges => reverseEdges.Reverse());

                var allEdges = tempEdges.Concat(reverseEdges).ToList();

                // var matches = allEdges.Where(x => edges.Any(y => y.SequenceEqual(x))).Select(x => x.Stringify());
                var temp = edges.Count(x => tempEdges.Any(y => y.SequenceEqual(x))) - 4;
                var temp2 = edges.Count(x => reverseEdges.Any(y => y.SequenceEqual(x)));
                var tot = temp + temp2;
                if (tot == 2)
                {
                    cornerDict.Add(item.Key, item.Value);
                }
                else if (tot == 3)
                {
                    edgePieceDict.Add(item.Key, item.Value);
                }
                else
                {
                    centrePieceDict.Add(item.Key, item.Value);
                }
            }

            // return id's multiplied together
            return cornerDict.Keys.Aggregate((long)1, (acc, x) => acc * x);
        }

        private List<List<char>> GetEdges(GridObject<char> grid)
        {
            var edges = new List<List<char>>();
            edges.Add(grid.GetRow(0));
            edges.Add(grid.GetRow(grid.MaxY));
            edges.Add(grid.GetColumn(0));
            edges.Add(grid.GetColumn(grid.MaxX));
            return edges;
        }

        private object Part2(bool isTest = false)
        {
            return 0;
        }
    }
}