using AdventLibrary;

namespace aoc2017
{
    public class Day15 : ISolver
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
            if (isTest)
            {
                return 1;
            }
            var input = new InputObjectCollection(_filePath);
            var numbers = input.Longs;
            var count = 0;

            var memo = new Dictionary<long, int>();

            var genA = new Generator(numbers[0], 16807);
            var genB = new Generator(numbers[1], 48271);

            for (var i = 0; i < 40000000; i++)
            {
                genA.Generate();
                genB.Generate();

                if (memo.ContainsKey(genA.CurrentValue) && memo.ContainsKey(genB.CurrentValue))
                {
                    if (memo[genA.CurrentValue] == memo[genB.CurrentValue])
                    {
                        count++;
                    }
                    else
                    {
                        continue;
                    }
                }

                var binaryA = -1;
                if (memo.ContainsKey(genA.CurrentValue))
                {
                    binaryA = memo[genA.CurrentValue];
                }
                else
                {
                    var temp = Convert.ToString(genA.CurrentValue, 2);
                    binaryA = GetFancyString(temp).GetHashCode();
                    memo.Add(genA.CurrentValue, binaryA);
                }

                var binaryB = -1;
                if (memo.ContainsKey(genB.CurrentValue))
                {
                    binaryB = memo[genB.CurrentValue];
                }
                else
                {
                    var temp = Convert.ToString(genB.CurrentValue, 2);
                    binaryB = GetFancyString(temp).GetHashCode();
                    memo.Add(genB.CurrentValue, binaryB);
                }

                if (binaryA.Equals(binaryB))
                {
                    count++;
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            if (isTest)
            {
                return 1;
            }
            var input = new InputObjectCollection(_filePath);
            var numbers = input.Longs;
            var count = 0;

            var memo = new Dictionary<long, string>();

            var genA = new Generator(numbers[0], 16807);
            var genB = new Generator(numbers[1], 48271);

            for (var i = 0; i < 5000000; i++)
            {
                genA.Generate();
                while (genA.CurrentValue % 4 != 0)
                {
                    genA.Generate();
                }
                genB.Generate();
                while (genB.CurrentValue % 8 != 0)
                {
                    genB.Generate();
                }

                var binaryA = string.Empty;
                if (memo.ContainsKey(genA.CurrentValue))
                {
                    binaryA = memo[genA.CurrentValue];
                }
                else
                {
                    var temp = Convert.ToString(genA.CurrentValue, 2);
                    binaryA = GetFancyString(temp);
                    memo.Add(genA.CurrentValue, binaryA);
                }

                var binaryB = string.Empty;
                if (memo.ContainsKey(genB.CurrentValue))
                {
                    binaryB = memo[genB.CurrentValue];
                }
                else
                {
                    var temp = Convert.ToString(genB.CurrentValue, 2);
                    binaryB = GetFancyString(temp);
                    memo.Add(genB.CurrentValue, binaryB);
                }

                if (binaryA.Equals(binaryB))
                {
                    count++;
                }
            }
            return count;
        }

        private string GetFancyString(string str)
        {
            var start = str.Length - 16;
            if (start < 0)
            {
                return str.PadLeft(16, '0');
            }
            else
            {
                return str.Substring(start, 16);
            }
        }

        private class Generator
        {
            private long _factor;
            public Generator(long startingNum, long factor)
            {
                CurrentValue = startingNum;
                _factor = factor;
            }

            public long CurrentValue { get; set; }

            public long Generate()
            {
                CurrentValue = (CurrentValue * _factor) % 2147483647;
                return CurrentValue;
            }
        }
    }
}