using AdventLibrary.CustomObjects;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventLibrary.Helpers.Grids
{
    public static class GridLocationHelper
    {
        public static double GetDistanceBetween<T>(GridLocation<T> point1, GridLocation<T> point2) where T : INumber<T>
        {
            var delta = point1 - point2;
            return MathHelper.PythagoreanTheorem<double>(Math.Abs(Convert.ToDouble(delta.X)), Math.Abs(Convert.ToDouble(delta.Y)));
        }

        // needs unit tests
        public static bool DoThesePointsMakeALine<T>(List<GridLocation<T>> points) where T : INumber<T>
        {
            var lines = new List<LineObject<T>>();
            for (var i = 0; i < points.Count-1; i++)
            {
                lines.Add(new LineObject<T>(points[i], points[i + 1]));
            }
            var valid = true;
            var slope = lines[0].Slope;
            for (var i = 1; i < lines.Count; i++)
            {
                var slope2 = lines[i].Slope;
                if (slope2 != slope)
                {
                    valid = false;
                }
            }

            return valid;
        }
    }
}
