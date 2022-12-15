using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2022
{
    public class Day06: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
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
                    if (ListHelper.AllItemsUnique(buffer))
                    {
                        return i;
                    }
                    else
                    {
                        buffer = ListTransforming.RotateListLeft(buffer);
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
                    if (ListHelper.AllItemsUnique(buffer))
                    {
                        return i;
                    }
                    else
                    {
                        buffer = ListTransforming.RotateListLeft(buffer);
                        buffer[13] = line[i];
                    }
                }
            }

            return count;
        }
    }
}