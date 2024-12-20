using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2020
{
    public class Day22: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1(bool isTest = false)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var nodes = ParseInput.ParseFileAsGraph(_filePath);
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var total = 1000000;
			var count = 0;

            var ln1 = lines[0];
            var ln2 = lines[1];
            for (var i = 0; i < lines.Count; i++)
            {

            }

			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars).ToList().GetRealStrings(delimiterChars);
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