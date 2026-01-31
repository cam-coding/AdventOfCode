using AdventLibrary;
using AdventLibrary.CustomObjects;

namespace aoc2021
{
    public class Day24 : ISolver
    {
        private string _filePath;
        private Registry _registry;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private List<bool> _isDivide26List;
        private List<int> _xFactorList;
        private List<int> _yFactorList;
        private string _answer;
        private Dictionary<(int, long, int), long> _memo;

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            var solution = new Solution();
            solution.Part1 = Part1(isTest);
            solution.Part2 = Part2(isTest);
            return solution;
        }

        private object Part1(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            _registry = new Registry(4, 'w', 0);
            _registry.SetAll(0);

            var groups = new List<List<string>>();
            var currentGroup = new List<string>();




            for (var i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("inp"))
                {
                    if (currentGroup.Count > 0)
                    {
                        groups.Add(currentGroup);
                    }
                    currentGroup = new List<string>();
                }
                currentGroup.Add(lines[i]);
            }
            groups.Add(currentGroup);

            var possibleZValues = new List<int>() { 0 };
            var possibleZValueOutputs = new List<List<int>>();

            /* should go through all the groups and pull out:
             * isDivide26, xFactor, yFactor
             * then just call GetInputGivenOutput and get answer
             * maybe pair an input/out with the digits so far?*/

            _yFactorList = new List<int> { };
            _xFactorList = new List<int> { };
            _isDivide26List = new List<bool> { };
            _answer = string.Empty;
            _memo = new Dictionary<(int, long, int), long>();


            for (var i = 0; i < groups.Count; i++)
            {
                var isDivide26 = groups[i][4].Contains("26");
                _isDivide26List.Add(isDivide26);
                var xFactor = StringParsing.GetIntssWithNegativesFromString(groups[i][5])[0];
                _xFactorList.Add(xFactor);
                var yFactor = StringParsing.GetIntsFromString(groups[i][15])[0];
                _yFactorList.Add(yFactor);
            }
            /*
            for (var i = 0; i < groups.Count; i++)
            {
                List<int> inputs = new List<int>() { 0 };
                if (i > 0)
                {
                    inputs = possibleZValueOutputs[i - 1];
                }
                var outputs = new HashSet<int>();

                foreach (var zInput in inputs)
                {
                    for (var w = 1; w < 10; w++)
                    {
                        outputs.Add(RunGroup(groups[i], w, zInput));
                    }
                }
                possibleZValueOutputs.Add(outputs.ToList());
            }*/

            /*
            Recursion(0, 0);

            _answer.Reverse();*/
            Recursion2(0, new List<(long z, string max)>() { (0, string.Empty) });
            return _answer;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            _registry = new Registry(4, 'w', 0);
            _registry.SetAll(0);

            var groups = new List<List<string>>();
            var currentGroup = new List<string>();

