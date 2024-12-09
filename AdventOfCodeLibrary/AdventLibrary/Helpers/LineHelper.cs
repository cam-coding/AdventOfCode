using System;
using System.Numerics;
using AdventLibrary.CustomObjects;

namespace AdventLibrary.Helpers
{
    public static class LineHelper<T> where T : INumber<T>
    {
        public static bool DoLinesIntersect(LineObject<T> line1, LineObject<T> line2)
        {
            var delta = line1.A * line2.B - line2.A * line1.B;
            if (delta == 0)
                return false;
            return true;
        }

        public static (decimal y, decimal x) FindIntersectionPoint(LineObject<T> line1, LineObject<T> line2)
        {
            var delta = line1.A * line2.B - line2.A * line1.B;
            if (delta == 0)
                throw new Exception("Lines do not intersect");
            var x = (line2.B * line1.C - line1.B * line2.C) / delta;
            var y = (line1.A * line2.C - line2.A * line1.C) / delta;

            return (y, x);
        }
    }
}
