using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day05: ISolver
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
			var counter = 0;
			
			foreach (var line in lines)
			{
                if (line.Contains("ab") ||
                    line.Contains("cd") ||
                    line.Contains("pq") ||
                    line.Contains("xy"))
                {
                    continue;
                }

                var vowels = 0;
                var boolDouble = false;

                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == 'a' ||
                        line[i] == 'e' ||
                        line[i] == 'i' ||
                        line[i] == 'o' ||
                        line[i] == 'u')
                    {
                        vowels++;
                    }

                    if (i > 0 && line[i] == line[i-1])
                    {
                        boolDouble = true;
                    }
                }

                if (vowels >= 3 && boolDouble)
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
                var vowels = 0;
                var boolDouble = false;
                var boolPair = false;
                var dict = new Dictionary<string, int>();

                for (var i = 0; i < line.Length; i++)
                {

                    if (i < line.Length - 1)
                    {
                        var substr = line.Substring(i, 2);
                        if (dict.ContainsKey(substr))
                        {
                            if (dict[substr] != i - 1)
                            {
                                boolPair = true;
                            }
                        }
                        else
                        {
                            dict.Add(substr, i);
                        }
                    }

                    if (i > 1 && line[i] == line[i - 2])
                    {
                        boolDouble = true;
                    }
                }

                if (boolPair && boolDouble)
                {
                    counter++;
                }


            }
            return counter;
        }
    }
}