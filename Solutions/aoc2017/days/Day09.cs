using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2017
{
    public class Day09: ISolver
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
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            long total = 1000000;
			long count = 0;
            long number = input.Long;
            
            var ln1 = lines != null && lines.Count > 0 ? lines[0] : string.Empty;
            var ln2 = lines != null && lines.Count > 1 ? lines[1] : string.Empty;
            for (var i = 0; i < lines.Count; i++)
            {

            }

			foreach (var line in lines)
			{
                var tokens = line.GetRealTokens(delimiterChars);
				var nums = StringParsing.GetNumbersFromString(line);

				foreach (var num in nums)
				{
				}

                for (var i = 0; i < 0; i++)
                {
                    for (var j = 0; j < 0; j++)
                    {

                    }
                }
			}
            return 0;
        }

        private object Part2(bool isTest = false)
        {
            return 0;
        }
    }
}