            for (var i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("inp"))
                {
                    if (currentGroup.Count > 0)
                    {
                        groups.Add(currentGroup);
                    }
                    currentGroup = new List<string>();
                }
                currentGroup.Add(lines[i]);
            }
            groups.Add(currentGroup);

            _yFactorList = new List<int> { };
            _xFactorList = new List<int> { };
            _isDivide26List = new List<bool> { };
            _answer = string.Empty;
            _memo = new Dictionary<(int, long, int), long>();


            for (var i = 0; i < groups.Count; i++)
            {
                var isDivide26 = groups[i][4].Contains("26");
                _isDivide26List.Add(isDivide26);
                var xFactor = StringParsing.GetIntssWithNegativesFromString(groups[i][5])[0];
                _xFactorList.Add(xFactor);
                var yFactor = StringParsing.GetIntsFromString(groups[i][15])[0];
                _yFactorList.Add(yFactor);
            }
            Recursion3(0, new List<(long z, string max)>() { (0, string.Empty) });
            return _answer;
        }

        /*
        private long TestProgram(List<List<string>> groups, string input)
        {
            var listy = input.ToList().Select(x => int.Parse(x.ToString())).ToList();
            long z = 0;

            for (var i = 0; i < groups.Count; i++)
            {
                z = RunGroup(groups[i], listy[i], z);
            }

            return z;
        }

        private long RunGroup(List<string> program, int w, long z)
        {
            _registry.SetAll(0);
            _registry.Set("z", z.ToString());

            foreach (var line in program)
            {
                PerformLine(line, w);
            }

            return _registry.GetValue("z");
        }*/

        private void Recursion2(int column, List<(long z, string max)> zValues)
        {
            if (column == 14)
            {
                var allAnswers = zValues.Where(x => x.z == 0);
                var answer = zValues.First(x => x.z == 0);
                // _answer = answer.max;

                var allAnswers2 = allAnswers.Select(x => x.max);
                var allAnswers3 = allAnswers2.OrderDescending();
                var blah = allAnswers3.First();
            }
            var zLookup = new HashSet<long>();
            var newZValues = new List<(long, string)>();
            foreach (var item in zValues)
            {
                for (var i = 9; i > 0; i--)
                {
                    var val = GetOutput(column, item.z, i);
                    if (column == 13 && val != 0)
                    {
                        continue;
                    }
                    if (zLookup.Add(val))
                    {
                        newZValues.Add((val, item.max + i));
                    }
                }
            }
            Recursion2(column + 1, newZValues);
        }
        private void Recursion3(int column, List<(long z, string max)> zValues)
        {
            Console.WriteLine("reached column " + column);
            if (column == 14)
            {
                var answer = zValues.First(x => x.z == 0);
                _answer = answer.max;
                return;
            }
            var zLookup = new HashSet<long>();
            var newZValues = new List<(long, string)>();
            var looper = 10;
            if (column == 0)
            {
                // due to some messing around I know it starts with a 1 and that reduces the problem space a lot
                looper = 2;
            }
            foreach (var item in zValues)
            {
                for (var i = 1; i < looper; i++)
                {
                    var val = GetOutput(column, item.z, i);
                    if (column == 13 && val != 0)
                    {
                        continue;
                    }
                    if (zLookup.Add(val))
                    {
                        newZValues.Add((val, item.max + i));
                    }
                }
            }
            Recursion3(column + 1, newZValues);
        }

        /* This is an equation reduction of the "programs"
         * after doing a bunch of work by hand to get it. */
        private long GetOutput(int column, long z, int w)
        {
            var tuple = (column, z, w);
            if (_memo.ContainsKey(tuple))
            {
                return _memo[tuple];
            }
            var isDivide26 = _isDivide26List[column];
            var xFactor = _xFactorList[column];
            var yFactor = _yFactorList[column];

            // this is always 0 or 1
            var xValBool = (z % 26) + xFactor != w;
            var yVal = xValBool ? 26 : 1;
            long zHalfwayVal = isDivide26 ? z / 26 : z;
            zHalfwayVal *= yVal;
            var yVal2 = xValBool ? w + yFactor : 0;
            long ans = zHalfwayVal + yVal2;

            _memo.Add(tuple, ans);
            return ans;
        }

        private void PerformLine(string line, int input)
        {
            var tokens = StringParsing.GetRealTokens(line, _delimiterChars);
            if (line.Contains("inp"))
            {
                _registry.Set(tokens[1], input);
            }
            else if (line.Contains("add"))
            {
                _registry.Add(tokens[1], tokens[2]);
            }
            else if (line.Contains("mul"))
            {
                _registry.Mul(tokens[1], tokens[2]);
            }
            else if (line.Contains("div"))
            {
                _registry.Div(tokens[1], tokens[2]);
            }
            else if (line.Contains("mod"))
            {
                _registry.Mod(tokens[1], tokens[2]);
            }
            else if (line.Contains("eql"))
            {
                var val1 = _registry.GetValue(tokens[1]);
                var val2 = _registry.GetValue(tokens[2]);
                var num = val1 == val2 ? 1 : 0;
                _registry.Set(tokens[1], num);
            }
        }
    }
}