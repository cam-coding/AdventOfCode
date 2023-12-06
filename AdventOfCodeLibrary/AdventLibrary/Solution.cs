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

        public object Part1;

        public object Part2;

        public TimeSpan? Span1;

        public TimeSpan? Span2;

        public void Output()
        {
            Console.WriteLine("Part 1: " + Part1.ToString());
            Console.WriteLine("Part 2: " + Part2.ToString());
        }

        public void Output2()
        {
            if (Span1 != null)
            {
                if (Span1.Value.Milliseconds > 1000)
                {
                    Console.WriteLine(string.Format("{0}:{1}", Math.Floor(Span1.Value.TotalMinutes), Span1.Value.ToString("ss\\.ff")));
                }
                else
                {
                    Console.WriteLine(Span1.Value.Milliseconds);
                }
            }
            Console.WriteLine("Part 1: " + Part1.ToString());
            Console.WriteLine("Part 2: " + Part2.ToString());
        }
    }
}