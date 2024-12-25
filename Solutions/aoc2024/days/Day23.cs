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
            var nodes = input.GraphUndirected;

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
            var nodes = input.GraphUndirected;

            var myHashy = new HashSet<(string, string, string)>();

            var best = 2;
            var bestList = new List<string>();

            var county = 0;

            var allCombos = new HashSet<List<string>>();

            foreach (var item in nodes)
            {
                county++;
                var fullList = new List<string>(item.Value);
                var res = fullList.GetJtoKCombinations(best, fullList.Count).ToList();
                /*
                foreach (var combo in res)
                {
                    combo.Sort();
                    allCombos.Add(combo);
                }*/
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
                        if (cur.Count >= best)
                        {
                            best = cur.Count + 1;
                            cur.Add(item.Key);
                            bestList = cur;
                        }
                    }
                }
            }

            bestList.Sort();
            bestList.Stringify(',');
            Console.WriteLine(bestList.Stringify(','));
            return best;
        }
    }
}