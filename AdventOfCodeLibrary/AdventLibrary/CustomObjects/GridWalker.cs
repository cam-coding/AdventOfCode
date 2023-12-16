using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventLibrary.CustomObjects
{
    public class GridWalker
    {
        public static WalkerDirection<int> Up = new WalkerDirection<int>(-1, 0);
        public static WalkerDirection<int> Down = new WalkerDirection<int>(1, 0);
        public static WalkerDirection<int> Left = new WalkerDirection<int>(0, -1);
        public static WalkerDirection<int> Right = new WalkerDirection<int>(0, 1);

        public GridWalker((int,int) current, WalkerDirection<int> direction)
        {
            Current = new WalkerDirection<int>(current.Item1, current.Item2);
            Direction = direction;

            // hmm this assums you want to count the starting position.
            Path = new List<(WalkerDirection<int>, WalkerDirection<int>)>() { (Current, Direction) };
            History = new HashSet<(WalkerDirection<int>, WalkerDirection<int>)>() { (Current, Direction) };
            Looping = false;
            Speed = 1;
        }

        public WalkerDirection<int> Current { get; set; }

        public WalkerDirection<int> Direction { get; set; }

        public List<(WalkerDirection<int>, WalkerDirection<int>)> Path { get; set; }

        public HashSet<(WalkerDirection<int>, WalkerDirection<int>)> History { get; set; }

        public int PathLength => Path.Count;

        public int UniqueLocationsVisited => History.Count;

        public bool Looping { get; set; }

        public int Speed { get; set; }

        public void Walk()
        {
            Current = Current + (Direction*Speed);
            Path.Add((Current, Direction));
        }
    }

    public record WalkerDirection<T>(T Item1, T Item2) : IComparable<WalkerDirection<T>> where T : INumber<T>, INumberBase<T>
    {
        // stolen from https://github.com/boombuler/adventofcode/blob/master/Utils/Point2D.cs
        // Allows math using my Tuple Class
        public static WalkerDirection<T> operator -(WalkerDirection<T> a)
        => new(-a.Item1, -a.Item2);
        public static WalkerDirection<T> operator -(WalkerDirection<T> a, WalkerDirection<T> b)
            => new(a.Item1 - b.Item1, a.Item2 - b.Item2);
        public static WalkerDirection<T> operator +(WalkerDirection<T> a, WalkerDirection<T> b)
            => new(a.Item1 + b.Item1, a.Item2 + b.Item2);
        public static WalkerDirection<T> operator *(WalkerDirection<T> a, T b)
            => new(a.Item1 * b, a.Item2 * b);
        public static WalkerDirection<T> operator *(T a, WalkerDirection<T> b)
            => b * a;
        public static WalkerDirection<T> operator /(WalkerDirection<T> a, T b)
            => new(a.Item1 / b, a.Item2 / b);
        public static WalkerDirection<T> operator %(WalkerDirection<T> a, T b)
            => new(a.Item1 % b, a.Item2 % b);
        public static WalkerDirection<T> operator %(WalkerDirection<T> a, WalkerDirection<T> b)
            => new(a.Item1 % b.Item1, a.Item2 % b.Item2);

        public int CompareTo(WalkerDirection<T> other)
        {
            var dy = Item2 - other.Item2;
            if (dy == T.Zero)
                return T.Sign(Item1 - other.Item1);
            return T.Sign(dy);
        }
    }
}
