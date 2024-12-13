using System;
using System.Collections.Generic;
using AdventLibrary;
using AdventLibrary.CustomObjects;
using AdventLibrary.Helpers;
using Microsoft.Z3;

namespace aoc2023
{
    public class Day24: ISolver
    {
        private string _filePath;
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(isTest), Part2(isTest));
        }

        private object Part1(bool isTest)
        {
            var rangeMin = isTest ? 7 : 200000000000000;
            var rangeMax = isTest ? 27 : 400000000000000;
            var lines = ParseInput.GetLinesFromFile(_filePath);
            long counter = 0;

            var lins = new List<LineObject<decimal>>();
            var minMax = new Dictionary<LineObject<decimal>, (decimal, decimal, decimal, decimal)>();
            foreach (var line in lines)
            {
                var nums = StringParsing.GetLongsWithNegativesFromString(line);
                long x1 = nums[0];
                long y1 = nums[1];
                long x2 = x1 + nums[3];
                long y2 = y1 + nums[4];

                var minX = nums[3] <= 0 ? long.MinValue : x1;
                var maxX = nums[3] <= 0 ? x1 : long.MaxValue;
                var minY = nums[4] <= 0 ? long.MinValue : y1;
                var maxY = nums[4] <= 0 ? y1 : long.MaxValue;

                var lineObject = new LineObject<decimal>((y1, x1), (y2, x2), true);
                minMax.Add(lineObject, (minX, maxX, minY, maxY));
                lins.Add(lineObject);
            }
            for (var i = 0; i < lins.Count; i++)
            {
                for (var j = i + 1; j < lins.Count; j++)
                {
                    if (LineHelper<decimal>.DoLinesIntersect(lins[i], lins[j]))
                    {
                        var coords = LineHelper<decimal>.FindIntersectionPoint(lins[i], lins[j]);
                        var x = coords.x;
                        var y = coords.y;
                        if (rangeMin <= x && x <= rangeMax &&
                            rangeMin <= y && y <= rangeMax)
                        {
                            if (Handle(minMax, lins[i], x, y) && Handle(minMax, lins[j], x, y))
                            {
                                counter++;
                            }
                        }
                    }
                }
            }
            return counter;
        }

        private bool Handle(Dictionary<LineObject<decimal>, (decimal minX, decimal maxX, decimal minY, decimal maxY)> minMax, LineObject<decimal> line, decimal x, decimal y)
        {
            var tup = minMax[line];
            return x >= tup.minX && x <= tup.maxX &&
                y >= tup.minY && y <= tup.maxY;
        }

        private object Part2(bool isTest)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var allHailstones = new List<Hailstone>();

            foreach (var line in lines)
            {
                var nums = StringParsing.GetLongsWithNegativesFromString(line);
                var hailstone = new Hailstone(nums[0], nums[1], nums[2], nums[3], nums[4], nums[5]);
                allHailstones.Add(hailstone);
            }
            //neat lil program called Z3 for the answer;
            var context = new Context();
            var solver = context.MkSolver();

            // the following are the 6 values we are solving for
            // Coordinates of the stone
            var uknownStoneStartingX = context.MkIntConst("x");
            var unkownStoneStartingY = context.MkIntConst("y");
            var unkownStoneStartingZ = context.MkIntConst("z");

            // Velocity of the stone
            var stoneVelocityX = context.MkIntConst("vx");
            var stoneVelocityY = context.MkIntConst("vy");
            var stoneVelocityZ = context.MkIntConst("vz");

            for (var i = 0; i < 3; i++)
            {
                var currentTime = context.MkIntConst($"t{i}");
                var hail = allHailstones[i];

                var hailStartingX = context.MkInt(hail.X);
                var hailStartingY = context.MkInt(hail.Y);
                var hailStartingZ = context.MkInt(hail.Z);

                var hailVelocityX = context.MkInt(hail.VelocityX);
                var hailVelocityY = context.MkInt(hail.VelocityY);
                var hailVelocityZ = context.MkInt(hail.VelocityZ);

                // give the formula for calculating these values
                var stoneCurrentX = context.MkAdd(uknownStoneStartingX, context.MkMul(currentTime, stoneVelocityX));
                var stoneCurrentY = context.MkAdd(unkownStoneStartingY, context.MkMul(currentTime, stoneVelocityY));
                var stoneCurrentZ = context.MkAdd(unkownStoneStartingZ, context.MkMul(currentTime, stoneVelocityZ));

                // give the formula for calculating these values
                var hailCurrentX = context.MkAdd(hailStartingX, context.MkMul(currentTime, hailVelocityX));
                var hailCurrentY = context.MkAdd(hailStartingY, context.MkMul(currentTime, hailVelocityY));
                var hailCurrentZ = context.MkAdd(hailStartingZ, context.MkMul(currentTime, hailVelocityZ));

                solver.Add(currentTime >= 0); // annoying wrinkle of only looking forward in time
                solver.Add(context.MkEq(stoneCurrentX, hailCurrentX));
                solver.Add(context.MkEq(stoneCurrentY, hailCurrentY));
                solver.Add(context.MkEq(stoneCurrentZ, hailCurrentZ));
            }

            solver.Check();
            var model = solver.Model;

            var stoneStartingX = model.Eval(uknownStoneStartingX).ToString();
            var stoneStartingY = model.Eval(unkownStoneStartingY).ToString();
            var stoneStartingZ = model.Eval(unkownStoneStartingZ).ToString();

            return Convert.ToInt64(stoneStartingX) + Convert.ToInt64(stoneStartingY) + Convert.ToInt64(stoneStartingZ);
        }

        private class Hailstone
        {
            public Hailstone(long x, long y, long z, long velocityX, long velocityY, long velocityZ)
            {
                X = x;
                Y = y;
                Z = z;
                VelocityX = velocityX;
                VelocityY = velocityY;
                VelocityZ = velocityZ;
            }

            public long X { get; set; }
            public long Y { get; set; }
            public long Z { get; set; }

            public long VelocityX { get; set; }
            public long VelocityY { get; set; }
            public long VelocityZ { get; set; }

        }
    }
}