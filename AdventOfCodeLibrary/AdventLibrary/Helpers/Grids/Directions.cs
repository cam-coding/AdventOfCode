using AdventLibrary.CustomObjects;
using System.Collections.Generic;

namespace AdventLibrary.Helpers.Grids
{
    public static class Directions
    {
        // Coords are form of (X,Y) and (0,0) is the top left of the graph
        public static LocationTuple<int> Up = new LocationTuple<int>(0, -1);
        public static LocationTuple<int> UpRight = new LocationTuple<int>(1, -1);
        public static LocationTuple<int> UpLeft = new LocationTuple<int>(-1, -1);
        public static LocationTuple<int> Down = new LocationTuple<int>(0, 1);
        public static LocationTuple<int> DownRight = new LocationTuple<int>(1, 1);
        public static LocationTuple<int> DownLeft = new LocationTuple<int>(-1, 1);
        public static LocationTuple<int> Left = new LocationTuple<int>(-1, 0);
        public static LocationTuple<int> Right = new LocationTuple<int>(1, 0);

        // All 8 directions, starting up and going clockwise.
        public static List<LocationTuple<int>> AllDirections = new List<LocationTuple<int>>()
        {
            Up,
            UpRight,
            Right,
            DownRight,
            Down,
            DownLeft,
            Left,
            UpLeft,
        };

        // 4 basic directions starting with up and going clockwise
        public static List<LocationTuple<int>> OrthogonalDirections = new List<LocationTuple<int>>()
        {
            Up,
            Right,
            Down,
            Left,
        };

        // 4 diagonal directions starting with upright and going clockwise
        public static List<LocationTuple<int>> DiagonalDirections = new List<LocationTuple<int>>()
        {
            UpRight,
            DownRight,
            DownLeft,
            UpLeft,
        };

        public static Dictionary<LocationTuple<int>, LocationTuple<int>> Opposites = new Dictionary<LocationTuple<int>, LocationTuple<int>>()
        {
            { Up, Down },
            { UpRight, DownLeft },
            { Right, Left },
            { DownRight, UpLeft },
            { Down, Up },
            { DownLeft, UpRight },
            { Left, Right },
            { UpLeft, DownRight },
        };
    }
}
