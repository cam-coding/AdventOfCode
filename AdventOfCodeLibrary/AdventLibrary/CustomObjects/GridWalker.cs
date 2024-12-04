using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace AdventLibrary.CustomObjects
{
    public class GridWalker
    {
        public GridWalker((int, int) current, LocationTuple<int> direction, int speed = 1)
        {
            Current = new LocationTuple<int>(current.Item1, current.Item2);
            Direction = direction;
            Speed = speed;

            // hmm this assums you want to count the starting position.
            Path = new List<(LocationTuple<int>, LocationTuple<int>)>() { (Current, Direction) };
            History = new HashSet<(LocationTuple<int>, LocationTuple<int>)>() { (Current, Direction) };
            Looping = false;
            OutOfBounds = false;
            Previous = Current - direction;
        }

        public GridWalker(GridWalker walker)
        {
            Current = walker.Current;
            Direction = walker.Direction;
            Path = new List<(LocationTuple<int>, LocationTuple<int>)>(walker.Path);
            History = new HashSet<(LocationTuple<int>, LocationTuple<int>)>(walker.History);
            Looping = walker.Looping;
            OutOfBounds = walker.OutOfBounds;
            Previous = walker.Previous;
            Speed = walker.Speed;
        }

        public LocationTuple<int> Current { get; set; }

        public LocationTuple<int> Previous { get; set; }

        public LocationTuple<int> Direction { get; set; }

        public List<(LocationTuple<int>, LocationTuple<int>)> Path { get; set; }

        public HashSet<(LocationTuple<int>, LocationTuple<int>)> History { get; set; }

        public int PathLength => Path.Count;

        public int UniqueLocationsVisited => History.Count;

        public bool Looping { get; set; }

        public bool OutOfBounds { get; set; }

        public int Speed { get; set; }

        public int Y => Current.Y;

        public int X => Current.X;

        public void Walk()
        {
            Previous = Current;
            Current = Current + (Direction*Speed);

            var key = (Current, Direction);
            Path.Add(key);
            if (!History.Add(key))
            {
                Looping = true;
            }
        }
    }

    public record LocationTuple<T>(T X, T Y) : IComparable<LocationTuple<T>> where T : INumber<T>, INumberBase<T>
    {
        // stolen from https://github.com/boombuler/adventofcode/blob/master/Utils/Point2D.cs
        // Allows math using my Tuple Class
        public static LocationTuple<T> operator -(LocationTuple<T> a)
        => new(-a.X, -a.Y);
        public static LocationTuple<T> operator -(LocationTuple<T> a, LocationTuple<T> b)
            => new(a.X - b.X, a.Y - b.Y);
        public static LocationTuple<T> operator +(LocationTuple<T> a, LocationTuple<T> b)
            => new(a.X + b.X, a.Y + b.Y);
        public static LocationTuple<T> operator *(LocationTuple<T> a, T b)
            => new(a.X * b, a.Y * b);
        public static LocationTuple<T> operator *(T a, LocationTuple<T> b)
            => b * a;
        public static LocationTuple<T> operator /(LocationTuple<T> a, T b)
            => new(a.X / b, a.Y / b);
        public static LocationTuple<T> operator %(LocationTuple<T> a, T b)
            => new(a.X % b, a.Y % b);
        public static LocationTuple<T> operator %(LocationTuple<T> a, LocationTuple<T> b)
            => new(a.X % b.X, a.Y % b.Y);

        public int CompareTo(LocationTuple<T> other)
        {
            var dy = Y - other.Y;
            if (dy == T.Zero)
                return T.Sign(X - other.X);
            return T.Sign(dy);
        }
    }
}
