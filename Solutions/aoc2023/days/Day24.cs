using System;
using System.Collections.Generic;
using AdventLibrary;
using AdventLibrary.CustomObjects;
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

                var lineObject = new LineObject<decimal>(y1, x1, y2, x2);
                minMax.Add(lineObject, (minX, maxX, minY, maxY));
                lins.Add(lineObject);
            }
            for (var i = 0; i < lins.Count; i++)
            {
                for (var j = i + 1; j < lins.Count; j++)
                {
                    if (LineHelper<decimal>.DoLinesIntersect(lins[i], lins[j],0))
                    {
                        var coords = LineHelper<decimal>.FindIntersectionPoint(lins[i], lins[j], 0);
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

            // Coordinates of the stone
            var constantX = context.MkIntConst("x");
            var constantY = context.MkIntConst("y");
            var constantZ = context.MkIntConst("z");

            // Velocity of the stone
            var constantVelocityX = context.MkIntConst("vx");
            var constantVelocityY = context.MkIntConst("vy");
            var constantVelocityZ = context.MkIntConst("vz");

            for (var i = 0; i < 3; i++)
            {
                var currentTime = context.MkIntConst($"t{i}");
                var hail = allHailstones[i];

                var xPoint = context.MkInt(hail.X);
                var yPoint = context.MkInt(hail.Y);
                var zPoint = context.MkInt(hail.Z);

                var velocityX = context.MkInt(hail.VelocityX);
                var velocityY = context.MkInt(hail.VelocityY);
                var velocityZ = context.MkInt(hail.VelocityZ);

                var x1 = context.MkAdd(constantX, context.MkMul(currentTime, constantVelocityX));
                var y1 = context.MkAdd(constantY, context.MkMul(currentTime, constantVelocityY));
                var z1 = context.MkAdd(constantZ, context.MkMul(currentTime, constantVelocityZ));

                var x2 = context.MkAdd(xPoint, context.MkMul(currentTime, velocityX));
                var y2 = context.MkAdd(yPoint, context.MkMul(currentTime, velocityY));
                var z2 = context.MkAdd(zPoint, context.MkMul(currentTime, velocityZ));

                solver.Add(currentTime >= 0); // annoying wrinkle of only looking forward in time
                solver.Add(context.MkEq(x1, x2));
                solver.Add(context.MkEq(y1, y2));
                solver.Add(context.MkEq(z1, z2));
            }

            solver.Check();
            var model = solver.Model;

            var rx = model.Eval(constantX).ToString();
            var ry = model.Eval(constantY).ToString();
            var rz = model.Eval(constantZ).ToString();

            return Convert.ToInt64(rx) + Convert.ToInt64(ry) + Convert.ToInt64(rz);
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