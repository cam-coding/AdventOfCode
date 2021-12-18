using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day09: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine("Part 1: " + Part1.ToString());
		*/
        private string _filePath;
        private List<List<int>> grid;
        private List<Tuple<int, int>> done;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
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
            var locationsChecked = new List<Tuple<int,int>>();
            foreach (var line in grid)
            {
                var j = 0;
                foreach (var num in line)
                {
                    if (!locationsChecked.Any(x => x.Item1 == i && x.Item2 == j))
                    {
                        var listy = new List<Tuple<int, int>>();
                        done = new List<Tuple<int, int>>();
                        var basinList = DepthSearch(i, j, listy);
                        locationsChecked = CombinedUnique(locationsChecked, basinList);
                        var basinSize = CountBasin(basinList);
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

        private int CountBasin(List<Tuple<int, int>> basin)
        {
            var count = 0;
            foreach(var point in basin)
            {
                if (grid[point.Item1][point.Item2] != 9)
                {
                    count++;
                }
            }
            return count;
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

        private List<Tuple<int, int>> DepthSearch(int i, int j, List<Tuple<int, int>> tail)
        {
            //if i or j outside grid, return original tail
            if (i < 0 || i >= grid.Count || j < 0 || j >= grid[i].Count)
            {
                return tail;
            }

            var toople = new Tuple<int, int>(i, j);
            //if tail contains toople, return original tail
            if (tail.Any(x => x.Item1 == i && x.Item2 == j))
            {
                return tail;
            }
            var currentTail = tail.ToList();
            currentTail.Add(toople);

            if (grid[i][j] == 9)
            {
                return currentTail;
            }

            //go right and call recur with currentTail
            //take return value from call and combine with currentTail keeping only unique
            currentTail = CombinedUnique(currentTail, DepthSearch(i, j+1, currentTail));
            // down
            currentTail = CombinedUnique(currentTail, DepthSearch(i+1, j, currentTail));
            // left
            currentTail = CombinedUnique(currentTail, DepthSearch(i, j-1, currentTail));
            // up
            currentTail = CombinedUnique(currentTail, DepthSearch(i-1, j, currentTail));

            return currentTail;
        }

        private List<Tuple<int, int>> CombinedUnique(List<Tuple<int, int>> list1, List<Tuple<int, int>> list2)
        {
            foreach (var item in list2)
            {
                if (!list1.Any(x => x.Item1 == item.Item1 && x.Item2 == item.Item2))
                {
                    list1.Add(item);
                }
            }
            return list1;
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
