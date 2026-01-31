using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2024
{
    public class Day17 : ISolver
    {
        private string _filePath;
        private string _programOutput;
        private string _goalString;
        private List<int> _instructions;
        private List<long> _part2Inputs;
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
            var groups = input.LineGroupsSeperatedByWhiteSpace;

            _programOutput = string.Empty;

            var group1 = groups[0];
            _instructions = StringParsing.GetDigitsFromString(groups[1][0]);
            MyProgram(StringParsing.GetIntsFromString(group1[0]).First());

            return _programOutput;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var groups = input.LineGroupsSeperatedByWhiteSpace;

            _programOutput = string.Empty;
            _instructions = StringParsing.GetDigitsFromString(groups[1][0]);
            _goalString = String.Join(',', _instructions);

            var inputsSoFar = new List<long>() { _instructions.Last() };
            BackTrack(inputsSoFar);
            return _part2Inputs.Last();
        }

        // working inputs builds backwards from the bottom up and starts with 0
        private bool BackTrack(List<long> workingInputs)
        {
            if (workingInputs.Count == (_goalString.Length + 3) / 2)
            {
                _part2Inputs = workingInputs;
                return true;
            }
            var orig = workingInputs.Last();
            var num = _goalString.Length - (workingInputs.Count * 2 - 1);
            var currentGoal = _goalString.Substring(num);
            for (long k = (orig * 8); k <= (orig + 1) * 8; k++)
            {
                _programOutput = string.Empty;
                MyProgram(k);
                var myOtherResult = Math.Floor((double)k / 8) == orig;
                if (_programOutput.Equals(currentGoal) && myOtherResult)
                {
                    var tempList = workingInputs.Clone();
                    tempList.Add(k);
                    if (BackTrack(tempList))
                    {
                        workingInputs.Add(k);
                        return true;
                    }
                }
            }
            return false;
        }

        private void MyProgram(long aValue)
        {
            var dict = new Dictionary<char, long>();
            dict.Add('A', aValue);
            dict.Add('B', 0);
            dict.Add('C', 0);

            var instructionNumber = 0;

            while (instructionNumber < _instructions.Count - 1)
            {
                var newInstructionNumber = instructionNumber;


                var comboInstruction = _instructions[instructionNumber + 1];
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

                var instructionValue = _instructions[instructionNumber];
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
                    MyOutput(comboVal % 8);
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
        }

        private void MyOutput(long val)
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

        /* This is essentially what my input program does each iteration
        private long MagicFunction(long num)
        {
            var tempQ = BitwiseHelper.XOR(num % 8, 5);
            var first = (long)Math.Floor(num / Math.Pow(2, tempQ));
            var second = BitwiseHelper.XOR(tempQ, 6);

            var myResult = BitwiseHelper.XOR(second, first);
            return myResult;
        }*/
    }
}