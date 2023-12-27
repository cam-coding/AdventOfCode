using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventLibrary.CustomObjects
{
    public class LineObject<T> where T : INumber<T>
    {
        public LineObject((T y, T x) start, (T y, T x) end) : this(start.y, start.x, end.y, end.x) { }
        public LineObject(List<(T y, T x)> point) : this(point[0].y, point[0].x, point[1].y, point[1].x) { }

        public LineObject(T y1, T x1, T y2, T x2)
        {
            A = y2 - y1;
            B = x1 - x2;
            C = A * x1 + B * y1;
        }

        public T A { get; set; }

        public T B { get; set; }

        public T C { get; set; }

        public bool InLine(T y, T x)
        {
            var temp = A * x + B * y;
            return temp == C;
        }
    }
}
