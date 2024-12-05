using System;
using System.Numerics;

namespace AdventLibrary.Helpers.Grids
{

    public record GridLocation<T>(T X, T Y) : IComparable<GridLocation<T>> where T : INumber<T>, INumberBase<T>
    {
        // stolen from https://github.com/boombuler/adventofcode/blob/master/Utils/Point2D.cs
        // Allows math using my Tuple Class
        public static GridLocation<T> operator -(GridLocation<T> a)
        => new(-a.X, -a.Y);
        public static GridLocation<T> operator -(GridLocation<T> a, GridLocation<T> b)
            => new(a.X - b.X, a.Y - b.Y);
        public static GridLocation<T> operator +(GridLocation<T> a, GridLocation<T> b)
            => new(a.X + b.X, a.Y + b.Y);
        public static GridLocation<T> operator *(GridLocation<T> a, T b)
            => new(a.X * b, a.Y * b);
        public static GridLocation<T> operator *(T a, GridLocation<T> b)
            => b * a;
        public static GridLocation<T> operator /(GridLocation<T> a, T b)
            => new(a.X / b, a.Y / b);
        public static GridLocation<T> operator %(GridLocation<T> a, T b)
            => new(a.X % b, a.Y % b);
        public static GridLocation<T> operator %(GridLocation<T> a, GridLocation<T> b)
            => new(a.X % b.X, a.Y % b.Y);

        public int CompareTo(GridLocation<T> other)
        {
            var dy = Y - other.Y;
            if (dy == T.Zero)
                return T.Sign(X - other.X);
            return T.Sign(dy);
        }

        public override int GetHashCode()
        {
            int result = 37;

            result *= 397;
            result += X.GetHashCode();
            result *= 397;
            result += Y.GetHashCode();
            result *= 397;

            return result;
        }
    }
}
