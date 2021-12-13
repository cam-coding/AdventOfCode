using System;
using System.Collections.Generic;

namespace AdventLibrary
{
    public class StackSolverClass
    {
        private Dictionary<char, char> _pairs;
        private Dictionary<char, int> _counts;
        private List<char> _leftoverStack;
        private string _inputString;
        public StackSolverClass(string inputStack, char[] pairs)
        {
            _inputString = inputStack;
            _pairs = new Dictionary<char, char>();
            for (var i = 0; i < pairs.Length; i=i+2)
            {
                _pairs.Add(pairs[i], pairs[i+1]);
            }
        }
        public StackSolverClass(string inputStack, string pairs)
        {
            new StackSolverClass(inputStack, pairs.ToCharArray());
        }

        public Dictionary<char, int> CountsDict { get => _counts; }

        public List<char> LeftoverStack { get { return HasExtras ? _leftoverStack : null;}}

        public bool HasExtras;

        public bool IsMalformed;
    }
}