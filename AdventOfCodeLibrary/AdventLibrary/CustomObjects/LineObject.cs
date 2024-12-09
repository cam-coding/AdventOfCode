using AdventLibrary.Helpers.Grids;
using System;
using System.Numerics;

namespace AdventLibrary.CustomObjects
{
    /* Standard form of a line Ax + By = C
     * A != 0 && B != 0
     * 
     * */
    public class LineObject<T> where T : INumber<T>
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
            Infinite = infinite;
        }

        public decimal A { get; set; }

        public decimal B { get; set; }

        public decimal C { get; set; }

        public decimal? Slope => GetSlope();

        public bool Infinite { get; set; }

        public GridLocation<decimal> GetYIntercept()
        {
            var yVal = C / B;
            return new GridLocation<decimal>(0, yVal);
        }

        public GridLocation<decimal> GetXIntercept()
        {
            var xVal = C / A;
            return new GridLocation<decimal>(xVal, 0);

        }
        public bool PointWithinLine(T x, T y)
        {
            return PointWithinLine(new GridLocation<T>(x, y));
        }

        public bool PointWithinLine(GridLocation<T> point)
        {
            var xDecimal = Convert.ToDecimal(point.X);
            var yDecimal = Convert.ToDecimal(point.Y);

            var result = A * xDecimal + B * yDecimal;
            if (result != C)
            {
                return false;
            }
            if (Infinite)
            {
                return true;
            }
            else
            {
                // need to do something to check it's between start and end
                return false;
            }
        }

        private decimal? GetSlope()
        {
            if (B == 0)
            {
                return null;
            }
            return (A / B) * -1;
        }
    }
}
