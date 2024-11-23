using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2023
{
    public class Day14: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var roller = new GridRoller<char>(
                grid,
                new HashSet<char>() { '.' },
                new HashSet<char>() { '#' },
                new HashSet<char>() { 'O' });
            grid = roller.RollUp();

            var count = Count(grid);
            return count;
        }

        private object Part2()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            var dict = new Dictionary<int, List<List<char>>>();
            var dict2 = new HashSet<string>();
            var listy = new List<int>();
            var magic = 200;
            for (var i = 0; i < magic; i++)
            {
                var roller = new GridRoller<char>(
                    grid,
                    new HashSet<char>() { '.' },
                    new HashSet<char>() { '#' },
                    new HashSet<char>() { 'O' });
                grid = roller.RollUp();
                grid = roller.RollLeft();
                grid = roller.RollDown();
                grid = roller.RollRight();

                dict.Add(i, grid.Clone2dList());
                if (!dict2.Contains(grid.Stringify()))
                {
                    dict2.Add(grid.Stringify());
                    listy.Add(i);
                }
            }

            // this was the last time we had a unique grid configuration + 1
            var cycleStart = listy.Last() + 1;
            var slow = cycleStart;
            var fast = slow + 1;
            var cycleLength = 1;
            while (!dict[slow].Stringify().Equals(dict[fast].Stringify()))
            {
                cycleLength++;
                slow++;
                fast += 2;
            }

            var diff = (1000000000 - 1) - (cycleStart);
            var rem = diff % cycleLength;
            return Count(dict[cycleStart + rem]);
        }

        private int Count(List<List<char>> grid)
        {
            var count = 0;
            var iter = 1;
            for (var i = grid.Count - 1; i >= 0; i--)
            {
                for (var j = 0; j < grid[i].Count; j++)
                {
                    if (grid[i][j] == 'O')
                    {
                        count += iter;
                    }
                }
                iter++;
            }
            return count;
        }
    }
}