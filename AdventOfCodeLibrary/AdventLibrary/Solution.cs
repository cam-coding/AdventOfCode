using System;

namespace AdventLibrary
{
    public class Solution
    {
        public Solution(object part1, object part2)
        {
            Part1 = part1;
            Part2 = part2;
        }
        public Solution(object part1, object part2, TimeSpan span1, TimeSpan span2)
        {
            Part1 = part1;
            Part2 = part2;
            Span1 = span1;
            Span2 = span2;
        }
        public Solution(object part1, TimeSpan span1, object part2, TimeSpan span2)
        {
            Part1 = part1;
            Part2 = part2;
            Span1 = span1;
            Span2 = span2;
        }

        public object Part1;

        public object Part2;

        public TimeSpan? Span1;

        public TimeSpan? Span2;

        public void Output2()
        {
            Console.WriteLine("Part 1: " + Part1.ToString());
            Console.WriteLine("Part 2: " + Part2.ToString());
        }

        public void Output()
        {
            var str = "Part 1: " + Part1.ToString() + "\n";
            str += OutputRunTime(Span1);
            str += "Part 2: " + Part2.ToString() + "\n";
            str += OutputRunTime(Span2);
            Console.WriteLine(str);
        }

        private string OutputRunTime(TimeSpan? timeSpan)
        {
            if (timeSpan != null)
            {
                if (timeSpan.Value.Milliseconds > 1000)
                {
                    return string.Format("{0}:{1}\n", Math.Floor(timeSpan.Value.TotalMinutes), timeSpan.Value.ToString("ss\\.ff"));
                }
                else
                {
                    return $"RunTime in miliseconds: {timeSpan.Value.Milliseconds}\n";
                }
            }
            return string.Empty;
        }
    }
}