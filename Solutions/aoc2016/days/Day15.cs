using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day15: ISolver
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
            var dict = new Dictionary<int,(int max, int start)>();
			var counter = 0;
			
			foreach (var line in lines)
			{
				var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
                dict.Add(nums[0]-1,(nums[1],nums[3]));
			}

            while (true)
            {
                var time = counter + 1;
                var i = 0;
                var success = true;
                while (i < dict.Count && success)
                {
                    var current = (dict[i].start + time) % dict[i].max;
                    if (current != 0)
                    {
                        success = false;
                    }
                    i++;
                    time = time + 1;
                }

                if (success)
                {
                    return counter;
                }
                counter++;
            }
            
            return 0;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var dict = new Dictionary<int,(int max, int start)>();
			var counter = 0;
			
			foreach (var line in lines)
			{
				var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);
                dict.Add(nums[0]-1,(nums[1],nums[3]));
			}
            dict.Add(dict.Count,(11,0));

            while (true)
            {
                var time = counter + 1;
                var i = 0;
                var success = true;
                while (i < dict.Count && success)
                {
                    var current = (dict[i].start + time) % dict[i].max;
                    if (current != 0)
                    {
                        success = false;
                    }
                    i++;
                    time = time + 1;
                }

                if (success)
                {
                    return counter;
                }
                counter++;
            }
            
            return 0;
        }
    }
}
