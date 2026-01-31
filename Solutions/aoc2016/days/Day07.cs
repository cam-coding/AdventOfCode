using AdventLibrary;

namespace aoc2016
{
    public class Day07 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t', '[', ']' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var count = 0;

            foreach (var line in lines)
            {
                var supernetStrings = new List<string>();
                var hypernetStrings = new List<string>();
                var tokens = line.Split(delimiterChars);
                var insideBrackets = false;

                for (var i = 0; i < tokens.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        supernetStrings.Add(tokens[i]);
                    }
                    else
                    {
                        hypernetStrings.Add(tokens[i]);
                    }
                }

                if (!ContainsPair(hypernetStrings))
                {
                    if (ContainsPair(supernetStrings))
                    {
                        count = count + 1;
                    }
                }
            }
            return count;
        }

        private bool ContainsPair(List<string> strings)
        {
            foreach (var item in strings)
            {
                var start = 0;
                var end = 3;

                while (end < item.Length)
                {
                    if (item[start] == item[end] &&
                        item[start + 1] == item[start + 2] &&
                        item[start] != item[start + 1])
                    {
                        return true;
                    }
                    start++;
                    end++;
                }
            }
            return false;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var count = 0;

            foreach (var line in lines)
            {
                var supernetStrings = new List<string>();
                var hypernetStrings = new List<string>();
                var tokens = line.Split(delimiterChars);
                var insideBrackets = false;

                for (var i = 0; i < tokens.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        supernetStrings.Add(tokens[i]);
                    }
                    else
                    {
                        hypernetStrings.Add(tokens[i]);
                    }
                }

                var foundMatch = false;
                foreach (var item in supernetStrings)
                {
                    var start = 0;
                    var end = 2;

                    while (end < item.Length)
                    {
                        if (item[start] == item[end] &&
                            item[start] != item[start + 1])
                        {
                            if (HasMatch(item[start], item[start + 1], hypernetStrings))
                            {
                                foundMatch = true;
                                break;
                            }
                        }
                        start++;
                        end++;
                    }
                    if (foundMatch)
                    {
                        break;
                    }
                }

                if (foundMatch)
                {
                    count++;
                }
            }
            return count;
        }

        private bool HasMatch(char char1, char char2, List<string> hypernetStrings)
        {
            foreach (var item in hypernetStrings)
            {
                var start = 0;
                var end = 2;

                while (end < item.Length)
                {
                    if (item[start] == char2 &&
                        item[end] == char2 &&
                        item[start + 1] == char1)
                    {
                        return true;
                    }
                    start++;
                    end++;
                }
            }
            return false;
        }
    }
}
