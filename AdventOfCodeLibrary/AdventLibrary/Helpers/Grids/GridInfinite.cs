using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventLibrary.Helpers.Grids
{
    public class GridInfinite<T>
    {
        private Dictionary<GridLocation<int>, T> _dict;
        private T _defaultValue;

        public GridInfinite()
        {
            _dict = new Dictionary<GridLocation<int>, T>();
            _defaultValue = default(T);
        }

        public GridInfinite(GridObject<T> startingGrid, T defaultValue = default(T))
        {
            _dict = new Dictionary<GridLocation<int>, T>();
            _defaultValue = defaultValue;

            foreach (var loc in startingGrid.GetAllLocations())
            {
                _dict.Add(loc, startingGrid.Get(loc));
            }
        }

        public GridInfinite(List<GridLocation<int>> locations, T value, T defaultValue = default(T))
        {
            _dict = new Dictionary<GridLocation<int>, T>();
            _defaultValue = defaultValue;

            foreach (var loc in locations)
            {
                _dict.Add(loc, value);
            }
        }

        public T Get(GridLocation<int> loc)
        {
            if (_dict.ContainsKey(loc))
            {
                return _dict[loc];
            }
            return _defaultValue;
        }

        public void Set(GridLocation<int> loc, T value)
        {
            if (_dict.ContainsKey(loc))
            {
                _dict[loc] = value;
            }
            else
            {
                _dict.Add(loc, value);
            }
        }

        public List<GridLocation<int>> GetAllLocations()
        {
            return _dict.Keys.ToList();
        }

        public List<GridLocation<int>> GetAllLocationsWhere(Func<T, bool> filter = null)
        {
            var listy = new List<GridLocation<int>>();
            foreach (var pair in _dict)
            {
                if (filter(pair.Value))
                {
                    listy.Add(pair.Key);
                }
            }

            return listy;
        }

        public List<GridLocation<int>> GetAllLocationWhereCellEqualsValue(T value)
        {
            var listy = new List<GridLocation<int>>();
            foreach (var pair in _dict)
            {
                if (pair.Value.Equals(value))
                {
                    listy.Add(pair.Key);
                }
            }

            return listy;
        }

        public List<GridLocation<int>> GetNeighbours(GridLocation<int> currentLocation, List<GridLocation<int>> directions)
        {
            var result = new List<GridLocation<int>>();
            foreach (var direction in directions)
            {
                result.Add(currentLocation + direction);
            }
            return result;
        }

        /// <summary>
        /// Will return a default value if the neighbour isn't in the dict/grid
        /// </summary>
        public List<T> GetNeighbourValues(GridLocation<int> currentLocation, List<GridLocation<int>> directions)
        {
            var result = new List<T>();
            foreach (var direction in directions)
            {
                if (_dict.ContainsKey(currentLocation + direction))
                {
                    result.Add(_dict[currentLocation + direction]);
                }
                else
                {
                    result.Add(_defaultValue);
                }
            }
            return result;
        }

        /// <summary>
        /// Will only return values that are in the dict/grid
        /// </summary>
        public List<T> GetNeighbourValues_NoDefaultValues(GridLocation<int> currentLocation, List<GridLocation<int>> directions)
        {
            var result = new List<T>();
            foreach (var direction in directions)
            {
                if (_dict.ContainsKey(currentLocation + direction))
                {
                    result.Add(_dict[currentLocation + direction]);
                }
            }
            return result;
        }
    }
}