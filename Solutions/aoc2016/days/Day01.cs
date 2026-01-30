using AdventLibrary;
using System.Collections.Generic;
using System.Linq;

namespace aoc2016
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
            var x = 0;
            var y = 0;
            var direction = 0;
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);

                foreach (var token in tokens)
                {
                    if (token.Equals(string.Empty))
                    {
                        continue;
                    }
                    var num = AdventLibrary.StringParsing.GetIntsFromString(token).First();

                    if (token[0] == 'L')
                        direction = direction - 1;
                    else if (token[0] == 'R')
                        direction = direction + 1;
                    
                    if (direction == -1)
                        direction = 3;
                    if (direction == 4)
                        direction = 0;

                    if (direction == 0)
                        y = y + num;
                    else if (direction == 1)
                        x = x + num;
                    else if (direction == 2)
                        y = y - num;
                    else if (direction == 3)
                        x = x - num;
                }
			}
            return x + y;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var x = 0;
            var y = 0;
            var direction = 0;
            var cache = new Dictionary<int,HashSet<int>>();
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);

                foreach (var token in tokens)
                {
                    if (token.Equals(string.Empty))
                    {
                        continue;
                    }
                    var num = AdventLibrary.StringParsing.GetIntsFromString(token).First();
                    
                    if (token[0] == 'L')
                        direction = direction - 1;
                    else if (token[0] == 'R')
                        direction = direction + 1;
                    
                    if (direction == -1)
                        direction = 3;
                    if (direction == 4)
                        direction = 0;

                    for (var j = 0; j < num; j++)
                    {

                        if (direction == 0)
                            y = y + 1;
                        else if (direction == 1)
                            x = x + 1;
                        else if (direction == 2)
                            y = y - 1;
                        else if (direction == 3)
                            x = x - 1;

                        if (cache.ContainsKey(x))
                        {
                            if (cache[x].Contains(y))
                            {
                                return x + y;
                            }
                            cache[x].Add(y);
                        }
                        else
                        {
                            cache.Add(x, new HashSet<int>() { y });
                        }
                    }
                }
			}
            return x + y;
        }
    }
}
