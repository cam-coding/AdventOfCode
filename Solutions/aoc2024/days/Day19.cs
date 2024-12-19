using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2024
{
    public class Day19: ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<string, bool> _dict;
        private Dictionary<string, long> _dict2;
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
			var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
			long count = 0;
            long number = input.Long;

            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var towels = StringParsing.GetRealTokens(groups[0][0], _delimiterChars);

            var designs = groups[1];
            _dict = new Dictionary<string, bool>();

            //var armourCombinations = designs.GetKCombinations(4).Count();
            foreach (var design in designs)
            {
                count += Possible(design, towels) ? 1 : 0;
                /*
                var temp = towels.Clone().Where(x => design.Contains(x)).ToList();
                // sort by length
                temp.Sort((a, b) => a.Length.CompareTo(b.Length));
                count += Build(towels.Clone(), "", design) ? 1 : 0;*/
            }
            return count;
        }

        private bool Possible(string s, List<string> list)
        {
            if (s.Length == 0)
            {
                return true;
            }
            if (_dict.ContainsKey(s))
            {
                return _dict[s];
            }
            _dict[s] = false;

            foreach (var item in list.Where(x => x.Length <= s.Length && s.StartsWith(x)))
            {
                var length = item.Length;
                var start = s.Substring(0, length);
                var end = s.Substring(length);
                if (Possible(end, list))
                {
                    _dict[s] = true;
                }
            }
            return _dict[s];
        }

        private long Possible2(string s, List<string> list)
        {
            if (s.Length == 0)
            {
                return 1;
            }
            if (_dict2.ContainsKey(s))
            {
                return _dict2[s];
            }
            _dict2[s] = 0;

            foreach (var item in list.Where(x => x.Length <= s.Length && s.StartsWith(x)))
            {
                var length = item.Length;
                var start = s.Substring(0, length);
                var end = s.Substring(length);
                var blah = Possible2(end, list);
                _dict2[s] += blah;
            }
            return _dict2[s];
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var towels = StringParsing.GetRealTokens(groups[0][0], _delimiterChars);

            var designs = groups[1];
            _dict2 = new Dictionary<string, long>();

            //var armourCombinations = designs.GetKCombinations(4).Count();
            foreach (var design in designs)
            {
                count += Possible2(design, towels);
                /*
                var temp = towels.Clone().Where(x => design.Contains(x)).ToList();
                // sort by length
                temp.Sort((a, b) => a.Length.CompareTo(b.Length));
                count += Build(towels.Clone(), "", design) ? 1 : 0;*/
            }
            return count;
        }
    }
}