using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2022
{
    public class Day06: ISolver
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

            Debug.WriteLine("Hello");
            return 0;
        }
        
        private object Part2()
        {
            return 1;
        }
    }
}