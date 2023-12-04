using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day04: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ':','|' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
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
                foreach (var item in ours)
                {
                    if (winners.Contains(item))
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
            var dict = new Dictionary<long, long>();

            for (var j = 0; j < lines.Count; j++)
            {
                dict.Add(j, 1);
            }

            for (var j = 0; j < lines.Count; j++)
            {
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
                if (counter > 0)
                {
                    var bigNum = 1 * dict[j];
                    total += bigNum;
                    //change
                    for (var i = 1; i <= counter; i++)
                    {
                        dict[j + i] += dict[j];
                    }
                }
                else
                {
                    total += dict[j];
                }
            }
            return total;
        }
    }
}