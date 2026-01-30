using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2022
{
    public class Day10: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var strength = 1;
			var cycle = 1;
            var total = 0;
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);

                total += CycleHit(cycle, strength);

                if (tokens[0].Equals("noop"))
                {
                    cycle += 1;
                }
                else
                {
                    cycle += 1; 
                    total += CycleHit(cycle, strength);
                    cycle += 1;
                    var nums = StringParsing.GetIntssWithNegativesFromString(tokens[1]);
                    strength += nums.First();
                }
			}

            return total;
        }
        
        private object Part2()
        {var lines = ParseInput.GetLinesFromFile(_filePath);
            var strength = 1;
			var cycle = 1;
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);

                Printer(cycle - 1, strength);
                if (tokens[0].Equals("noop"))
                {
                    cycle += 1;
                }
                else
                {
                    cycle += 1;
                    Printer(cycle - 1, strength);
                    cycle += 1;
                    var nums = StringParsing.GetIntssWithNegativesFromString(tokens[1]);
                    strength += nums.First();
                }
                
                if (cycle == 271)
                {
                    return 0;
                }
			}

            return 0;
        }

        private void Printer(int cycle, int strength)
        {
            strength = strength % 40;
            cycle = cycle % 40;
            if ((cycle) % 40 == 0)
            {
                Console.Write("\n");
            }
            if (cycle == strength || cycle == strength - 1 || cycle == strength + 1)
            {
                Console.Write("#");
            }
            else{
                
                Console.Write(".");
            }
        }

        private int CycleHit(int cycle, int strength)
        {
            if (cycle == 20 ||
            cycle == 60 ||
            cycle == 100 ||
            cycle == 140 ||
            cycle == 180 ||
            cycle == 220)
            {
                return cycle * strength;
            }
            return 0;
        }
    }
}