using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day03: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
			var numbers = input.Longs;
            var nodes = input.Graph;
            var grid = input.CharGrid;
            long total = 1000000;
			long count = 0;

            var currentLayer = 1;
            var baseFactor = 2;
            var lastHigh = 1;
            var high = 1 + 8 * currentLayer;

            while (high <= numbers[0])
            {
                currentLayer++;
                lastHigh = high;
                var mathy = 2 * currentLayer + 1;
                high = (int)Math.Pow(mathy, 2);
            }

            // currentLayer--;
            var factor = currentLayer * 2;

            // corners, [3] is bottom right aka the high
            var corners = new List<long>() { high - factor * 3, high - factor * 2, high - factor, high };
            // mids, [3] is bottom, [2] left, [1] top, [0] right
            var mids = corners.Select(x => x-currentLayer).ToList();

            var best = long.MaxValue;

            foreach (var m in mids)
            {
                best = Math.Min(best, (long)Math.Abs(m - numbers[0]));
            }

            return GridHelper.TaxicabDistance((0, 0), ((int)currentLayer, (int)best));


            /*
            var mid1 = lastHigh + layer;
            var mid2 = 

            double power = 0;
            var index = 7;

            while (power < numbers[0])
            {
                power = Math.Pow(index, 2);
                index += 2;
            }
            var nextPower = power;
            var actualPower = Math.Pow(index - 4, 2);*/
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var grid = GridHelper.GenerateSquareGrid(500);
            var gridObject = new GridObject<int>(grid);
            var currentx = 251;
            var currenty = 250;
            gridObject.Set(250, 250, 1);
            // direction heading 0 up, 1 left, 2 down, 3 right
            var spiralState = 0;
            var current = 1;

            var goal = input.Longs[0];

            while (current < goal)
            {
                var currentLocation = new GridLocation<int>(currentx, currenty);
                current = gridObject.GetAllNeighbours(currentLocation).Sum(node => gridObject.Get(node));
                gridObject.Set(currentLocation,current);

                if (spiralState == 0)
                {
                    if (gridObject.Get(currentx - 1, currenty) == 0)
                    {
                        spiralState++;
                        currentx--;
                    }
                    else
                    {
                        currenty++;
                    }
                }
                else if (spiralState == 1)
                {
                    if (gridObject.Get(currentx,currenty-1) == 0)
                    {
                        spiralState++;
                        currenty--;
                    }
                    else
                    {
                        currentx--;
                    }
                }
                else if (spiralState == 2)
                {
                    if (gridObject.Get(currentx+1, currenty) == 0)
                    {
                        spiralState++;
                        currentx++;
                    }
                    else
                    {
                        currenty--;
                    }
                }
                else if (spiralState == 3)
                {
                    if (gridObject.Get(currentx, currenty + 1) == 0)
                    {
                        spiralState = 0;
                        currenty++;
                    }
                    else
                    {
                        currentx++;
                    }
                }
                else
                {
                    throw new Exception("State is wrong");
                }
            }

            return current;
        }
    }
}