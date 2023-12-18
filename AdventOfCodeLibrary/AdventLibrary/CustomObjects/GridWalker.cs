using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace AdventLibrary.CustomObjects
{
    public class GridWalker
    {
        // These are static direction helpers.
        public static LocationTuple<int> Up = new LocationTuple<int>(-1, 0);
        public static LocationTuple<int> Down = new LocationTuple<int>(1, 0);
        public static LocationTuple<int> Left = new LocationTuple<int>(0, -1);
        public static LocationTuple<int> Right = new LocationTuple<int>(0, 1);
        public static List<LocationTuple<int>> Directions = new List<LocationTuple<int>>() { Up, Down, Left, Right };
        public static Dictionary<LocationTuple<int>, LocationTuple<int>> Opposites = new Dictionary<LocationTuple<int>, LocationTuple<int>>()
        {
            { Up, Down },
            { Down, Up },
            { Left, Right },
            { Right, Left },
        };

        public static LocationTuple<long> UpLong = new LocationTuple<long>(-1, 0);
        public static LocationTuple<long> DownLong = new LocationTuple<long>(1, 0);
        public static LocationTuple<long> LeftLong = new LocationTuple<long>(0, -1);
        public static LocationTuple<long> RightLong = new LocationTuple<long>(0, 1);
        public static List<LocationTuple<long>> DirectionsLong = new List<LocationTuple<long>>() { UpLong, DownLong, LeftLong, RightLong };
        public static Dictionary<LocationTuple<long>, LocationTuple<long>> OppositesLong = new Dictionary<LocationTuple<long>, LocationTuple<long>>()
        {
            { UpLong, DownLong },
            { DownLong, UpLong },
            { LeftLong, RightLong },
            { RightLong, LeftLong },
        };

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
