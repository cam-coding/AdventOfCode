using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2017
{
    public class Day04 : ISolver
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
            var ln1 = lines != null && lines.Count > 0 ? lines[0] : string.Empty;
            var ln2 = lines != null && lines.Count > 1 ? lines[1] : string.Empty;
            long count = 0;

            foreach (var line in lines)
            {
                var valid = true;
                HashSet<string> names = new HashSet<string>();
                var tokens = line.Split(delimiterChars).ToList().GetRealStrings(delimiterChars);

                foreach (var token in tokens)
                {
                    var res = names.Add(token);
                    if (!res)
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    count++;
                }
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            long count = 0;

            foreach (var line in lines)
            {
                var valid = true;
                HashSet<string> names = new HashSet<string>();
                var tokens = line.Split(delimiterChars).ToList().GetRealStrings(delimiterChars);

                foreach (var token in tokens)
                {
                    var res2 = names.Any(x => token.IsAnagram(x));
                    var res = names.Add(token);
                    if (!res || res2)
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid)
                {
                    count++;
                }
            }
            return count;
        }
    }
}