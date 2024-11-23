using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2023
{
    public class Day15: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '>', '<', '+', '\t' };
        private char[] delimiterChars2 = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            var tokens = lines[0].Split(delimiterChars).ToList().OnlyRealStrings(delimiterChars);
            var total = 0;
            foreach (var item in tokens)
            {
                var current = 0;
                for (var i = 0; i < item.Length; i++)
                {
                    if (item[i] != '\n')
                    {
                        var intCode = (int)item[i];
                        current += intCode;
                        current *= 17;
                        current = current % 256;
                    }
                }
                total += current;
            }
            return total;
        }
        
        private object Part2()
        {
            var boxes = new List<List<string>>();
            var boxes2 = new List<Dictionary<string, int>>();
            var labelLookup = new Dictionary<string, int>();
            for (var j = 0; j < 256; j++)
            {
                boxes.Add(new List<string>());
                boxes2.Add(new Dictionary<string, int>());
            }
            var lines = ParseInput.GetLinesFromFile(_filePath);

            var tokens = lines[0].Split(delimiterChars).ToList().OnlyRealStrings(delimiterChars);
            var total = 0;
            foreach (var item in tokens)
            {
                var toks = item.Split(delimiterChars2).ToList().OnlyRealStrings(delimiterChars2);
                var label = toks[0];
                var hash = 0;
                for (var i = 0; i < toks[0].Length; i++)
                {
                    if (toks[0][i] != '\n')
                    {
                        var intCode = (int)toks[0][i];
                        hash += intCode;
                        hash *= 17;
                        hash = hash % 256;
                    }
                }
                
                if (toks.Count == 2)
                {
                    var focal = int.Parse(toks[1]);
                    if (boxes2[hash].ContainsKey(label))
                    {
                        boxes2[hash][label] = focal;
                    }
                    else
                    {
                        boxes[hash].Add(label);
                        boxes2[hash].Add(label, focal);
                    }

                }
                else
                {
                    if (boxes2[hash].ContainsKey(label))
                    {
                        var index = boxes[hash].IndexOf(label);

                        if (index != -1)
                        {
                            boxes[hash].RemoveAt(index);
                            labelLookup.Remove(label);
                            boxes2[hash].Remove(label);
                        }
                    }
                }
            }
            var bigTotal = 0;
            for (var j = 0; j < 256; j++)
            {
                var county = 1;
                foreach (var item in boxes[j])
                {
                    var item1 = 1 + j;
                    var item2 = county;
                    var item3 = boxes2[j][item];
                    bigTotal += item1 * item2 * item3;

                    county++;
                }
            }
            return bigTotal;
        }
    }
}