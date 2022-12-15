using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2022
{
    public class Day05: ISolver
    {
        private string _filePath;
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var counter = 0;
            var stacks = new List<Stack<char>>();
            // empty stack to make the numbers easier
            stacks.Add(new Stack<char>());

            var keyLinePos = 0;
            for (var i = 0; i < lines.Count; i++)
            {
                if (lines[i][1] == '1')
                {
                    keyLinePos = i;
                    break;
                }
            }

            string keyLine = lines[keyLinePos];

            for (var i = 0; i < keyLine.Count(); i++)
            {
                if (keyLine[i] != ' ')
                {
                    var currentStack = keyLine[i] - '0';
                    stacks.Add(new Stack<char>());

                    for (var j = keyLinePos-1; j >= 0; j--)
                    {
                        var value = lines[j][i];
                        if (value == ' ')
                        {
                            break;
                        }
                        else
                        {
                            stacks[currentStack].Push(value);
                        }

                    }
                }
            }

            for (var i = keyLinePos + 2; i < lines.Count; i++)
            {
                var movements = StringParsing.GetNumbersFromString(lines[i]);

                for (var j = 0; j < movements[0]; j++)
                {
                    var value = stacks[movements[1]].Pop();
                    stacks[movements[2]].Push(value);
                }
            }

            var answer = "";
            for (var i = 1; i < stacks.Count; i++)
            {
                answer += stacks[i].Peek();
            }
            return answer;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var counter = 0;
            var stacks = new List<Stack<char>>();
            // empty stack to make the numbers easier
            stacks.Add(new Stack<char>());

            var keyLinePos = 0;
            for (var i = 0; i < lines.Count; i++)
            {
                if (lines[i][1] == '1')
                {
                    keyLinePos = i;
                    break;
                }
            }

            string keyLine = lines[keyLinePos];

            for (var i = 0; i < keyLine.Count(); i++)
            {
                if (keyLine[i] != ' ')
                {
                    var currentStack = keyLine[i] - '0';
                    stacks.Add(new Stack<char>());

                    for (var j = keyLinePos-1; j >= 0; j--)
                    {
                        var value = lines[j][i];
                        if (value == ' ')
                        {
                            break;
                        }
                        else
                        {
                            stacks[currentStack].Push(value);
                        }

                    }
                }
            }

            for (var i = keyLinePos + 2; i < lines.Count; i++)
            {
                var movements = StringParsing.GetNumbersFromString(lines[i]);

                var blah = StackHelper.PopMultipleFlipped(stacks[movements[1]], movements[0]);
                StackHelper.PushMultiple(stacks[movements[2]], blah);
            }

            var answer = "";
            for (var i = 1; i < stacks.Count; i++)
            {
                answer += stacks[i].Peek();
            }
            return answer;
        }
    }
}