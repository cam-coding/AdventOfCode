using AdventLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace aoc2023
{
    public class Day03 : ISolver
    {
        private string _filePath;
        private char[] symbols = { '!', '@', '#', '$', '%', '^', '&', '*', '+', '/', '=', '-' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;

            var timer = new Stopwatch();
            timer.Start();
            var solution = new Solution(
                Part1(),
                timer.Elapsed,
                Part2(),
                timer.Elapsed);
            timer.Stop();
            return solution;
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var inputGrid = ParseInput.ParseFileAsCharGrid(_filePath);
            var counter = 0;

            // grid of unique id per number in input to actual value
            var dopeGrid = new List<List<(string, int)>>();

            for (var i = 0; i < inputGrid.Count; i++)
            {
                var listy = new List<(string, int)>();
                for (var j = 0; j < inputGrid[0].Count; j++)
                {
                    listy.Add(("", 0));
                }
                dopeGrid.Add(listy);
            }

            for (var j = 0; j < lines.Count; j++)
            {
                foreach (var numIndex in StringParsing.GetNumbersWithIndexesFromString(lines[j]))
                {
                    var str = "" + numIndex.Item1;
                    var start = numIndex.Item2;
                    var end = start + str.Length - 1;
                    for (var i = start; i <= end; i++)
                    {
                        dopeGrid[j][i] = ($"x:{j}y:{start}", numIndex.Item1);
                    }
                }
            }

            var lookup = new Dictionary<string, int>();
            for (var i = 0; i < inputGrid.Count; i++)
            {
                for (var j = 0; j < inputGrid[0].Count; j++)
                {
                    if (symbols.Contains(inputGrid[i][j]))
                    {
                        var neigh = GridHelper.GetOrthoginalNeighbours(inputGrid, i, j);
                        foreach (var k in neigh)
                        {
                            if (!dopeGrid[k.Item1][k.Item2].Item1.Equals(""))
                            {
                                lookup.TryAdd(dopeGrid[k.Item1][k.Item2].Item1, dopeGrid[k.Item1][k.Item2].Item2);
                            }
                        }
                    }
                }
            }

            foreach (var value in  lookup.Values)
            {
                counter += value;
            }

            return counter;
        }

        private object Part2()
        {
            // need library to make rectangle grid.
            // need library to get nums and indexes
            // maybe library to get all chars?
            // need library to store answers tried
            // maybe a Grid object I can return with things like width?
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var inputGrid = ParseInput.ParseFileAsCharGrid(_filePath);
            var counter = 0;

            // grid of unique id per number in input to actual value
            var dopeGrid = new List<List<(string, int)>>();

            for (var i = 0; i < inputGrid.Count; i++)
            {
                var listy = new List<(string, int)>();
                for (var j = 0; j < inputGrid[0].Count; j++)
                {
                    listy.Add(("", 0));
                }
                dopeGrid.Add(listy);
            }

            for (var j = 0; j < lines.Count; j++)
            {
                foreach (var numIndex in StringParsing.GetNumbersWithIndexesFromString(lines[j]))
                {
                    var str = "" + numIndex.Item1;
                    var start = numIndex.Item2;
                    var end = start + str.Length - 1;
                    for (var i = start; i <= end; i++)
                    {
                        dopeGrid[j][i] = ($"x:{j}y:{start}", numIndex.Item1);
                    }
                }
            }

            for (var i = 0; i < inputGrid.Count; i++)
            {
                for (var j = 0; j < inputGrid[0].Count; j++)
                {
                    if (inputGrid[i][j] == '*')
                    {
                        var neigh = GridHelper.GetOrthoginalNeighbours(inputGrid, i, j);
                        var lookup = new Dictionary<string, int>();
                        foreach (var k in neigh)
                        {
                            if (!dopeGrid[k.Item1][k.Item2].Item1.Equals(""))
                            {
                                lookup.TryAdd(dopeGrid[k.Item1][k.Item2].Item1, dopeGrid[k.Item1][k.Item2].Item2);
                            }
                        }
                        if (lookup.Count == 2)
                        {
                            var keys = lookup.Values.ToList();
                            counter += keys[0]* keys[1];
                        }
                    }
                }
            }

            return counter;
        }
    }
}