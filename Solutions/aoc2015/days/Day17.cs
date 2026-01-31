using AdventLibrary;

namespace aoc2015
{
    public class Day17 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        List<int> _containers = new List<int>();
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var numbers = ParseInput.GetNumbersFromFile(_filePath);
            _containers.Clear();
            foreach (var num in numbers)
            {
                _containers.Add(num);
            }
            _containers.Sort();
            _containers.Reverse();

            var listy2 = new int[20];
            return Containerize(listy2, 0);
        }

        private int Containerize(int[] arr, int i)
        {
            var total = TotalArray(arr);
            if (total == 150)
            {
                return 1;
            }
            if (i == arr.Length)
            {
                return 0;
            }
            else if (total < 150)
            {
                var result = 0;
                var newArr = new List<int>(arr).ToArray();
                newArr[i] = 1;
                result += Containerize(arr, i + 1);
                result += Containerize(newArr, i + 1);
                return result;
            }
            return 0;
        }

        private int TotalArray(int[] arr)
        {
            var total = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 1)
                {
                    total += _containers[i];
                }
            }
            return total;
        }

        private Dictionary<int, int> _counter = new Dictionary<int, int>();
        private int Containerize2(int[] arr, int i)
        {
            var total = TotalArray(arr);
            if (total == 150)
            {
                var count = arr.Count(x => x == 1);
                _counter.TryAdd(count, 0);
                _counter[count]++;
                return 0;
            }
            if (i == arr.Length)
            {
                return 0;
            }
            else if (total < 150)
            {
                var result = 0;
                var newArr = new List<int>(arr).ToArray();
                newArr[i] = 1;
                result += Containerize2(arr, i + 1);
                result += Containerize2(newArr, i + 1);
                return 0;
            }
            return 0;
        }

        private object Part2()
        {
            var numbers = ParseInput.GetNumbersFromFile(_filePath);
            _containers.Clear();
            foreach (var num in numbers)
            {
                _containers.Add(num);
            }
            _containers.Sort();
            _containers.Reverse();

            var listy2 = new int[20];
            Containerize2(listy2, 0);
            var min = _counter.Min(x => x.Key);
            return _counter[min];
        }
    }
}