using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day24: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
			var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var targetGroupCount = numbers.Sum() / 3;
            long bestQE = long.MaxValue;

            for (var j = 2; j <= numbers.Count / 3; j++)
            {
                var potentialGroups = numbers.GetKCombinations(j);
                var listy = new List<IEnumerable<int>>();

                foreach (var group1 in potentialGroups)
                {
                    long qe = 1;
                    foreach (var item in group1)
                    {
                        qe *= item;
                    }
                    if (qe < bestQE && group1.Sum() == targetGroupCount)
                    {
                        var remaining = numbers.Except(group1).ToList();
                        for (var i = j; i < remaining.Count / 2; i++)
                        {
                            var stopSearching = false;
                            var potentialGroup2 = remaining.GetKCombinations(i);
                            foreach (var group2 in potentialGroup2)
                            {
                                if (group2.Sum() == targetGroupCount)
                                {
                                    bestQE = Math.Min(bestQE, qe);
                                    stopSearching = true;
                                    break;
                                }
                            }
                            if (stopSearching)
                            {
                                break;
                            }
                        }
                    }
                }

                if (bestQE != long.MaxValue)
                {
                    return bestQE;
                }

            }
            return bestQE;
        }
        
        private object Part2()
        {
            var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var targetGroupWeight = numbers.Sum() / 4;
            long bestQE = long.MaxValue;

            for (var group1Size = 1; group1Size <= numbers.Count / 4; group1Size++)
            {
                var potentialGroups = numbers.GetKCombinations(group1Size);

                foreach (var group1 in potentialGroups)
                {
                    long qe = 1;
                    foreach (var item in group1)
                    {
                        qe *= item;
                    }
                    if (qe < bestQE && group1.Sum() == targetGroupWeight)
                    {
                        var remaining = numbers.Except(group1).ToList();
                        var maxGroupSize = remaining.Count - (group1Size*2);
                        for (var i = group1Size; i <= maxGroupSize; i++)
                        {
                            var stopSearching = false;
                            var potentialGroup2 = remaining.GetKCombinations(i);
                            foreach (var group2 in potentialGroup2)
                            {
                                if (group2.Sum() == targetGroupWeight)
                                {
                                    var remaining34 = remaining.Except(group2).ToList();
                                    for (var k = group1Size; k <= maxGroupSize; k++)
                                    {
                                        var potentialGroup3 = remaining34.GetKCombinations(k);
                                        foreach (var group3 in potentialGroup3)
                                        {
                                            if (group3.Sum() == targetGroupWeight)
                                            {
                                                bestQE = Math.Min(bestQE, qe);
                                                stopSearching = true;
                                                break;
                                            }
                                        }
                                        if (stopSearching)
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (stopSearching)
                                {
                                    break;
                                }
                            }
                            if (stopSearching)
                            {
                                break;
                            }
                        }
                    }
                }

                if (bestQE != long.MaxValue)
                {
                    return bestQE;
                }

            }
            return bestQE;
        }
    }
}