using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace aoc2024
{
    public class Day14: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
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

            var robots = new List<(GridLocation<int>, GridLocation<int>)>();
            foreach (var line in lines)
            {
                var nums = StringParsing.GetNumbersWithNegativesFromString(line);
                var pos = new GridLocation<int>(nums[0], nums[1]);
                var vel = new GridLocation<int>(nums[2], nums[3]);
                robots.Add((pos, vel));
            }

            var width = 101;
            var height = 103;
            if (isTest)
            {
                width = 11;
                height = 7;
            }

            var magic = 100;
            var midWidth = (width - 1) / 2;
            var midHeight = (height - 1) / 2;

            var myGrid = GridHelper.GenerateGrid(width, height, 0);
            var finalPost = new List<GridLocation<int>>();
            var county = new List<int>() { 0,0,0,0};
            foreach (var robot in robots)
            {
                var pos = robot.Item1;
                var vel = robot.Item2;

                var maxPos = vel * magic + pos;
                var x = maxPos.X % width;
                var y = maxPos.Y % height;

                if (x < 0)
                {
                    x = width + x;
                }
                if (y < 0)
                {
                    y = height + y;
                }
                var posy = new GridLocation<int>(x, y);
                finalPost.Add(posy);
                if (posy.X < midWidth)
                {
                    if (posy.Y < midHeight)
                    {
                        county[0]++;
                    }
                    else if (posy.Y > midHeight)
                    {
                        county[1]++;
                    }
                }
                else if (posy.X > midWidth)
                {
                    if (posy.Y < midHeight)
                    {
                        county[2]++;
                    }
                    else if (posy.Y > midHeight)
                    {
                        county[3]++;
                    }
                }
            }
            return county[0] * county[1] * county[2] * county[3] ;
        }

        private object Part2(bool isTest = false)
        {
            
            if (isTest)
            {
                return 0;
            }
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;

            var robots = new List<(GridLocation<int>, GridLocation<int>)>();
            foreach (var line in lines)
            {
                var nums = StringParsing.GetNumbersWithNegativesFromString(line);
                var pos = new GridLocation<int>(nums[0], nums[1]);
                var vel = new GridLocation<int>(nums[2], nums[3]);
                robots.Add((pos, vel));
            }

            var width = 101;
            var height = 103;
            if (isTest)
            {
                width = 11;
                height = 7;
            }

            var start = 0;
            for (var i = start; i < 20000; i++)
            {
                var myGrid = GridHelper.GenerateGrid(width, height, ' ');
                var grid = new GridObject<char>(myGrid);
                var hashy = new HashSet<GridLocation<int>>();
                foreach (var robot in robots)
                {
                    var pos = robot.Item1;
                    var vel = robot.Item2;

                    var maxPos = vel * i + pos;
                    var x = maxPos.X % width;
                    var y = maxPos.Y % height;
                    if (x < 0)
                    {
                        x = width + x;
                    }
                    if (y < 0)
                    {
                        y = height + y;
                    }

                    var posy = new GridLocation<int>(x, y);
                    hashy.Add(posy);
                    grid.Set(posy, '*');
                }
                if ( Potential(grid))
                {
                    grid.Print();

                    Console.WriteLine($"answer equals {i}");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            return 0;
        }

        private bool Potential(GridObject<char> grid)
        {
            var midWidth = (grid.Width - 1) / 2;
            for (var y = 0; y < grid.Height - 4; y++)
            {
                for (var x = 3; x < grid.Width - 3; x++)
                {
                    if (grid.Get(x, y) == '*')
                    {
                        var cur = new GridLocation<int>(x, y);
                        var loly = new List<GridLocation<int>>()
                        {
                            cur + Directions.DownLeft,
                            cur + Directions.DownRight,
                            cur + Directions.Down,
                            cur + Directions.Down + Directions.Down,
                            cur + Directions.DownLeft + Directions.DownLeft,
                            cur + Directions.DownRight + Directions.DownRight,
                            cur + Directions.DownLeft + Directions.DownLeft + Directions.DownLeft,
                            cur + Directions.DownRight + Directions.DownRight + Directions.DownRight,
                        };

                        var count = loly.Count(x => grid.Get(x) == '*');

                        if (count == loly.Count)
                        {
                            return true;
                        }
                            /*
                            for (var j = -1; j < 2; j++)
                            {
                            }
                        }
                        /*
                        var cur2 = cur + Directions.DownLeft;
                        var cur3 = cur + Directions.DownLeft + Directions.DownLeft;
                        var cur4 = cur + Directions.DownLeft + Directions.DownLeft + Directions.DownLeft;
                        var cur5 = cur + Directions.DownRight;
                        var cur6 = cur + Directions.DownRight + Directions.DownRight;
                        var cur7 = cur + Directions.DownRight + Directions.DownRight + Directions.DownRight;
                        var cur5 = cur + Directions.DownRight;
                        var cur6 = cur + Directions.DownRight + Directions.DownRight;
                        var cur7 = cur + Directions.DownRight + Directions.DownRight + Directions.DownRight;
                        var valid = true;*/
                            /*
                            for (var i = -3; i < 4; i++)
                            {
                                if (grid.Get(x + i, y) != '*')
                                {
                                    valid = false;
                                }
                                /*
                                for (var j = -1; j < 2; j++)
                                {
                                }
                            }
                            return valid; */
                        }
                    }
            }
            return false;
        }
    }
}