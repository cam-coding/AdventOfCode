using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day13: ISolver
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
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var grid = new bool[2000,2000];
			
			foreach (var line in lines)
			{
                if (string.IsNullOrWhiteSpace(line));
                else if (!line.Contains("fold"))
                {
                    var tokens = line.Split(delimiterChars);
                    grid[Convert.ToInt32(tokens[1]),Convert.ToInt32(tokens[0])] = true;
                }
                else
                {
                    var num = AdventLibrary.StringParsing.GetNumbersFromString(line).First();
                    if (line.Contains("x"))
                    {
                        grid = Fold(grid, num, false);
                    }
                    else
                    {
                        grid = Fold(grid, num, true);
                    }
            
                    var count2 = 0;
                    for (var i = 0; i < 2000; i++)
                    {
                        for(var j = 0; j < 2000; j++)
                        {
                            if (grid[i,j])
                            {
                                count2++;
                            }
                        }
                    }
                    return count2;
                }
			}
            return 0;
        }
        
        private object Part2()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var grid = new bool[2000,2000];
			
			foreach (var line in lines)
			{
                if (string.IsNullOrWhiteSpace(line));
                else if (!line.Contains("fold"))
                {
                    var tokens = line.Split(delimiterChars);
                    grid[Convert.ToInt32(tokens[1]),Convert.ToInt32(tokens[0])] = true;
                }
                else
                {
                    var num = AdventLibrary.StringParsing.GetNumbersFromString(line).First();
                    if (line.Contains("x"))
                    {
                        grid = Fold(grid, num, false);
                    }
                    else
                    {
                        grid = Fold(grid, num, true);
                    }
                }
			}

            var count = 0;
            for (var i = 0; i < 2000; i++)
            {
                for(var j = 0; j < 2000; j++)
                {
                    if (grid[i,j])
                    {
                        count++;
                    }
                }
            }
            for (var i = 0; i < 6; i++)
            {
                for(var j = 0; j < 39; j++)
                {
                    if (grid[i,j])
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            return count;
        }

        private bool[,] Fold(bool[,] grid, int num, bool isYfold)
        {
            var delta = Math.Min(num, 2000-num);
            var current = num + delta;
            for (var i = num-delta; i < num; i++)
            {
                for(var j = 0; j < 2000; j++)
                {
                    if (isYfold)
                    {
                        if (grid[current,j])
                        {
                            grid[i,j] = true;
                        }
                    }
                    else
                    {
                        if (grid[j, current])
                        {
                            grid[j, i] = true;
                        }
                    }
                }
                current--;
            }

            for (var i = num; i < 2000; i++)
            {
                for(var j = 0; j < 2000; j++)
                {
                    if (isYfold)
                    {
                        grid[i,j] = false;
                    }
                    else
                    {
                        grid[j,i] = false;
                    }
                }
            }
            return grid;
        }
    }
}