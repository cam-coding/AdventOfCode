using System.Diagnostics;

namespace AdventLibrary
{
    public class Solution
    {
        private Stopwatch _timer;
        private object _part1;
        private object _part2;

        public Solution()
        {
            _timer = new Stopwatch();
            _timer.Start();
            _part1 = 0;
            _part2 = 0;
        }
        public Solution(object part1, object part2)
        {
            _part1 = part1;
            _part2 = part2;
        }

        public Solution(object part1, TimeSpan span1, object part2, TimeSpan span2)
        {
            _part1 = part1;
            _part2 = part2;
            TimePart1 = span1;
            TimePart2 = span2;
        }

        public object Part1
        {
            get
            {
                return _part1;
            }
            set
            {
                _part1 = value;
                if (_timer.IsRunning)
                {
                    TimePart1 = _timer.Elapsed;
                    _timer.Restart();
                }
            }
        }

        public object Part2
        {
            get
            {
                return _part2;
            }
            set
            {
                _part2 = value;
                if (_timer.IsRunning)
                {
                    TimePart2 = _timer.Elapsed;
                    _timer.Restart();
                }
            }
        }

        public TimeSpan? TimePart1;

        public TimeSpan? TimePart2;

        public void Output()
        {
            Console.WriteLine("Part 1: " + _part1.ToString());
            Console.WriteLine("Part 2: " + _part2.ToString());
        }

        public void OutputWithTime()
        {
            var str = "Part 1: " + _part1.ToString() + "\n";
            str += OutputRunTime(TimePart1);
            str += "Part 2: " + _part2.ToString() + "\n";
            str += OutputRunTime(TimePart2);
            Console.WriteLine(str);
        }

        public string GetHistoryOutput()
        {
            string output = $"Part 1: {_part1}      at: {DateTime.Now}\n";
            output += $"Part 2: {_part2}      at: {DateTime.Now}\n";
            output += "\n";

            return output;
        }

        private string OutputRunTime(TimeSpan? timeSpan)
        {
            if (timeSpan != null)
            {
                if (timeSpan.Value.TotalMilliseconds > 1000)
                {
                    return string.Format("                                                 Runtime in M:S:m {0}:{1}\n", Math.Floor(timeSpan.Value.TotalMinutes), timeSpan.Value.ToString("ss\\:fff"));
                }
                else
                {
                    return $"                                                 RunTime in miliseconds: {timeSpan.Value.Milliseconds}\n";
                }
            }
            return string.Empty;
        }
    }
}