namespace AdventLibrary.Helpers.Grids
{
    /* Class originally created for 2024 Day 15
     * Similar to grid walker, the guy walks around the grid.
     * * Won't walk into walls
     * * Walks into empty space
     * When reaching a moveable object (or line of moveable objects)
     * Will push them all by one if there is room past the end of the objects
     * */
    public class GridPusher<T>
    {
        public GridPusher(
            GridObject<T> grid,
            GridLocation<int> pusherLocation,
            T pusherValue,
            T emptySpaceValues,
            T wallValues,
            T movableValues) : this(
                grid,
                pusherLocation,
                pusherValue,
                new List<T>() { emptySpaceValues },
                new List<T>() { wallValues },
                new List<T>() { movableValues })
        {
        }

        public GridPusher(
            GridObject<T> grid,
            GridLocation<int> pusherLocation,
            T pusherValue,
            List<T> emptySpaceValues,
            List<T> wallValues,
            List<T> movableValues)
        {
            Grid = grid;
            PusherLocation = pusherLocation;
            PusherValue = pusherValue;
            EmptySpaceValues = emptySpaceValues;
            WallValues = wallValues;
            MovableValues = movableValues;
        }

        public GridObject<T> Grid { get; set; }

        public List<T> EmptySpaceValues { get; set; }

        public List<T> WallValues { get; set; }

        public List<T> MovableValues { get; set; }

        public GridLocation<int> PusherLocation { get; set; }

        public T PusherValue { get; set; }

        public bool MoveAndPush(GridLocation<int> dir)
        {
            var nextSpaceValue = Grid.Get(PusherLocation + dir);
            if (EmptySpaceValues.Contains(nextSpaceValue))
            {
                MovePusher(dir);
            }
            else if (WallValues.Contains(nextSpaceValue))
            {
                return false;
            }
            else if (MovableValues.Contains(nextSpaceValue))
            {
                var count = 1;
                var old = PusherLocation;
                // we already know the current nextSpace is moveable so check the one beyond
                var currentSpace = PusherLocation + dir + dir;
                while (MovableValues.Contains(Grid.Get(currentSpace)))
                {
                    currentSpace = currentSpace + dir;
                    count++;
                }

                // this is the space after the line of moveable objects. Either a wall or empty
                var val = Grid.Get(currentSpace);
                if (WallValues.Contains(val))
                {
                    // there's a wall after the line of moveable objects, so we can't push anything
                    return false;
                }
                else
                {
                    for (var i = count; i > 0; i--)
                    {
                        Grid.Set(currentSpace, Grid.Get(currentSpace - dir));
                        currentSpace = currentSpace - dir;
                    }

                    // now we move the pusher into the spot originally taken up by the first moveable object
                    MovePusher(dir);
                }
            }
            return true;
        }

        private void MovePusher(GridLocation<int> dir)
        {
            var oldLocation = PusherLocation;
            PusherLocation = PusherLocation + dir;
            var oldValueAtNewLocation = Grid.Get(PusherLocation);

            // if the new space was empty we just re-use that value or "swap" the empty and the pusher
            // if it was full we just place in the default empty value
            if (!EmptySpaceValues.Contains(oldValueAtNewLocation))
            {
                oldValueAtNewLocation = EmptySpaceValues[0];
            }

            Grid.Set(oldLocation, oldValueAtNewLocation);
            Grid.Set(PusherLocation, PusherValue);
        }
    }
}
