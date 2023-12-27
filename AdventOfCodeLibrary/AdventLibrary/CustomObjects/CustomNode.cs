using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventLibrary.CustomObjects
{
    public class CustomNode<T>
    {
        public CustomNode(T key)
        {
            Key = key;
            EdgesOut = new List<CustomEdge<T>>();
        }

        public T Key { get; set; }

        public List<CustomEdge<T>> EdgesOut { get; set; }

        public List<CustomEdge<T>> EdgesIn { get; set; }
    }
}
