using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2022
{
    public class Day03: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
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
			var counter = 0;
			
			foreach (var line in lines)
			{
                var half = line.Length / 2;
                var half1 = line.Substring(0, half);
                var half2 = line.Substring(half, half);

                for (var i = 0; i < half; i++)
                {
                    if (half2.Contains(half1[i]))
                    {
                        var value = half1[i];
                        counter += value - 38;
                    }
                    
                }
			}
            return counter;
        }
        
        private object Part2()
        {
            return 0;
        }
    }
}