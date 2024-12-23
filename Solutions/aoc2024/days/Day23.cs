using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day23 : ISolver
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
            var grid = input.GridChar;
            var graph = input.Graph;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var nodes = new Dictionary<string, List<string>>();
            foreach (var line in lines)
            {
                var tokens = StringParsing.GetRealTokens(line, _delimiterChars);
                var start = tokens[0];
                var ends = tokens.SubList(1);
                var myListy = new List<string>();

                if (!nodes.ContainsKey(start))
                {
                    nodes.Add(start, myListy);
                }
                else
                {
                    myListy = nodes[start];
                }

                foreach (var end in ends)
                {
                    if (!myListy.Contains(end))
                    {
                        myListy.Add(end);
                    }

                    if (!nodes.ContainsKey(end))
                    {
                        nodes.Add(end, new List<string>() { start });
                    }
                    else
                    {
                        if (!nodes[end].Contains(start))
                        {
                            nodes[end].Add(start);
                        }
                    }
                }
            }

            foreach (var item in graph)
            {
                graph.TryAdd(item.Value[0], new List<string>() { item.Key });
            }

            var myHashy = new HashSet<(string, string, string)>();

            foreach (var item in nodes)
            {
                for (var i = 0; i < item.Value.Count; i++)
                {
                    for (var j = i + 1; j < item.Value.Count; j++)
                    {
                        var first = item.Value[i];
                        var second = item.Value[j];

                        if (nodes[first].Contains(second) &&
                            nodes[second].Contains(first))
                        {
                            var listy = new List<string>() { item.Key, first, second };
                            listy.Sort();
                            var tup = (listy[0], listy[1], listy[2]);
                            myHashy.Add(tup);
                        }
                    }
                }
            }

            var county = myHashy.Count(x => x.Item1.StartsWith('t') || x.Item2.StartsWith('t') || x.Item3.StartsWith('t'));

            return county;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var grid = input.GridChar;
            var graph = input.Graph;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var nodes = new Dictionary<string, List<string>>();
            foreach (var line in lines)
            {
                var tokens = StringParsing.GetRealTokens(line, _delimiterChars);
                var start = tokens[0];
                var ends = tokens.SubList(1);
                var myListy = new List<string>();

                if (!nodes.ContainsKey(start))
                {
                    nodes.Add(start, myListy);
                }
                else
                {
                    myListy = nodes[start];
                }

                foreach (var end in ends)
                {
                    if (!myListy.Contains(end))
                    {
                        myListy.Add(end);
                    }

                    if (!nodes.ContainsKey(end))
                    {
                        nodes.Add(end, new List<string>() { start });
                    }
                    else
                    {
                        if (!nodes[end].Contains(start))
                        {
                            nodes[end].Add(start);
                        }
                    }
                }
            }

            foreach (var item in graph)
            {
                graph.TryAdd(item.Value[0], new List<string>() { item.Key });
            }

            var myHashy = new HashSet<(string, string, string)>();

            var best = 1;
            var bestList = new List<string>();

            var county = 0;

            foreach (var item in nodes)
            {
                county++;
                var fullList = new List<string>() { item.Key };
                fullList.AddRange(item.Value);
                var res = fullList.Get0toKCombinations(fullList.Count).ToList();
                foreach (var thing in res)
                {
                    var cur = thing.ToList();
                    if (thing.Count() < best) continue;
                    var valid = true;
                    for (var i = 0; i < cur.Count; i++)
                    {
                        for (var j = i + 1; j < cur.Count; j++)
                        {
                            var first = cur[i];
                            var second = cur[j];

                            if (nodes[first].Contains(second) &&
                                nodes[second].Contains(first))
                            {
                                continue;
                            }
                            else
                            {
                                valid = false;
                                break;
                            }
                        }
                    }

                    if (valid)
                    {
                        if (cur.Count > best)
                        {
                            best = cur.Count;
                            bestList = cur;
                        }
                    }
                }
            }

            bestList.Sort();

            var str = string.Empty;
            foreach (var t in bestList)
            {
                str += t + ",";
            }
            Console.WriteLine(str);
            return best;
        }
    }
}