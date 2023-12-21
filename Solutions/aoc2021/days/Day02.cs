using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day02: ISolver
    {
        public Solution Solve(string filePath, bool isTest = false)
        {
            /*
            var letters = AdventLibrary.StringParsing.GetStringBetweenTwoCharacters(input, "a", "b");
            var letters = AdventLibrary.StringParsing.GetLettersFromString(input);
            var letters = AdventLibrary.StringParsing.GetLettersFromString(input);
            var digits = AdventLibrary.StringParsing.GetDigitsFromString(input);
            var sub = item.Substring(0, 1);
            Console.WriteLine("Part 1: " + Part1.ToString());
            */
            return new Solution(Part1(filePath), Part2(filePath));
        }

        private object Part1(string filePath)
        {
            var strings = AdventLibrary.ParseInput.GetLinesFromFile(filePath);
            var hor = 0;
            var ver = 0;

            foreach (var item in strings)
            {
                var letters = AdventLibrary.StringParsing.GetLettersFromString(item);
                var digits = AdventLibrary.StringParsing.GetDigitsFromString(item);
                if (letters.StartsWith("f"))
                {
                    hor+= digits.First();
                }
                else if (letters.StartsWith("d"))
                {
                    ver+= digits.First();
                }
                else if (letters.StartsWith("u"))
                {
                    ver-= digits.First();
                }
            }
            return ver*hor;
        }
        
        private object Part2(string filePath)
        {
            var strings = AdventLibrary.ParseInput.GetLinesFromFile(filePath);
            var hor = 0;
            var dep = 0;
            var aim = 0;

            foreach (var item in strings)
            {
                var letters = AdventLibrary.StringParsing.GetLettersFromString(item);
                var digits = AdventLibrary.StringParsing.GetDigitsFromString(item);
                if (letters.StartsWith("f"))
                {
                    hor+= digits.First();
                    dep += (digits.First()*aim);
                }
                else if (letters.StartsWith("d"))
                {
                    aim += digits.First();
                }
                else if (letters.StartsWith("u"))
                {
                    aim -= digits.First();
                }
            }
            return dep*hor;
        }
    }
}
