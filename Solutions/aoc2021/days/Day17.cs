using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day17: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
        private string _filePath;
        private int _probeX;
        private int _probeY;
        private int _probeVX;
        private int _probeVY;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var nums = AdventLibrary.StringParsing.GetIntsFromString(lines[0]);
            var minX = nums[0];
            var maxX = nums[1];
            var minY = nums[2]*-1;
            var maxY = nums[3]*-1;

            var highest = 0;
            var highestY = 0;
            _probeX = 0;
            _probeY = 0;
            var i = 1;
            while (i < 1000)
            {
                var j = 1;
                while (j < 1000)
                {
                    _probeX = 0;
                    _probeY = 0;
                    _probeVX = i;
                    _probeVY = j;
                    var madeIt = false;
                    var potentialHighest = 0;
                    while (_probeX < maxX)
                    {
                        if (_probeX < minX && _probeVX == 0)
                        {
                            break;
                        }

                        if (_probeY < minY && _probeVY < 0)
                        {
                            break;
                        }
                        madeIt = InsideTarget(minX, maxX, minY, maxY);
                        if (_probeY > potentialHighest)
                        {
                            potentialHighest = _probeY;
                        }
                        Step();
                    }

                    if (madeIt)
                    {
                        if (potentialHighest > highest)
                        {
                            highest = potentialHighest;
                            highestY = j;
                        }
                    }
                    j++;
                }
                i++;
            }
            return highest;
        }
        
        private object Part2()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var nums = AdventLibrary.StringParsing.GetIntsFromString(lines[0]);
            var minX = nums[0];
            var maxX = nums[1];
            var minY = nums[2]*-1;
            var maxY = nums[3]*-1;
            var count = 0;

            var highest = 0;
            var highestY = 0;
            _probeX = 0;
            _probeY = 0;
            var i = 1;
            while (i < 1000)
            {
                var j = -1000;
                while (j < 1000)
                {
                    _probeX = 0;
                    _probeY = 0;
                    _probeVX = i;
                    _probeVY = j;
                    var madeIt = false;
                    var potentialHighest = 0;
                    while (_probeX < maxX)
                    {
                        if (_probeX < minX && _probeVX == 0)
                        {
                            break;
                        }

                        if (_probeY < minY && _probeVY < 0)
                        {
                            break;
                        }
                        madeIt = madeIt || InsideTarget(minX, maxX, minY, maxY);
                        if (_probeY > potentialHighest)
                        {
                            potentialHighest = _probeY;
                        }
                        Step();
                    }

                    madeIt = madeIt || InsideTarget(minX, maxX, minY, maxY);
                    if (madeIt)
                    {
                        count++;
                        if (potentialHighest > highest)
                        {
                            highest = potentialHighest;
                            highestY = j;
                        }
                    }
                    j++;
                }
                i++;
            }
            return count;
        }

        private void Step()
        {
            _probeX = _probeX + _probeVX;
            _probeY = _probeY + _probeVY;

            if (_probeVX < 0)
            {
                _probeVX = _probeVX + 1;
            }
            else if (_probeVX > 0)
            {
                _probeVX = _probeVX - 1;
            }
            _probeVY = _probeVY - 1;
        }

        private bool InsideTarget(int minX, int maxX, int minY, int maxY)
        {
            return _probeX >= minX  && _probeX <= maxX && _probeY >= minY && _probeY <= maxY;
        }
    }
}