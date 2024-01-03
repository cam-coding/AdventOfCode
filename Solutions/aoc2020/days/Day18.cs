using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2020
{
    public class Day18: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private List<(string value, bool isNum)> _stack;
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1(bool isTest = false)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            long total = 0;
			foreach (var line in lines)
            {
                var ln = line.RemoveWhitespace();
                _stack = new List<(string value,bool isNum)>();
                var tokens = ln.Select(x => x.ToString());
                foreach (var token in tokens)
                {
                    var tok = token[0];
                    var isNum = Char.IsDigit(tok);
                    _stack.Add((token, isNum));
                    DoTheThing();
                }
                total += long.Parse(_stack[0].value);
			}
            return total;
        }

        private object Part2(bool isTest = false)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            long total = 0;
            foreach (var line in lines)
            {
                var ln = line.RemoveWhitespace();
                _stack = new List<(string value, bool isNum)>();
                var tokens = ln.Select(x => x.ToString());
                foreach (var token in tokens)
                {
                    var tok = token[0];
                    var isNum = Char.IsDigit(tok);
                    _stack.Add((token, isNum));
                    DoTheThing2();
                }
                ClearBracketsFromStack();
                DoTheThing2();
                ClearMultiplyFromStack();
                DoTheThing2();
                var ans = long.Parse(_stack[0].value);
                total += ans;
            }
            return total;
        }

        private void DoTheThing()
        {
            if (_stack.Count < 3)
            {
                return;
            }
            var item1 = _stack[^3];
            var item2 = _stack[^2];
            var item3 = _stack[^1];
            if (item1.isNum && !item2.isNum && item3.isNum)
            {
                var num1 = long.Parse(item1.value);
                var num3 = long.Parse(item3.value);
                _stack.RemoveEverythingAfter(_stack.Count - 4);
                if (item2.value.Equals("+"))
                {
                    _stack.Add(((num1 + num3).ToString(),true));
                    DoTheThing();
                }
                else if (item2.value.Equals("*"))
                {
                    _stack.Add(((num1 * num3).ToString(), true));
                    DoTheThing();
                }
            }
            else if (!item1.isNum && item2.isNum && !item3.isNum)
            {
                if (item1.value[0] == '(' &&
                    item3.value[0] == ')')
                {
                    _stack.RemoveEverythingAfter(_stack.Count - 4);
                    _stack.Add(item2);
                    DoTheThing();
                }
            }
        }

        private void DoTheThing2()
        {
            if (_stack.Count < 3)
            {
                return;
            }
            var item1 = _stack[^3];
            var item2 = _stack[^2];
            var item3 = _stack[^1];
            if (item1.isNum && !item2.isNum && item3.isNum)
            {
                var num1 = long.Parse(item1.value);
                var num3 = long.Parse(item3.value);
                if (item2.value.Equals("+"))
                {
                    _stack.RemoveEverythingAfter(_stack.Count - 4);
                    _stack.Add(((num1 + num3).ToString(), true));
                    DoTheThing2();
                }
            }
            else if (item3.value[0] == ')')
            {
                ClearBracketsFromStack();
            }
        }

        private void ClearBracketsFromStack()
        {
            if (_stack.Count < 3)
            {
                return;
            }
            if (_stack[^1].value[0] == ')')
            {
                _stack.RemoveAt(_stack.Count - 1);

                if (_stack.Count == 2)
                {
                    _stack.RemoveAt(0);
                    return;
                }

                var item1 = _stack[^3];
                var item2 = _stack[^2];
                var item3 = _stack[^1];

                while (!item1.Equals("(") &&
                    !item2.Equals("(") &&
                    !item3.Equals("("))
                {
                    if (item1.isNum && !item2.isNum && item3.isNum)
                    {
                        var num1 = long.Parse(item1.value);
                        var num3 = long.Parse(item3.value);
                        if (item2.value.Equals("*"))
                        {
                            _stack.RemoveEverythingAfter(_stack.Count - 4);
                            _stack.Add(((num1 * num3).ToString(), true));
                            ClearMultiplyFromStack();
                        }
                        else
                        {
                            break;
                        }
                        if (_stack.Count > 2)
                        {
                            item1 = _stack[^3];
                        }
                        else
                        {
                            item1 = (string.Empty, false);
                        }
                        if (_stack.Count > 1)
                        {
                            item2 = _stack[^2];
                        }
                        else
                        {
                            item2 = (string.Empty, false);
                        }
                        if (_stack.Count > 0)
                        {
                            item3 = _stack[^1];
                        }
                        else
                        {
                            item3 = (string.Empty, false);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (item3.value.Equals("("))
                {
                    _stack.RemoveAt(_stack.Count - 1);
                }
                else if (item2.value.Equals("("))
                {
                    _stack.RemoveAt(_stack.Count - 2);
                }
                else if (item1.value.Equals("("))
                {
                    _stack.RemoveAt(_stack.Count - 3);
                }
            }
            DoTheThing2();
        }

        private void ClearMultiplyFromStack()
        {
            if (_stack.Count < 3)
            {
                return;
            }
            if (_stack[^1].value[0] == ')')
            {
                _stack.RemoveAt(_stack.Count - 1);
                for (var i = _stack.Count - 1; i >= 0; i--)
                {
                    if (_stack[i].value[0] == '(')
                    {
                        _stack.RemoveAt(i);
                        break;
                    }
                }
            }
            if (_stack.Count < 3)
            {
                return;
            }
            var item1 = _stack[^3];
            var item2 = _stack[^2];
            var item3 = _stack[^1];
            if (item1.isNum && !item2.isNum && item3.isNum)
            {
                var num1 = long.Parse(item1.value);
                var num3 = long.Parse(item3.value);
                if (item2.value.Equals("*"))
                {
                    _stack.RemoveEverythingAfter(_stack.Count - 4);
                    _stack.Add(((num1 * num3).ToString(), true));
                    ClearMultiplyFromStack();
                }
            }
        }
    }
}