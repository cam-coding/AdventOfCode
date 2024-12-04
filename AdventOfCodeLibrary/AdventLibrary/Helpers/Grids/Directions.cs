using AdventLibrary.CustomObjects;
using System.Collections.Generic;
using System.Linq;

namespace AdventLibrary.Helpers.Grids
{
    public static class Directions
    {
        // Coords are form of (X,Y) and (0,0) is the top left of the graph
        public static GridLocation<int> Up = new GridLocation<int>(0, -1);
        public static GridLocation<int> UpRight = new GridLocation<int>(1, -1);
        public static GridLocation<int> UpLeft = new GridLocation<int>(-1, -1);
        public static GridLocation<int> Down = new GridLocation<int>(0, 1);
        public static GridLocation<int> DownRight = new GridLocation<int>(1, 1);
        public static GridLocation<int> DownLeft = new GridLocation<int>(-1, 1);
        public static GridLocation<int> Left = new GridLocation<int>(-1, 0);
        public static GridLocation<int> Right = new GridLocation<int>(1, 0);

        // All 8 directions, starting up and going clockwise.
        public static List<GridLocation<int>> AllDirections = new List<GridLocation<int>>()
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
        public static List<GridLocation<int>> OrthogonalDirections = new List<GridLocation<int>>()
        {
            Up,
            Right,
            Down,
            Left,
        };

        // 4 diagonal directions starting with upright and going clockwise
        public static List<GridLocation<int>> DiagonalDirections = new List<GridLocation<int>>()
        {
            UpRight,
            DownRight,
            DownLeft,
            UpLeft,
        };

        public static Dictionary<GridLocation<int>, GridLocation<int>> Opposites = new Dictionary<GridLocation<int>, GridLocation<int>>()
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

        public static Dictionary<List<string>, GridLocation<int>> StringLookup = new Dictionary<List<string>, GridLocation<int>>()
        {
            { new List<string>() { "U", "UP", "^" }, Up },
            { new List<string>() { "R", "RIGHT", ">" }, Right },
            { new List<string>() { "D", "DOWN", "V" }, Down },
            { new List<string>() { "L", "LEFT", "<" }, Left },
        };

        public static GridLocation<int> GetDirectionByString(string str)
        {
            var upper = str.ToUpper();
            var key = StringLookup.Keys.FirstOrDefault(x => x.Contains(upper));
            if (key != null)
            {
                return StringLookup[key];
            }
            return null;
        }
    }
}
