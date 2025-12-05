using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2025
{
    public class Day03 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<int, long> _myDict;
        private int _maxLength = 12;

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
            long count = 0;

            foreach (var line in lines)
            {
                var perms = line.ToList().GetPermutationsOrderedOfSize(2);
                var max = 0;
                foreach (var perm in perms)
                {
                    max = Math.Max(max, perm.ConcatListToString().ConvertToInt());
                }

                count += max;
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            long count = 0;

            foreach (var line in lines)
            {
                _myDict = DictionaryHelper.CreateIndexDictionary((long)0, 13);
                RecursionTime(line, string.Empty);
                count += _myDict[12];
            }
            return count;
        }

        private void RecursionTime(string str, string current)
        {
            var length = current.Length;
            // don't try and parse the empty string.
            long currentVal = current.Length > 0 ? long.Parse(current) : 0;
            if (length > 0 && currentVal <= _myDict[length])
            {
                return;
            }
            else
            {
                _myDict[length] = currentVal;
            }
            if (length == _maxLength)
            {
                return;
            }
            for (var j = 9; j >= 0; j--)
            {
                var indx = str.IndexOf("" + j);
                if (indx >= 0)
                {
                    var subStr = str.Substring(indx + 1);
                    // don't go deeper if there's not enough characters to form a string of the required length
                    if (subStr.Count() >= (_maxLength - length))
                    {
                        RecursionTime(str.Substring(indx + 1), current + str[indx]);
                    }
                }
            }
        }
    }
}