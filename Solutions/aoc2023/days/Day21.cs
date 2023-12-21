using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;
using AdventLibrary.PathFinding;

namespace aoc2023
{
    public class Day21: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            
            var numGrid = new List<List<int>>();
            (int, int) starting = (0,0);
            for (var i = 0; i < grid.Count; i++)
            {
                numGrid.Add(new List<int>());
                for (var j = 0; j < grid[i].Count; j++)
                {
                    if (grid[i][j] == '.')
                    {
                        numGrid[i].Add(1);
                    }
                    else if (grid[i][j] == '#')
                    {
                        numGrid[i].Add(10000);
                    }
                    else if (grid[i][j] == 'S')
                    {
                        starting = (i, j);
                        numGrid[i].Add(1);
                    }
                }
            }
            var results = Dijkstra.Search(numGrid, new Tuple<int, int>(starting.Item1, starting.Item2)).ToImmutableSortedDictionary();
            var blah1 = results.Where(x => x.Value <= 1).Count();
            var blah2 = results.Where(x => x.Value <= 2 && x.Value%2 == 0).Count();
            var blah3 = results.Where(x => x.Value <= 3 && x.Value % 2 == 0).Count();
            return results.Where(x => x.Value <= 64 && x.Value % 2 == 0).Count();
        }
        
        private object Part2()
        {
            return 0;
        }
    }
}