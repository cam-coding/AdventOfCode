using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day03: ISolver
    {
        public Solution Solve(string filePath, bool isTest = false)
        {
            return new Solution(Part1(filePath), Part2(filePath));
        }
        private object Part1(string filePath)
        {
            var grid = ParseInput.ParseFileAsBoolGrid(filePath, '1');
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
            var grid2 = ParseInput.ParseFileAsBoolGrid(filePath, '1');
            var i = 0;
            var o2 = grid2.ToList();
            while (o2.Count > 1)
            {
                var half = Math.Ceiling((double)o2.Count/2);
                var count = o2.Count(x => x[i]);
                o2 = o2.Where(x => x[i] == count >= half).ToList();
                i++; 
            }

            var co2 = grid2.ToList();
            i=0;
            while (co2.Count > 1)
            {
                var half = Math.Ceiling((double)co2.Count/2);
                var count = co2.Count(x => x[i]);
                co2 = co2.Where(x => x[i] == count < half).ToList();
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
    }
}
