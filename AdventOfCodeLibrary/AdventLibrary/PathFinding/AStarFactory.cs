using AdventLibrary.Helpers.Grids;

namespace AdventLibrary.PathFinding
{
    public static class AStarFactory
    {
        public static AStarSearcher<GridLocation<int>> CreateFromGrid<T>(GridObject<T> grid)
        {
            var aStarGrid = new AStar_GridObject<T>(grid, new List<T>(), grid.GetOrthogonalNeighbours);
            var aStarSearcher = new AStarSearcher<GridLocation<int>>(aStarGrid);
            return aStarSearcher;
        }
    }
}