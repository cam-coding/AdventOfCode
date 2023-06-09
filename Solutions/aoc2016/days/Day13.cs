using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day13: ISolver
  {
        private string _filePath;
        private int _solution;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var visited = new HashSet<(int,int)>();
            _solution = 100;

            Queue<(int x, int y, int count)> q = new Queue<(int x, int y, int count)>();
            q.Enqueue((1,1,0));
            while (q.Count > 0)
            {
                var current = q.Dequeue();

                if (visited.Contains((current.x,current.y)))
                {
                    continue;
                }
                
                if (IsWall(current.x, current.y) ||
                    current.count >= _solution ||
                    current.x < 0 ||
                    current.y < 0)
                {
                    continue;
                }

                visited.Add((current.x,current.y));

                if (current.x == 31 && current.y == 39)
                {
                    if (current.count < _solution)
                        _solution = current.count;
                    continue;
                    
                }
                else
                {
                    q.Enqueue((current.x+1, current.y, current.count + 1));
                    q.Enqueue((current.x-1, current.y, current.count + 1));
                    q.Enqueue((current.x, current.y+1, current.count + 1));
                    q.Enqueue((current.x, current.y-1, current.count + 1));
                }
            }

            return _solution;
        }

        private bool IsWall(int x, int y)
        {
            var num = (x*x) + (3*x) + (2*x*y) + y + (y*y);
            num = num + 1362;
            var unum = Convert.ToUInt32(num);
            if (CountBits(unum) % 2 == 0)
            {
                return false;
            }
            return true;
        }

        private int CountBits(uint value)
        {
            int count = 0;
            while (value != 0)
            {
                count++;
                value &= value - 1;
            }
            return count;
        }
        
        private object Part2()
        {
            var visited = new HashSet<(int,int)>();
            var max = 50;

            Queue<(int x, int y, int count)> q = new Queue<(int x, int y, int count)>();
            q.Enqueue((1,1,0));
            while (q.Count > 0)
            {
                var current = q.Dequeue();

                if (visited.Contains((current.x,current.y)))
                {
                    continue;
                }
                
                if (IsWall(current.x, current.y) ||
                    current.count > max ||
                    current.x < 0 ||
                    current.y < 0)
                {
                    continue;
                }

                visited.Add((current.x,current.y));
                q.Enqueue((current.x+1, current.y, current.count + 1));
                q.Enqueue((current.x-1, current.y, current.count + 1));
                q.Enqueue((current.x, current.y+1, current.count + 1));
                q.Enqueue((current.x, current.y-1, current.count + 1));
            }

            return visited.Count();
        }
    }
}
