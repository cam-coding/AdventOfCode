using System;
using System.Collections.Generic;
using System.Numerics;
using AdventLibrary.Helpers.Grids;

namespace AdventLibrary.CustomObjects
{
    /// <summary>
    /// Standard form of a line Ax + By = C
    /// A or B can be zero but not both.
    /// A = 0 is horizontal line
    /// B = 0 is vertical line
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LineObject<T> : IEquatable<LineObject<T>> where T : INumber<T>
    {
        public LineObject((T y, T x) start, (T y, T x) end, bool infinite = true)
            : this(new GridLocation<T>(start.x, start.y), new GridLocation<T>(end.x, end.y), infinite)
        {
        }

        public LineObject(GridLocation<T> start, GridLocation<T> end, bool infinite = true)
        {
            var y1decimal = Convert.ToDecimal(start.Y);
            var y2decimal = Convert.ToDecimal(end.Y);
            var x1decimal = Convert.ToDecimal(start.X);
            var x2decimal = Convert.ToDecimal(end.X);
            A = y2decimal - y1decimal;
            B = x1decimal - x2decimal;
            C = A * x1decimal + B * y1decimal;

            /* if you need y=mx+b
            m = line.Slope;
            b = line.GetYIntercept()?.Y;
            */
            Infinite = infinite;
            Start = start;
            End = end;
        }

        /// <summary>
        /// y2 - y1. 0 if horizontal line.
        /// </summary>
        public decimal A { get; set; }

        /// <summary>
        /// x2 - x1. 0 if vertical line
        /// </summary>
        public decimal B { get; set; }

        /// <summary>
        /// x1y2 - x2y1. If 0 the line passes through 0,0
        /// </summary>
        public decimal C { get; set; }

        public decimal? Slope => GetSlope();

        public bool Infinite { get; set; }

        public GridLocation<T> Start { get; set; }

        public GridLocation<T> End { get; set; }

        public bool IsHoriztonalLine => A == 0;

        public bool IsVerticalLine => B == 0;

        public bool IsDiagonalLine => ((A != 0) && (B != 0));

        /// <summary>
        /// Returns null if vertical line.
        /// </summary>
        public GridLocation<decimal> GetYIntercept()
        {
            if (B == 0) return null;
            var yVal = C / B;
            return new GridLocation<decimal>(0, yVal);
        }

        /// <summary>
        /// Returns null if horizontal line.
        /// </summary>
        public GridLocation<decimal> GetXIntercept()
        {
            if (A == 0) return null;
            var xVal = C / A;
            return new GridLocation<decimal>(xVal, 0);
        }

        public bool PointOnLine(T x, T y)
        {
            return PointOnLine(new GridLocation<T>(x, y));
        }

        public bool PointOnLine(GridLocation<T> point)
        {
            if (!Infinite)
            {
                var within = (point.X <= T.Max(Start.X, End.X) &&
                point.X >= T.Min(Start.X, End.X) &&
                point.Y <= T.Max(Start.Y, End.Y) &&
                point.Y >= T.Min(Start.Y, End.Y));
                if (!within)
                {
                    return false;
                }
            }
            return PointOnLine(Start, point, End);
        }

        private bool PointOnLine(GridLocation<T> start, GridLocation<T> point, GridLocation<T> end)
        {
            var startDecimal = new GridLocation<decimal>(Convert.ToDecimal(start.X), Convert.ToDecimal(start.Y));
            var pointDecimal = new GridLocation<decimal>(Convert.ToDecimal(point.X), Convert.ToDecimal(point.Y));
            var endDecimal = new GridLocation<decimal>(Convert.ToDecimal(end.X), Convert.ToDecimal(end.Y));
            var crossproduct = (endDecimal.Y - startDecimal.Y) * (pointDecimal.X - startDecimal.X) - (endDecimal.X - startDecimal.X) * (pointDecimal.Y - startDecimal.Y);

            // floating point shananigans
            if (crossproduct != 0)
                return false;

            var dotProduct = (startDecimal.X - endDecimal.X) * (pointDecimal.Y - endDecimal.Y) - (startDecimal.Y - endDecimal.Y) * (pointDecimal.X - endDecimal.X);
            if (dotProduct != 0)
                return false;

            return true;
        }

        public GridLocation<decimal> GetLinesIntersectionPoint(LineObject<T> otherLine)
        {
            decimal delta = A * otherLine.B - otherLine.A * B;

            if (delta == 0)
                return null;

            decimal x = (otherLine.B * C - B * otherLine.C) / delta;
            decimal y = (A * otherLine.C - otherLine.A * C) / delta;
            return new GridLocation<decimal>(x, y);

            return null;
        }

        /// <summary>
        /// Needs testing. Assumes lines have starts and ends.
        /// https://www.geeksforgeeks.org/dsa/check-if-two-given-line-segments-intersect/
        /// </summary>
        /// <param name="otherLine"></param>
        /// <returns></returns>
        public bool DoLinesIntersect(LineObject<T> otherLine)
        {
            if (Infinite && otherLine.Infinite)
            {
                return GetLinesIntersectionPoint(otherLine) != null;
            }
            decimal delta = A * otherLine.B - otherLine.A * B;

            if (delta == 0)
                return false;

            // find the four orientations needed
            // for general and special cases
            int o1 = orientation(Start, End, otherLine.Start);
            int o2 = orientation(Start, End, otherLine.End);
            int o3 = orientation(otherLine.Start, otherLine.End, Start);
            int o4 = orientation(otherLine.Start, otherLine.End, End);

            // general case
            if (o1 != o2 && o3 != o4)
                return true;

            // special cases
            // p1, q1 and p2 are collinear and p2 lies on segment p1q1
            if (o1 == 0 &&
            PointOnLine(Start, otherLine.Start, End)) return true;

            // p1, q1 and q2 are collinear and q2 lies on segment p1q1
            if (o2 == 0 &&
            PointOnLine(Start, otherLine.End, End)) return true;

            // p2, q2 and p1 are collinear and p1 lies on segment p2q2
            if (o3 == 0 &&
            PointOnLine(otherLine.Start, Start, otherLine.End)) return true;

            // p2, q2 and q1 are collinear and q1 lies on segment p2q2
            if (o4 == 0 &&
            PointOnLine(otherLine.Start, End, otherLine.End)) return true;

            return false;
        }

        private decimal? GetSlope()
        {
            if (B == 0)
            {
                return null;
            }
            return (A / B) * -1;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LineObject<T>);
        }

        public bool Equals(LineObject<T> other)
        {
            return other is not null &&
                   A == other.A &&
                   B == other.B &&
                   C == other.C &&
                   Slope == other.Slope &&
                   Infinite == other.Infinite;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(A, B, C, Slope, Infinite);
        }

        // function to find orientation of ordered triplet (p, q, r)
        // 0 --> p, q and r are collinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        private static int orientation(GridLocation<T> p, GridLocation<T> q, GridLocation<T> r)
        {
            T val = (q.Y - p.Y) * (r.X - q.X) -
                      (q.X - p.X) * (r.Y - q.Y);

            // collinear
            if (val == T.Zero) return 0;

            // clock or counterclock wise
            // 1 for clockwise, 2 for counterclockwise
            return (val > T.Zero) ? 1 : 2;
        }
    }
}