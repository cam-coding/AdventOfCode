using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2017
{
    public class Day08: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
			var numbers = input.Longs;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            long total = 1000000;
			long count = 0;
            long number = input.Long;
            var dict = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var tokens = line.GetRealTokens(delimiterChars);
                var key = tokens[0];
                dict.TryAdd(key, 0);
                if (GetBool(tokens[4], tokens[5], tokens[6], dict))
                {
                    var val = int.Parse(tokens[2]);
                    if (tokens[1].Equals("inc"))
                    {
                        dict[key] += val;
                    }
                    else
                    {
                        dict[key] -= val;
                    }
                }
            }
            return dict.Values.Max();
        }

        private bool GetBool(string regKey, string symbol, string value, Dictionary<string,int> dict)
        {
            var regValue = dict.ContainsKey(regKey) ? dict[regKey] : 0;
            return ComparisonOperatorHelper.RunOperation(symbol, regValue, int.Parse(value));
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            long total = 1000000;
            long count = 0;
            long number = input.Long;
            var dict = new Dictionary<string, int>();
            var maxy = int.MinValue;

            foreach (var line in lines)
            {
                var tokens = line.GetRealTokens(delimiterChars);
                var key = tokens[0];
                dict.TryAdd(key, 0);
                if (GetBool(tokens[4], tokens[5], tokens[6], dict))
                {
                    var val = int.Parse(tokens[2]);
                    if (tokens[1].Equals("inc"))
                    {
                        dict[key] += val;
                    }
                    else
                    {
                        dict[key] -= val;
                    }
                }
                maxy = Math.Max(maxy, dict.Values.Max());
            }
            return maxy;
        }
    }
}