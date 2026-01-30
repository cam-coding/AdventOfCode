using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day21: ISolver
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
            var str = "abcdefgh";
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);
				var nums = AdventLibrary.StringParsing.GetIntsFromString(line);

                if (tokens[0] == "swap" && nums.Count == 2)
                {
                    str = SwapPos(str, nums[0], nums[1]);
                }
                else if (tokens[0] == "swap")
                {
                    str = SwapLetter(str, tokens[2][0], tokens[5][0]);
                }
                else if (tokens[0] == "rotate" && nums.Count == 1)
                {
                    if (tokens[1] == "left")
                    {
                        var value = str.ToArray();
                        value = ArrayHelper.RotateArrayLeft(value, nums[0]);
                        str = new string(value);
                    }
                    else if (tokens[1] == "right")
                    {
                        var value = str.ToArray();
                        value = ArrayHelper.RotateArrayRight(value, nums[0]);
                        str = new string(value);
                    }
                }
                else if (tokens[0] == "rotate")
                {
                    str = RotateAround(str, tokens[6][0]);
                }
                else if (tokens[0] == "reverse")
                {
                    str = ReverseSubString(str, nums[0], nums[1]);
                }
                else if (tokens[0] == "move")
                {
                    str = MoveCharToPos(str, nums[0], nums[1]);
                }
            }
            return str;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var str = "fbgdceah";

            for (var i = lines.Count - 1; i >= 0; i--)
            {
                var line = lines[i];
                var tokens = line.Split(delimiterChars);
                var nums = AdventLibrary.StringParsing.GetIntsFromString(line);

                if (tokens[0] == "swap" && nums.Count == 2)
                {
                    str = SwapPos(str, nums[0], nums[1]);
                }
                else if (tokens[0] == "swap")
                {
                    str = SwapLetter(str, tokens[2][0], tokens[5][0]);
                }
                else if (tokens[0] == "rotate" && nums.Count == 1)
                {
                    if (tokens[1] == "left")
                    {
                        var value = str.ToArray();
                        value = ArrayHelper.RotateArrayRight(value, nums[0]);
                        str = new string(value);
                    }
                    else if (tokens[1] == "right")
                    {
                        var value = str.ToArray();
                        value = ArrayHelper.RotateArrayLeft(value, nums[0]);
                        str = new string(value);
                    }
                }
                else if (tokens[0] == "rotate")
                {
                    if (str.Length == 5)
                    {
                        str = UnRotateAround2(str, tokens[6][0]);
                    }
                    else
                    {
                        str = UnRotateAround(str, tokens[6][0]);
                    }
                }
                else if (tokens[0] == "reverse")
                {
                    str = ReverseSubString(str, nums[0], nums[1]);
                }
                else if (tokens[0] == "move")
                {
                    str = MoveCharToPos(str, nums[1], nums[0]);
                }
            }

            return str;
        }

        private string SwapPos(string input, int x, int y)
        {
            var value = input.ToArray();
            value[x] = input[y];
            value[y] = input[x];
            return new string(value);
        }

        private string SwapLetter(string input, char x, char y)
        {
            var pos1 = input.IndexOf(x);
            var pos2 = input.IndexOf(y);
            return SwapPos(input, pos1, pos2);
        }

        private string RotateAround(string input, char x)
        {
            var value = input.ToArray();
            var pos1 = input.IndexOf(x);
            var rotations = 1 + pos1;
            if (pos1 >= 4)
            {
                rotations = rotations + 1;
            }
            value = ArrayHelper.RotateArrayRight(value, rotations);
            return new string(value);
        }

        private string ReverseSubString(string input, int x, int y)
        {
            if (x == y)
            {
                return input;
            }

            var min = Math.Min(x, y);
            var max = Math.Max(y, x);
            var value = input.ToArray();
            var j = max;
            for (var i = min; i <= max; i++)
            {
                value[i] = input[j];
                j--;
            }
            return new string(value);
        }

        private string MoveCharToPos(string input, int x, int y)
        {
            var value = input.ToList();
            var temp = input[x];
            value.RemoveAt(x);
            value.Insert(y, temp);
            return new string(value.ToArray());
        }

        private string UnRotateAround(string input, char x)
        {
            var value = input.ToArray();
            var endingPos = input.IndexOf(x);
            value = ArrayHelper.RotateArrayLeft(value, 1);
            var currentPos = Array.IndexOf(value, x);

            if (currentPos == 1)
            {
                value = ArrayHelper.RotateArrayLeft(value, 5);
            }
            else if (currentPos == 2)
            {
                value = ArrayHelper.RotateArrayLeft(value, 1);
            }
            else if (currentPos == 3)
            {
                value = ArrayHelper.RotateArrayLeft(value, 6);
            }
            else if (currentPos == 4)
            {
                value = ArrayHelper.RotateArrayLeft(value, 2);
            }
            else if (currentPos == 5)
            {
                value = ArrayHelper.RotateArrayLeft(value, 7);
            }
            else if (currentPos == 6)
            {
                value = ArrayHelper.RotateArrayLeft(value, 3);
            }
            else if (currentPos == 7)
            {
                value = ArrayHelper.RotateArrayLeft(value, 8);
            }

            return new string(value);
        }

        private string UnRotateAround2(string input, char x)
        {
            var value = input.ToArray();
            var endingPos = input.IndexOf(x);
            value = ArrayHelper.RotateArrayLeft(value, 1);
            var currentPos = Array.IndexOf(value, x);



            if (currentPos == 1)
            {
                value = ArrayHelper.RotateArrayLeft(value, 3);
            }
            else if (currentPos == 2)
            {
                value = ArrayHelper.RotateArrayLeft(value, 1);
            }
            else if (currentPos == 3)
            {
                Console.WriteLine("error!!");
            }
            else if (currentPos == 4)
            {
                value = ArrayHelper.RotateArrayLeft(value, 5);
            }

            return new string(value);
        }
    }
}