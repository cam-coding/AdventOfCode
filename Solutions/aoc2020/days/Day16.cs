using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2020
{
    public class Day16: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1(bool isTest = false)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var index = 0;
            var listy = new Dictionary<int, List<int>>();
            List<int> yourTicket;
            while (!string.IsNullOrEmpty(lines[index]))
            {
                var nums = lines[index].GetIntsFromString();
                listy.Add(index, nums);
                index++;
            }

            index += 2;
            yourTicket = lines[index].GetIntsFromString();
            index += 3;

            var total = 0;
            while (index < lines.Count)
            {
                var nums = lines[index].GetIntsFromString();
                foreach (var num in nums)
                {
                    if (!listy.Any(x => InRange(num, x.Value)))
                    {
                        total += num;
                    }
                }
                index++;
            }
            return total;
        }

        private bool InRange(int num, List<int> rangeNums)
        {
            return (rangeNums[0] <= num && num <= rangeNums[1])
                || (rangeNums[2] <= num && num <= rangeNums[3]);
        }

        private object Part2(bool isTest = false)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var index = 0;
            var lookup = new Dictionary<int, List<int>>();
            List<int> yourTicket;
            while (!string.IsNullOrEmpty(lines[index]))
            {
                var nums = lines[index].GetIntsFromString();
                lookup.Add(index, nums);
                index++;
            }

            index += 2;
            yourTicket = lines[index].GetIntsFromString();
            index += 3;

            var validTickets = new List<List<int>>();
            var dictKeys = lookup.Keys.ToList();
            var posistionToPossible = new List<List<int>>();
            for (var i = 0; i < yourTicket.Count; i++)
            {
                posistionToPossible.Add(dictKeys);
            }
            while (index < lines.Count)
            {
                var nums = lines[index].GetIntsFromString();
                var valid = nums.All(currentLineNum => lookup.Any(x => InRange(currentLineNum, x.Value)));

                if (valid)
                {
                    validTickets.Add(nums);
                    for (var i = 0; i < nums.Count; i++)
                    {
                        var num = nums[i];
                        var validFields = lookup.Where(x => InRange(num, x.Value));
                        var keys = validFields.Select(x => x.Key).ToList();

                        posistionToPossible[i] = posistionToPossible[i].Intersect(keys).ToList();
                    }
                }
                index++;
            }
            while (posistionToPossible.Any(x => x.Count > 1))
            {
                for (var i =0; i < posistionToPossible.Count; i++)
                {
                    if (posistionToPossible[i].Count == 1)
                    {
                        var special = posistionToPossible[i][0];
                        for (var j = 0; j < posistionToPossible.Count; j++)
                        {
                            if (i != j)
                            {
                                posistionToPossible[j].Remove(special);
                            }
                        }
                    }
                }
            }
            var posToKey = new Dictionary<int, int>();
            for (var j = 0; j < posistionToPossible.Count; j++)
            {
                posToKey.Add(j, posistionToPossible[j][0]);
            }
            if (!isTest)
            {
                long total = 1;
                for (var j = 0; j < posToKey.Count; j++)
                {
                    if (posToKey[j] < 6)
                    {
                        total *= yourTicket[j];
                    }
                }
                return total;
            }
            return 0;
        }
    }
}