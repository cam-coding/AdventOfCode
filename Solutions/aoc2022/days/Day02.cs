using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2022
{
    public class Day02: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var total = 0;

            var replace = new Dictionary<char, char>()
            {
                {'X', 'A'},
                {'Y', 'B'},
                {'Z', 'C'},
            };
            var values  = new Dictionary<char, int>()
            {
                {'X', 1},
                {'Y', 2},
                {'Z', 3},
            };
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);

                total += tokens[1][0] - 'W';
                total += MatchResult(tokens[0][0], replace[tokens[1][0]]);
			}
            return total;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var total = 0;

            var plays = new Dictionary<char, string>()
            {
                {'A', "BAC"},
                {'B', "CBA"},
                {'C', "ACB"},
            };
            var values  = new Dictionary<char, int>()
            {
                {'X', 2},
                {'Y', 1},
                {'Z', 0},
            };
            var points = new Dictionary<char, int>()
            {
                {'A', 1},
                {'B', 2},
                {'C', 3},
            };
            var points2 = new Dictionary<int, int>()
            {
                {0, 6},
                {1, 3},
                {2, 0},
            };
			
			foreach (var line in lines)
			{
                var tokens = line.Split(delimiterChars);

                var pos = values[tokens[1][0]];
                var myPlay = plays[tokens[0][0]][pos];
                total += points[myPlay];
                total += points2[pos];
			}
            return total;
        }

        private int MatchResult(char opp, char me)
        {
            if (me == opp)
            {
                return 3;
            }
            return Win(me, opp) ? 6 : 0 ;
        }

        private bool Win(char me, char opp)
        {
            if (opp == 'A')
            {
                return me == 'B';
            }
            else if (opp == 'B')
            {
                return me == 'C';
            }
            else // (opp == 'C')
            {
                return me == 'A';
            }
        }
    }
}