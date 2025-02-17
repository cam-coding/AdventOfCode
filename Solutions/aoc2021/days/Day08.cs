using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2021
{
    public class Day08 : ISolver
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
            var counter = 0;
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                counter = counter + parts[1].Split(' ').Count(item => item.Length == 2 || item.Length == 3 || item.Length == 4 || item.Length == 7);
            }
            return counter;
        }

        private object Part2()
        {
            var bigOleTotal = 0;
            var solved = new string[10];
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);

            foreach (var line in lines)
            {
                var parts = line.Split('|');
                var inputs = parts[0].Split(' ').ToList();
                var codes = parts[1].Split(' ').ToList().GetAllExceptFirstItem();
                solved[1] = inputs.Where(x => x.Length == 2).First();
                inputs.Remove(solved[1]);
                solved[4] = inputs.Where(x => x.Length == 4).First();
                inputs.Remove(solved[4]);
                solved[7] = inputs.Where(x => x.Length == 3).First();
                inputs.Remove(solved[7]);
                solved[8] = inputs.Where(x => x.Length == 7).First();
                inputs.Remove(solved[8]);
                var partFour = StringExtensions.RemoveLettersFromString(solved[4], solved[1]);
                solved[0] = inputs.Where(x => x.Length == 6 &&
                    !StringExtensions.ContainsAllLetters(x, partFour)).First();
                inputs.Remove(solved[0]);
                solved[9] = inputs.Where(x => x.Length == 6 &&
                    StringExtensions.ContainsAllLetters(x, solved[1])).First();
                inputs.Remove(solved[9]);
                solved[6] = inputs.Where(x => x.Length == 6).First();
                inputs.Remove(solved[6]);
                solved[3] = inputs.Where(x => x.Length == 5 &&
                    StringExtensions.ContainsAllLetters(x, solved[1])).First();
                inputs.Remove(solved[3]);
                solved[5] = inputs.Where(x => x.Length == 5 &&
                    StringExtensions.ContainsAllLetters(x, partFour)).First();
                inputs.Remove(solved[5]);
                solved[2] = inputs.Where(x => x.Length == 5).First();

                var str = string.Empty;
                var solvedList = solved.ToList();
                foreach (var code in codes)
                {
                    str = str + solvedList.IndexOf(solvedList.First(x => x.Length == code.Length && StringExtensions.ContainsAllLetters(x, code)));
                }
                bigOleTotal = bigOleTotal + Convert.ToInt32(str);
            }
            return bigOleTotal;
        }
    }
}