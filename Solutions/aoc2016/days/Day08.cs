using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day08: ISolver
  {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            // grid[row, column]
            var grid = new char[6,50];
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i,j] = '.';
                }
            }
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
				var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
                
				if (tokens[0].Equals("rect"))
                {
                    for (var i = 0; i < nums[1]; i++)
                    {
                        for (var j = 0; j < nums[0]; j++)
                        {
                            grid[i,j] = '#';
                        }
                    }
                }
                else if (tokens[1].Equals("column"))
                {
                    for (var i = 0; i < nums[1]; i++)
                    {
                        grid = AdventLibrary.GridHelper.RotateColumnDownWithWrap(grid, nums[0]);
                    }
                }
                else
                {
                    for (var i = 0; i < nums[1]; i++)
                    {
                        grid = AdventLibrary.GridHelper.RotateRowRightWithWrap(grid, nums[0]);
                    }
                }
			}
            var count = 0;
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i,j] == 1)
                        count++;
                }
            }
            return count;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            // grid[row, column]
            var grid = new char[6,50];
            for (var i = 0; i < grid.GetLength(0); i++)
            {
                for (var j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i,j] = '.';
                }
            }
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
				var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
                
				if (tokens[0].Equals("rect"))
                {
                    for (var i = 0; i < nums[1]; i++)
                    {
                        for (var j = 0; j < nums[0]; j++)
                        {
                            grid[i,j] = '#';
                        }
                    }
                }
                else if (tokens[1].Equals("column"))
                {
                    for (var i = 0; i < nums[1]; i++)
                    {
                        grid = AdventLibrary.GridHelper.RotateColumnDownWithWrap(grid, nums[0]);
                    }
                }
                else
                {
                    for (var i = 0; i < nums[1]; i++)
                    {
                        grid = AdventLibrary.GridHelper.RotateRowRightWithWrap(grid, nums[0]);
                    }
                }
			}
            AdventLibrary.GridHelper.PrintGrid(grid);

            return 0;
        }
    }
}
