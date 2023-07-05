using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day25: ISolver
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
            var nums = AdventLibrary.StringParsing.GetNumbersFromString(lines[0]);
            var iter = GetCodeNumber(nums[0], nums[1]);
            long current = 20151125;
            for (var i = 1; i < iter; i++)
            {
                current = (current * 252533) % 33554393;
            }
            return current;
        }
        
        private object Part2()
        {
            return 0;
        }

        private int GetCodeNumber(int row, int col)
        {
            var specialRow = 1;
            for (var i = 1; i < row; i++)
            {
                specialRow += i;
            }
            var specialColumn = specialRow;
            var adder = row + 1;
            for (var i = 1; i < col; i++)
            {
                specialColumn += adder;
                adder++;
            }
            return specialColumn;
        }
    }
}