using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day10: ISolver
    {
        private string _filePath;
        private Dictionary<char, char> pairs;
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            pairs = new Dictionary<char, char>();
            pairs.Add('(', ')');
            pairs.Add('[', ']');
            pairs.Add('{', '}');
            pairs.Add('<', '>');
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
			long counter = 0;
			
			foreach (var line in lines)
			{
                counter = counter + StackStuff(line);
			}
            return counter;
        }
        
        private object Part2()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
			var counter = new List<long>();
			
			foreach (var line in lines)
			{
                var num = StackStuff2(line);
                if (num != 0)
                {
                    counter.Add(num);
                }
			}
            counter.Sort();
            return counter[counter.Count / 2];
        }

        private long StackStuff2(string line)
        {
            var stacky = new Stack<char>();
            foreach (var c in line)
            {
                if (pairs.ContainsKey(c))
                {
                    stacky.Push(c);
                }
                else
                {
                    var pop = stacky.Pop();
                    if (c != pairs[pop])
                    {
                        return 0;
                    }
                }
            }

            return Points2(stacky);
        }

        private long StackStuff(string line)
        {
            var stacky = new Stack<char>();
            foreach (var c in line)
            {
                if (pairs.ContainsKey(c))
                {
                    stacky.Push(c);
                }
                else
                {
                    var pop = stacky.Pop();
                    if (c != pairs[pop])
                    {
                        return Points(c);
                    }
                }
            }
            return 0;
        }

        private long Points(char c)
        {
            var dict = new Dictionary<char, int>();
            dict.Add(')', 3);
            dict.Add(']', 57);
            dict.Add('}', 1197);
            dict.Add('>', 25137);
            return (dict[c]);
        }

        private long Points2(Stack<char> stacky)
        {
            var dict = new Dictionary<char, int>();
            dict.Add('(', 1);
            dict.Add('[', 2);
            dict.Add('{', 3);
            dict.Add('<', 4);
            var top = stacky.Count;
            long county = 0;
            for (var j = 0; j < top; j++)
            {
                var thing = stacky.Pop();
                county = county*5 + dict[thing];
            }
            return county;
        }
    }
}