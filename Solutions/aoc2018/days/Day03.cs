using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2018
{
    public class Day03 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };

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
            var grid = GridHelper.GenerateSquareGrid(1000, 0);
            var gridObject = new GridObject<int>(grid);

            foreach (var line in lines)
            {
                var nums = line.GetNumbersFromString();
                var x = nums[1];
                var y = nums[2];
                var width = nums[3];
                var height = nums[4];
                for (var w = 0; w < width; w++)
                {
                    for (var h = 0; h < height; h++)
                    {
                        var loc = new GridLocation<int>(x + w, y + h);
                        gridObject.Set(loc, gridObject.Get(loc) + 1);
                    }
                }
            }
            return gridObject.GetAllLocationsWhereValue(x => x > 1).Count();
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var grid = GridHelper.GenerateSquareGrid(1000, 0);
            var gridObject = new GridObject<int>(grid);

            foreach (var line in lines)
            {
                var nums = line.GetNumbersFromString();
                var x = nums[1];
                var y = nums[2];
                var width = nums[3];
                var height = nums[4];
                for (var w = 0; w < width; w++)
                {
                    for (var h = 0; h < height; h++)
                    {
                        var loc = new GridLocation<int>(x + w, y + h);
                        gridObject.Set(loc, gridObject.Get(loc) + 1);
                    }
                }
            }

            foreach (var line in lines)
            {
                if (DoThing(gridObject, line))
                {
                    var nums = line.GetNumbersFromString();
                    return nums[0];
                }
            }
            return gridObject.GetAllLocationsWhereValue(x => x > 1).Count();
        }

        private bool DoThing(GridObject<int> gridObject, string line)
        {
            var nums = line.GetNumbersFromString();
            var x = nums[1];
            var y = nums[2];
            var width = nums[3];
            var height = nums[4];
            for (var w = 0; w < width; w++)
            {
                for (var h = 0; h < height; h++)
                {
                    var loc = new GridLocation<int>(x + w, y + h);
                    if (gridObject.Get(loc) > 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}