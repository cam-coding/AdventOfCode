using AdventLibrary.Extensions;

namespace AdventLibrary.PathFinding
{
    public class DepthFirstSearch<T>
    {
        private HashSet<T> _visited;
        private Dictionary<T, (int Distance, List<T> Path)> _distanceDictionary;
        private bool _goalAchieved;
        private List<T> _goalPath;

        public DepthFirstSearch()
        {
            _visited = new HashSet<T>();
            _distanceDictionary = new Dictionary<T, (int Distance, List<T> Path)>();
            _goalAchieved = false;
            _goalPath = null;
        }

        public Dictionary<T, (int Distance, List<T> Path)> DistanceDictionary
        { get { return _distanceDictionary; } }

        public bool GoalAchieved
        { get { return _goalAchieved; } }

        public List<T> GoalPath
        { get { return _goalPath; } }

        // STILL NEEDS TESTING
        // search from starting point to every point if you don't put a goal.
        // start to end if you do put a goal
        public void DFSgeneric(
            (int Distance, List<T> Path) current,
            Func<T, List<T>> GetNeighboursFunc,
            Func<T, bool> GoalEvaluation = null,
            Func<T, int> GetWeight = null
            )
        {
            var currentNode = current.Path.Last();
            var currentPath = current.Path;

            if (_goalAchieved)
            {
                return;
            }

            if ((GoalEvaluation != null && GoalEvaluation(currentNode)))
            {
                _goalAchieved = true;
                _goalPath = currentPath;
                return;
            }

            // only process each node once
            if (!_visited.Add(currentNode))
            {
                return;
            }

            // each node we see starts with an infinite distance
            if (!_distanceDictionary.ContainsKey(currentNode))
            {
                _distanceDictionary.Add(currentNode, (Int32.MaxValue, currentPath));
            }

            // Neighbours are figured out before objects are passed in, so no logic around that here.
            foreach (var neighbour in GetNeighboursFunc(currentNode).Where(x => !_visited.Contains(x)))
            {
                var newPath = currentPath.Clone();
                newPath.Add(neighbour);
                if (!_distanceDictionary.ContainsKey(neighbour))
                {
                    _distanceDictionary.Add(neighbour, (Int32.MaxValue, newPath));
                }
                var weight = GetWeight == null ? 1 : GetWeight(neighbour);
                var distance = _distanceDictionary[currentNode].Distance + weight;
                if (distance < _distanceDictionary[neighbour].Distance)
                {
                    var tup = (distance, newPath);
                    _distanceDictionary[neighbour] = tup;
                    DFSgeneric(tup, GetNeighboursFunc, GoalEvaluation, GetWeight);
                }
            }
        }

        public void DFS_Weightless(
            List<T> current,
            Func<T, List<T>> GetNeighboursFunc,
            Func<T, bool> GoalEvaluation = null
            )
        {
            var currentNode = current.Last();
            var currentPath = current;

            if (_goalAchieved)
            {
                return;
            }

            if ((GoalEvaluation != null && GoalEvaluation(currentNode)))
            {
                _goalAchieved = true;
                _goalPath = currentPath;
                return;
            }

            // only process each node once
            if (!_visited.Add(currentNode))
            {
                return;
            }

            // Neighbours are figured out before objects are passed in, so no logic around that here.
            foreach (var neighbour in GetNeighboursFunc(currentNode).Where(x => !_visited.Contains(x)))
            {
                var newPath = currentPath.Clone();
                newPath.Add(neighbour);
                DFS_Weightless(newPath, GetNeighboursFunc, GoalEvaluation);
            }
        }
    }
}