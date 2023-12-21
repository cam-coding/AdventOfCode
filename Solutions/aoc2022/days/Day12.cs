using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2022
{
    public class Day12: ISolver
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
            var line = lines[0];
            var buffer = new List<char>();
            
            for (var i = 0; i < line.Count(); i++)
            {
                if (i < 4)
                {
                    buffer.Add(line[i]);
                }
                else
                {
                    if (buffer.AllItemsUnique())
                    {
                        return i;
                    }
                    else
                    {
                        buffer = buffer.RotateListLeft();
                        buffer[3] = line[i];
                    }
                }
            }

            return 0;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var line = lines[0];
            var buffer = new List<char>();
            var count = 0;
            
            for (var i = 0; i < line.Count(); i++)
            {
                if (i < 14)
                {
                    buffer.Add(line[i]);
                }
                else
                {
                    if (buffer.AllItemsUnique())
                    {
                        return i;
                    }
                    else
                    {
                        buffer = buffer.RotateListLeft();
                        buffer[13] = line[i];
                    }
                }
            }

            Console.WriteLine("hello");

            return 0;
        }
    }
}