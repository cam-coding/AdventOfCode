using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using AdventLibrary;
using AdventLibrary.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace aoc2023
{
    public class Day07: ISolver
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
            var total = 1000000;
			var count = 0;

            var dict = new Dictionary<string, int>();

			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
                var bid = tokens[1];
                dict.Add(tokens[0], int.Parse(tokens[1]));
			}
            // var sorted = dict.OrderBy(item => item, new CustomStringComparer());
            var blah = new SortedDictionary<string, int>(dict, new CustomStringComparer());
            var tots = 1;
            foreach (var item in blah)
            {
                count += item.Value * tots;
                tots++;
            }
            return count;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var total = 1000000;
            var count = 0;

            var dict = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars);
                var bid = tokens[1];
                dict.Add(tokens[0], int.Parse(tokens[1]));
            }
            // var sorted = dict.OrderBy(item => item, new CustomStringComparer());
            var blah = new SortedDictionary<string, int>(dict, new CustomStringComparer());
            var tots = 1;
            foreach (var item in blah)
            {
                count += item.Value * tots;
                tots++;
            }
            return count;
        }

        private class CustomStringComparer : IComparer<string>
        {
            private Dictionary<char, int> lookup = new Dictionary<char, int>()
        {
            { 'A', 1 },
            { 'K', 2 },
            { 'Q', 3 },
            { 'T', 4 },
            { '9', 5 },
            { '8', 6 },
            { '7', 7 },
            { '6', 8 },
            { '5', 9 },
            { '4', 10 },
            { '3', 11 },
            { '2', 12 },
            { 'J', 13 },
        };
            private readonly IComparer<string> _baseComparer;
            public CustomStringComparer()
            {
            }

            public int Compare(string x, string y)
            {
                var rankX = GetRank(x);
                var rankY = GetRank(y);
                if (rankX < rankY)
                {
                    return 1;
                }
                if (rankY < rankX)
                {
                    return -1;
                }
                if (rankX == rankY)
                {
                    for (var j = 0; j < x.Length; j++)
                    {
                        if (lookup[x[j]] < lookup[y[j]])
                        {
                            return 1;
                        }
                        if (lookup[y[j]] < lookup[x[j]])
                        {
                            return -1;
                        }
                    }
                }
                return 0;
            }

            private int GetRank(string str)
            {
                var jCount = str.Count(x => x == 'J');
                var rank = 0;
                var same = HowManyLettersSame2(str);
                if (same[0] == 5)
                {
                    rank = 1;
                }
                else if (same[0] == 4)
                {
                    if (str.Contains('J'))
                    {
                        if (jCount == 1 || jCount == 4)
                        {
                            rank = 1;
                        }
                        else
                        {
                            rank = 2;
                        }
                    }
                    else
                    {
                        rank = 2;
                    }
                }
                else if (same[0] == 3)
                {
                    if (str.Contains('J'))
                    {
                        if (same.Count == 2)
                        {
                            rank = 1;
                        }
                        else
                        {
                            if (jCount == 1 || jCount == 3)
                            {
                                rank = 2;
                            }
                            else
                            {
                                rank = 4;
                            }
                        }
                    }
                    else
                    {
                        if (same.Count == 2)
                        {
                            rank = 3;
                        }
                        else
                        {
                            rank = 4;
                        }
                    }
                }
                else if (same[0] == 2)
                {
                    if (same.Count == 3)
                    {
                        if (str.Contains('J'))
                        {
                            if (jCount == 1)
                            {
                                rank = 3;
                            }
                            else if (jCount == 2)
                            {
                                rank = 2;
                            }
                        }
                        else
                        {
                            rank = 5;
                        }
                    }
                    // single pair
                    else
                    {
                        if (str.Contains('J'))
                        {
                            rank = 4;
                        }
                        else
                        {
                            rank = 6;
                        }
                    }
                }
                else
                {
                    if (str.Contains('J'))
                    {
                        rank = 6;
                    }
                    else
                    {
                        rank = 7;
                    }
                }
                return rank;
            }

            private int HowManyLettersSame(string str)
            {
                var max = 0;
                foreach (var c in str)
                {
                    var count = 0;
                    for (var i = 0; i < str.Length; i++)
                    {
                        if (str[i] == c)
                        {
                            count++;
                        }
                    }
                    max = Math.Max(max, count);
                }
                return max;
            }

            private List<int> HowManyLettersSame2(string str)
            {
                var max = 0;
                var dict = new Dictionary<char, int>();
                foreach (var c in str)
                {
                    if (dict.ContainsKey(c))
                    {
                        dict[c]++;
                    }
                    else
                    {
                        dict.Add(c, 1);
                    }
                }
                var blah = dict.ToImmutableSortedDictionary().Values.ToList();
                blah.Sort();
                blah.Reverse();
                return blah;
            }
        }
    }
}