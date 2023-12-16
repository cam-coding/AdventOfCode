using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventLibrary.CustomObjects
{
    public class GridWalker
    {
        public static LocationTuple<int> Up = new LocationTuple<int>(-1, 0);
        public static LocationTuple<int> Down = new LocationTuple<int>(1, 0);
        public static LocationTuple<int> Left = new LocationTuple<int>(0, -1);
        public static LocationTuple<int> Right = new LocationTuple<int>(0, 1);

        public GridWalker((int, int) current, LocationTuple<int> direction)
        {
            Current = new LocationTuple<int>(current.Item1, current.Item2);
            Direction = direction;

            // hmm this assums you want to count the starting position.
            Path = new List<(LocationTuple<int>, LocationTuple<int>)>() { (Current, Direction) };
            History = new HashSet<(LocationTuple<int>, LocationTuple<int>)>() { (Current, Direction) };
            Looping = false;
            OutOfBounds = false;
            Previous = Current - direction;

            Speed = 1;
        }

        public GridWalker(GridWalker walker)
        {
            Current = walker.Current;
            Direction = walker.Direction;
            Path = walker.Path;
            History = walker.History;
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

        public int Y => Current.Item1;

        public int X => Current.Item2;

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

    public record LocationTuple<T>(T Item1, T Item2) : IComparable<LocationTuple<T>> where T : INumber<T>, INumberBase<T>
    {
        // stolen from https://github.com/boombuler/adventofcode/blob/master/Utils/Point2D.cs
        // Allows math using my Tuple Class
        public static LocationTuple<T> operator -(LocationTuple<T> a)
        => new(-a.Item1, -a.Item2);
        public static LocationTuple<T> operator -(LocationTuple<T> a, LocationTuple<T> b)
            => new(a.Item1 - b.Item1, a.Item2 - b.Item2);
        public static LocationTuple<T> operator +(LocationTuple<T> a, LocationTuple<T> b)
            => new(a.Item1 + b.Item1, a.Item2 + b.Item2);
        public static LocationTuple<T> operator *(LocationTuple<T> a, T b)
            => new(a.Item1 * b, a.Item2 * b);
        public static LocationTuple<T> operator *(T a, LocationTuple<T> b)
            => b * a;
        public static LocationTuple<T> operator /(LocationTuple<T> a, T b)
            => new(a.Item1 / b, a.Item2 / b);
        public static LocationTuple<T> operator %(LocationTuple<T> a, T b)
            => new(a.Item1 % b, a.Item2 % b);
        public static LocationTuple<T> operator %(LocationTuple<T> a, LocationTuple<T> b)
            => new(a.Item1 % b.Item1, a.Item2 % b.Item2);

        public int CompareTo(LocationTuple<T> other)
        {
            var dy = Item2 - other.Item2;
            if (dy == T.Zero)
                return T.Sign(Item1 - other.Item1);
            return T.Sign(dy);
        }
    }
}
