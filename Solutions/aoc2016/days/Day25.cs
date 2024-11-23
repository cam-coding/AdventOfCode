using AdventLibrary;
using AdventLibrary.Extensions;
using System;
using System.Collections.Generic;

namespace aoc2016
{
    public class Day25: ISolver
    {
        private List<bool> _historyBool;
        private bool _valid;

        public Solution Solve(string filePath, bool isTest = false)
        {
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            // carved out the underlying program from the AssemBunny
            var k = 0;
            while (true)
            {
                var d = k + (170 * 15);
                _valid = true;
                var a = d;
                _historyBool = new List<bool>();
                while (a != 0 && _valid)
                {
                    var b = a % 2;
                    a = a / 2;
                    Output(b);
                }
                if (_valid)
                {
                    return k;
                }
                k++;
            }
        }
        
        private object Part2()
        {
            return 0;
        }
        private void Output(int value1)
        {
            bool booly = Convert.ToBoolean(value1);
            if (_historyBool.Count == 0)
            {
                _historyBool.Add(booly);
            }
            else
            {
                var last = _historyBool.LastItem();
                if (booly == last)
                {
                    _valid = false;
                }
                else
                {
                    _historyBool.Add(booly);
                }
            }
            if (_historyBool.Count > 10)
            {
                Console.WriteLine("success");
            }
        }
    }
}