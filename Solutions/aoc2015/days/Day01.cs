using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day01: ISolver
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

            var openCount = 0;
            var closeCount = 0;
			
			foreach (var c in lines[0])
			{
                if (c == '(')
                {
                    openCount++;
                }
                else if (c == ')')
                {
                    closeCount++;
                }
			}
            return openCount-closeCount;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var opens = new Stack<char>();
            var counter = 1;

            foreach (var c in lines[0])
            {
                if (c == '(')
                {
                    opens.Push(c);
                }
                else if (c == ')')
                {
                    if (opens.Count > 0)
                    {
                        opens.Pop();
                    }
                    else
                    {
                        return counter;
                    }
                }
                counter++;

            }
            return counter;
        }
    }
}