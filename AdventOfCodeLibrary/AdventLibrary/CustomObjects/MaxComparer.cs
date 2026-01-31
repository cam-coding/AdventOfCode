namespace AdventLibrary.CustomObjects
{
    public class MaxComparer : IComparer<int>
    {
        public int Compare(int x, int y) => y.CompareTo(x);
    }
}
