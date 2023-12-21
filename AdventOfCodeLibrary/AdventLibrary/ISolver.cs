namespace AdventLibrary
{
    public interface ISolver
    {
        Solution Solve(string filePath, bool isTest = false);
    }
}