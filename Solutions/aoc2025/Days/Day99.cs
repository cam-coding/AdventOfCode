using AdventLibrary;
using AdventLibrary.CustomObjects;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers.Grids;

namespace aoc2025
{
    public class Day99 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private List<GridLocation<long>> _polygon;
        private List<LineObject<long>> _lines;
        private List<(GridLocation<long>, GridLocation<long>)> _polygonLinesAsPairs;

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

        private object Part3(bool isTest = false)
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

            _polygon = new List<GridLocation<long>>();
            foreach (var line in lines)
            {
                var longs = line.GetLongsFromString();
                var loc = new GridLocation<long>(longs[0], longs[1]);
                _polygon.Add(loc);
            }

            var slantList = new List<Slant>();
            for (var i = 1; i < _polygon.Count; i++)
            {
                var mySlant = new Slant(
                    _polygon[i - 1].X,
                    _polygon[i].X,
                    _polygon[i - 1].Y,
                    _polygon[i].Y);
                slantList.Add(mySlant);
            }
            var mySlant2 = new Slant(
                _polygon[_polygon.Count - 1].X,
                _polygon[0].X,
                _polygon[_polygon.Count - 1].Y,
                _polygon[0].Y);
            slantList.Add(mySlant2);

            var pairs = _polygon.GetPairs_Unique();

            long best = 0;

