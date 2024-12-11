using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day08: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
			var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            var hashy = new HashSet<char>();
            var count = 0;

            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var val = grid.Get(x, y);
                    if (val != '.')
                    {
                        hashy.Add(val);
                    }
                }
            }

            var dict = new Dictionary<char, List<GridLocation<int>>>();
            var listy2 = new List<(char, GridLocation<int>)>();

            foreach (var item in hashy)
            {
                var stuff = grid.GetAllLocationWhereCellEqualsValue(item);

                foreach (var thing in stuff)
                {
                    listy2.Add((item, thing));
                }
                dict.Add(item,stuff);
            }

            var hashAnswer = new HashSet<GridLocation<int>>();
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    foreach (var pair in listy2)
                    {
                        var loc = new GridLocation<int>(x,y);
                        var delta = loc - pair.Item2;

                        var others = listy2.Where(x => x.Item1 == pair.Item1).ToList();

                        foreach (var item in others)
                        {
                            if (item == pair)
                            {
                                continue;
                            }
                            var delta2 = loc - item.Item2;
                            if (delta + delta == delta2)
                            {
                                hashAnswer.Add(loc);
                            }
                        }
                    }
                    count += hashAnswer.Count;
                }
            }

            return hashAnswer.Count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            var hashy = new HashSet<char>();
            var count = 0;

            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var val = grid.Get(x, y);
                    if (val != '.')
                    {
                        hashy.Add(val);
                    }
                }
            }

            var dict = new Dictionary<char, List<GridLocation<int>>>();
            var listy2 = new List<(char, GridLocation<int>)>();

            foreach (var item in hashy)
            {
                var stuff = grid.GetAllLocationWhereCellEqualsValue(item);

                foreach (var thing in stuff)
                {
                    listy2.Add((item, thing));
                }
                dict.Add(item, stuff);
            }

            var hashAnswer = new HashSet<GridLocation<int>>();
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    foreach (var pair in listy2)
                    {
                        var loc = new GridLocation<int>(x, y);
                        if (loc == pair.Item2)
                        {
                            continue;
                        }
                        double slope = 0;
                        try
                        {
                            slope = ((double)loc.Y - (double)pair.Item2.Y) / ((double)loc.X - (double)pair.Item2.X);
                        }
                        catch (Exception e)
                        {
                            continue;
                        }

                        var others = listy2.Where(x => x.Item1 == pair.Item1).ToList();

                        foreach (var item in others)
                        {
                            if (item == pair)
                            {
                                continue;
                            }
                            double slope2 = 0;
                            try
                            {
                                slope2 = ((double)pair.Item2.Y - (double)item.Item2.Y) / ((double)pair.Item2.X - (double)item.Item2.X);
                            }
                            catch (Exception e)
                            {
                                continue;
                            }
                            if (slope == slope2)
                            {
                                hashAnswer.Add(loc);
                                break;
                            }
                        }
                    }
                    count += hashAnswer.Count;
                }
            }

            return hashAnswer.Count;
        }

        /*
        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            var gridStart = new GridLocation<int>(0, 0);
            var hashy = new HashSet<char>();
            var count = 0;

            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    var val = grid.Get(x, y);
                    if (val != '.')
                    {
                        hashy.Add(val);
                    }
                }
            }

            var dict = new Dictionary<char, List<GridLocation<int>>>();
            var listy2 = new List<(char, GridLocation<int>)>();

            foreach (var item in hashy)
            {
                var stuff = grid.GetAllLocationWhereCellEqualsValue(item);

                foreach (var thing in stuff)
                {
                    listy2.Add((item, thing));
                }
                dict.Add(item, stuff);
            }

            var hashAnswer = new HashSet<GridLocation<int>>();
            var zero = new GridLocation<int>(0,0);
            for (var y = 0; y < grid.Height; y++)
            {
                for (var x = 0; x < grid.Width; x++)
                {
                    foreach (var pair in listy2)
                    {
                        var loc = new GridLocation<int>(x, y);
                        var delta = loc - pair.Item2;

                        var others = listy2.Where(x => x.Item1 == pair.Item1).ToList();

                        foreach (var item in others)
                        {
                            if (item == pair)
                            {
                                continue;
                            }
                            var delta2 = loc - item.Item2;
                            var xVal = -1;
                            if (delta.X == 0)
                            {
                                if (delta2.X == 0)
                                {
                                    xVal = 0;
                                }
                            }
                            else
                            {
                                xVal = delta2.X % delta.X;
                            }
                            var yVal = -1;
                            if (delta.Y == 0)
                            {
                                if (delta2.Y == 0)
                                {
                                    yVal = 0;
                                }
                            }
                            else
                            {
                                yVal = delta2.Y % delta.Y;
                            }
                            if (xVal == 0 && yVal == 0)
                            {
                                hashAnswer.Add(loc);
                            }
                        }
                    }
                    count += hashAnswer.Count;
                }
            }

            return hashAnswer.Count;
        }*/
    }
}