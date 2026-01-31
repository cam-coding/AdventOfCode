namespace aoc2016
{
    internal class AssemBunny
    {
        private List<string> _startingInput;
        private List<string> _workingInput;
        private Dictionary<string, Action<string, string[]>> _commands;
        private Dictionary<char, int> _registers;
        private int _pc;
        private char[] delimiterChars = { ' ' };

        public AssemBunny(List<string> input)
        {
            _startingInput = input;
            SetupCommands();
            SetRegisters();
        }

        public Dictionary<char, int> Registers => _registers;

        public object RunInput()
        {
            _pc = 0;
            _workingInput = _startingInput;
            var max = _workingInput.Count;

            while (_pc < max)
            {
                var tokens = _workingInput[_pc].Split(delimiterChars);

                if (_commands.ContainsKey(tokens[0]))
                {
                    _commands[tokens[0]](_workingInput[_pc], tokens);
                }
                _pc++;
            }
            return 0;
        }

        public void UpdateInput(List<string> newInput)
        {
            _startingInput = newInput;
        }

        public void Reset()
        {
            SetRegisters();
            _pc = 0;
            if (_commands == null)
            {
                SetupCommands();
            }
        }

        private void Command_Copy(string line, string[] tokens)
        {
            var value1 = 0;
            if (!int.TryParse(tokens[1], out value1))
            {
                value1 = _registers[tokens[1][0]];
            }
            if (int.TryParse(tokens[2], out _))
            {
                // skip if 2nd arg isn't a register
                return;
            }
            else
            {
                _registers[tokens[2][0]] = value1;
            }
        }

        private void Command_Increment(string line, string[] tokens)
        {
            var reg1 = tokens[1][0];
            _registers[reg1]++;
        }

        private void Command_Decrement(string line, string[] tokens)
        {
            var reg1 = tokens[1][0];
            _registers[reg1]--;
        }

        private void Command_JumpNotZero(string line, string[] tokens)
        {
            var value1 = 0;
            var value2 = 0;
            if (!int.TryParse(tokens[1], out value1))
            {
                value1 = _registers[tokens[1][0]];
            }
            if (!int.TryParse(tokens[2], out value2))
            {
                value2 = _registers[tokens[2][0]];
            }
            if (value1 != 0)
            {
                _pc = _pc + value2 - 1;
            }
        }

        private void Command_Multiply(string line, string[] tokens)
        {
            var value1 = _registers[tokens[1][0]];
            var value2 = _registers[tokens[2][0]];
            _registers[tokens[3][0]] = _registers[tokens[3][0]] + (value1 * value2);
        }

        private void Command_Toggle(string line, string[] tokens)
        {
            var reg1 = tokens[1][0];
            var target = _pc + _registers[reg1];

            if (target >= 0 && target < _workingInput.Count)
            {
                var targetLine = _workingInput[target];
                var targetTokens = targetLine.Split(delimiterChars);
                var newLine = string.Empty;

                if (targetTokens.Length == 2)
                {
                    if (targetTokens[0].Equals("inc"))
                    {
                        newLine = $"dec {targetTokens[1]}";
                    }
                    else
                    {
                        newLine = $"inc {targetTokens[1]}";
                    }
                }
                else if (targetTokens.Length == 3)
                {
                    if (targetTokens[0].Equals("jnz"))
                    {
                        newLine = $"cpy {targetTokens[1]} {targetTokens[2]}";
                    }
                    else
                    {
                        newLine = $"jnz {targetTokens[1]} {targetTokens[2]}";
                    }
                }
                _workingInput[target] = newLine;
            }
        }

        private void SetupCommands()
        {
            _commands = new Dictionary<string, Action<string, string[]>>()
            {
                // cpy x y
                {"cpy", Command_Copy},
                // inc x
                {"inc", Command_Increment},
                // dec x
                {"dec", Command_Decrement},
                // jnz x y
                {"jnz", Command_JumpNotZero},
                // tgl x
                {"tgl", Command_Toggle},
                // custom
                {"mul", Command_Multiply},
            };
        }

        private void SetRegisters()
        {
            _registers = new Dictionary<char, int>()
            {
                {'a', 0},
                {'b', 0},
                {'c', 0},
                {'d', 0},
            };
        }
    }
}
