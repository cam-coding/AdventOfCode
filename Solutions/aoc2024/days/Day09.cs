using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace aoc2024
{
    public class Day09: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
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
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
			long count = 0;
            long number = input.Long;

            var text = input.Text;
            var numbs = StringParsing.GetDigitsFromString(text);
            var listy = new List<int>();
            var fileId = 0;
            for (var i = 0; i < numbs.Count; i++)
            {
                var isEven = i % 2 == 0;
                if (isEven)
                {
                    for (var j = 0; j < numbs[i]; j++)
                    {
                        listy.Add(fileId);
                    }
                    fileId++;
                }
                else
                {
                    for (var j = 0; j < numbs[i]; j++)
                    {
                        listy.Add(-1);
                    }
                }

            }
            for (var i = listy.Count-1; i >= 0; i--)
            {
                if (listy[i] != -1)
                {
                    var index = listy.IndexOf(-1);

                    if (index > i)
                    {
                        break;
                    }
                    listy.SwapItemsAtIndexes(i, index);
                }
            }

            for (var i = 0; i < listy.Count; i++)
            {
                if (listy[i] == -1)
                {
                    continue;
                }
                count += listy[i] * i;
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var text = input.Text;
            var numbs = StringParsing.GetDigitsFromString(text);
            var listy = new List<int>();
            var fileId = 0;
            var spaces = new List<(int index, int space)>();
            var files = new List<(int index, int space, int fileId)>();
            for (var i = 0; i < numbs.Count; i++)
            {
                var isEven = i % 2 == 0;
                if (isEven)
                {
                    files.Add((listy.Count, numbs[i], fileId));
                    for (var j = 0; j < numbs[i]; j++)
                    {
                        listy.Add(fileId);
                    }
                    fileId++;
                }
                else
                {
                    if (numbs[i] != 0)
                    {
                        spaces.Add((listy.Count, numbs[i]));
                        for (var j = 0; j < numbs[i]; j++)
                        {
                            listy.Add(-1);
                        }
                    }
                }

            }
            for (var i = files.Count - 1; i >= 0; i--)
            {
                var file = files[i];
                var spaceIndex = spaces.FirstOrDefault(x => x.space >= file.space);
                if (spaceIndex == default)
                {
                    continue;
                }

                var space = spaceIndex;
                var spaceLooper = space.index;
                var fileLooper = file.index;

                if (spaceLooper > fileLooper)
                {
                    continue;
                }
                for (var j = 0; j < file.space; j++)
                {
                    listy.SwapItemsAtIndexes(spaceLooper, fileLooper);
                    spaceLooper++;
                    fileLooper++;
                }
                var dif = space.space - file.space;
                if (dif == 0)
                {
                    spaces.Remove(space);
                }
                else
                {
                    var ind = spaces.IndexOf(space);
                    spaces[ind] = (space.index + file.space, dif);
                }
            }
            count = 0;
            for (var i = 0; i < listy.Count; i++)
            {
                if (listy[i] == -1)
                {
                    continue;
                }
                count += listy[i] * i;
            }
            return count;
        }
    }
}