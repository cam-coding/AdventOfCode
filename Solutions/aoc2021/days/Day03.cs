using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day03: ISolver
    {
        public Solution Solve(string filePath)
        {
            /*
            var letters = AdventLibrary.StringParsing.GetStringBetweenTwoCharacters(input, "a", "b");
            var letters = AdventLibrary.StringParsing.GetLettersFromString(input);
            var letters = AdventLibrary.StringParsing.GetLettersFromString(input);
            var digits = AdventLibrary.StringParsing.GetDigitsFromString(input);
            var sub = item.Substring(0, 1);
            Console.WriteLine("");
            */
            return new Solution(Part1(filePath), Part2(filePath));
        }

        private object Part1(string filePath)
        {
            var strings = AdventLibrary.ParseInput.GetLinesFromFile(filePath);
            var grid = AdventLibrary.TransformInput.ParseBoolGrid(strings, '1');
            var width = grid.First().Count;
            var length = grid.Count;
            var half = length/2;
            var gamma = string.Empty;
            var epsilon = string.Empty;

            for (var i = 0; i < width; i++)
            {
                var count = 0;
                for (var j = 0; j < length; j++)
                {
                    if (grid[j][i])
                    {
                        count++;
                    }
                }
                if (count >= half)
                {
                    gamma  = gamma  + '1';
                    epsilon  = epsilon  + '0';
                }
                else
                {
                    gamma  = gamma  + '0';
                    epsilon  = epsilon  + '1';
                }
            }
            return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
        }
        
        private object Part2(string filePath)
        {
            var strings = AdventLibrary.ParseInput.GetLinesFromFile(filePath);
            var grid = AdventLibrary.TransformInput.ParseBoolGrid(strings, '1');
            var width = grid.First().Count;
            var length = grid.Count;
            var gamma = string.Empty;
            var epsilon = string.Empty;
            var i = 0;

            var o2 = grid;
            while (o2.Count > 1)
            {
                o2 = GetOxygenRating(o2, i);
                i++;
            }

            var co2 = grid;
            i=0;
            while (co2.Count > 1)
            {
                co2 = GetCarbonRating(co2, i);
                i++;
            }

            return ConvertBooleanListToInt(o2.First()) * ConvertBooleanListToInt(co2.First());
        }

        private int ConvertBooleanListToInt(List<bool> list)
        {
            var line = string.Empty;
            foreach (var item in list)
            {
                if (item)
                {
                    line = line + '1';
                }
                else
                {
                    line = line + '0';
                }
            }
            return Convert.ToInt32(line, 2);
        }

        private List<List<bool>> GetOxygenRating(List<List<bool>> grid, int i)
        {
            var length = grid.Count;
            var half = length/2;
            var count = 0;

            for (var j = 0; j < length; j++)
            {
                if (grid[j][i])
                {
                    count++;
                }
            }

            if (count >= half)
            {
                return grid.Where(x => x[i]).ToList();
            }

            return grid.Where(x => !x[i]).ToList();
        }

        private List<List<bool>> GetCarbonRating(List<List<bool>> grid, int i)
        {
            var length = grid.Count;
            var half = length/2;
            var count = 0;

            for (var j = 0; j < length; j++)
            {
                if (grid[j][i])
                {
                    count++;
                }
            }

            if (count <= half)
            {
                return grid.Where(x => !x[i]).ToList();
            }

            return grid.Where(x => x[i]).ToList();
        }
    }
}
