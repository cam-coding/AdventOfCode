using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day06: ISolver
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
            var lights = new bool[1000,1000];
            var counter = 0;
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
                var nums = StringParsing.GetNumbersFromString(line);
                if (tokens[0].Equals("toggle"))
                {
                    for (var i = nums[0]; i <= nums[2]; i++)
                    {
                        for (var j = nums[1]; j <= nums[3]; j++)
                        {
                            lights[j, i] = !lights[j, i];
                        }
                    }
                }
                else if (tokens[1].Equals("on"))
                {
                    for (var i = nums[0]; i <= nums[2]; i++)
                    {
                        for (var j = nums[1]; j <= nums[3]; j++)
                        {
                            lights[j, i] = true;
                        }
                    }
                }
                else if (tokens[1].Equals("off"))
                {
                    for (var i = nums[0]; i <= nums[2]; i++)
                    {
                        for (var j = nums[1]; j <= nums[3]; j++)
                        {
                            lights[j, i] = false;
                        }
                    }
                }
            }

            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    if (lights[j, i])
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var lights = new int[1000, 1000];
            var counter = 0;

            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    lights[j, i] = 0;
                }
            }

            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars);
                var nums = StringParsing.GetNumbersFromString(line);
                if (tokens[0].Equals("toggle"))
                {
                    for (var i = nums[0]; i <= nums[2]; i++)
                    {
                        for (var j = nums[1]; j <= nums[3]; j++)
                        {
                            lights[j, i] += 2;
                        }
                    }
                }
                else if (tokens[1].Equals("on"))
                {
                    for (var i = nums[0]; i <= nums[2]; i++)
                    {
                        for (var j = nums[1]; j <= nums[3]; j++)
                        {
                            lights[j, i] += 1;
                        }
                    }
                }
                else if (tokens[1].Equals("off"))
                {
                    for (var i = nums[0]; i <= nums[2]; i++)
                    {
                        for (var j = nums[1]; j <= nums[3]; j++)
                        {
                            lights[j, i] = Math.Max(0, lights[j,i] - 1);
                        }
                    }
                }
            }

            for (var i = 0; i < 1000; i++)
            {
                for (var j = 0; j < 1000; j++)
                {
                    counter += lights[j, i];
                }
            }
            return counter;
        }
    }
}