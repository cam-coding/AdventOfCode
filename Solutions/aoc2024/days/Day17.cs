using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using Microsoft.Z3;

namespace aoc2024
{
    public class Day17: ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private string _programOutput;
        private List<long> _programOutputList;
        private List<long> _goal;
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
            var lines = input.Lines;
			var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
			long count = 0;
            long number = input.Long;

            _programOutput = string.Empty;
            var dict = new Dictionary<char, int>();
            dict.Add('A', 0);
            dict.Add('B', 0);
            dict.Add('C', 0);

            var group1 = groups[0];

            dict['A'] = StringParsing.GetNumbersFromString(group1[0]).First();
            dict['B'] = StringParsing.GetNumbersFromString(group1[1]).First();
            dict['C'] = StringParsing.GetNumbersFromString(group1[2]).First();

            var instructions = StringParsing.GetDigitsFromString(groups[1][0]);

            var instructionNumber = 0;

            while (instructionNumber < instructions.Count - 1)
            {
                var newInstructionNumber = instructionNumber;


                var comboInstruction = instructions[instructionNumber+1];
                var comboVal = 0;
                if (comboInstruction >= 0 && comboInstruction <= 3)
                {
                    comboVal = comboInstruction;
                }
                else if (comboInstruction == 4)
                {
                    comboVal = dict['A'];
                }
                else if (comboInstruction == 5)
                {
                    comboVal = dict['B'];
                }
                else if (comboInstruction == 6)
                {
                    comboVal = dict['C'];
                }
                else if (comboInstruction == 7)
                {
                }

                var instructionValue = instructions[instructionNumber];
                double instructionResult;

                // adv
                if (instructionValue == 0)
                {
                    instructionResult = dict['A'] / Math.Pow(2, comboVal);
                    dict['A'] = (int)Math.Floor(instructionResult);
                }
                // bxl
                else if (instructionValue == 1)
                {
                    dict['B'] = BitwiseHelper.XOR(dict['B'], comboInstruction);
                }
                // bst
                else if (instructionValue == 2)
                {
                    dict['B'] = comboVal % 8;
                }
                // jnz
                else if (instructionValue == 3)
                {
                    if (dict['A'] != 0)
                    {
                        newInstructionNumber = comboInstruction;
                    }
                }
                // bxc
                else if (instructionValue == 4)
                {
                    dict['B'] = BitwiseHelper.XOR(dict['B'], dict['C']);
                }
                // out
                else if (instructionValue == 5)
                {
                    MyOutput(comboVal % 8);
                }
                // bdv
                else if (instructionValue == 6)
                {
                    instructionResult = dict['A'] / Math.Pow(2, comboVal);
                    dict['B'] = (int)Math.Floor(instructionResult);
                }
                // cdv
                else if (instructionValue == 7)
                {
                    instructionResult = dict['A'] / Math.Pow(2, comboVal);
                    dict['C'] = (int)Math.Floor(instructionResult);
                }

                if (newInstructionNumber == instructionNumber)
                {
                    instructionNumber += 2;
                }
                else
                {
                    instructionNumber = newInstructionNumber;
                }
            }

