using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2025
{
    public class Day01 : ISolver
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

            var dial = 50;

            foreach (var line in lines)
            {
                var num = line.GetIntsFromString()[0];
                if (line[0] == 'L')
                {
                    num = num * -1;
                }

                dial = dial + num;

                while (dial > 99)
                {
                    dial = dial - 100;
                }
                while (dial < 0)
                {
                    dial = dial + 100;
                }
                if (dial == 0)
                {
                    count++;
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
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

            var dial = 50;

            foreach (var line in lines)
            {
                var num = line.GetIntsFromString()[0];
                var isNegative = line[0] == 'L';

                for (var i = 0; i < num; i++)
                {
                    if (!isNegative)
                    {
                        dial++;
                    }
                    else
                    {
                        dial--;
                    }
                    if (dial == 100)
                    {
                        dial = 0;
                    }
                    if (dial == 0)
                    {
                        count++;
                    }
                    if (dial == -1)
                    {
                        dial = 99;
                    }
                }
                /*
                var dialWas = false;

                if (dial == 0)
                {
                    dialWas = true;
                }

                dial = dial + num;

                if (dial > 99)
                {
                    var blah = dial / 100;
                    count += blah;
                    dial = dial % 100;
                }
                else if (dial < 0)
                {
                    var blah = (dial / -100) + 1;
                    if (dialWas)
                    {
                        count--;
                    }
                    count += blah;
                    var temp = dial % -100;
                    dial = 100 + temp;
                }
                else if (dial == 0)
                {
                    count++;
                }*/

                /*
                while (dial > 99)
                {
                    count++;
                    dial = dial - 100;
                }
                while (dial < 0)
                {
                    count++;
                    dial = dial + 100;
                }*/
                /*
                if (dial == 0)
                {
                    count++;
                }*/
            }
            return count;
        }
    }
}