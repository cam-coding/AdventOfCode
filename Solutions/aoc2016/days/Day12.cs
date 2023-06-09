using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day12: ISolver
  {
        private string _filePath;
        private char[] delimiterChars = { ' '};
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var counter = 0;
            var registers = new Dictionary<char, int>()
            {
                {'a', 0},
                {'b', 0},
                {'c', 0},
                {'d', 0},
            };
			
            var pc = 0;
            var max = lines.Count();
            while (pc < max)
            {
                var tokens = lines[pc].Split(delimiterChars);

                var reg1 = tokens[1][0];

                if (tokens[0][0].Equals('c'))
                {
                    var reg2 = tokens[2][0];
				    var nums = AdventLibrary.StringParsing.GetNumbersFromString(lines[pc]);
                    if (nums.Count() != 0)
                    {
                        registers[reg2] = nums[0];
                    }
                    else
                    {
                        registers[reg2] = registers[reg1];
                    }
                }
                else if (tokens[0][0].Equals('i'))
                {
                    registers[reg1]++;
                }
                else if (tokens[0][0].Equals('d'))
                {
                    registers[reg1]--;
                }
                else if (tokens[0][0].Equals('j'))
                {
				    var nums = AdventLibrary.StringParsing.GetNumbersWithNegativesFromString(lines[pc]);
                    if (nums.Count() == 2)
                    {
                        if (nums[0] != 0)
                        {
                            pc = pc + nums[1] - 1;
                        }
                    }
                    else
                    {
                        if (registers[reg1] != 0)
                        {
                            pc = pc + nums[0] - 1;
                        }
                    }
                }

                pc++;
            }
            return registers['a'];
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var counter = 0;
            var registers = new Dictionary<char, int>()
            {
                {'a', 0},
                {'b', 0},
                {'c', 1},
                {'d', 0},
            };
			
            var pc = 0;
            var max = lines.Count();
            while (pc < max)
            {
                var tokens = lines[pc].Split(delimiterChars);

                var reg1 = tokens[1][0];

                if (tokens[0][0].Equals('c'))
                {
                    var reg2 = tokens[2][0];
				    var nums = AdventLibrary.StringParsing.GetNumbersFromString(lines[pc]);
                    if (nums.Count() != 0)
                    {
                        registers[reg2] = nums[0];
                    }
                    else
                    {
                        registers[reg2] = registers[reg1];
                    }
                }
                else if (tokens[0][0].Equals('i'))
                {
                    registers[reg1]++;
                }
                else if (tokens[0][0].Equals('d'))
                {
                    registers[reg1]--;
                }
                else if (tokens[0][0].Equals('j'))
                {
				    var nums = AdventLibrary.StringParsing.GetNumbersWithNegativesFromString(lines[pc]);
                    if (nums.Count() == 2)
                    {
                        if (nums[0] != 0)
                        {
                            pc = pc + nums[1] - 1;
                        }
                    }
                    else
                    {
                        if (registers[reg1] != 0)
                        {
                            pc = pc + nums[0] - 1;
                        }
                    }
                }

                pc++;
            }
            return registers['a'];
        }
    }
}
