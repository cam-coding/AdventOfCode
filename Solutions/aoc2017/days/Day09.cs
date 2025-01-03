using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2017
{
    public class Day09 : ISolver
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
            long total = 0;

            foreach (var line in lines)
            {
                var garbageMode = false;
                var stack = new Stack<char>();
                var count = 0;
                for (var i = 0; i < line.Count(); i++)
                {
                    var current = line[i];
                    switch (current)
                    {
                        case '<':
                            if (!garbageMode)
                            {
                                garbageMode = true;
                            }
                            break;

                        case '>':
                            if (garbageMode)
                            {
                                garbageMode = false;
                            }
                            break;

                        case '{':
                            if (!garbageMode)
                            {
                                stack.Push(current);
                            }
                            break;

                        case '}':
                            if (!garbageMode)
                            {
                                count += stack.Count;
                                stack.Pop();
                            }
                            break;

                        case '!':
                            if (garbageMode)
                            {
                                i += 1;
                            }
                            break;
                    }
                }
                total += count;
            }
            return total;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            long total = 0;

            foreach (var line in lines)
            {
                var garbageMode = false;
                var stack = new Stack<char>();
                var count = 0;
                for (var i = 0; i < line.Count(); i++)
                {
                    var current = line[i];
                    switch (current)
                    {
                        case '<':
                            if (!garbageMode)
                            {
                                garbageMode = true;
                            }
                            else
                            {
                                count++;
                            }
                            break;

                        case '>':
                            if (garbageMode)
                            {
                                garbageMode = false;
                            }
                            break;

                        case '{':
                            if (!garbageMode)
                            {
                                stack.Push(current);
                            }
                            else
                            {
                                count++;
                            }
                            break;

                        case '}':
                            if (!garbageMode)
                            {
                                stack.Pop();
                            }
                            else
                            {
                                count++;
                            }
                            break;

                        case '!':
                            if (garbageMode)
                            {
                                i += 1;
                            }
                            break;

                        default:
                            if (garbageMode)
                            {
                                count++;
                            }
                            break;
                    }
                }
                total += count;
            }
            return total;
        }
    }
}