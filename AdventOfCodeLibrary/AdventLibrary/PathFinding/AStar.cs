using AdventLibrary.Helpers.Grids;

/*
* Taken from RedBlobGames https://www.redblobgames.com/pathfinding/a-star/implementation.html#python-dijkstra
*/

namespace AdventLibrary.PathFinding
{
    // A* needs only a WeightedGraph and a location type L, and does *not*
    // have to be a grid. However, in the example code I am using a grid.
    public interface AStarWeightedGraph<T>
    {
        double Cost(T location);

        IEnumerable<T> Neighbors(T location);
    }

    public struct AStarLocation
    {
        // Implementation notes: I am using the default Equals but it can
        // be slow. You'll probably want to override both Equals and
        // GetHashCode in a real project.

        public readonly int X, Y;

        public AStarLocation(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class AStar_GridObject<T> : AStarWeightedGraph<GridLocation<int>>
    {
        private GridObject<T> _grid;
        private List<T> _walls;
        private Func<GridLocation<int>, List<GridLocation<int>>> _getNeighbours;

        public AStar_GridObject(
            GridObject<T> grid,
            List<T> wallCharacters,
            Func<GridLocation<int>, List<GridLocation<int>>> getNeighbours)
        {
            Width = grid.Width;
            Height = grid.Height;
            _grid = grid;
            _walls = wallCharacters;
            _getNeighbours = getNeighbours;
        }

        public int Width { get; }
        public int Height { get; }

        public bool InBounds(GridLocation<int> location)
        {
            return _grid.WithinGrid(location);
        }

        public bool Passable(GridLocation<int> location)
        {
            return !_walls.Contains(_grid.Get(location));
        }

        public double Cost(GridLocation<int> location)
        {
            return Convert.ToDouble(_grid.Get(location));
        }

        public IEnumerable<GridLocation<int>> Neighbors(GridLocation<int> location)
        {
            return _getNeighbours(location);
        }
    }

    public class SquareGrid : AStarWeightedGraph<AStarLocation>
    {
        // Implementation notes: I made the fields public for convenience,
        // but in a real project you'll probably want to follow standard
        // style and make them private.

        public static readonly AStarLocation[] DIRS = new[]
        {
            new AStarLocation(1, 0),
            new AStarLocation(0, -1),
            new AStarLocation(-1, 0),
            new AStarLocation(0, 1)
        };

        public int width, height;
        public HashSet<AStarLocation> walls = new HashSet<AStarLocation>();
        public HashSet<AStarLocation> forests = new HashSet<AStarLocation>();

        public SquareGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public bool InBounds(AStarLocation id)
        {
            return 0 <= id.X && id.X < width
                && 0 <= id.Y && id.Y < height;
        }

        public bool Passable(AStarLocation id)
        {
            return !walls.Contains(id);
        }

        public double Cost(AStarLocation a)
        {
            return forests.Contains(a) ? 5 : 1;
        }

        public IEnumerable<AStarLocation> Neighbors(AStarLocation id)
        {
            foreach (var dir in DIRS)
            {
                AStarLocation next = new AStarLocation(id.X + dir.X, id.Y + dir.Y);
                if (InBounds(next) && Passable(next))
                {
                    yield return next;
                }
            }
        }
    }

    public class AStarSearcher<T>
    {
        private AStarWeightedGraph<T> _graph;
        private T _start;
        private T _end;

        public Dictionary<T, T> cameFrom
            = new Dictionary<T, T>();

        public Dictionary<T, double> costSoFar
            = new Dictionary<T, double>();

        // A better version would abstract this out more
        public static double Heuristic(T a, T b)
        {
            if (a is AStarLocation aLoc && b is AStarLocation bLoc)
            {
                return Math.Abs(aLoc.X - bLoc.X) + Math.Abs(aLoc.Y - bLoc.Y);
            }
            else if (a is GridLocation<int> aGridLoc && b is GridLocation<int> bGridLoc)
            {
                return Math.Abs(aGridLoc.X - bGridLoc.X) + Math.Abs(aGridLoc.Y - bGridLoc.Y);
            }
            else
            {
                throw new ArgumentException("Invalid types for A* heuristic");
            }
        }

        public AStarSearcher(AStarWeightedGraph<T> graph)
        {
            _graph = graph;
        }

        public void Search(T start, T end)
        {
            _start = start;
            _end = end;

            var frontier = new PriorityQueue<T, double>();
            frontier.Enqueue(start, 0);

            cameFrom[start] = start;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.Equals(end))
                {
                    break;
                }

                foreach (var next in _graph.Neighbors(current))
                {
                    double newCost = costSoFar[current]
                        + _graph.Cost(next);
                    if (!costSoFar.ContainsKey(next)
                        || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        double priority = newCost + Heuristic(next, end);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }
        }

        public List<T> GetPath(T node)
        {
            var path = new List<T>();
            T current = node;
            while (!current.Equals(_start))
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Add(_start);
            path.Reverse();
            return path;
        }

        public double GetCost(T node)
        {
            return costSoFar[node];
        }
    }
}