using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day17: ISolver
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
			var numbers = input.Longs;

            var megaIndex = (int)numbers[0];
            var megaList = new List<int>() { 0 };
            var currentPos = 0;

            for (var i = 1; i < 2018; i++)
            {
                var index = megaList.GetWrappedIndex(currentPos + megaIndex) + 1;
                if (index == megaList.Count)
                {
                    megaList.Add(i);
                }
                else
                {
                    megaList.Insert(index, i);
                }
                currentPos = index;
            }

            var index2017 = megaList.IndexOf(2017);
            return megaList[index2017+1];
        }

        private object Part2(bool isTest = false)
        {
            if (isTest)
                return 0;
            var input = new InputObjectCollection(_filePath);
            var numbers = input.Longs;

            var megaIndex = (int)numbers[0];
            var currentPos = 0;
            var listSize = 1;
            var indexOfZero = 0;
            var valueAfterZero = -1;

            for (var i = 1; i < 50000001; i++)
            {
                var index = GetWrappedIndex(listSize, currentPos + megaIndex) + 1;
                if (index == indexOfZero + 1)
                {
                    valueAfterZero = i;
                }
                if (index <= indexOfZero)
                {
                    indexOfZero++;
                }
                currentPos = index;
                listSize++;
            }

            return valueAfterZero;
        }

        public static int GetWrappedIndex(int size, int index)
        {
            var realIndex = -1;
            if (index >= 0)
            {
                realIndex = index % size;
            }
            else if (index < 0)
            {
                realIndex = size + (index % (size * -1));
            }
            return realIndex;
        }
    }
}