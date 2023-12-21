using System;
using System.Collections.Generic;
using System.Diagnostics;
using AdventLibrary;

namespace aoc2023
{
    public class Day04: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ':','|' };
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
            double total = 0;

			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
                var winners = AdventLibrary.StringParsing.GetNumbersFromString(tokens[1]);
                var ours = AdventLibrary.StringParsing.GetNumbersFromString(tokens[2]);

                double counter = 0;
                foreach (var number in ours)
                {
                    if (winners.Contains(number))
                    {
                        counter++;
                    }
                }
                if (counter> 0)
                {
                    total += Math.Pow(2.0, counter - 1);
                }
			}
            return total;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            double total = 0;
            long max = 0;
            var dict = new Dictionary<long, long>();

            for (var j = 0; j < lines.Count; j++)
            {
                dict.Add(j, 1);
            }

            for (var j = 0; j < lines.Count; j++)
            {
                foreach (var value in dict.Values)
                {
                    if (value > max)
                    {
                        max = value;
                    }
                }
                var tokens = lines[j].Split(delimiterChars);
                var winners = AdventLibrary.StringParsing.GetNumbersFromString(tokens[1]);
                var ours = AdventLibrary.StringParsing.GetNumbersFromString(tokens[2]);

                long counter = 0;
                foreach (var item in ours)
                {
                    if (winners.Contains(item))
                    {
                        counter++;
                    }
                }
                var numberOfCurrentCard = dict[j];
                total += numberOfCurrentCard;
                if (counter > 0)
                {
                    for (var i = 1; i <= counter; i++)
                    {
                        dict[j + i] += numberOfCurrentCard;
                    }
                }
            }
            return total;
        }
    }
}