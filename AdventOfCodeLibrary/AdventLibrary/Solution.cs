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

        public object Part1;

        public object Part2;

        public void Output()
        {
            Console.WriteLine("Part 1: " + Part1.ToString());
            Console.WriteLine("Part 2: " + Part2.ToString());
        }
    }
}