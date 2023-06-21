using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day08: ISolver
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
            var codeCounter = 0;
            var realCounter = 0;
			
			foreach (var line in lines)
			{
                codeCounter += line.Length;
                for (var i = 1; i < line.Length-1; i++)
                {
                    if (line[i] != '\\')
                    {
                        realCounter++;
                    }
                    else
                    {
                        if (line[i+1] == '"' || line[i+1] == '\\')
                        {
                            realCounter++;
                            i++;
                        }
                        else if (line[i+1] == 'x')
                        {
                            realCounter++;
                            i = i + 3;
                        }
                    }
                }

			}
            return codeCounter - realCounter;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var codeCounter = 0;
            var realCounter = 0;

            foreach (var line in lines)
            {
                codeCounter += line.Length;
                realCounter++; // open quote
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '"')
                    {
                        realCounter += 2; // " -> \"
                    }
                    else if (line[i] == '\\')
                    {
                        realCounter += 2; // \ -> \\
                        if (line[i+1] == '"')
                        {
                            realCounter += 2; // " -> \"
                            i++;
                        }
                    }
                    else
                    {
                        realCounter++;
                    }
                }
                realCounter++; // end quote

            }
            return realCounter - codeCounter;
        }
    }
}