            return _programOutput;
        }

        private object Part2(bool isTest = false)
        {
            if (isTest)
            {
                return "skipped";
            }
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.Graph;
            var grid = input.GridChar;
            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            _programOutput = string.Empty;
            var dict = new Dictionary<char, long>();
            dict.Add('A', 0);
            dict.Add('B', 0);
            dict.Add('C', 0);

            var group1 = groups[0];

            dict['A'] = StringParsing.GetNumbersFromString(group1[0]).First();
            dict['B'] = StringParsing.GetNumbersFromString(group1[1]).First();
            dict['C'] = StringParsing.GetNumbersFromString(group1[2]).First();

            var instructions = StringParsing.GetDigitsFromString(groups[1][0]);
            _goal = instructions.Clone().Select(x => (long)x).ToList();

            var instructionNumber = 0;
            var magicMin = 0;
            var magicMax = long.MaxValue;

            var backwards = instructions.Clone();
            backwards.Reverse();
            var result = BackTrack(backwards);

            return result.Last();

            // var tempList = new List<long>() { 0,3,5,6,30,222,1758,14046,112350,898782,7190238,57521886,460175070 };
            var tempList = new List<long>() { 0 };
            long latest = tempList.Last();
            while (tempList.Count < 17)
            {
                var temp = tempList.Count;
                for (long k = latest*8; k <= (latest+1)*8; k++)
                {
                    var myResult = MagicFunction(k);
                    var myOtherResult = Math.Floor((double)k / 8) == latest;
                    if (myResult % 8 == backwards[tempList.Count-1] && myOtherResult)
                    {
                        tempList.Add(k);
                        latest = k;
                        break;
                    }
                }
                if (temp == tempList.Count)
                {
                    Console.WriteLine("bad");
                }
            }

            var myListy = new List<long>();
            tempList.Reverse();
            _programOutputList = new List<long>();
            foreach (var item in tempList)
            {
                myListy.Add(RunProgram(item, instructions));
            }

            for (var i = latest; i < magicMax; i++)
            {
                /*
                var tempQ = BitwiseHelper.XOR(i % 8, 5);
                var first = (int)Math.Floor(i / Math.Pow(2, tempQ));
                var second = BitwiseHelper.XOR(tempQ, 6);

                var myResult = BitwiseHelper.XOR(second, first);
                if (myResult % 8 != 2)
                {
                    continue;
                }*/

                // XOR(XOR(XOR(A%8,5),6),MATH.FLOOR(A / MATH.POW(2,XOR(A%8,5)))) % 8 = 2
                // A = 1-7

                dict['A'] = i;
                _programOutput = string.Empty;
                _programOutputList = new List<long>();
                instructionNumber = 0;

                while (instructionNumber < instructions.Count - 1)
                {
                    var newInstructionNumber = instructionNumber;


                    var comboInstruction = instructions[instructionNumber + 1];
                    long comboVal = 0;
                    if (comboInstruction >= 0 && comboInstruction <= 3)
                    {
                        comboVal = comboInstruction;
                    }
                    else if (comboInstruction == 4)
                    {
                        comboVal = dict['A'];
                    }
                    else if (comboInstruction == 5)
                    {
                        comboVal = dict['B'];
                    }
                    else if (comboInstruction == 6)
                    {
                        comboVal = dict['C'];
                    }
                    else if (comboInstruction == 7)
                    {
                    }

                    var instructionValue = instructions[instructionNumber];
                    double instructionResult;

                    // adv
                    if (instructionValue == 0)
                    {
                        instructionResult = dict['A'] / Math.Pow(2, comboVal);
                        dict['A'] = (long)Math.Floor(instructionResult);
                    }
                    // bxl
                    else if (instructionValue == 1)
                    {
                        dict['B'] = BitwiseHelper.XOR(dict['B'], comboInstruction);
                    }
                    // bst
                    else if (instructionValue == 2)
                    {
                        dict['B'] = comboVal % 8;
                    }
                    // jnz
                    else if (instructionValue == 3)
                    {
                        if (dict['A'] != 0)
                        {
                            newInstructionNumber = comboInstruction;
                        }
                    }
                    // bxc
                    else if (instructionValue == 4)
                    {
                        dict['B'] = BitwiseHelper.XOR(dict['B'], dict['C']);
                    }
                    // out
                    else if (instructionValue == 5)
                    {
                        if (!MyOutput2(comboVal % 8))
                        {
                            break;
                        }
                    }
                    // bdv
                    else if (instructionValue == 6)
                    {
                        instructionResult = dict['A'] / Math.Pow(2, comboVal);
                        dict['B'] = (long)Math.Floor(instructionResult);
                    }
                    // cdv
                    else if (instructionValue == 7)
                    {
                        instructionResult = dict['A'] / Math.Pow(2, comboVal);
                        dict['C'] = (long)Math.Floor(instructionResult);
                    }

                    if (newInstructionNumber == instructionNumber)
                    {
                        instructionNumber += 2;
                    }
                    else
                    {
                        instructionNumber = newInstructionNumber;
                    }
                }
                if (_programOutputList.SequenceEqual(_goal))
                {
                    return i;
                }
            }

            return $"FAIL I got to: {magicMax - 1}";
        }

        private void MyOutput(int val)
        {
            if (_programOutput.Equals(string.Empty))
            {
                _programOutput = val.ToString();
            }
            else
            {
                _programOutput += $",{val}";
            }
        }

        private bool MyOutput2(long val)
        {
            _programOutputList.Add(val);
            return false;
            for (var j = 0; j < _programOutputList.Count; j++)
            {
                if (_programOutputList[j] != _goal[j])
                {
                    return false;
                }
            }

            if (_programOutput.Equals(string.Empty))
            {
                _programOutput = val.ToString();
            }
            else
            {
                _programOutput += $",{val}";
            }
            return true;
        }

        private long MagicFunction(long num)
        {
            var tempQ = BitwiseHelper.XOR(num % 8, 5);
            var first = (long)Math.Floor(num / Math.Pow(2, tempQ));
            var second = BitwiseHelper.XOR(tempQ, 6);

            var myResult = BitwiseHelper.XOR(second, first);
            return myResult;
        }

        private long RunProgram(long starting, List<int> instructions)
        {

            var dict = new Dictionary<char, long>();
            dict.Add('A', 0);
            dict.Add('B', 0);
            dict.Add('C', 0);

            dict['A'] = starting;
            _programOutput = string.Empty;
            // _programOutputList = new List<long>();
            var instructionNumber = 0;

            while (instructionNumber < instructions.Count - 1)
            {
                var newInstructionNumber = instructionNumber;


                var comboInstruction = instructions[instructionNumber + 1];
                long comboVal = 0;
                if (comboInstruction >= 0 && comboInstruction <= 3)
                {
                    comboVal = comboInstruction;
                }
                else if (comboInstruction == 4)
                {
                    comboVal = dict['A'];
                }
                else if (comboInstruction == 5)
                {
                    comboVal = dict['B'];
                }
                else if (comboInstruction == 6)
                {
                    comboVal = dict['C'];
                }
                else if (comboInstruction == 7)
                {
                }

                var instructionValue = instructions[instructionNumber];
                double instructionResult;

                // adv
                if (instructionValue == 0)
                {
                    instructionResult = dict['A'] / Math.Pow(2, comboVal);
                    dict['A'] = (long)Math.Floor(instructionResult);
                }
                // bxl
                else if (instructionValue == 1)
                {
                    dict['B'] = BitwiseHelper.XOR(dict['B'], comboInstruction);
                }
                // bst
                else if (instructionValue == 2)
                {
                    dict['B'] = comboVal % 8;
                }
                // jnz
                else if (instructionValue == 3)
                {
                    if (dict['A'] != 0)
                    {
                        newInstructionNumber = comboInstruction;
                    }
                }
                // bxc
                else if (instructionValue == 4)
                {
                    dict['B'] = BitwiseHelper.XOR(dict['B'], dict['C']);
                }
                // out
                else if (instructionValue == 5)
                {
                    MyOutput2(comboVal % 8);
                    return dict['A'];
                }
                // bdv
                else if (instructionValue == 6)
                {
                    instructionResult = dict['A'] / Math.Pow(2, comboVal);
                    dict['B'] = (long)Math.Floor(instructionResult);
                }
                // cdv
                else if (instructionValue == 7)
                {
                    instructionResult = dict['A'] / Math.Pow(2, comboVal);
                    dict['C'] = (long)Math.Floor(instructionResult);
                }

                if (newInstructionNumber == instructionNumber)
                {
                    instructionNumber += 2;
                }
                else
                {
                    instructionNumber = newInstructionNumber;
                }
            }
            return dict['A'];
        }

        private List<long> BackTrack(List<int> backwards)
        {
            var tempList = new List<long>() { 0 };
            long latest = tempList.Last();
            List<long> result;
            BackTrack2(tempList, latest, backwards, out result);
            return result;
        }

        private bool BackTrack2(List<long> currentList, long latest, List<int> backwards, out List<long> result)
        {
            if (currentList.Count == 17)
            {
                result = currentList;
                return true;
            }
            var orig = latest;
            for (long k = (orig * 8); k <= (orig + 2) * 8; k++)
            {
                var myResult = MagicFunction(k);
                var myOtherResult = Math.Floor((double)k / 8) == orig;
                if (myResult % 8 == backwards[currentList.Count - 1] && myOtherResult)
                {
                    var tempList = currentList.Clone();
                    tempList.Add(k);
                    latest = k;
                    if (BackTrack2(tempList, latest, backwards, out result))
                    {
                        currentList.Add(k);
                        return true;
                    }
                }
            }
            result = new List<long>();
            return false;
        }
    }
}