namespace AdventLibrary.Helpers.Grids
{
    public class HexGridObject<T>
    {
        public Dictionary<string, HexagonCoords> Directions = new Dictionary<string, HexagonCoords>
        {
            { "N", new HexagonCoords(0,-1,1) },
            { "NE", new HexagonCoords(1,-1,0) },
            { "SE", new HexagonCoords(1,0,-1) },
            { "S", new HexagonCoords(0,1,-1) },
            { "SW", new HexagonCoords(-1,1,0) },
            { "NW", new HexagonCoords(-1,0,1) },
        };
        public HexGridObject(Dictionary<HexagonCoords, Hexagon<T>> grid)
        {
            Grid = grid;
        }

        Dictionary<HexagonCoords, Hexagon<T>> Grid { get; set; }

        public Hexagon<T> GetHexagon(int x, int y, int z)
        {
            return GetHexagon(new HexagonCoords(x, y, z));
        }

        public Hexagon<T> GetHexagon(HexagonCoords coords)
        {
            return Grid[coords];
        }

        public int GetDistance(HexagonCoords start, HexagonCoords end)
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
                var temp = new HexagonCoords(current.Coords);
                temp.Add(dir);
                result.Add(Grid[temp]);
            }
            return result;
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
        }

        public class HexagonCoords
        {
            public HexagonCoords(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public HexagonCoords(HexagonCoords obj)
            {
                X = obj.X;
                Y = obj.Y;
                Z = obj.Z;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public void Add(HexagonCoords other)
            {
                X += other.X;
                Y += other.Y;
                Z += other.Z;
            }

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

            public override int GetHashCode()
            {
                int result = 37;

                result *= 397;
                result += X.GetHashCode();
                result *= 397;
                result += Y.GetHashCode();
                result *= 397;
                result += Z.GetHashCode();
                result *= 397;

                return result;
            }
        }
    }
}
