using System;
using System.Numerics;
using AdventLibrary.CustomObjects;

namespace AdventLibrary
{
    public static class LineHelper<T> where T : INumber<T>
    {
        public static bool DoLinesIntersect(LineObject<T> line1, LineObject<T> line2, T zero)
        {
            T delta = line1.A * line2.B - line2.A * line1.B;
            if (delta == zero)
                return false;
            return true;
        }

        public static (T y, T x) FindIntersectionPoint(LineObject<T> line1, LineObject<T> line2, T zero)
        {
            T delta = line1.A * line2.B - line2.A * line1.B;
            if (delta == zero)
                throw new Exception("Lines do not intersect");
            T x = (line2.B * line1.C - line1.B * line2.C) / delta;
            T y = (line1.A * line2.C - line2.A * line1.C) / delta;

            return (y, x);
        }
    }
}
