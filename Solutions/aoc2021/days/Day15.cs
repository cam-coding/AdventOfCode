using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day15: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
        private string _filePath;
        private List<List<int>> _grid;
        private Dictionary<Tuple<int,int>, List<Tuple<int,int>>> graph;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var grid = AdventLibrary.ParseInput.ParseFileAsGrid(_filePath);
            var blah = grid.Count/2;
            Tuple<int,int> toople = null;
            var weight = int.MaxValue;
            var myDict = new Dictionary<Tuple<int,int>, int>();
            for (var k = 0; k < 2; k++)
            {
                for (var m = 0; m < 2; m++)
                {
                    var currentWeight = 0;
                    for (var i = blah*k; i < blah*(k+1); i++)
                    {
                        for (var j = blah*m; j < blah*(m+1); j++)
                        {
                            currentWeight = currentWeight + grid[i][j];
                        }
                    }
                    myDict.Add(new Tuple<int, int>(k, m), currentWeight);
                    if (currentWeight < weight)
                    {
                        weight = currentWeight;
                        toople = new Tuple<int, int>(k, m);
                    }
                }
            }
            var graph = GetGraphFromGrid(grid);
            var total = 1000000;
			var counter = 0;
            return 0;
        }
        
        private object Part2()
        {
            return 0;
        }

        private Dictionary<Tuple<int,int>, List<Tuple<int,int>>> GetGraphFromGrid(List<List<int>> grid)
        {
            var dict = new Dictionary<Tuple<int,int>, List<Tuple<int,int>>>();
            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[0].Count; j++)
                {
                    var toople = new Tuple<int,int>(i, j);
                    dict.Add(toople, AdventLibrary.GridHelper.GetAdjacent(grid, i, j));
                }
            }
            return dict;
        }
    }
}