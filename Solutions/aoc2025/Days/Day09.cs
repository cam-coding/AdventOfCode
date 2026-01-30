using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AdventLibrary;
using AdventLibrary.CustomObjects;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;
using static aoc2025.Day99;

namespace aoc2025
{
    public class Day09 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private List<GridLocation<long>> _polygonPoints;
        private List<myLine> _polygonLines;
        private long _lowY;
        private long _highY;

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
            var numbers = input.Longs;
            var longLines = input.LongLines;
            var nodes = input.GraphDirected;
            var grid = input.GridChar;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long count = 0;
            long number = input.Long;

            var myList = new List<GridLocation<long>>();
            foreach (var line in lines)
            {
                var longs = line.GetLongsFromString();
                var loc = new GridLocation<long>(longs[0], longs[1]);
                myList.Add(loc);
            }

            var pairs = myList.GetPairs_Unique();

            long best = 0;

            for (var i = 0; i < pairs.Count; i++)
            {
                if (GetArea(pairs[i]) > best)
                {
                    best = Math.Max(best, GetArea(pairs[i]));
                    // Console.WriteLine(best);
                }
            }
            return best;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var longLines = input.LongLines;
            var gridStart = new GridLocation<int>(0, 0);
            long total = 1000000;
            long number = input.Long;
            long best = 0;
            (GridLocation<long>, GridLocation<long>) bestPair = default;

            _polygonLines = new List<myLine>();

            _polygonPoints = new List<GridLocation<long>>();
            foreach (var longs in longLines)
            {
                var loc = new GridLocation<long>(longs[0], longs[1]);
                _polygonPoints.Add(loc);
            }

            for (var i = 1; i < _polygonPoints.Count; i++)
            {
                _polygonLines.Add(new myLine(_polygonPoints[i], _polygonPoints[i - 1]));
            }
            _polygonLines.Add(new myLine(_polygonPoints.Last(), _polygonPoints.First()));

            var uniqueCornerPairs = _polygonPoints.GetPairs_Unique();
            uniqueCornerPairs.Sort((x, y) => GetArea(x).CompareTo(GetArea(y)));
            uniqueCornerPairs.Reverse();

            if (!isTest)
            {
                LongestLines();
            }

            foreach (var cornerPair in uniqueCornerPairs)
            {
                var area = GetArea(cornerPair);
                var rectangleCorners = new List<GridLocation<long>>();
                rectangleCorners.Add(cornerPair.Item1);
                rectangleCorners.Add(new GridLocation<long>(cornerPair.Item1.X, cornerPair.Item2.Y));
                rectangleCorners.Add(cornerPair.Item2);
                rectangleCorners.Add(new GridLocation<long>(cornerPair.Item2.X, cornerPair.Item1.Y));
                var rectangleLines = new List<myLine>();
                rectangleLines.Add(new myLine(rectangleCorners[0], rectangleCorners[1]));
                rectangleLines.Add(new myLine(rectangleCorners[0], rectangleCorners[3]));
                rectangleLines.Add(new myLine(rectangleCorners[2], rectangleCorners[1]));
                rectangleLines.Add(new myLine(rectangleCorners[2], rectangleCorners[3]));

                if (!rectangleCorners.All(x => IsPointInPolygon(x)))
                {
                    continue;
                }
                if (!isTest && area <= best)
                {
                    continue;
                }

                if (rectangleCorners.All(corner => corner.Y <= _lowY) ||
                    rectangleCorners.All(corner => corner.Y >= _highY))
                {
                    if (area > best)
                    {
                        best = area;
                        bestPair = cornerPair;
                    }
                }
                continue;

                var valid = true;
                foreach (var rectangleLine in rectangleLines)
                {
                    if (!valid)
                        break;
                    var intersectingLinesCount = 0;
                    foreach (var polygonLine in _polygonLines)
                    {
                        if (DoLinesIntersect(polygonLine, rectangleLine))
                        {
                            valid = false;
                            break;
                        }
                    }
                    /*
                    foreach (var polygonLine in _polygonLines)
                    {
                        if (DoLinesIntersect(polygonLine, rectangleLine))
                        {
                            intersectingLinesCount++;
                        }
                    }
                    if (intersectingLinesCount > 1)
                    {
                        valid = false;
                        break;
                    }*/
                }
                if (valid)
                {
                    Console.WriteLine(
                        $"Area: {area}." +
                        $"{cornerPair.Item1.X},{cornerPair.Item1.Y} & " +
                        $"{cornerPair.Item2.X},{cornerPair.Item2.Y}");
                    if (area > best)
                    {
                        best = area;
                        bestPair = cornerPair;
                    }
                }
            }

