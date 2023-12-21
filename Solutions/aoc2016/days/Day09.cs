using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day09: ISolver
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
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var counter = 0;
            var inMarker = false;
            var markerLength = 0;
            var markerRepeat = 0;
            var markerStack = new List<char>();

            string concat = string.Join("", lines.ToArray());

                var i = 0;
                while (i < concat.Length)
                {
                    if (!Char.IsWhiteSpace(concat[i]))
                    {
                        if (!inMarker)
                        {
                            if (concat[i] == '(')
                            {
                                var start = i;
                                var end = i+1;
                                while (concat[end] != ')')
                                {
                                    end++;
                                }
                                i = end;
                                var substring = concat.Substring(start, end-start);
                                var nums = AdventLibrary.StringParsing.GetNumbersFromString(substring);
                                markerLength = nums[0];
                                markerRepeat = nums[1];
                                inMarker = true;
                            }
                            else
                            {
                                counter++;
                            }
                        }
                        else
                        {
                            markerStack.Add(concat[i]);

                            if (markerStack.Count() == markerLength)
                            {
                                counter = counter + (markerLength * markerRepeat);
                                inMarker = false;
                                markerLength = 0;
                                markerRepeat = 0;
                                markerStack = new List<char>();
                            }
                        }
                    }
                    i++;
                }
            return counter;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			long counter = 0;
            var inMarker = false;
            var markerLength = 0;
            var markerRepeat = 0;
            var markerStack = new List<char>();

            string concat = string.Join("", lines.ToArray());
            string fullString = String.Concat(concat.Where(c => !Char.IsWhiteSpace(c)));

            var i = 0;
            while (i < concat.Length)
            {
                if (concat[i] == '(')
                {
                    var open = i;
                    var close = i+1;
                    while (fullString[close] != ')')
                    {
                        close++;
                    }
                    i = close;
                    var substring = fullString.Substring(open, close-open);
                    var nums = AdventLibrary.StringParsing.GetNumbersFromString(substring);
                    markerLength = nums[0];
                    markerRepeat = nums[1];
                    var newStart = close + 1;
                    long result = Recursion(newStart, newStart + markerLength, fullString) * markerRepeat;
                    counter = counter + result;
                    i = i + markerLength;
                }
                else
                {
                    counter++;
                }
                i++;
            }

            return counter;
        }

        private long Recursion(int start, int end, string fullString)
        {
            var i = start;
            long count = 0;
            var markerLength = 0;
            var markerRepeat = 0;
            var markerStack = new List<char>();
            while (i < end)
            {
                if (fullString[i] == '(')
                {
                    var open = i;
                    var close = i+1;
                    while (fullString[close] != ')')
                    {
                        close++;
                    }
                    i = close;
                    var substring = fullString.Substring(open, close-open);
                    var nums = AdventLibrary.StringParsing.GetNumbersFromString(substring);
                    markerLength = nums[0];
                    markerRepeat = nums[1];
                    var newStart = close + 1;
                    long result = Recursion(newStart, newStart + markerLength, fullString) * markerRepeat;
                    count = count + result;
                    i = close + markerLength; // expect to get incremented by 1
                }
                else
                {
                    count++;
                }
                i++;
            }

            return count;
        }
    }
}
