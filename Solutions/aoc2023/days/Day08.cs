using AdventLibrary;

namespace aoc2023
{
    public class Day08 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t', '(', ')', '=' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var total = 1000000;
            var counter = 0;
            var dict = new Dictionary<string, (string, string)>();
            var dir = lines[0];
            for (var j = 2; j < lines.Count; j++)
            {
                var tokens = lines[j].Split(delimiterChars).Where(x => !string.IsNullOrWhiteSpace(x) && x.Length == 3).ToList();
                dict.Add(tokens[0], (tokens[1], tokens[2]));
            }

            var current = "AAA";

            counter = 0;
            var i = 0;
            while (!current.Equals("ZZZ"))
            {
                if (dir[i] == 'L')
                {
                    current = dict[current].Item1;
                }
                else
                {
                    current = dict[current].Item2;
                }
                i++;
                if (i == dir.Length)
                {
                    i = 0;
                }
                counter++;
            }
            return counter;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var dict = new Dictionary<string, (string, string)>();
            var directionalInput = lines[0];
            var indexer = 0;
            var currentPositions = new Dictionary<int, string>();
            for (var j = 2; j < lines.Count; j++)
            {
                var tokens = lines[j].Split(delimiterChars).Where(x => !string.IsNullOrWhiteSpace(x) && x.Length == 3).ToList();
                if (tokens[0][2] == 'A')
                {
                    currentPositions.Add(indexer, tokens[0]);
                    indexer++;
                }
                dict.Add(tokens[0], (tokens[1], tokens[2]));
            }

            var i = 0;
            var repetitionLengthPerPosistion = new Dictionary<int, HashSet<long>>();
            var countPerPosistion = new Dictionary<int, int>();
            for (var j = 0; j < currentPositions.Count; j++)
            {
                repetitionLengthPerPosistion.Add(j, new HashSet<long>());
                countPerPosistion.Add(j, 0);
            }
            while (currentPositions.Any(x => x.Value[2] != 'Z'))
            {
                var dfirection = directionalInput[i];
                for (var j = 0; j < currentPositions.Count; j++)
                {
                    if (dfirection == 'L')
                    {
                        currentPositions[j] = dict[currentPositions[j]].Item1;
                    }
                    else
                    {
                        currentPositions[j] = dict[currentPositions[j]].Item2;
                    }
                    countPerPosistion[j]++;
                    if (currentPositions[j][2] == 'Z')
                    {
                        repetitionLengthPerPosistion[j].Add(countPerPosistion[j]);
                        countPerPosistion[j] = 0;
                    }
                }
                i++;
                if (i == directionalInput.Length)
                {
                    i = 0;
                }
                if (repetitionLengthPerPosistion.All(x => x.Value.Count == 1))
                {
                    return repetitionLengthPerPosistion.Select(x => x.Value.First()).Aggregate((S, val) => S * val / LCM(S, val));
                }
            }
            return 0;
        }

        // stolen so hard https://www.w3resource.com/csharp-exercises/math/csharp-math-exercise-20.php
        private long LCM(long n1, long n2)
        {
            if (n2 == 0)
            {
                return n1;
            }
            else
            {
                return LCM(n2, n1 % n2);
            }
        }
    }
}