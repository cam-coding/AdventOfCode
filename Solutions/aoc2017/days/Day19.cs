using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day19 : ISolver
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
            var grid = input.GridChar;
            var count = 0;
            var currentLocation = new GridLocation<int>(0, 0);
            var currentDirection = Directions.Down;

            for (var j = 0; j < grid.Width; j++)
            {
                if (grid.Get(j, 0) != ' ')
                {
                    currentLocation = new GridLocation<int>(j, 0);
                    break;
                }
            }

            var path = string.Empty;
            while (grid.WithinGrid(currentLocation) && grid.Get(currentLocation) != ' ')
            {
                var val = grid.Get(currentLocation);
                if (char.IsLetter(val))
                {
                    path += val;
                }
                else if (val == '+')
                {
                    if (!grid.WithinGrid(currentLocation + currentDirection) || grid.Get(currentLocation + currentDirection) == ' ')
                    {
                        var potentialDirections = Directions.OrthogonalDirections.Clone();
                        potentialDirections.Remove(currentDirection);
                        potentialDirections.Remove(currentDirection * -1);

                        foreach (var dir in potentialDirections)
                        {
                            if (grid.WithinGrid(currentLocation + dir) && grid.Get(currentLocation + dir) != ' ')
                            {
                                currentDirection = dir;
                                break;
                            }
                        }
                    }
                }
                currentLocation += currentDirection;
            }

            return path;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = input.GridChar;
            var count = 0;
            var currentLocation = new GridLocation<int>(0, 0);
            var currentDirection = Directions.Down;

            for (var j = 0; j < grid.Width; j++)
            {
                if (grid.Get(j, 0) != ' ')
                {
                    currentLocation = new GridLocation<int>(j, 0);
                    break;
                }
            }

            var path = string.Empty;
            while (grid.WithinGrid(currentLocation) && grid.Get(currentLocation) != ' ')
            {
                var val = grid.Get(currentLocation);
                if (char.IsLetter(val))
                {
                    path += val;
                }
                else if (val == '+')
                {
                    if (!grid.WithinGrid(currentLocation + currentDirection) || grid.Get(currentLocation + currentDirection) == ' ')
                    {
                        var potentialDirections = Directions.OrthogonalDirections.Clone();
                        potentialDirections.Remove(currentDirection);
                        potentialDirections.Remove(currentDirection * -1);

                        foreach (var dir in potentialDirections)
                        {
                            if (grid.WithinGrid(currentLocation + dir) && grid.Get(currentLocation + dir) != ' ')
                            {
                                currentDirection = dir;
                                break;
                            }
                        }
                    }
                }
                currentLocation += currentDirection;
                count++;
            }

            return count;
        }
    }
}