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

        private static Dictionary<string, GridLocation<int>> StringLookup = new Dictionary<string, GridLocation<int>>()
        {
            { "U", Up},
            { "UP", Up},
            { "^", Up},
            { "R", Right},
            { "RIGHT", Right},
            { ">", Right},
            { "D", Down},
            { "DOWN", Down},
            { "V", Down},
            { "L", Left},
            { "LEFT", Left},
            { "<", Left},
        };

        public static GridLocation<int> GetDirectionByString(char chr)
        {
            return GetDirectionByString(chr.ToString());
        }

        public static GridLocation<int> GetDirectionByString(string str)
        {
            var upper = str.ToUpper();
            GridLocation<int> result;
            if (StringLookup.TryGetValue(upper, out result))
            {
                return result;
            }
            return null;
        }

        #region Turning
        public static GridLocation<int> TurnRightAll(GridLocation<int> current)
        {
            return TurnRight(current, AllDirections);
        }

        public static GridLocation<int> TurnRightDiagonal(GridLocation<int> current)
        {
            return TurnRight(current, DiagonalDirections);
        }

        public static GridLocation<int> TurnRightOrthogonal(GridLocation<int> current)
        {
            return TurnRight(current, OrthogonalDirections);
        }

        public static GridLocation<int> TurnleftAll(GridLocation<int> current)
        {
            return Turnleft(current, AllDirections);
        }

        public static GridLocation<int> TurnleftDiagonal(GridLocation<int> current)
        {
            return Turnleft(current, DiagonalDirections);
        }

        public static GridLocation<int> TurnleftOrthogonal(GridLocation<int> current)
        {
            return Turnleft(current, OrthogonalDirections);
        }

        private static GridLocation<int> TurnRight(GridLocation<int> current, List<GridLocation<int>> directions)
        {
            var cur = directions.IndexOf(current);
            cur++;
            if (cur == directions.Count)
            {
                cur = 0;
            }
            return directions[cur];
        }

        private static GridLocation<int> Turnleft(GridLocation<int> current, List<GridLocation<int>> directions)
        {
            var cur = directions.IndexOf(current);
            cur--;
            if (cur == -1)
            {
                cur = directions.Count-1;
            }
            return directions[cur];
        }
        #endregion turning
    }
}
