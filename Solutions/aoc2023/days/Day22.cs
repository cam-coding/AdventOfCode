using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day22: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<string,int> _history = new Dictionary<string,int>();
        private Dictionary<int,HashSet<int>> _brickToSupporters = new Dictionary<int, HashSet<int>>();
        private Dictionary<int, HashSet<int>> _brickToSupporting = new Dictionary<int, HashSet<int>>();
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private int Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var maxNum = lines.Select(x => StringParsing.GetNumbersFromString(x).Max()).Max();
            var allBricks = new List<((int startZ, int startY, int startX), (int endZ, int endY, int endX), int key)>();
            var iter = 1;
            var maxNum2 = 0;
            foreach (var line in lines)
            {
                var tokens = line.Split('~').ToList().OnlyRealStrings(delimiterChars);
                var start = StringParsing.GetNumbersFromString(tokens[0]);
                var end = StringParsing.GetNumbersFromString(tokens[1]);
                maxNum2 = Math.Max(maxNum2, new List<int>() { start[1], start[0], end[1], end[0] }.Max());
                allBricks.Add(((start[2], start[1], start[0]), (end[2], end[1], end[0]), iter));
                iter++;
            }
            allBricks.Sort((a, b) => a.Item1.startZ.CompareTo(b.Item1.startZ));

            // map x,y coord to first height free and what's underneath that;
            var topHeight = new Dictionary<(int y, int x), (int,int)>();

            for (var i = 0; i <= maxNum2; i++)
            {
                for (var j = 0; j <= maxNum2; j++)
                {
                    topHeight.Add((i, j), (0,0));
                }
            }

            var supportBricks = new HashSet<int>();

            foreach (var brick in allBricks)
            {
                var cubes = new List<(int z, int y, int x)>();

                for (var zLoop = brick.Item1.startZ; zLoop <= brick.Item2.endZ; zLoop++)
                {
                    for (var yLoop = brick.Item1.startY; yLoop <= brick.Item2.endY; yLoop++)
                    {
                        for (var xLoop = brick.Item1.startX; xLoop <= brick.Item2.endX; xLoop++)
                        {
                            cubes.Add((zLoop, yLoop, xLoop));
                        }
                    }
                }

                var minZ = 0;
                var listy = new List<int>();
                foreach (var cube in cubes)
                {
                    var z = topHeight[(cube.y, cube.x)].Item1;
                    var pieceBelow = topHeight[(cube.y, cube.x)].Item2;
                    if (z == minZ)
                    {
                        if (pieceBelow != 0)
                        {
                            listy.Add(pieceBelow);
                        }
                    }
                    else if (z > minZ)
                    {
                        minZ = z;
                        listy = new List<int>();
                        if (pieceBelow != 0)
                        {
                            listy.Add(pieceBelow);
                        }
                    }
                }
                var dif = cubes[0].z - (minZ + 1);
                foreach (var cube in cubes)
                {
                    topHeight[(cube.y, cube.x)] = (cube.z - dif, brick.key);
                }
                listy = listy.Distinct().ToList();
                if (listy.Count == 1)
                {
                    supportBricks.Add(listy[0]);
                }
            }
            return allBricks.Count - supportBricks.Count;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var maxNum = lines.Select(x => StringParsing.GetNumbersFromString(x).Max()).Max();
            var allBricks = new List<((int startZ, int startY, int startX), (int endZ, int endY, int endX), int key)>();
            var iter = 1;
            var maxNum2 = 0;
            foreach (var line in lines)
            {
                var tokens = line.Split('~').ToList().OnlyRealStrings(delimiterChars);
                var start = StringParsing.GetNumbersFromString(tokens[0]);
                var end = StringParsing.GetNumbersFromString(tokens[1]);
                maxNum2 = Math.Max(maxNum2, new List<int>() { start[1], start[0], end[1], end[0] }.Max());
                allBricks.Add(((start[2], start[1], start[0]), (end[2], end[1], end[0]), iter));
                iter++;
            }
            allBricks.Sort((a, b) => a.Item1.startZ.CompareTo(b.Item1.startZ));

            // map x,y coord to first height free and what's underneath that;
            var topHeight = new Dictionary<(int y, int x), (int, int)>();

            for (var i = 0; i <= maxNum2; i++)
            {
                for (var j = 0; j <= maxNum2; j++)
                {
                    topHeight.Add((i, j), (0, 0));
                }
            }

            var supportBricks = new Dictionary<int, List<int>>();

            foreach (var brick in allBricks)
            {
                var cubes = new List<(int z, int y, int x)>();

                for (var zLoop = brick.Item1.startZ; zLoop <= brick.Item2.endZ; zLoop++)
                {
                    for (var yLoop = brick.Item1.startY; yLoop <= brick.Item2.endY; yLoop++)
                    {
                        for (var xLoop = brick.Item1.startX; xLoop <= brick.Item2.endX; xLoop++)
                        {
                            cubes.Add((zLoop, yLoop, xLoop));
                        }
                    }
                }

                var minZ = 0;
                var listy = new List<int>();
                foreach (var cube in cubes)
                {
                    var z = topHeight[(cube.y, cube.x)].Item1;
                    var pieceBelow = topHeight[(cube.y, cube.x)].Item2;
                    if (z == minZ)
                    {
                        if (pieceBelow != 0)
                        {
                            listy.Add(pieceBelow);
                        }
                    }
                    else if (z > minZ)
                    {
                        minZ = z;
                        listy = new List<int>();
                        if (pieceBelow != 0)
                        {
                            listy.Add(pieceBelow);
                        }
                    }
                }
                var dif = cubes[0].z - (minZ + 1);
                foreach (var cube in cubes)
                {
                    topHeight[(cube.y, cube.x)] = (cube.z - dif, brick.key);
                }
                listy = listy.Distinct().ToList();
                if (!_brickToSupporters.ContainsKey(brick.key))
                {
                    _brickToSupporters.Add(brick.key, listy.ToHashSet());
                }
                else
                {
                    foreach (var item in listy)
                    {
                        _brickToSupporters[brick.key].Add(item);
                    }
                }
                foreach (var item in listy)
                {
                    if (!_brickToSupporting.ContainsKey(item))
                    {
                        _brickToSupporting.Add(item, new HashSet<int>() { brick.key });
                    }
                    else
                    {
                        _brickToSupporting[item].Add(brick.key);
                    }
                }
                if (listy.Count == 1)
                {
                    if (!supportBricks.ContainsKey(listy[0]))
                    {
                        supportBricks.Add(listy[0], new List<int>() { brick.key });
                    }
                    else
                    {
                        supportBricks[listy[0]].Add(brick.key);
                    }
                }
            }
            var best = 0;

            foreach (var item in supportBricks)
            {
                best += CountDominoEffect(new HashSet<int>() { item.Key }, item.Value.ToHashSet());
            }
            return best;
        }

        private int CountDominoEffect(HashSet<int> fallen, HashSet<int> currentKeys)
        {
            var key = fallen.Stringify() + "$" + currentKeys.Stringify();
            if (_history.ContainsKey(key))
            {
                return _history[key];
            }
            var nextUp = new HashSet<int>();
            foreach (var currentKey in currentKeys)
            {
                var supporters = _brickToSupporters[currentKey];
                if (supporters.All(x => fallen.Contains(x)))
                {
                    fallen.Add(currentKey);
                    if (_brickToSupporting.ContainsKey(currentKey))
                    {
                        foreach (var item in _brickToSupporting[currentKey])
                        {
                            nextUp.Add(item);
                        }
                    }
                }
            }
            var total = fallen.Count - 1;
            if (nextUp.Count > 0)
            {
                total = CountDominoEffect(fallen, nextUp);
            }
            _history.Add(key, total);
            return total;
        }
    }
}