            for (var i = 0; i < pairs.Count; i++)
            {
                var pair = pairs[i];
                var startX = Math.Min(pair.Item1.X, pair.Item2.X) + 1;
                var endX = Math.Max(pair.Item1.X, pair.Item2.X) - 1;
                var startY = Math.Min(pair.Item1.Y, pair.Item2.Y) + 1;
                var endY = Math.Max(pair.Item1.Y, pair.Item2.Y) - 1;

                var valid = true;
                if (startX == 3 && endX == 10 && startY == 2 && endY == 2)
                {
                    Console.WriteLine("hello");
                }
                if (GetArea(pairs[i]) < best)
                {
                    continue;
                }
                /*
                if (!valid)
                {
                    break;
                }
                for (var x = startX + 1; x < endX; x++)
                {
                    if (!valid)
                    {
                        break;
                    }
                    for (var y = startY + 1; y < endY; y++)
                    {
                        var loc2 = new GridLocation<long>(x, y);
                        if (!IsPointInPolygon4(loc2))
                        {
                            valid = false;
                            break;
                        }

                        if (!slantList.Any(slant =>
                        InSlant(slant, x, y)))
                        {
                            valid = false;
                            break;
                        }
                    }
                }
                */
                /*
                var loc2 = new GridLocation<long>(endX - 1, startY + 1);
                var loc3 = new GridLocation<long>(endX - 1, endY - 1);
                var loc4 = new GridLocation<long>(startX + 1, startY + 1);
                var loc5 = new GridLocation<long>(startX + 1, endY - 1);
                */

                while (
                    startX <= endX && startY <= endY && valid)
                {
                    var loc2 = new GridLocation<long>(endX, startY);
                    var loc3 = new GridLocation<long>(endX, endY);
                    var loc4 = new GridLocation<long>(startX, startY);
                    var loc5 = new GridLocation<long>(startX, endY);

                    if (!IsPointInPolygon(loc2) || !IsPointInPolygon(loc3) || !IsPointInPolygon(loc4) || !IsPointInPolygon(loc5))
                    {
                        valid = false;
                        break;
                    }
                    startX++;
                    endX--;
                    startY++;
                    endY--;
                }
                if (valid)
                {
                    best = Math.Max(best, GetArea(pairs[i]));
                }
            }
            return best;
        }

        private object Part2(bool isTest = false)
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

            _polygonLinesAsPairs = new List<(GridLocation<long>, GridLocation<long>)>();

            _polygon = new List<GridLocation<long>>();
            foreach (var line in lines)
            {
                var longs = line.GetLongsFromString();
                var loc = new GridLocation<long>(longs[0], longs[1]);
                _polygon.Add(loc);
            }

            var slantList = new List<Slant>();
            var lineList = new List<LineObject<long>>();
            for (var i = 1; i < _polygon.Count; i++)
            {
                var mySlant = new Slant(
                    _polygon[i - 1].X,
                    _polygon[i].X,
                    _polygon[i - 1].Y,
                    _polygon[i].Y);
                slantList.Add(mySlant);
                var myLine = new LineObject<long>(_polygon[i - 1], _polygon[i]);
                lineList.Add(myLine);
                _polygonLinesAsPairs.Add((_polygon[i - 1], _polygon[i]));
            }
            var mySlant2 = new Slant(
                _polygon[_polygon.Count - 1].X,
                _polygon[0].X,
                _polygon[_polygon.Count - 1].Y,
                _polygon[0].Y);
            slantList.Add(mySlant2);
            var myLine2 = new LineObject<long>(_polygon[_polygon.Count - 1], _polygon[0]);
            lineList.Add(myLine2);
            _polygonLinesAsPairs.Add((_polygon[_polygon.Count - 1], _polygon[0]));
            _lines = lineList;

            var uniquePairs = _polygon.GetPairs_Unique();

            long best = 0;

            for (var i = 0; i < uniquePairs.Count; i++)
            {
                var pair = uniquePairs[i];

                if (!isTest && !(GetArea(uniquePairs[i]) > best))
                {
                    continue;
                }
                var point1 = new GridLocation<long>(pair.Item1.X, pair.Item1.Y);
                var point2 = new GridLocation<long>(pair.Item1.X, pair.Item2.Y);
                var point3 = new GridLocation<long>(pair.Item2.X, pair.Item1.Y);
                var point4 = new GridLocation<long>(pair.Item2.X, pair.Item2.Y);

                var myPoints = new List<GridLocation<long>>() { point1, point2, point3, point4 };
                var pairsMakingRectLines = new List<(GridLocation<long>, GridLocation<long>)>();
                pairsMakingRectLines.Add((point1, point2));
                pairsMakingRectLines.Add((point1, point3));
                pairsMakingRectLines.Add((point4, point2));
                pairsMakingRectLines.Add((point4, point3));

                var LLLL = GridLocationHelper.GetDistanceBetween(point1, point3);
                var wwww = GridLocationHelper.GetDistanceBetween(point1, point2);
                var area = LLLL * wwww;

                if (pair.Item1.X == 9 && pair.Item2.X == 2 && pair.Item1.Y == 5 && pair.Item2.Y == 3)
                {
                    Console.WriteLine("hello");
                }

                if (IsPointInPolygon(point1) && IsPointInPolygon(point2) && IsPointInPolygon(point3) && IsPointInPolygon(point4))
                {
                    /*
                    var valid = true;
                    var rectLines = new List<LineObject<long>>();
                    rectLines.Add(new LineObject<long>(point1, point2));
                    rectLines.Add(new LineObject<long>(point1, point3));
                    rectLines.Add(new LineObject<long>(point4, point2));
                    rectLines.Add(new LineObject<long>(point4, point3));
                    foreach (var line in lineList)
                    {
                        if (!valid)
                        {
                            break;
                        }
                        foreach (var rectLine in rectLines)
                        {
                            if (IntersectOtherLines(line, rectLine))
                            {
                                valid = false;
                                break;
                            }
                        }
                    }
                    if (valid)
                        {
                            best = Math.Max(best, GetArea(pairs[i]));
                            // best = Math.Max(best, (long)area);
                        }
                    }*/
                    var valid = true;
                    if (pair.Item1.X == 11 && pair.Item1.Y == 1 && pair.Item2.X == 7 && pair.Item2.Y == 3)
                    {
                        Console.WriteLine("hello");
                    }
                    foreach (var pairsMakingRedLine in _polygonLinesAsPairs)
                    {
                        foreach (var pairRectLine in pairsMakingRectLines)
                        {
                            if (BetterIntersect(
                                pairsMakingRedLine.Item1,
                                pairsMakingRedLine.Item2,
                                pairRectLine.Item1,
                                pairRectLine.Item2))
                            {
                                valid = false;
                                break;
                            }
                        }
                    }
                    if (valid)
                    {
                        var thisArea = GetArea(uniquePairs[i]);
                        if (isTest)
                        {
                            AllPointsInPolygon_BruteForceCheck(pair);
                            Console.WriteLine($"Area: {thisArea}. {pair.Item1.X},{pair.Item1.Y} &  {pair.Item2.X},{pair.Item2.Y}");
                        }
                        best = Math.Max(best, thisArea);
                        // best = Math.Max(best, (long)area);
                    }
                }
            }
            return best;
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

        private bool BetterIntersect(
            GridLocation<long> A,
            GridLocation<long> B,
            GridLocation<long> C,
            GridLocation<long> D)
        {
            var main = new LineObject<long>(A, B, false);
            var other = new LineObject<long>(C, D, false);
            if (main.Equals(other))
            {
                return false;
            }
            decimal delta = main.A * other.B - other.A * main.B;

            if (delta == 0)
                return false;

            decimal x = (other.B * main.C - main.B * other.C) / delta;
            decimal y = (main.A * other.C - other.A * main.C) / delta;
            var myDumb = x + y;
            var loc = new GridLocation<long>((long)x, (long)y);

            var pointInPoints = _polygon.Any(x => x.Equals(loc));
            var onTheLine = onSegment(A, loc, B);
            if (pointInPoints || !onTheLine)
            {
                return false;
            }
            return !onTheLine;
            /*
            if (!pointInPoints && (onSegment(A, loc, B) && onSegment(C, loc, D)))
            {
                return true;
            }
            return false;
            */

            /*
            if (!IsPointInPolygon(loc))
            {
                var anyAny = _pairs.Any(pair => PointOnLine(loc, pair));
                return !anyAny;
            }
            else
            {
                return false;
            }*/
        }

        private bool EdgeIntersection(GridLocation<long> A, GridLocation<long> B,
            GridLocation<long> C, GridLocation<long> D)
        {
            //A, B: endpoints of first segment
            //C, D: endpoints of second segment

            //Compute orientations of key point triplets
            var o1 = orientation(A, B, C);
            var o2 = orientation(A, B, D);
            var o3 = orientation(C, D, A);
            var o4 = orientation(C, D, B);

            // General case: segments intersect in their interiors
            if (o1 != o2 && o3 != o4)
                return true;

            //Special cases: collinear and overlapping segments
            if (o1 == 0 && onSegment(A, C, B)) return true;
            if (o2 == 0 && onSegment(A, D, B)) return true;
            if (o3 == 0 && onSegment(C, A, D)) return true;
            if (o4 == 0 && onSegment(C, B, D)) return true;

            return false;
        }

        private int orientation(GridLocation<long> p, GridLocation<long> q, GridLocation<long> r)
        {
            var val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
            if (val == 0)
            {
                return 0;
            }
            if (val > 0)
            {
                return 1;
            }
            return 2;
        }

        private bool onSegment(GridLocation<long> p, GridLocation<long> q, GridLocation<long> r)
        {
            return (q.X <= Math.Max(p.X, r.X) &&
                q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) &&
                q.Y >= Math.Min(p.Y, r.Y));
        }

        private bool IntersectOtherLines(LineObject<long> main, LineObject<long> other)
        {
            decimal delta = main.A * other.B - other.A * main.B;

            if (delta == 0)
                return false;

            decimal x = (other.B * main.C - main.B * other.C) / delta;
            decimal y = (main.A * other.C - other.A * main.C) / delta;
            var myDumb = x + y;
            var loc = new GridLocation<long>((long)x, (long)y);
            var anyAny = _polygonLinesAsPairs.Any(pair => PointOnLine(loc, pair));
            var within = IsPointInPolygon(loc) || anyAny;
            if (anyAny || !within)
            {
                return false;
            }

            return !within;

            /*
            if (!IsPointInPolygon(loc))
            {
                var anyAny = _pairs.Any(pair => PointOnLine(loc, pair));
                return !anyAny;
            }
            else
            {
                return false;
            }*/
        }

        private bool PointOnLine(GridLocation<long> point, (GridLocation<long>, GridLocation<long>) pair)
        {
            var dxc = point.X - pair.Item1.X;
            var dyc = point.Y - pair.Item1.Y;

            var dxl = pair.Item2.X - pair.Item1.X;
            var dyl = pair.Item2.Y - pair.Item1.Y;

            var cross = dxc * dyl - dyc * dxl;
            if (cross != 0)
            {
                return false;
            }
            if (Math.Abs(dxl) >= Math.Abs(dyl))
                return dxl > 0 ?
                  pair.Item1.X <= point.X && point.X <= pair.Item2.X :
                  pair.Item2.X <= point.X && point.X <= pair.Item1.X;
            else
                return dyl > 0 ?
                  pair.Item1.Y <= point.Y && point.Y <= pair.Item2.Y :
                  pair.Item2.Y <= point.Y && point.Y <= pair.Item1.Y;
        }

        private long GetArea((GridLocation<long>, GridLocation<long>) pair)
        {
            var xDiff = Math.Abs(pair.Item1.X - pair.Item2.X) + 1;
            var yDiff = Math.Abs(pair.Item1.Y - pair.Item2.Y) + 1;
            return xDiff * yDiff;
        }

        private bool InSlant(Slant slant, long x, long y)
        {
            if (slant.startX == x &&
                slant.endX == x &&
                slant.startY <= y &&
                slant.endY >= y)
            {
                return true;
            }
            if (slant.startY == y &&
                slant.endY == y &&
                slant.startX <= x &&
                slant.endX >= x)
            {
                return true;
            }
            return false;
        }

        public bool IsPointInPolygon2(GridLocation<long> testPoint)
        {
            if (_polygon.Any(x => x.Equals(testPoint)))
            {
                return true;
            }
            bool result = false;
            int j = _polygon.Count() - 1;
            for (int i = 0; i < _polygon.Count(); i++)
            {
                float iX = _polygon[i].X;
                float iY = _polygon[i].Y;
                float jX = _polygon[j].X;
                float jY = _polygon[j].Y;
                float testX = testPoint.X;
                float testY = testPoint.Y;
                if (iY < testPoint.Y && jY >= testY ||
                    jY < testPoint.Y && iY >= testY)
                {
                    if (iX + (testY - iY) /
                       (jY - iY) *
                       (jX - iX) < testX)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        public bool IsPointInPolygon(GridLocation<long> testPoint)
        {
            return (IsOnPolygonLines(testPoint) || IsPointInPolygon_WITHIN(testPoint));
        }

        public bool IsOnPolygonLines(GridLocation<long> testPoint)
        {
            return _polygonLinesAsPairs.Any(x => onSegment(x.Item1, testPoint, x.Item2));
        }

        public bool IsPointInPolygon_WITHIN(GridLocation<long> testPoint)
        {
            GridLocation<long> p1, p2;
            bool inside = false;

            if (_polygon.Count < 3)
            {
                return inside;
            }

            var oldPoint = _polygon.Last();

            for (int i = 0; i < _polygon.Count; i++)
            {
                var newPoint = new GridLocation<long>(_polygon[i].X, _polygon[i].Y);

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

        public class Slant
        {
            public Slant(
                long x1,
                long x2,
                long y1,
                long y2)
            {
                startX = Math.Min(x1, x2);
                startY = Math.Min(y1, y2);
                endX = Math.Max(x1, x2);
                endY = Math.Max(y1, y2);
            }

            public long startX;
            public long endX;
            public long startY;
            public long endY;
        }
    }
}