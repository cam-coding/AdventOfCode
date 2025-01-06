using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day16: ISolver
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

            var myChar = 'a';
            var max = isTest ? 5 : 16;
            var programs = new List<char>();

            for (var i = 0; i < max; i++)
            {
                programs.Add(myChar);
                myChar++;
            }

            var text = input.Text;

            foreach (var instruction in StringParsing.GetRealTokens(text, [',']))
            {
                var nums = StringParsing.GetNumbersFromString(instruction);
                if (instruction[0] == 's')
                {
                    var num = nums[0];
                    var index = programs.Count - num;
                    var sublist = programs.SubList(index);
                    programs.RemoveEverythingAfter(index-1);
                    programs.InsertRange(0, sublist);
                }
                else if (instruction[0] == 'x')
                {
                    var index1 = nums[0];
                    var index2 = nums[1];
                    programs.SwapItemsAtIndexes(index1, index2);
                }
                else if (instruction[0] == 'p')
                {
                    var index1 = programs.IndexOf(instruction[1]);
                    var index2 = programs.IndexOf(instruction[3]);
                    programs.SwapItemsAtIndexes(index1, index2);
                }
            }
            return programs.Stringify("");
        }

        private object Part2(bool isTest = false)
        {
            if (isTest)
            {
                return 0;
            }
            var input = new InputObjectCollection(_filePath);

            var myChar = 'a';
            var max = isTest ? 5 : 16;
            var programs = new List<char>();

            for (var i = 0; i < max; i++)
            {
                programs.Add(myChar);
                myChar++;
            }

            var startingOrder = programs.Clone();

            var text = input.Text;

            var cycleDict = new Dictionary<int, int>();
            var count = 0;
            var savedList = new List<char>();
            var seqNum = 0;

            while (cycleDict.Count < 16)
            {
                foreach (var instruction in StringParsing.GetRealTokens(text, [',']))
                {
                    var nums = StringParsing.GetNumbersFromString(instruction);
                    if (instruction[0] == 's')
                    {
                        var num = nums[0];
                        var index = programs.Count - num;
                        var sublist = programs.SubList(index);
                        programs.RemoveEverythingAfter(index - 1);
                        programs.InsertRange(0, sublist);
                    }
                    else if (instruction[0] == 'x')
                    {
                        var index1 = nums[0];
                        var index2 = nums[1];
                        programs.SwapItemsAtIndexes(index1, index2);
                    }
                    else if (instruction[0] == 'p')
                    {
                        var index1 = programs.IndexOf(instruction[1]);
                        var index2 = programs.IndexOf(instruction[3]);
                        programs.SwapItemsAtIndexes(index1, index2);
                    }
                }
                count++;
                if (count == 1)
                {
                    savedList = programs.Clone();
                }
                foreach (var letter in startingOrder)
                {
                    var origIndex = startingOrder.IndexOf(letter);
                    var currentIndex = programs.IndexOf(letter);
                    if (origIndex == currentIndex)
                    {
                        if (!cycleDict.ContainsKey(origIndex))
                        {
                            cycleDict.Add(origIndex, count);
                        }
                    }
                }
                if (seqNum == 0 && programs.SequenceEqual(startingOrder))
                {
                    seqNum = count;
                }
            }

            var myDict = new Dictionary<int, int>();
            foreach (var letter in startingOrder)
            {
                myDict.Add(startingOrder.IndexOf(letter), savedList.IndexOf(letter));
            }

            var LCM = MathHelper.LCM(cycleDict.Values.Distinct());
            var real = 1000000000 % seqNum;

            programs = startingOrder.Clone();
            for (var i = 0; i < real; i++)
            {
                foreach (var instruction in StringParsing.GetRealTokens(text, [',']))
                {
                    var nums = StringParsing.GetNumbersFromString(instruction);
                    if (instruction[0] == 's')
                    {
                        var num = nums[0];
                        var index = programs.Count - num;
                        var sublist = programs.SubList(index);
                        programs.RemoveEverythingAfter(index - 1);
                        programs.InsertRange(0, sublist);
                    }
                    else if (instruction[0] == 'x')
                    {
                        var index1 = nums[0];
                        var index2 = nums[1];
                        programs.SwapItemsAtIndexes(index1, index2);
                    }
                    else if (instruction[0] == 'p')
                    {
                        var index1 = programs.IndexOf(instruction[1]);
                        var index2 = programs.IndexOf(instruction[3]);
                        programs.SwapItemsAtIndexes(index1, index2);
                    }
                }
            }
            return programs.Stringify("");
        }
    }
}