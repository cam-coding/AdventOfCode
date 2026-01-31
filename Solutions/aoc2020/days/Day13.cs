using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;

namespace aoc2020
{
    public class Day13 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1();
            solution.Part2 = Part2();
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var time = numbers[0];
            var buses = numbers.GetWithout(0);

            var best = long.MaxValue;
            var bestBus = long.MaxValue;

            foreach (var bus in buses)
            {
                var blah = time / bus;
                var real = (blah + 1) * bus;
                if (real - time < best)
                {
                    bestBus = bus;
                    best = real - time;
                }
            }
            return best * bestBus;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var numbers = input.Longs;
            var tokens = lines[1].Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            var buses = new List<long>();
            var rems = new List<long>();

            var index = 0;
            var last = index;
            foreach (var tok in tokens)
            {
                long res;
                if (long.TryParse(tok, out res))
                {
                    buses.Add(res);
                    rems.Add((res - index) % res);
                    last = index;
                }
                index++;
            }
            return MathHelper.ChineseRemainderTheorem(buses, rems);
        }
    }
}