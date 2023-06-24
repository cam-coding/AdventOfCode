using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day15: ISolver
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
            var best = 0;
            for (var i = 100; i > 0; i--)
            {
                for (var j = 0; j <= (100 - i); j++)
                {
                    for (var k = 0; k <= ((100 - j) - i); k++)
                    {
                        for (var m  = 0; m <= (((100 - j) - i) -k); m++)
                        {
                            var result = Calc(i, j, k, m);
                            if (result > best)
                            {
                                best = result;
                            }
                        }
                    }
                }
            }

            return best;

            /*var frosting = 0;
            var pb = 0;
            var sprinkles = 0;
            var sugar = 0;
            var frostingMin = 1;
            var pbMin = Math.Max(sprinkles, frosting) / 3 + 1;
            var sugarMin = 1;
            var sprinklesMin = Math.Max(pb, sugar) / 5 + 1;

            var frosting2 = 100;
            var sugar2 = 1;
            var sprinklesMin2 = (Math.Max(sprinkles, frosting) / 3 + 1) / 5 + 1;
            var pbMin2 = Math.Max(sprinkles, frosting) / 3 + 1;
            */
        }

        private int Calc(int frosting, int pb, int sugar, int sprinkles)
        {
            var sprinklesMin = Math.Max(pb, sugar) / 5 + 1;
            var pbMin = Math.Max(sprinkles, frosting) / 3 + 1;

            if (pb >= pbMin && sprinkles >= sprinklesMin)
            {
                if (sugar + frosting + sprinkles + pb == 100)
                {
                    var cap = sprinkles * 5 - pb - sugar;
                    var dur = pb * 3 - sprinkles - frosting;
                    var flav = frosting * 4;
                    var text = sugar * 2;
                    var cal = sprinkles * 5 + pb + frosting * 6 + sugar * 8;
                    if (cap <= 0 || dur <= 0 || flav <= 0 || text <= 0 || cal != 500)
                    {
                        return -1;
                    }
                    return cap * dur * flav * text;
                }
            }
            return -1;
        }
        
        private object Part2()
        {
            return 0;
        }
    }
}