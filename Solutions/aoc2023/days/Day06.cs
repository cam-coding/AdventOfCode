using System;
using System.Collections.Generic;
using System.Linq;
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
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
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
            return total;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var times = AdventLibrary.StringParsing.GetNumbersFromString(lines[0]);
            var totalTime = "";
            foreach (var item in times)
            {
                totalTime += item;
            }
            var distances = AdventLibrary.StringParsing.GetNumbersFromString(lines[1]);
            var totalDistance = "";
            foreach (var item in distances)
            {
                totalDistance += item;
            }
            var total = 1;
            var totDis = long.Parse(totalDistance);
            var totTime = long.Parse(totalTime);

            var counter = 0;
            for (var j = 1; j < totTime; j++)
            {
                var left = totTime - j;
                if (j * left > totDis)
                {
                    counter++;
                }
            }
            total *= counter;
            return total;
        }
    }
}