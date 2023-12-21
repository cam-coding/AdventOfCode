using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day09bad: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine("Part 1: " + Part1.ToString());
		*/
        private string _filePath;
        private List<List<int>> grid;
        private List<Tuple<int, int>> done;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var counter = 0;
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            grid = new List<List<int>>();
			
			foreach (var line in lines)
			{
				var things = line.Split(delimiterChars);
				var nums = AdventLibrary.StringParsing.GetDigitsFromString(line);
                grid.Add(nums);
			}

            var i =0;
            foreach (var line in grid)
            {
                var j = 0;
                foreach (var num in line)
                {
                    if (LowerThan(i, j))
                    {
                        counter = counter + (1 + grid[i][j]);
                    }
                    j++;
                }
                i++;
            }
            return counter;
        }
        
        private object Part2()
        {
            var counter = 0;
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            grid = new List<List<int>>();
            done = new List<Tuple<int, int>>();
			
			foreach (var line in lines)
			{
				var things = line.Split(delimiterChars);
				var nums = AdventLibrary.StringParsing.GetDigitsFromString(line);
                grid.Add(nums);
			}

            var i =0;
            var largeBasins = new List<int>();
            foreach (var line in grid)
            {
                var j = 0;
                foreach (var num in line)
                {
                    var listy = new List<Tuple<int, int>>();
                    done = new List<Tuple<int, int>>();
                    var basinList = GetAdjInGrid(i, j, listy);
                    var basinSize = basinList.GroupBy(x => new {x.Item1, x.Item2}).Select(x => x.First()).ToList().Count;
                    if (largeBasins.Count < 3)
                    {
                        largeBasins.Add(basinSize);
                    }
                    else
                    {
                        if (basinSize > largeBasins.Min())
                        {
                            largeBasins.Remove(largeBasins.Min());
                            largeBasins.Add(basinSize);
                        }
                    }
                    if (LowerThan(i, j))
                    {
                        counter = counter + (1 + grid[i][j]);
                    }
                    j++;
                }
                i++;
            }
            return largeBasins[0] * largeBasins[1] * largeBasins[2];
        }

        private bool LowerThan(int i, int j)
        {
            return grid[i][j] < GetAdjacentNumbersInGrid(grid, i, j).Min();
        }

        private List<int> GetAdjacentNumbersInGrid(List<List<int>> grid, int i, int j)
        {
            var adjacentNumbers = new List<int>();
            if (i > 0)
            {
                adjacentNumbers.Add(grid[i - 1][j]);
            }
            if (i < grid.Count - 1)
            {
                adjacentNumbers.Add(grid[i + 1][j]);
            }
            if (j > 0)
            {
                adjacentNumbers.Add(grid[i][j - 1]);
            }
            if (j < grid[i].Count - 1)
            {
                adjacentNumbers.Add(grid[i][j + 1]);
            }
            return adjacentNumbers;
        }
        
        private List<Tuple<int, int>> GetAdjInGrid(int i, int j, List<Tuple<int, int>> listy)
        {
            var toople = new Tuple<int, int>(i, j);
            if (grid[i][j] == 9)
            {
                listy.Add(toople);
                return listy;
            }
            if (done.Any(x => x.Item1 == i && x.Item1 == j))
            {
                return listy;
            }
            else
            {
                listy.Add(toople);
            }
            if (j < 0 || j > 8 || i < 0 || i > 8)
            {
                return listy;
            }
            var adjacentNumbers = new List<int>();
            if (i > 0 && i < grid.Count)
            {
                if (grid[i - 1][j] != 9)
                {
                    listy.AddRange(GetAdjInGrid(i-1, j, listy));
                }
            }
            if (i < grid.Count - 1 && i > -1)
            {
                if (grid[i + 1][j] != 9)
                {
                    listy.AddRange(GetAdjInGrid(i+1, j, listy));
                }
            }
            if (j > 0 && j < grid[i].Count)
            {
                if (grid[i][j-1] != 9)
                {
                    listy.AddRange(GetAdjInGrid(i, j-1, listy));
                }
            }
            if (j < grid[i].Count - 1 && j > -1)
            {
                if (grid[i][j + 1] != 9)
                {
                    listy.AddRange(GetAdjInGrid(i, j+1, listy));
                }
            }

            /*
            if (listy.Any(x => x.Item1 == i && x.Item1 == j))
            {
                return 0;
            } */
            return listy;
        }
    }
}
