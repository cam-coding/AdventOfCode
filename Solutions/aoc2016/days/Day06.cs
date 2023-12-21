using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
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

            var dict = new Dictionary<int,Dictionary<char,int>>();
            for (var i = 0; i < lines[0].Length; i++)
            {
                dict.Add(i, new Dictionary<char, int>());
            }
			
			foreach (var line in lines)
			{
                for (var i = 0; i < line.Length; i++)
                {
                    if (dict[i].ContainsKey(line[i]))
                    {
                        dict[i][line[i]]++;
                    }
                    else
                    {
                        dict[i].Add(line[i], 1);
                    }
                }
			}

            var answer = string.Empty;

            foreach (var lookup in dict.Values)
            {
                var entry = lookup.OrderByDescending(x => x.Value).First();
                answer = answer + entry.Key;
            }
            return answer;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            var dict = new Dictionary<int,Dictionary<char,int>>();
            for (var i = 0; i < lines[0].Length; i++)
            {
                dict.Add(i, new Dictionary<char, int>());
            }
			
			foreach (var line in lines)
			{
                for (var i = 0; i < line.Length; i++)
                {
                    if (dict[i].ContainsKey(line[i]))
                    {
                        dict[i][line[i]]++;
                    }
                    else
                    {
                        dict[i].Add(line[i], 1);
                    }
                }
			}

            var answer = string.Empty;

            foreach (var lookup in dict.Values)
            {
                var entry = lookup.OrderBy(x => x.Value).First();
                answer = answer + entry.Key;
            }
            return answer;
        }
    }
}
