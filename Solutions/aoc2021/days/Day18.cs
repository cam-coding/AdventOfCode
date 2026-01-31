using AdventLibrary;

namespace aoc2021
{
    public class Day18 : ISolver
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

            var snailfish = lines[0];

            for (var i = 1; i < lines.Count; i++)
            {
                snailfish = Reduce(snailfish);
                snailfish = $"[{snailfish},{lines[i]}]";
            }
            snailfish = Reduce(snailfish);
            return GetMagnitude(snailfish);
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var best = 0;

            for (var i = 0; i < lines.Count; i++)
            {
                for (var j = 0; j < lines.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    var snail = $"[{lines[i]},{lines[j]}]";
                    snail = Reduce(snail);
                    best = Math.Max(best, GetMagnitude(snail));
                }
            }
            return best;
        }

        private int GetMagnitude(string input)
        {
            var magnitudeStack = new Stack<int>();
            for (var i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    magnitudeStack.Push(input[i] - '0');
                }
                else if (input[i] == ']')
                {
                    var num2 = magnitudeStack.Pop();
                    var num1 = magnitudeStack.Pop();
                    var num3 = num1 * 3 + num2 * 2;
                    magnitudeStack.Push(num3);
                }
            }
            return magnitudeStack.Pop();
        }

        private string Reduce(string input)
        {
            var lastInput = input;
            var changed = true;
            while (changed)
            {
                input = Explode(input, out changed);
                if (changed)
                {
                    lastInput = input;
                    continue;
                }
                input = Split(input, out changed);
                lastInput = input;
            }
            return input;
        }

        private string Explode(string input, out bool didExplode)
        {
            var count = 0;

            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] == '[')
                {
                    count++;
                }
                else if (input[i] == ']')
                {
                    count--;
                }
                if (count == 5)
                {
                    var nextIndex = input.IndexOf(']', i + 1);
                    var sub = input.Substring(i, (nextIndex - i) + 1);
                    // input = input.ReplaceFirstInstanceOf(sub, "0");
                    input = input.Remove(i, sub.Length);
                    input = input.Insert(i, "0");

                    var substringNums = StringParsing.GetIntsFromString(sub);
                    var allNums = StringParsing.GetIntsWithIndexesFromString(input);

                    var left = allNums.LastOrDefault(x => x.index < i);
                    if (left != default)
                    {
                        input = input.Remove(left.index, left.number.ToString().Length);
                        input = input.Insert(left.index, (left.number + substringNums[0]).ToString());
                    }
                    allNums = StringParsing.GetIntsWithIndexesFromString(input);
                    var right = allNums.FirstOrDefault(x => x.index > i + 1);
                    if (right != default)
                    {
                        input = input.Remove(right.index, right.number.ToString().Length);
                        input = input.Insert(right.index, (right.number + substringNums[1]).ToString());
                    }
                    didExplode = true;
                    return input;
                }
            }
            didExplode = false;
            return input;
        }

        private string Split(string input, out bool didExplode)
        {
            didExplode = false;
            var allNums = StringParsing.GetIntsWithIndexesFromString(input);
            var num = allNums.FirstOrDefault(x => x.number > 9);
            if (num != default)
            {
                input = input.Remove(num.index, num.number.ToString().Length);
                int left = num.number / 2;

                // divide by 2 and round up
                int right = (int)Math.Ceiling((decimal)num.number / 2);
                var newStr = $"[{left},{right}]";
                input = input.Insert(num.index, newStr);
                didExplode = true;
            }

            return input;
        }
    }
}