using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2017
{
    public class Day01: ISolver
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
            var total = 1000000;
			var counter = 0;

            var line = lines[0];

            for (var j = 1; j < line.Length; j++)
            {
                if (line[j] == line[j-1])
                {
                    counter += line[j];
                }
            }
            if (line[line.Length-1] == line[0])
            {
                counter += line[line.Length - 1];
            }
            return counter;
        }
        
        private object Part2()
        {
            return 0;
        }
    }
}