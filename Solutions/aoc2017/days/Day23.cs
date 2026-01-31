using AdventLibrary;
using AdventLibrary.CustomObjects;
using AdventLibrary.Helpers;

namespace aoc2017
{
    public class Day23 : ISolver
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
            var lines = input.Lines;
            long count = 0;
            var registers = new Registry(8, 'a', 0);

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var tokens = line.GetRealTokens(' ');
                var command = tokens[0];
                var reg = tokens[1];
                var value = registers.GetValue(tokens[2]);
                if (command.Equals("set"))
                {
                    registers.Set(reg, tokens[2]);
                }
                else if (command.Equals("sub"))
                {
                    registers.Sub(reg, tokens[2]);
                }
                else if (command.Equals("mul"))
                {
                    count++;
                    registers.Mul(reg, tokens[2]);
                }
                else if (command.Equals("jnz"))
                {
                    if (registers.GetValue(tokens[1]) != 0)
                    {
                        i = i + (int)(registers.GetValue(tokens[2]) - 1);
                    }
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var myCount = 0;
            for (var i = 106700; i <= 123700; i += 17)
            {
                if (!MathHelper.IsPrime(i))
                {
                    myCount++;
                }
            }
            /* reg b starts at 106700 and increase by 17 each run
             * reg c starts at 123,700 and program ends when the registers are equal
             * The program counts each iteration where b is not prime
             * During a loop the following important things happen:
             *      g is set to every value that can be made by 2...b * 2...b
             *      If g is ever equal to b, that loop is count.
             *  QED count the iterations where b isn't prime
             * */
            return myCount;
            /*
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            long count = 0;
            var registry = new Registry(8, 'a', 0);
            registry.Registers['a'] = 1;

            for (var i = 0; i < lines.Count; i++)
            {
                if (i == 8 || i == 24 || i == 28 || i == 31)
                {
                    var str = registry.Registers.Stringify();
                    Console.WriteLine(str);
                }
                var line = lines[i];
                var tokens = line.GetRealTokens(' ');
                var command = tokens[0];
                var reg = tokens[1];
                var value = registry.GetValue(tokens[2]);
                if (command.Equals("set"))
                {
                    registry.Set(reg, tokens[2]);
                }
                else if (command.Equals("sub"))
                {
                    registry.Sub(reg, tokens[2]);
                }
                else if (command.Equals("mul"))
                {
                    count++;
                    registry.Mul(reg, tokens[2]);
                }
                else if (command.Equals("jnz"))
                {
                    if (registry.GetValue(tokens[1]) != 0)
                    {
                        i = i + (int)(registry.GetValue(tokens[2]) - 1);
                    }
                }
            }
            return count;
            */
        }

        private Registry RunLines11to19(Registry reg)
        {
            reg.Registers['e'] = reg.Registers['b'];
            reg.Registers['f'] = 0;
            reg.Registers['g'] = 0;
            return reg;
        }

        private Registry RunLines20to23(Registry reg)
        {
            reg.Registers['d'] = reg.Registers['b'];
            return reg;
        }
    }
}