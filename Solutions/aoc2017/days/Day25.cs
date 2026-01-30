using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day25: ISolver
    {
        private Dictionary<long, int> _tape;
        private long _currentPosition;
        private char _state;
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            var groups = input.LineGroupsSeperatedByWhiteSpace;
			var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
			long count = 0;
            long number = input.Long;
            var state = 'A';
            _state = 'A';
            _currentPosition = 0;
            _tape = new Dictionary<long, int>();

            var actions = new Dictionary<char, List<Action>>();

            var iterations = (int)numbers[0];

            for (var i = 1; i < groups.Count; i++)
            {
                var stateActions = new List<Action>();
                var cur = _state - 'A' + 1;

                stateActions.Add(() =>
                {
                    Write(StringParsing.GetIntsFromString(groups[cur][2])[0]);
                    var tapeAdjustment = 1;
                    if (groups[cur][3].Contains("left"))
                    {
                        tapeAdjustment = -1;
                    }
                    _currentPosition += tapeAdjustment;
                    var newState = groups[cur][4].GetRealTokens(_delimiterChars).Last();
                    _state = newState[0];
                });

                stateActions.Add(() =>
                {
                    Write(StringParsing.GetIntsFromString(groups[cur][6])[0]);
                    var tapeAdjustment = 1;
                    if (groups[cur][7].Contains("left"))
                    {
                        tapeAdjustment = -1;
                    }
                    _currentPosition += tapeAdjustment;
                    var newState = groups[cur][8].GetRealTokens(_delimiterChars).Last();
                    _state = newState[0];
                });

                actions.Add(_state, stateActions);
                _state++;
            }

            _state = 'A';

            for (var j = 0; j < iterations; j++)
            {
                var index = Read();
                actions[_state][index].Invoke();
            }
            return _tape.Values.Count(x => x == 1);
        }

        private object Part2(bool isTest = false)
        {
            return 0;
        }

        private void Write(int val)
        {
            if (_tape.ContainsKey(_currentPosition))
            {
                _tape[_currentPosition] = val;
            }
            else
            {
                _tape.Add(_currentPosition, val);
            }
        }

        private int Read()
        {
            if (_tape.ContainsKey(_currentPosition))
            {
                return _tape[_currentPosition];
            }
            else
            {
                return 0;
            }
        }
    }
}