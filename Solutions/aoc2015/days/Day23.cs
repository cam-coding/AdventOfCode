using AdventLibrary;

namespace aoc2015
{
    public class Day23 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '>', '<', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }
        private Dictionary<char, int> _registers;
        private List<string> _lines;

        private object Part1()
        {
            _lines = ParseInput.GetLinesFromFile(_filePath);

            _registers = new Dictionary<char, int>();
            _registers.Add('a', 0);
            _registers.Add('b', 0);
            RunProgram();

            return _registers['b'];
        }

        private object Part2()
        {
            _registers = new Dictionary<char, int>();
            _registers.Add('a', 1);
            _registers.Add('b', 0);
            RunProgram();

            return _registers['b'];
        }

        private void RunProgram()
        {
            var pc = 0;
            while (pc < _lines.Count)
            {
                var line = _lines[pc];
                var tokens = line.Split(delimiterChars);
                var nums = StringParsing.GetIntssWithNegativesFromString(line);
                var reg = tokens[1][0];

                if (tokens[0].Equals("hlf"))
                {
                    _registers[reg] = _registers[reg] / 2;
                }
                else if (tokens[0].Equals("tpl"))
                {
                    _registers[reg] = _registers[reg] * 3;
                }
                else if (tokens[0].Equals("inc"))
                {
                    _registers[reg] = _registers[reg] + 1;
                }
                else if (tokens[0].Equals("jmp"))
                {
                    var num = nums[0] - 1;
                    pc += num;
                }
                else if (tokens[0].Equals("jie"))
                {
                    if (_registers[reg] % 2 == 0)
                    {
                        var num = nums[0] - 1;
                        pc += num;
                    }
                }
                else if (tokens[0].Equals("jio"))
                {
                    if (_registers[reg] == 1)
                    {
                        var num = nums[0] - 1;
                        pc += num;
                    }
                }

                pc++;
            }
        }
    }
}