using AdventLibrary;
using AdventLibrary.Extensions;
using System.Diagnostics;

namespace aoc2023
{
    public class Day06 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1()
        {
            var timer = new Stopwatch();
            timer.Start();
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var times = AdventLibrary.StringParsing.GetIntsFromString(lines[0]);
            var distances = AdventLibrary.StringParsing.GetIntsFromString(lines[1]);
            var total = 1;

            for (var i = 0; i < times.Count; i++)
            {
                var counter = 0;
                var time = times[i];
                var distance = distances[i];
                for (var j = 1; j < time; j++)
                {
                    var left = time - j;
                    if (j * left > distance)
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
            var times2 = lines[0].GetNumbersFromStringAsStrings();
            var times = long.Parse(times2.ConcatListToString());
            var distances2 = lines[1].GetNumbersFromStringAsStrings();
            var distances = long.Parse(distances2.ConcatListToString());

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