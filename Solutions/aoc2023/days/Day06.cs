using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day06: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            var timer = new Stopwatch();
            timer.Start();
            var part1 = Part1();
            var time1 = timer.Elapsed;
            timer.Restart();
            var part2 = Part2();
            var time2 = timer.Elapsed;
            timer.Stop();
            return new Solution(part1, part2, time1, time2);
        }

        private object Part1()
        {
            var timer = new Stopwatch();
            timer.Start();
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var times = AdventLibrary.StringParsing.GetNumbersFromString(lines[0]);
            var distances = AdventLibrary.StringParsing.GetNumbersFromString(lines[1]);
            var total = 1;

            for (var i = 0; i < times.Count; i++)
            {
                var counter = 0;
                var time = times[i];
                var distance = distances[i];
                for (var j = 1; j < time; j++)
                {
                    var left = time - j;
                    if (j*left > distance)
                    {
                        counter++;
                    }
                }
                total *= counter;
            }
            timer.Stop();
            return total;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var times2 = AdventLibrary.StringParsing.GetNumbersFromStringAsStrings(lines[0]);
            var times = long.Parse(StringParsing.ConcatListOfStrings(times2));
            var distances2 = AdventLibrary.StringParsing.GetNumbersFromStringAsStrings(lines[1]);
            var distances = long.Parse(StringParsing.ConcatListOfStrings(distances2));

            var counter = 0;
            for (var j = 1; j < times; j++)
            {
                var remaining = times - j;
                if (j * remaining > distances)
                {
                    counter++;
                }
            }
            return counter;
        }
    }
}