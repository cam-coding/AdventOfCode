namespace AdventLibrary
{
    public class StackSolverClass
    {
        private readonly Dictionary<char, char> _pairs;
        private readonly Dictionary<char, int> _counts;
        private readonly List<char> _leftoverStack;
        private readonly string _inputString;
        public StackSolverClass(string inputStack, char[] pairs)
        {
            _inputString = inputStack;
            _pairs = new Dictionary<char, char>();
            for (var i = 0; i < pairs.Length; i = i + 2)
            {
                _pairs.Add(pairs[i], pairs[i + 1]);
            }
        }
        public StackSolverClass(string inputStack, string pairs)
        {
            new StackSolverClass(inputStack, pairs.ToCharArray());
        }

        public Dictionary<char, int> CountsDict { get => _counts; }

        public List<char> LeftoverStack { get { return HasExtras ? _leftoverStack : null; } }

        public bool HasExtras;

        public bool IsMalformed;
    }
}