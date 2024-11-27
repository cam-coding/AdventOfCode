using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.PathFinding;

namespace aoc2020
{
    public class Day07 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<string, List<(int bagsCount, string key)>> _lookup;

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1(bool isTest = false)
        {
            var goldKey = "shinygold";
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var lookup = new Dictionary<string, List<(int count, string key)>>();
            var hasGoldenBagsInside = new HashSet<string>();
            foreach (var line in lines)
            {
                var tokens = line.Split("bags contain").ToList().GetRealStrings(delimiterChars);
                var key = tokens[0].RemoveWhitespace();

                var listy = new List<(int count, string key)>();
                if (!tokens[1].Equals(" no other bags."))
                {
                    var inner = tokens[1].Split(',').ToList().GetRealStrings(delimiterChars);
                    foreach (var item in inner)
                    {
                        var innerTokens = item.Split("bag").ToList().GetRealStrings(delimiterChars);
                        var count = innerTokens[0].GetNumbersFromString()[0];
                        var innerKey = innerTokens[0].RemoveDigitsFromString().RemoveWhitespace();
                        if (innerKey.Equals(goldKey))
                        {
                            hasGoldenBagsInside.Add(key);
                        }
                        listy.Add((count, innerKey));
                    }
                }
                lookup.Add(key, listy);
            }

            Func<string, List<string>> NeighboursFunc = (bag) =>
            {
                var neighbours = new List<string>();
                foreach (var innerBag in lookup[bag])
                {
                    neighbours.Add(innerBag.key);
                }
                return neighbours;
            };

            Func<string, bool> GoalFunc = (bag) =>
            {
                return hasGoldenBagsInside.Contains(bag) ||
                bag.Equals(goldKey);
            };
            var counter = 0;

            foreach (var pair in lookup)
            {
                var DFS = new DepthFirstSearch<string>();
                var start = pair.Key;
                if (!start.Equals(goldKey))
                {
                    DFS.DFSgeneric((0, new List<string>() { start }), NeighboursFunc, GoalFunc);
                    if (DFS.GoalAchieved)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        private object Part2(bool isTest = false)
        {
            var goldKey = "shinygold";
            var lines = ParseInput.GetLinesFromFile(_filePath);
            _lookup = new Dictionary<string, List<(int bagsCount, string key)>>();
            var hasGoldenBagsInside = new HashSet<string>();
            foreach (var line in lines)
            {
                var tokens = line.Split("bags contain").ToList().GetRealStrings(delimiterChars);
                var key = tokens[0].RemoveWhitespace();

                var listy = new List<(int bagsCount, string key)>();
                if (!tokens[1].Equals(" no other bags."))
                {
                    var inner = tokens[1].Split(',').ToList().GetRealStrings(delimiterChars);
                    foreach (var item in inner)
                    {
                        var innerTokens = item.Split("bag").ToList().GetRealStrings(delimiterChars);
                        var count = innerTokens[0].GetNumbersFromString()[0];
                        var innerKey = innerTokens[0].RemoveDigitsFromString().RemoveWhitespace();
                        if (innerKey.Equals(goldKey))
                        {
                            hasGoldenBagsInside.Add(key);
                        }
                        listy.Add((count, innerKey));
                    }
                }
                _lookup.Add(key, listy);
            }
            var answer = DFS(goldKey);
            return answer - 1;
        }

        private int DFS(string current)
        {
            var total = 1;
            var innerBags = _lookup[current];
            if (innerBags.Count != 0)
            {
                foreach (var innerBag in innerBags)
                {
                    var count2 = innerBag.bagsCount;
                    var key = innerBag.key;
                    total += count2 * DFS(key);
                }
            }
            return total;
        }
    }
}