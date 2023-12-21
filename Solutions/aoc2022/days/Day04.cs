using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2022
{
    public class Day04: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
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
			var counter = 0;
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
                
				var nums = tokens.Select(x => Int32.Parse(x)).ToList();

                if ((nums[0] >= nums[2] && nums[1] <= nums[3]))
                {
                    counter++;
                }
                else if ((nums[2] >= nums[0] && nums[3] <= nums[1]))
                {
                    counter++;
                }
			}
            return counter;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var counter = 0;
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
                
				var nums = tokens.Select(x => Int32.Parse(x)).ToList();

                if (
                    (nums[0] >= nums[2] && nums[0] <= nums[3]) ||
                    (nums[1] >= nums[2] && nums[1] <= nums[3]) ||
                    (nums[2] >= nums[0] && nums[2] <= nums[1]) ||
                    (nums[3] >= nums[0] && nums[3] <= nums[1])
                   )
                {
                    counter++;
                }
			}
            return counter;
        }
    }
}