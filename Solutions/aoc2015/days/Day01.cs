using System;
using AdventLibrary;

namespace aoc2015
{
    public class Day01: ISolver
    {
        public Solution Solve(string input)
        {
            AdventLibrary.SolveProblemsGuessingAnswers.SolveStackBasedProblemWithNegatives(input);
            return new Solution(Part1(input), Part2(input));
        }

        private object Part1(string input)
        {
            var floor = 0;
            foreach (var character in input)
            {
                if (character == '(')
                {
                    floor++;
                }
                else if (character == ')')
                {
                    floor--;
                }
            }

            return floor;
        }
        
        private object Part2(string input)
        {
            var floor = 0;
            var count = 0;
            foreach (var character in input)
            {
                if (character == '(')
                {
                    floor++;
                }
                else if (character == ')')
                {
                    floor--;
                }

                count++;

                if (floor == -1)
                {
                    return count;
                }
            }

            return count;
        }
    }
}
