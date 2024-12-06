using System.Collections.Generic;
using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2015
{
    public class Day07 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        private List<string> _gates = new List<string>() { "AND", "OR", "NOT", "LSHIFT", "RSHIFT" };
        private Dictionary<string, ushort> _dict = new Dictionary<string, ushort>();
        private ushort defaultVal = 9005;

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var obj = new InputObjectCollection(_filePath);
            var tokenLines = obj.TokenLines;

            foreach (var line in tokenLines)
            {
                foreach (var token in line)
                {
                    if (!_gates.Contains(token))
                    {
                        if (!token.IsNumeric())
                        {
                            _dict.TryAdd(token, defaultVal);
                        }
                    }
                }
            }

            var nextCommandsToRun = new List<List<string>>();
            foreach (var line in tokenLines)
            {
                if (line[0].IsNumeric() && line.Count == 2)
                {
                    _dict[line.GetLastItem()] = ushort.Parse(line[0]);
                }
                else
                {
                    nextCommandsToRun.Add(line);
                }
            }

            return doTheWork(nextCommandsToRun);
        }

        private object Part2()
        {
            var obj = new InputObjectCollection(_filePath);
            var tokenLines = obj.TokenLines;

            foreach (var line in tokenLines)
            {
                foreach (var token in line)
                {
                    if (!_gates.Contains(token))
                    {
                        if (!token.IsNumeric())
                        {
                            _dict.TryAdd(token, defaultVal);
                        }
                    }
                }
            }

            var nextCommandsToRun = new List<List<string>>();
            foreach (var line in tokenLines)
            {
                if (line[0].IsNumeric() && line.Count == 2)
                {
                    _dict[line.GetLastItem()] = ushort.Parse(line[0]);
                }
                else
                {
                    nextCommandsToRun.Add(line);
                }
            }

            ushort temp = doTheWork(nextCommandsToRun);

            foreach (var item in _dict)
            {
                _dict[item.Key] = defaultVal;
            }

            foreach (var line in tokenLines)
            {
                if (line[0].IsNumeric() && line.Count == 2)
                {
                    _dict[line.GetLastItem()] = ushort.Parse(line[0]);
                }
                else
                {
                    nextCommandsToRun.Add(line);
                }
            }

            if (_dict.ContainsKey("b"))
            {
                _dict["b"] = temp;
            }
            else
            {
                return 0;
            }

            return doTheWork(nextCommandsToRun);
        }

        private ushort doTheWork(List<List<string>> commands)
        {
            var commandsToRun = commands.Clone2dList();
            var nextCommandsToRun = new List<List<string>>();

            while (commandsToRun.Count != 0)
            {
                foreach (var line in commandsToRun)
                {
                    var ran = false;
                    if (line.Count == 2)
                    {
                        ran = Move(line);
                    }
                    else if (line[0].IsNumeric())
                    {
                        ran = NumWithWire(line);
                    }
                    else if (line[2].IsNumeric())
                    {
                        ran = Shift(line);
                    }
                    else if (line.Count == 3)
                    {
                        ran = Not(line);
                    }
                    else
                    {
                        ran = Command(line);
                    }

                    if (!ran)
                    {
                        nextCommandsToRun.Add(line);
                    }
                }

                commandsToRun = nextCommandsToRun;
                nextCommandsToRun = new List<List<string>>();
            }

            if (_dict.ContainsKey("a"))
            {
                return _dict["a"];
            }
            return 0;
        }

        private bool NumWithWire(List<string> tokens)
        {
            if (_dict[tokens[2]] == defaultVal)
            {
                return false;
            }
            else
            {
                Command(uint.Parse(tokens[0]), _dict[tokens[2]], tokens[1], tokens[3]);
                return true;
            }
        }

        private bool Move(List<string> tokens)
        {
            if (_dict[tokens[0]] == defaultVal)
            {
                return false;
            }
            else
            {
                _dict[tokens[1]] = _dict[tokens[0]];
                return true;
            }
        }

        private bool Not(List<string> tokens)
        {
            if (_dict[tokens[1]] == defaultVal)
            {
                return false;
            }
            else
            {
                _dict[tokens[2]] = (ushort)~_dict[tokens[1]];
                return true;
            }
        }

        private bool Command(List<string> tokens)
        {
            if (_dict[tokens[0]] == defaultVal || _dict[tokens[2]] == defaultVal)
            {
                return false;
            }
            else
            {
                Command(_dict[tokens[0]], _dict[tokens[2]], tokens[1], tokens[3]);
                return true;
            }
        }

        private bool Command(uint input1, uint input2, string gateType, string output)
        {
            ushort outputVal = 0;
            switch (gateType)
            {
                case "AND":
                    outputVal = (ushort)(input1 & input2);
                    break;

                case "OR":
                    outputVal = (ushort)(input1 | input2);
                    break;
            }

            _dict[output] = outputVal;

            return true;
        }

        private bool Shift(List<string> tokens)
        {
            if (_dict[tokens[0]] == defaultVal)
            {
                return false;
            }
            else
            {
                if (tokens[1].Equals("LSHIFT"))
                {
                    _dict[tokens[3]] = (ushort)(_dict[tokens[0]] << ushort.Parse(tokens[2]));
                    return true;
                }
                else if (tokens[1].Equals("RSHIFT"))
                {
                    _dict[tokens[3]] = (ushort)(_dict[tokens[0]] >> ushort.Parse(tokens[2]));
                    return true;
                }
            }
            return false;
        }
    }
}