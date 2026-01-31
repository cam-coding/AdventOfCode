using AdventLibrary;

namespace aoc2022
{
    public class Day01 : ISolver
    {
        public Solution Solve(string filePath, bool isTest = false)
        {
            return new Solution(Part1(filePath), Part2(filePath));
        }

        private object Part1(string filePath)
        {
            var increased = 0;
            var previous = int.MaxValue;
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(filePath);

            var total = 0;
            var i = 0;
            var counter = new List<int>();
            counter.Add(0);

            foreach (var line in lines)
            {
                if (!line.Equals(string.Empty))
                {
                    counter[i] += Int32.Parse(line);
                }
                else
                {
                    i++;
                    counter.Add(0);
                }
            }

            return counter.Max();
        }

        private object Part2(string filePath)
        {
            var increased = 0;
            var previous = int.MaxValue;
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(filePath);

            var total = 0;
            var i = 0;
            var counter = new List<int>();
            counter.Add(0);

            foreach (var line in lines)
            {
                if (!line.Equals(string.Empty))
                {
                    counter[i] += Int32.Parse(line);
                }
                else
                {
                    i++;
                    counter.Add(0);
                }
            }

            counter.Sort();

            return counter[counter.Count - 1] + counter[counter.Count - 3] + counter[counter.Count - 2];
        }
    }
}
