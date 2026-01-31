using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2025
{
    public class Day05 : ISolver
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

            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var listy = new List<(long, long)>();

            foreach (var line in groups[0])
            {
                var nums = line.GetLongsFromString();
                listy.Add((nums[0], nums[1]));
            }

            foreach (var line in groups[1])
            {
                var nums = line.GetLongsFromString();
                if (listy.Any(x => nums[0] >= x.Item1 && nums[0] <= x.Item2))
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

            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var freshRanges = new List<(long, long)>();

            foreach (var line in groups[0])
            {
                var nums = line.GetLongsFromString();
                var willInsert = false;
                var insertTuple = (nums[0], nums[1]);

                while (willInsert == false)
                {
                    willInsert = true;
                    var low = Math.Min(insertTuple.Item1, insertTuple.Item2);
                    var high = Math.Max(insertTuple.Item1, insertTuple.Item2);
                    for (var i = 0; i < freshRanges.Count; i++)
                    {
                        (long, long) tempTuple = (0, 0);
                        if (MathHelper.InRange_Inclusive(freshRanges[i], low))
                        {
                            tempTuple = freshRanges[i];
                            tempTuple.Item2 = Math.Max(tempTuple.Item2, high);
                        }
                        else if (MathHelper.InRange_Inclusive(freshRanges[i], high))
                        {
                            tempTuple = freshRanges[i];
                            tempTuple.Item1 = Math.Min(tempTuple.Item1, low);
                        }
                        else if (MathHelper.InRange_Inclusive(insertTuple, freshRanges[i]))
                        {
                            tempTuple = insertTuple;
                        }

                        if (tempTuple != (0, 0))
                        {
                            freshRanges.RemoveAt(i);
                            insertTuple = tempTuple;
                            willInsert = false;
                            break;
                        }
                    }
                    if (willInsert)
                    {
                        freshRanges.Add(insertTuple);
                    }
                }
            }

            return freshRanges.Sum(x => (x.Item2 - x.Item1) + 1);
        }
    }
}