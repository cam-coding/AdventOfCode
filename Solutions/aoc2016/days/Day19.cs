using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day19: ISolver
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
            var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var masterList = new List<int>();

            for (var i = 0; i < numbers[0]; i++)
            {
                masterList.Add(i + 1);
            }

            while (masterList.Count() > 1)
            {
                var temp = new List<int>();
                var skip = false;
                var i = 0;
                if (masterList.Count % 2 == 1)
                {
                    i = 2;
                }

                while (i < masterList.Count)
                {
                    if (!skip)
                    {
                        temp.Add(masterList[i]);
                    }
                    skip = !skip;
                    i++;
                }
                masterList = temp;
            }
            return masterList[0];
        }
        
        private object Part2()
        {
            // awful brute force
            // need to understand the stack + queue from here https://www.reddit.com/r/adventofcode/comments/5j4lp1/2016_day_19_solutions/dbdf9mn/
            var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var masterList = new List<int>();
            var current = 0;

            for (var i = 0; i < numbers[0]; i++)
            {
                masterList.Add(i + 1);
            }

            var j = 0;
            while (masterList.Count() > 1)
            {
                var removal = j + (masterList.Count / 2);
                if (removal >= masterList.Count)
                {
                    removal = removal - masterList.Count;
                }
                masterList.RemoveAt(removal);
                if (removal > j)
                {
                    j++;
                }
                if (j == masterList.Count)
                {
                    j = 0;
                }
            }
            return masterList[0];
        }
    }
}
