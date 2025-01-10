using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using Microsoft.Z3;

namespace aoc2017
{
    public class Day21: ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        Dictionary<int, Dictionary<GridObject<char>, GridObject<char>>> _rules;
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
            if (isTest)
                return 1;
            var input = new InputObjectCollection(_filePath);
            _rules = new Dictionary<int, Dictionary<GridObject<char>, GridObject<char>>>();
            var lines = input.Lines;

            var startingGridText = ".#.\r\n..#\r\n###\r\n";
            var inputParser = InputParserFactory.CreateFromText(startingGridText);
            var startingGrid = inputParser.GetLinesAsGrid<char>();
            var allGrids = new List<GridObject<char>>() { startingGrid };

			foreach (var line in lines)
			{
                var tokens = StringParsing.GetRealTokens(line, [' ', '=', '>']);
                var ruleInput = new GridObject<char>(GetGrid(tokens[0]));
                var ruleOutput = new GridObject<char>(GetGrid(tokens[1]));
                var key = ruleInput.Width;
                if (_rules.ContainsKey(key))
                {
                    _rules[key].Add(ruleInput, ruleOutput);
                }
                else
                {
                    _rules[key] = new Dictionary<GridObject<char>, GridObject<char>>() { { ruleInput, ruleOutput } };
                }
            }

            for (var i = 0; i < 18; i++)
            {
                var newGrids = new List<GridObject<char>>();
                foreach (var grid in allGrids)
                {
                    var grids = new List<GridObject<char>>() { grid };
                    if (grid.Width > 2 && grid.Width % 2 == 0)
                    {
                        grids = SubDivideGrid(2, grid);
                    }
                    else if (grid.Width > 3 && grid.Width % 3 == 0)
                    {
                        grids = SubDivideGrid(3, grid);
                    }
                    foreach (var griddy in grids)
                    {
                        newGrids.Add(GetNewGridFromRules(griddy));
                    }
                }
                if (newGrids.Count > 1)
                {
                    allGrids = new List<GridObject<char>>() { GetMegaGrid(newGrids) };
                    var count2 = allGrids[0].GetAllLocationsWhere(c => c == '#').Count;
                }
                else
                {
                    allGrids = newGrids;
                }
            }
            var count = 0;
            return allGrids[0].GetAllLocationsWhere(c => c == '#').Count;
        }

        private GridObject<char> GetMegaGrid(List<GridObject<char>> grids)
        {
            var megaGrid = new List<List<char>>();

            // aka 9 4x4's morph into one large 12x12
            var factor = Math.Sqrt(grids.Count);
            var dimensions = Math.Sqrt(grids.Count) * grids[0].Width;
            var widthMax = grids[0].Width;
            var currentWidth = 0;

            var currentRows = new List<List<char>>();

            for (var z = 0; z < widthMax; z++)
            {
                currentRows.Add(new List<char>());
            }
            for (var i = 0; i < grids.Count; i++)
            {
                if (currentWidth == factor)
                {
                    megaGrid.AddRange(currentRows);
                    currentRows = new List<List<char>>();
                    for (var z = 0; z < widthMax; z++)
                    {
                        currentRows.Add(new List<char>());
                    }
                    currentWidth = 0;
                }
                for (var y = 0; y < grids[i].Height; y++)
                {
                    currentRows[y].AddRange(grids[i].Grid[y]);
                }
                currentWidth++;
            }

            megaGrid.AddRange(currentRows);
            return new GridObject<char>(megaGrid);
        }

        private List<List<char>> GetGrid(string input)
        {
            var tempGrid = new List<List<char>>();
            var currentRow = new List<char>();
            foreach (var c in input)
            {
                if (c == '/')
                {
                    tempGrid.Add(currentRow);
                    currentRow = new List<char>();
                }
                else
                {
                    currentRow.Add(c);
                }
            }
            tempGrid.Add(currentRow);
            return tempGrid;
        }

        private GridObject<char> GetNewGridFromRules(GridObject<char> grid)
        {
            var flipHor = grid.Clone();
            GridHelper.FlipAboutHorizontal(flipHor.Grid);
            var flipVert = grid.Clone();
            GridHelper.FlipAboutVertical(flipVert.Grid);
            var dict = _rules[grid.Width];

            foreach (var g in new List<GridObject<char>>() { grid, flipHor, flipVert })
            {
                for (var i = 0; i < 4; i++)
                {
                    foreach (var item in dict)
                    {
                        if (item.Key.Equals(g))
                        {
                            return item.Value;
                        }
                    }
                    g.Grid = GridHelper.RotateGridRight(g.Grid);
                }
            }

            return null;
        }

        private List<GridObject<char>> SubDivideGrid(int special, GridObject<char> grid)
        {
            var newGrids = new List<GridObject<char>>();
            for (var y = 0; y < grid.Height/special; y++)
            {
                for (var x = 0; x < grid.Width / special; x++)
                {
                    newGrids.Add(grid.GetSubGrid(
                        new GridLocation<int>(x*special, y*special),
                        new GridLocation<int>((x+1) * special - 1, (y + 1) * special - 1)));
                }
            }
            return newGrids;
        }

        private object Part2(bool isTest = false)
        {
            return 0;
        }
    }
}