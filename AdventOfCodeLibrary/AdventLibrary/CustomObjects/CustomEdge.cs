namespace AdventLibrary.CustomObjects
{
    public class CustomEdge<T>
    {
        public CustomEdge(CustomNode<T> start, CustomNode<T> end, bool directed = false, int weight = 1)
        {
            Start = start;
            End = end;
            Directed = directed;
            Weight = weight;
        }

        public CustomNode<T> Start { get; set; }

        public CustomNode<T> End { get; set; }

        public int Weight { get; set; }

        public bool Directed { get; set; }

        public string GetKey(char seperator = '-')
        {
            return Start.Key.ToString() + seperator + End.Key.ToString();
        }

        public CustomNode<T> GetOtherEnd(CustomNode<T> start)
        {
            if (Directed || start.Equals(Start))
            {
                return End;
            }
            return Start;
        }
    }
}
