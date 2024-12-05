using System;
using System.Collections.Generic;

namespace AdventLibrary.Helpers.Grids
{
    public class HexGridObject<T>
    {
        public Dictionary<string,Hexagon<T>> Directions = new Dictionary<string,Hexagon<T>>
        {
            { "N", new Hexagon<T>(0,-1,1) },
            { "NE", new Hexagon<T>(1,-1,0) },
            { "SE", new Hexagon<T>(1,0,-1) },
            { "S", new Hexagon<T>(0,1,-1) },
            { "SW", new Hexagon<T>(-1,1,0) },
            { "NW", new Hexagon<T>(-1,0,1) },
        };
        public HexGridObject(Dictionary<string, Hexagon<T>> grid)
        {
            Grid = grid;
        }

        Dictionary<string, Hexagon<T>> Grid { get; set; }

        public Hexagon<T> GetHexagon(int x, int y, int z)
        {
            var key = GenerateKey(x, y, z);
            return Grid[key];
        }

        public int GetDistance (Hexagon<T> start, Hexagon<T> end)
        {
            var deltaX = Math.Abs(end.X - start.X);
            var deltaY = Math.Abs(end.Y - start.Y);
            var deltaZ = Math.Abs(end.Z - start.Z);
            return Math.Max(Math.Max(deltaX, deltaY), deltaZ);
        }

        public List<Hexagon<T>> GetNeighbours(Hexagon<T> current)
        {
            var dirs = Directions.Values;
            var result = new List<Hexagon<T>>();
            foreach (var dir in dirs)
            {
                result.Add(Add(current, dir));
            }
            return result;
        }

        public Hexagon<T> Add(Hexagon<T> current, Hexagon<T> direction)
        {
            var key = GenerateKey(current.X + direction.X, current.Y + direction.Y, current.Z + direction.Z);
            return Grid[key];
        }

        private static string GenerateKey(int x, int y, int z)
        {
            return $"X:{x}Y:{y}Z:{z}";
        }

        public class Hexagon<T>
        {
            public Hexagon(HexagonCoords coords, T value = default(T))
            {
                Coords = coords;
                Value = value;
            }

            public HexagonCoords Coords { get; set; }

            public T Value { get; set; }

            public string GetKey()
            {
                return HexGridObject<T>.GenerateKey(X,Y,Z);
            }
        }

        public class HexagonCoords
        {
            public HexagonCoords(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public override bool Equals(object obj)
            {
                return Equals(obj as HexagonCoords);
            }

            public bool Equals(HexagonCoords other)
            {
                return other != null &&
                    X == other.X &&
                    Y == other.Y &&
                    Z == other.Z;
            }
        }
    }
}
