using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.PathFinding;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace aoc2023
{
    public class Day21: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(isTest), Part2(isTest));
        }

        private object Part1(bool isTest = false)
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
            var results = DijkstraTuple.Search(numGrid, new Tuple<int, int>(starting.Item1, starting.Item2)).ToImmutableSortedDictionary();
            var blah = results.Where(x => x.Value <= 6 && x.Value % 2 == 0).Count();
            var blah2 = results.Where(x => x.Value <= 10 && x.Value % 2 == 0).Count();
            return results.Where(x => x.Value <= 64 && (64 - x.Value) % 2 == 0).Count();
        }

        private object Part2(bool isTest = false)
        {
            var grid = ParseInput.ParseFileAsCharGrid(_filePath);
            // long totalSteps = 26501365;
            long totalSteps = 100;
            var vertical = true;
            var horizontal = true;
            var mid = grid.Count / 2;
            var numGrid = new List<List<int>>();
            (int, int) starting = (0, 0);
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
            IDontNeedSleepINeedAnswers(numGrid, mid);
            return MathyMath(numGrid);
            var entryPoints = new List<Dictionary<Tuple<int, int>, int>>()
            {
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(mid, mid)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, mid)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(mid, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(mid, grid[0].Count-1)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(grid.Count-1, mid)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, grid.Count-1)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(grid.Count-1, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(grid.Count-1, grid.Count-1)) },
            };
            var quarterDistanceList = DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, 0));
            var halfDistanceList = DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, mid));

            var maxes = new List<int>().FillEmptyListWithValue(0, entryPoints.Count);
            for (var i = 0; i < grid.Count; i++)
            {
                for (var j = 0; j < grid[0].Count; j++)
                {
                    var tup = new Tuple<int, int>(i, j);
                    for (var k = 0; k < entryPoints.Count; k++)
                    {
                        if (entryPoints[k][tup] < 1000 && entryPoints[k][tup] > maxes[k])
                        {
                            maxes[k] = entryPoints[k][tup];
                        }
                    }
                }
            }
            var maxDistanceInGrid = maxes[1]; // this is 195 or 19
            var plotsPerGrid2 = entryPoints[1].Where(x => x.Value < 1000 && x.Value % 2 == 0).Count(); //15427 or 81 per grid
            var plotsPerGrid = entryPoints[0].Where(x => x.Value < 1000 && x.Value % 2 == 0).Count();

            /*
            var listy = new List<(int,int,int)>();
            for (int  i = 0;  i < 80;  i++)
            {
                //grid,grid
                var f1 = entryPoints[6].Where(x => x.Value < i && x.Value % 2 == 0).Count();
                //mid,mid
                var f2 = entryPoints[0].Where(x => x.Value < i && x.Value % 2 == 0).Count();
                //gridCount,mid
                var f3 = entryPoints[4].Where(x => x.Value < i-mid && x.Value % 2 == 0).Count();
                listy.Add((f1, f2,f3));
            }*/
            var listy = new List<(int, int, int,int)>();
            for (int i = 0; i < 26; i++)
            {
                var f1 = entryPoints[5].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count();
                var f2 = entryPoints[6].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count();
                var f3 = entryPoints[7].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count();
                var f4 = entryPoints[8].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count();
                listy.Add((f1, f2, f3,f4));
            }

            var naiveGridsWeCanReach = ((totalSteps / mid) - 1) / 2;
            var gridsWeCanReach = naiveGridsWeCanReach;
            var rem = totalSteps % mid;
            while (rem < maxDistanceInGrid)
            {
                rem = rem + mid*2;
                gridsWeCanReach -= 1;
            }

            // add in a plotsPerGrid for the initial one
            var max1 = totalSteps - mid - 1;
            var mover = mid * 2 + 1;
            var halfMover = mid + 1;
            var fullGridsStraightLine = 0;
            var remainingSteps = max1;
            while (remainingSteps > maxDistanceInGrid)
            {
                remainingSteps -= mover;
                fullGridsStraightLine++;
            }

            var halfGridSteps = mover;
            var quarterGridSteps = remainingSteps - halfMover;
            var halfGridPlots = halfDistanceList.Where(x => x.Value <= halfGridSteps && (halfGridSteps - x.Value) % 2 == 0).Count();
            var quarterGridPlots = quarterDistanceList.Where(x => x.Value <= quarterGridSteps && (quarterGridSteps - x.Value) % 2 == 0).Count();
            var numberOfFullGrids = (fullGridsStraightLine + 1) * fullGridsStraightLine / 2;
            var quarterAnswer = (numberOfFullGrids * plotsPerGrid) +
                (halfGridPlots * (fullGridsStraightLine + 1)) +
                (quarterGridPlots * (fullGridsStraightLine + 1));
            var answer = quarterAnswer * 4 + plotsPerGrid;

            var quartsBad = quarterGridPlots * (fullGridsStraightLine + 1) * 4;
            var quarts = GetQuarters(numGrid, fullGridsStraightLine, quarterGridSteps);
            var halves = GetHalves(numGrid, mid, fullGridsStraightLine, halfGridSteps);
            var halvesBad = halfGridPlots * (fullGridsStraightLine + 1) * 4;
            var full = numberOfFullGrids * plotsPerGrid * 4;
            var newAnswer = full + halves + quarts; // should be 8665 for test

            var mathy1 = gridsWeCanReach * 4;
            var mathy2 = ((gridsWeCanReach - 1) * gridsWeCanReach) / 2;
            var mathy3 = (mathy2 + gridsWeCanReach) * 4;
            var totalPlots = mathy3 * plotsPerGrid; // 972

            // PrintDistanceGrid(numGrid, grid, starting);
            // look for hidey holes
            var newGrid = new List<List<char>>();
            for (var i = 0; i < grid.Count; i++)
            {
                newGrid.Add(new List<char>());
                newGrid[i].AddRange(grid[i]);
                newGrid[i].AddRange(grid[i]);
                newGrid[i].AddRange(grid[i]);
            }
            var newGrid2 = new List<List<char>>();
            newGrid2.AddRange(newGrid);
            newGrid2.AddRange(newGrid);
            newGrid2.AddRange(newGrid);
            return 0;
        }

        private void TestThings(List<List<int>> numGrid, int mid)
        {
            var entryPoints = new List<Dictionary<Tuple<int, int>, int>>()
            {
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(mid, mid)) },
                // top mid, left mid, right mid, bottom mid
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, mid)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(mid, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(mid, numGrid[0].Count-1)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(numGrid.Count-1, mid)) },
                // corners
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, numGrid.Count-1)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(numGrid.Count-1, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(numGrid.Count-1, numGrid.Count-1)) },
            };
            var listy = new List<(int, int, int, int, int, int, int, int, int)>();
            for (int i = 0; i < 26; i++)
            {
                listy.Add((
                    entryPoints[0].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[1].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[2].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[3].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[4].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[5].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[6].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[7].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[8].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count()));
            }
            var evenStepsNeeded = 24;
            var oddStepsNeeded = 25;
            var evenMax = entryPoints[8].Where(x => x.Value <= evenStepsNeeded && (evenStepsNeeded - x.Value) % 2 == 0).Count();
            var oddMax = entryPoints[8].Where(x => x.Value <= oddStepsNeeded && (oddStepsNeeded - x.Value) % 2 == 0).Count();
            var width = numGrid.Count;
            var steps = 100;
            // get to edge of next grid
            var stepsRemaining = steps - mid + 1;
            var remainingStart = stepsRemaining;
            var doubleMaxes = evenMax + oddMax;
            var doublesCount = remainingStart / (width*2);
            var countSoFar = doublesCount * doubleMaxes;
            stepsRemaining = stepsRemaining - ((width * 2) * doublesCount);
            // at this point we are on an odd (for 100 at least)
            var count = entryPoints[4].Where(x => x.Value <= stepsRemaining && (stepsRemaining - x.Value) % 2 == 0).Count();
            stepsRemaining -= width;
            countSoFar += count;
            count = entryPoints[4].Where(x => x.Value <= stepsRemaining && (stepsRemaining - x.Value) % 2 == 0).Count();
            countSoFar += count;
            stepsRemaining = 0;
            countSoFar = countSoFar * 4;
        }
        private void IDontNeedSleepINeedAnswers(List<List<int>> numGrid, int mid)
        {
            var entryPoints = new List<Dictionary<Tuple<int, int>, int>>()
            {
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(mid, mid)) },
                // top mid, left mid, right mid, bottom mid
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, mid)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(mid, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(mid, numGrid[0].Count-1)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(numGrid.Count-1, mid)) },
                // corners
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, numGrid.Count-1)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(numGrid.Count-1, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(numGrid.Count-1, numGrid.Count-1)) },
            };
            var listy = new List<(int, int, int, int, int, int, int, int, int)>();
            for (int i = 0; i < 406; i++)
            {
                listy.Add((
                    entryPoints[0].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[1].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[2].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[3].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[4].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[5].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[6].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[7].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count(),
                    entryPoints[8].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count()));
            }
            var evenStepsNeeded = 24;
            var oddStepsNeeded = 25;
            var evenMax = entryPoints[8].Where(x => x.Value <= evenStepsNeeded && (evenStepsNeeded - x.Value) % 2 == 0).Count();
            var oddMax = entryPoints[8].Where(x => x.Value <= oddStepsNeeded && (oddStepsNeeded - x.Value) % 2 == 0).Count();
            var width = numGrid.Count;
            var counter = HandleCentralCross(listy, entryPoints);
            counter += HandleCorners(entryPoints);
        }

        private int HandleCentralCross(
            List<(int, int, int, int, int, int, int, int, int)> listy,
            List<Dictionary<Tuple<int, int>, int>> entryPoints)
        {
            var counter = 0;

            var quarterCounter = 4 * listy[24].Item2;
            quarterCounter += 3 * listy[25].Item2;
            quarterCounter += listy[15].Item2;
            quarterCounter += listy[2].Item2;
            counter += quarterCounter * 4;
            counter += listy[24].Item1; // this is for the dead centre item
            return counter;
        }

        private int HandleCorners(
            List<Dictionary<Tuple<int, int>, int>> entryPoints)
        {
            var listy = new List<List<int>>();
            for (int i = 0; i < 26; i++)
            {
                listy.Add(new List<int>());
                for (var j = 0; j < 9; j++)
                {
                    listy[i].Add(entryPoints[j].Where(x => x.Value <= i && (i - x.Value) % 2 == 0).Count());
                }
            }
            var counter = 0;
            counter += listy[25][8] * 6;
            counter += listy[24][8] * 9;
            counter = counter * 4;
            for (var i = 5; i < 9; i++)
            {
                counter += listy[21][i] * 6;
                counter += listy[8][i] * 7;
            }
            return counter;
        }

        private long MathyMath(List<List<int>> numGrid)
        {
            long n = 202300;
            long count = 0;
            var mid = numGrid.Count / 2;
            var entryPoints = new List<Dictionary<Tuple<int, int>, int>>()
            {
                // corners
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(0, numGrid.Count-1)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(numGrid.Count-1, 0)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(numGrid.Count-1, numGrid.Count-1)) },
                { DijkstraTuple.Search(numGrid, new Tuple<int, int>(mid, mid)) },
            };

            var evenCounts = new List<int>();
            var oddCounts = new List<int>();
            var evenCornerCounts = new List<int>();
            var oddCornerCounts = new List<int>();
            for (var i = 0; i < 4; i++)
            {
                evenCounts.Add(entryPoints[i].Where(x => x.Value < 600 && x.Value % 2 == 0).Count());
                oddCounts.Add(entryPoints[i].Where(x => x.Value < 600 && x.Value % 2 == 1).Count());
                evenCornerCounts.Add(entryPoints[i].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 0).Count());
                oddCornerCounts.Add(entryPoints[i].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 1).Count());
            }
            var evenCount = entryPoints[0].Where(x => x.Value < 600 && x.Value  % 2 == 0).Count();
            var oddCount = entryPoints[0].Where(x => x.Value < 600 && x.Value % 2 == 1).Count();
            var evenCornerCount = entryPoints[4].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 0).Count();
            var oddCornerCount = entryPoints[4].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 1).Count();
            /*
            var evenCornerCount = entryPoints[0].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 0).Count();
            var oddCornerCount = entryPoints[0].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 1).Count();
            var evenCornerCount2 = entryPoints[1].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 0).Count();
            var oddCornerCount2 = entryPoints[1].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 1).Count();
            var evenCornerCount3 = entryPoints[2].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 0).Count();
            var oddCornerCount3 = entryPoints[2].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 1).Count();
            var evenCornerCount4 = entryPoints[3].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 0).Count();
            var oddCornerCount4 = entryPoints[3].Where(x => x.Value < 600 && x.Value > 65 && x.Value % 2 == 1).Count();
            */

            var oddity = (oddCornerCounts[0] + oddCornerCounts[1] + oddCornerCounts[2] + oddCornerCounts[3]) / 4;
            var evenity = (evenCornerCounts[0] + evenCornerCounts[1] + evenCornerCounts[2] + evenCornerCounts[3]) / 4;


            count += (n + 1) * (n + 1) * oddCount;
            count += n * n * evenCount;
            count -= (n + 1) * oddCornerCount;
            count += n * evenCornerCount;

            return count;
        }

        private int GetQuarters(List<List<int>> grid, int wholeGrids, long stepsRemaining)
        {
            var entryPoints = new List<Dictionary<Tuple<int, int>, int>>()
            {
                { DijkstraTuple.Search(grid, new Tuple<int, int>(0, 0)) },
                { DijkstraTuple.Search(grid, new Tuple<int, int>(0, grid.Count-1)) },
                { DijkstraTuple.Search(grid, new Tuple<int, int>(grid.Count-1, 0)) },
                { DijkstraTuple.Search(grid, new Tuple<int, int>(grid.Count-1, grid.Count-1)) },
            };

            var answers = new List<int>();
            for (var i = 0; i < entryPoints.Count; i++)
            {
                answers.Add(entryPoints[i].Where(x => x.Value <= stepsRemaining && x.Value % 2 == 0).Count());
            }
            var fullAnswer = answers.Sum() * (wholeGrids + 1);

            return fullAnswer;
        }

        private int GetHalves(List<List<int>> grid, int mid, int wholeGrids, long stepsRemaining)
        {
            var entryPoints = new List<Dictionary<Tuple<int, int>, int>>()
            {
                { DijkstraTuple.Search(grid, new Tuple<int, int>(0, mid)) },
                { DijkstraTuple.Search(grid, new Tuple<int, int>(mid, 0)) },
                { DijkstraTuple.Search(grid, new Tuple<int, int>(mid, grid[0].Count-1)) },
                { DijkstraTuple.Search(grid, new Tuple<int, int>(grid.Count-1, mid)) },
            };

            var answers = new List<int>();
            for (var i = 0; i < entryPoints.Count; i++)
            {
                answers.Add(entryPoints[i].Where(x => x.Value <= stepsRemaining && x.Value % 2 == 0).Count());
            }
            var easyOnes = answers.Sum();
            var harder = Math.Max(answers[0], answers[1]) +
                Math.Max(answers[0], answers[2]) +
                Math.Max(answers[3], answers[1]) +
                Math.Max(answers[3], answers[2]);
            var harder2 = harder * (wholeGrids);

            return easyOnes + harder2;
        }

        private void PrintDistanceGrid(List<List<int>> numGrid, List<List<char>> grid, (int,int) starting)
        {
            var results = DijkstraTuple.Search(numGrid, new Tuple<int, int>(starting.Item1, starting.Item2)).ToImmutableSortedDictionary();

            var distanceGrid = new List<List<string>>();
            for (var i = 0; i < grid.Count; i++)
            {
                distanceGrid.Add(new List<string>());
                for (var j = 0; j < grid[i].Count; j++)
                {
                    var tuple = new Tuple<int, int>(i, j);
                    var value = results[tuple];
                    var valueStr = "[";
                    if (value > 1000)
                    {
                        valueStr += "###" + "]";
                    }
                    else
                    {
                        valueStr += results[tuple].ToString().PadLeft(3, '0') + "]";
                    }
                    distanceGrid[i].Add(valueStr);
                }
            }
            // look for hidey holes
            GridHelper.PrintGrid(distanceGrid);
        }
    }
}