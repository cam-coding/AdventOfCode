using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day22: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var maxNum = lines.Select(x => StringParsing.GetNumbersFromString(x).Max()).Max();
            var numbers = ParseInput.GetNumbersFromFile(_filePath);

            var threeGrid = new List<List<List<int>>>();
            for (var i = 0; i <= maxNum; i++)
            {
                threeGrid.Add(new List<List<int>>());
                for (var j = 0; j <= maxNum; j++)
                {
                    threeGrid[i].Add(new List<int>());
                    for (var k = 0; k <= maxNum; k++)
                    {
                        threeGrid[i][j].Add(0);
                    }
                }
            }

            var count = 0;
            var lineCounter = 1;
            var lookup = new Dictionary<int, Brick>();
            // setup initial brick pos
            foreach (var line in lines)
			{
                var tokens = line.Split('~').ToList().OnlyRealStrings(delimiterChars);
                var start = StringParsing.GetNumbersFromString(tokens[0]);
                var end = StringParsing.GetNumbersFromString(tokens[1]);
                var brick = new Brick((start[0], start[1], start[2]), (end[0], end[1], end[2]), lineCounter);
                lookup.Add(lineCounter, brick);
                if (start[0] != end[0])
                {
                    for (var i = start[0]; i <= end[0]; i++)
                    {
                        threeGrid[i][start[1]][start[2]] = lineCounter;
                        brick.Posistions.Add((i, start[1], start[2]));
                        count++;
                    }
                }
                else if (start[1] != end[1])
                {
                    for (var i = start[1]; i <= end[1]; i++)
                    {
                        threeGrid[start[0]][i][start[2]] = lineCounter;
                        brick.Posistions.Add((start[0], i, start[2]));
                        count++;
                    }
                }
                else if (start[2] != end[2])
                {
                    for (var i = start[2]; i <= end[2]; i++)
                    {
                        threeGrid[start[0]][start[1]][i] = lineCounter;
                        brick.Posistions.Add((start[0], start[1], i));
                        count++;
                    }
                }
                lineCounter++;
            }
            var hasChanged = true;
            while (hasChanged)
            {
                hasChanged = false;
                // 0 is ground and lowest possible for brick is layer 1 so we start at 2
                for (var z = 2; z <= maxNum; z++)
                {
                    for (var y = 0; y <= maxNum; y++)
                    {
                        for (var x = 0; x <= maxNum; x++)
                        {
                            var currentBrickValue = threeGrid[x][y][z];
                            if (currentBrickValue != 0)
                            {
                                var currentBrick = lookup[currentBrickValue];
                                var newPosistions = new List<(int x, int y, int z)>();
                                foreach (var cube in currentBrick.Posistions)
                                {
                                    if (threeGrid[cube.x][cube.y][cube.z - 1] == 0)
                                    {
                                        newPosistions.Add((cube.x, cube.y, cube.z - 1));
                                    }
                                }
                                if (newPosistions.Count == currentBrick.Posistions.Count)
                                {
                                    foreach (var oldCube in currentBrick.Posistions)
                                    {
                                        threeGrid[oldCube.x][oldCube.y][oldCube.z] = 0;
                                    }
                                    foreach (var newCube in newPosistions)
                                    {
                                        threeGrid[newCube.x][newCube.y][newCube.z] = currentBrickValue;
                                    }
                                    currentBrick.Posistions = newPosistions;
                                    currentBrick.Start = newPosistions[0];
                                    currentBrick.End = newPosistions[^1];
                                    hasChanged = true;
                                }
                            }
                        }
                    }
                }
            }
            for (var i = 0; i <= maxNum; i++)
            {
                for (var j = 0; j <= maxNum; j++)
                {
                    for (var k = 0; k < maxNum; k++)
                    {
                        var val = threeGrid[i][j][k];
                        if (val != 0)
                        {
                            var val2 = threeGrid[i][j][k+1];
                            if (val2 != 0 && val2 != val)
                            {
                                lookup[val].Supporting.Add(lookup[val2]);
                                lookup[val2].SupportedBy.Add(lookup[val]);
                            }
                        }
                    }
                }
            }
            var answer = 0;
            foreach (var bricky in lookup.Values)
            {
                if (bricky.Supporting.Count == 0)
                {
                    answer++;
                }
                else
                {
                    if (bricky.Supporting.All(x => x.SupportedBy.Count > 1))
                    {
                        answer++;
                    }
                }
            }
            return answer;
        }

        private class Brick
        {
            public Brick()
            {
            }

            public Brick((int, int, int) start, (int, int, int) end, int key)
            {
                Start = start;
                End = end;
                Key = key;
                Posistions = new List<(int,int, int)>();
                SupportedBy = new List<Brick>();
                Supporting = new List<Brick>();
            }

            public (int,int,int) Start { get; set; }

            public (int,int,int) End { get; set; }

            public List<(int x, int y, int z)> Posistions { get; set; }

            public int Key { get; set; }

            public List<Brick> Supporting { get; set; }

            public List<Brick> SupportedBy { get; set; }
        }

        private object Part2()
        {
            return 0;
        }
    }
}