            // AllPointsInPolygon_BruteForceCheck(bestPair);
            return best;
        }

        private void LongestLines()
        {
            var myLines = _polygonLines;
            myLines.Sort((x, y) => x.Length.CompareTo(y.Length));
            myLines.Reverse();
            var lowY = Math.Min(myLines[0].yVal.Value, myLines[1].yVal.Value);
            var highY = Math.Max(myLines[0].yVal.Value, myLines[1].yVal.Value);
            var firsty = myLines.First();
        }

        private bool AllPointsInPolygon_BruteForceCheck((GridLocation<long> A, GridLocation<long> B) pair)
        {
            var startX = Math.Min(pair.A.X, pair.B.X);
            var startY = Math.Min(pair.A.Y, pair.B.Y);
            var endX = Math.Max(pair.A.X, pair.B.X);
            var endY = Math.Max(pair.A.Y, pair.B.Y);

            for (var x = startX; x <= endX; x++)
            {
                for (var y = startY; y <= endY; y++)
                {
                    var currentLocation = new GridLocation<long>(x, y);
                    if (!IsPointInPolygon(currentLocation))
                    {
                        Console.WriteLine("BADMAN");
                        return false;
                    }
                }
            }
            return true;
        }

        private bool DoLinesIntersect(myLine polygonLine, myLine rectangleLine)
        {
            // if my rectangle is 3,0 -> 5,0 and comparing to polygon line 1,0 -> 7,0
            // it's not an intersection per say.
            if (rectangleLine.WithinOtherLine(polygonLine))
            {
                return false;
            }

            var main = new LineObject<long>(polygonLine.start, polygonLine.end, false);
            var other = new LineObject<long>(rectangleLine.start, rectangleLine.end, false);

            // parralel never intersect or are on top of each other so it's always false
            if (main.Equals(other))
            {
                return false;
            }
            decimal delta = main.A * other.B - other.A * main.B;

            if (delta == 0)
                return false;

            decimal x = (other.B * main.C - main.B * other.C) / delta;
            decimal y = (main.A * other.C - other.A * main.C) / delta;
            var intersectPoint = new GridLocation<long>((long)x, (long)y);

            // if you intersect on a corner/polygon point we don't care
            if (_polygonPoints.Any(x => x.Equals(intersectPoint)))
            {
                return false;
            }

            var endIntersect = intersectPoint.Equals(rectangleLine.start) ||
                intersectPoint.Equals(rectangleLine.end);
            return !endIntersect;
            /*
            var intersectOnPolygonLine = polygonLine.IsPointOnLine(intersectPoint);
            return intersectOnPolygonLine;
            */
        }

        public bool IsPointInPolygon(GridLocation<long> testPoint)
        {
            return (IsOnPolygonLines(testPoint) || IsPointInPolygon_WITHIN(testPoint));
        }

        public bool IsOnPolygonLines(GridLocation<long> testPoint)
        {
            return _polygonLines.Any(x => x.IsPointOnLine(testPoint));
        }

        public bool IsPointInPolygon_WITHIN(GridLocation<long> testPoint)
        {
            GridLocation<long> p1, p2;
            bool inside = false;

            if (_polygonPoints.Count < 3)
            {
                return inside;
            }

            var oldPoint = _polygonPoints.Last();

            for (int i = 0; i < _polygonPoints.Count; i++)
            {
                var newPoint = new GridLocation<long>(_polygonPoints[i].X, _polygonPoints[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < testPoint.X) == (testPoint.X <= oldPoint.X)
                    && (testPoint.Y - p1.Y) * (p2.X - p1.X)
                    < (p2.Y - p1.Y) * (testPoint.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }

        private long GetArea((GridLocation<long>, GridLocation<long>) pair)
        {
            var xDiff = Math.Abs(pair.Item1.X - pair.Item2.X) + 1;
            var yDiff = Math.Abs(pair.Item1.Y - pair.Item2.Y) + 1;
            return xDiff * yDiff;
        }

        public class myLine
        {
            public GridLocation<long> start;
            public GridLocation<long> end;
            public long? xVal;
            public long? yVal;
            public long Length;

            public myLine(GridLocation<long> start, GridLocation<long> end)
            {
                this.start = start;
                this.end = end;
                xVal = start.X == end.X ? start.X : null;
                yVal = start.Y == end.Y ? start.Y : null;
                Length = (long)GridLocationHelper.GetDistanceBetween(start, end);
            }

            public bool IsPointOnLine(GridLocation<long> point)
            {
                return (point.X <= Math.Max(start.X, end.X) &&
                    point.X >= Math.Min(start.X, end.X) &&
                    point.Y <= Math.Max(start.Y, end.Y) &&
                    point.Y >= Math.Min(start.Y, end.Y));
            }

            public bool WithinOtherLine(myLine other)
            {
                var onSameX = this.xVal != null && other.xVal != null && this.xVal == other.xVal;
                var onSameY = this.yVal != null && other.yVal != null && this.yVal == other.yVal;
                if (!onSameX && !onSameY)
                {
                    return false;
                }
                if (onSameX)
                {
                    return ((this.start.X >= other.start.X && this.start.X <= other.end.X) ||
                        (this.start.X <= other.start.X && this.start.X >= other.end.X));
                }
                if (onSameY)
                {
                    return ((this.start.Y >= other.start.Y && this.start.Y <= other.end.Y) ||
                        (this.start.Y <= other.start.Y && this.start.Y >= other.end.Y));
                }
                throw new Exception("Shouldn't get here");
                return false;
            }
        }
    }
}