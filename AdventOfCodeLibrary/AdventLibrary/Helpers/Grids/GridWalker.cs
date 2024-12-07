using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AdventLibrary.Helpers.Grids
{
    public class GridWalker
    {
        public GridWalker(GridLocation<int> current, GridLocation<int> direction, int speed = 1)
        {
            Current = current;
            Direction = direction;
            Speed = speed;

            // hmm this assums you want to count the starting position.
            Path = new List<(GridLocation<int>, GridLocation<int>)>() { (Current, Direction) };
            History = new HashSet<int>() { Current.GetHashCode() * Direction.GetHashCode() };
            Looping = false;
            OutOfBounds = false;
            Previous = Current - direction;
        }

        public GridWalker(GridWalker walker)
        {
            Current = walker.Current;
            Direction = walker.Direction;
            Path = new List<(GridLocation<int>, GridLocation<int>)>(walker.Path);
            History = new HashSet<int>(walker.History);
            Looping = walker.Looping;
            OutOfBounds = walker.OutOfBounds;
            Previous = walker.Previous;
            Speed = walker.Speed;
        }

        public GridLocation<int> Current { get; set; }

        public GridLocation<int> Previous { get; set; }

        public GridLocation<int> Direction { get; set; }

        public List<(GridLocation<int>, GridLocation<int>)> Path { get; set; }

        public List<GridLocation<int>> UniqueLocationsVisited => Path.Select(x => x.Item1).Distinct().ToList();

        public HashSet<int> History { get; set; }

        public int PathLength => Path.Count;

        public int UniqueStates => History.Count;

        public bool Looping { get; set; }

        public bool OutOfBounds { get; set; }

        public int Speed { get; set; }

        public int Y => Current.Y;

        public int X => Current.X;

        public GridLocation<int> GetNextLocation()
        {
            return Current + Direction * Speed;
        }

        public void Walk()
        {
            Previous = Current;
            Current = GetNextLocation();

            var key = (Current, Direction);
            var hashKey = Current.GetHashCode() * Direction.GetHashCode();
            Path.Add(key);
            if (!History.Add(hashKey))
            {
                Looping = true;
            }
        }

        public void SetPathAndHistory(List<(GridLocation<int>, GridLocation<int>)> path)
        {
            foreach (var item in path)
            {
                AddToPathAndHistory(item.Item1, item.Item2);
            }
        }

        private void AddToPathAndHistory(GridLocation<int> current, GridLocation<int> direction)
        {
            var key = (current, direction);
            var hashKey = current.GetHashCode() * direction.GetHashCode();
            Path.Add(key);
            if (!History.Add(hashKey))
            {
                Looping = true;
            }
        }
    }
}
