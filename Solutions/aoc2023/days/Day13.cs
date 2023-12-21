using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2023
{
    public class Day13: ISolver
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
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var counter = 0;

            var currentGrid = new List<string>();

			foreach (var line in lines)
			{
                if (string.IsNullOrWhiteSpace(line))
                {
                    counter += HandleGrid(currentGrid);
                    currentGrid = new List<string>();
                }
                else
                {
                    currentGrid.Add(line);
                }
            }
            counter += HandleGrid(currentGrid);
            return counter;
        }

        private int HandleGrid(List<string> grid)
        {
            var cols = grid.GetColumns();
            var mirror = FindMirror(grid);
            if (mirror == -1)
            {
                mirror = FindMirror(cols);
                return mirror + 1;
            }
            return (mirror + 1) * 100;
        }

        // mirror is between return and return + 1;
        private int FindMirror(List<string> lines)
        {
            for (var i = 0; i < lines.Count-1; i++)
            {
                var valid = true;
                for (var j = 0; j < lines.Count/2; j++)
                {
                    var bottom = i - j;
                    var top = i + j + 1;
                    if (bottom < 0 || top >= lines.Count)
                    {
                        break;
                    }
                    if (!lines[bottom].Equals(lines[top]))
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    return i;
                }
            }
            return -1;
        }

        private int FindMirror2(List<string> lines)
        {
            var winner = -1;
            for (var i = 0; i < lines.Count - 1; i++)
            {
                var count = 0;
                for (var j = 0; j < lines.Count / 2; j++)
                {
                    var bottom = i - j;
                    var top = i + j + 1;
                    if (bottom < 0 || top >= lines.Count)
                    {
                        break;
                    }
                    var differences = ListExtensions.CountDifferences<char>(lines[bottom].ToList(), lines[top].ToList());
                    count += differences;
                    if (differences > 1)
                    {
                        break;
                    }
                }
                if (count == 1)
                {
                    winner = i;
                }
            }
            return winner;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var counter = 0;
            var currentGrid = new List<string>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    counter += HandleGrid2(currentGrid);
                    currentGrid = new List<string>();
                }
                else
                {
                    currentGrid.Add(line);
                }

            }
            counter += HandleGrid2(currentGrid);
            return counter;
        }

        private int HandleGrid2(List<string> grid)
        {
            var cols = grid.GetColumns();
            var mirror = FindMirror2(grid);
            var count = 0;
            if (mirror == -1)
            {
                mirror = FindMirror2(cols);
                return mirror + 1;
            }
            return count = (mirror + 1) * 100;
        }
    }
}