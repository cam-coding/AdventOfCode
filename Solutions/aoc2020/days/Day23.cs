using AdventLibrary;
using AdventLibrary.Extensions;

namespace aoc2020
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
            var magic = 9;

            var lookup = new List<int>();
            lookup.FillEmptyListWithValue(0, 10);
            var line = lines[0];

            for (var i = 0; i < lines[0].Count() - 1; i++)
            {
                var val = int.Parse(line[i].ToString());
                var index = line.IndexOf(val.ToString());
                lookup[val] = int.Parse(line[index + 1].ToString());
            }

            /*
            var val2 = line.Last();
            var index2 = line.IndexOf(val2.ToString());
            lookup[int.Parse(val2.ToString())] = 10;

            for (var i = 10; i < magic + 1; i++)
            {
                lookup.Add(i + 1);
            }
            var val3 = int.Parse(line[0].ToString());
            lookup[magic] = val3;
            var currentIndex = val3;*/
            var lastVal = int.Parse(line.Last().ToString());
            var firstVal = int.Parse(line[0].ToString());
            lookup[lastVal] = firstVal;
            var currentIndex = firstVal;

            Console.WriteLine(GetOutput(lookup));

            for (var i = 0; i < 100; i++)
            {
                var cups = new List<(int cup, int nextCup)>();
                cups.Add((lookup[currentIndex], lookup[lookup[currentIndex]]));
                cups.Add((lookup[cups[0].cup], lookup[cups[0].nextCup]));
                cups.Add((lookup[cups[1].cup], lookup[cups[1].nextCup]));
                lookup[currentIndex] = cups[2].nextCup;

                var dest = GetDestinationIndex(currentIndex, magic, cups.Select(x => x.cup).ToList());
                var tempNext = lookup[dest];
                lookup[dest] = cups[0].cup;
                lookup[cups[2].cup] = tempNext;
                currentIndex = lookup[currentIndex];
            }
            return GetOutput(lookup);
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;

            var magic = 1000000;

            var lookup = new List<int>();
            lookup.FillEmptyListWithValue(0, 10);
            var line = lines[0];

            for (var i = 0; i < lines[0].Count() - 1; i++)
            {
                var val = int.Parse(line[i].ToString());
                var index = line.IndexOf(val.ToString());
                lookup[val] = int.Parse(line[index + 1].ToString());
            }

            var val2 = line.Last();
            var index2 = line.IndexOf(val2.ToString());
            lookup[int.Parse(val2.ToString())] = 10;

            for (var i = 10; i < magic + 1; i++)
            {
                lookup.Add(i + 1);
            }
            var val3 = int.Parse(line[0].ToString());
            lookup[magic] = val3;
            var currentIndex = val3;

            for (var i = 0; i < 10000000; i++)
            {
                var cups = new List<(int cup, int nextCup)>();
                cups.Add((lookup[currentIndex], lookup[lookup[currentIndex]]));
                cups.Add((lookup[cups[0].cup], lookup[cups[0].nextCup]));
                cups.Add((lookup[cups[1].cup], lookup[cups[1].nextCup]));
                lookup[currentIndex] = cups[2].nextCup;

                var dest = GetDestinationIndex(currentIndex, magic, cups.Select(x => x.cup).ToList());
                var tempNext = lookup[dest];
                lookup[dest] = cups[0].cup;
                lookup[cups[2].cup] = tempNext;
                currentIndex = lookup[currentIndex];
            }
            int output1 = lookup[1];
            long output2 = lookup[output1];
            return (long)output1 * output2;
        }

        private int GetDestinationIndex(int currentIndex, int maxSize, List<int> cups)
        {
            var destination = currentIndex;
            for (var i = 0; i < 4; i++)
            {
                destination = destination - 1;
                if (destination == 0)
                {
                    destination = maxSize;
                }
                if (!cups.Contains(destination))
                {
                    return destination;
                }
            }
            return -1;
        }

        private string GetOutput(List<int> lookup)
        {
            var index = lookup[1];
            var output = "";
            for (var i = 1; i < lookup.Count - 1; i++)
            {
                output += index;
                index = lookup[index];
            }
            return output;
        }
    }
}