using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventLibrary.CustomObjects
{
    public class CustomNode<T>
    {
        public CustomNode(T value) : this(value, string.Empty) { }

        public CustomNode(T value, string name)
        {
            Value = value;
            Name = name;
            EdgesOut = new List<CustomEdge<T>>();
        }

        public T Value { get; set; }

        public String Name { get; set; }

        public List<CustomEdge<T>> EdgesOut { get; set; }

        public List<CustomEdge<T>> EdgesIn { get; set; }
    }
}
