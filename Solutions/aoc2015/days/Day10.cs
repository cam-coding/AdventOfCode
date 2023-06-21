using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day10: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            var current = lines[0];

            for (var i = 0; i < 40; i++)
            {
                var newCurrent = string.Empty;
                var currentVal = current[0];
                var counter = 0;
                for (var j = 0; j < current.Length; j++)
                {
                    if (current[j] == currentVal)
                    {
                        counter++;
                    }
                    else
                    {
                        newCurrent += counter + "" + currentVal;
                        counter = 1;
                        currentVal = current[j];
                    }
                }
                newCurrent += counter + "" + currentVal;
                current = newCurrent;
            }
            return current.Length;
        }

        private object Part2()
        {
            // stolen because I don't know regex and strings were taking forever
            // https://www.reddit.com/r/adventofcode/comments/3w6h3m/day_10_solutions/cxtso95/
            var lines = ParseInput.GetLinesFromFile(_filePath);

            var current = lines[0];

            for (var i = 0; i < 50; i++)
            {
                current = LookAndSay(current);
            }
            return current.Length;
        }

        private string LookAndSay(string arg)
        {
            var captures = Regex.Match(arg, @"((.)\2*)+").Groups[1].Captures;
            return string.Concat(
                from c in captures.Cast<Capture>()
                let v = c.Value
                select v.Length + v.Substring(0, 1));
        }
    }
}