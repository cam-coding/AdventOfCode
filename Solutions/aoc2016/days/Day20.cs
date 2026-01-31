using AdventLibrary;

namespace aoc2016
{
    public class Day20 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        private List<(long start, long end)> tempRanges;

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var ranges = new List<(long start, long end)>
            {
                (0, 4294967295)
            };

            foreach (var line in lines)
            {
                tempRanges = new List<(long start, long end)>();
                var nums = line.GetLongsFromString();
                var low = nums[0];
                var high = nums[1];

                var i = 0;
                while (i < ranges.Count())
                {
                    var current = ranges[i];
                    var remove = false;
                    if (current.start >= low)
                    {
                        if (current.end <= high)
                        {
                            remove = true;
                        }
                        else if (current.start <= high)
                        {
                            remove = true;
                            AddToRange(high + 1, current.end);
                        }
                    }
                    else if (low <= current.end)
                    {
                        if (high < current.end)
                        {
                            remove = true;
                            AddToRange(current.start, low - 1);
                            AddToRange(high + 1, current.end);
                        }
                        else
                        {
                            remove = true;
                            AddToRange(current.start, low - 1);
                        }
                    }

                    if (remove)
                    {
                        ranges.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

                ranges.AddRange(tempRanges);
            }
            var min = long.MaxValue;

            foreach (var range in ranges)
            {
                if (range.start < min)
                    min = range.start;
            }
            return min;
        }

        private void AddToRange(long low, long high)
        {
            if (low <= high)
            {
                tempRanges.Add((low, high));
            }
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var ranges = new List<(long start, long end)>
            {
                (0, 4294967295)
            };

            foreach (var line in lines)
            {
                tempRanges = new List<(long start, long end)>();
                var nums = line.GetLongsFromString();
                var low = nums[0];
                var high = nums[1];

                var i = 0;
                while (i < ranges.Count())
                {
                    var current = ranges[i];
                    var remove = false;
                    if (current.start >= low)
                    {
                        if (current.end <= high)
                        {
                            remove = true;
                        }
                        else if (current.start <= high)
                        {
                            remove = true;
                            AddToRange(high + 1, current.end);
                        }
                    }
                    else if (low <= current.end)
                    {
                        if (high < current.end)
                        {
                            remove = true;
                            AddToRange(current.start, low - 1);
                            AddToRange(high + 1, current.end);
                        }
                        else
                        {
                            remove = true;
                            AddToRange(current.start, low - 1);
                        }
                    }

                    if (remove)
                    {
                        ranges.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

                ranges.AddRange(tempRanges);
            }
            long total = 0;
            foreach (var range in ranges)
            {
                total = total + (range.end - range.start) + 1;
            }
            return total;
        }
    }
}