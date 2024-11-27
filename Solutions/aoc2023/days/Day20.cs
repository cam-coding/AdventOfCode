using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace aoc2023
{
    public class Day20: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        public Dictionary<bool,int> myTotals;

        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(isTest), Part2(isTest));
        }

        private object Part1(bool isTest = false)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var dict = new Dictionary<string, IModule>();

            myTotals = new Dictionary<bool, int>() { { false, 0 }, { true, 0 } };

            foreach (var line in lines)
			{
                var firstTokens = line.Split('-','>',' ').ToList().GetRealStrings(delimiterChars);
                if (firstTokens[0].ToLower().Equals("broadcaster"))
                {
                    dict.Add("broadcaster", new BroadcastModule(myTotals, "broadcaster"));
                }
                else
                {
                    var tokens = firstTokens[0].Split('%','&', ' ').ToList().GetRealStrings(delimiterChars);
                    if (firstTokens[0][0] == '%')
                    {
                        dict.Add(tokens[0], new FlipFlopModule(myTotals, tokens[0]));
                    }
                    else if (firstTokens[0][0] == '&')
                    {
                        dict.Add(tokens[0], new ConjunctionModule(myTotals, tokens[0]));
                    }
                }
			}
            dict.Add("button", new ButtonModule(myTotals, "button"));

            foreach (var line in lines)
            {
                var firstTokens = line.Split('-', '>').ToList().GetRealStrings(delimiterChars);
                var tokens = firstTokens[0].Split('%', '&').ToList().GetRealStrings(delimiterChars);
                var module = dict[tokens[0].RemoveWhitespace()];
                var connections = firstTokens[1].Split(',', ' ').ToList().GetRealStrings(delimiterChars);

                foreach (var con in connections)
                {
                    IModule moduleCon;
                    if (!dict.ContainsKey(con))
                    {
                        moduleCon = new DudModule(myTotals, "dud");
                        dict.Add(con, moduleCon);
                    }
                    else
                    {
                        moduleCon = dict[con];
                    }
                    if (moduleCon is ConjunctionModule conj)
                    {
                        conj.Inputs.Add(module);
                    }
                    module.Connections.Add(moduleCon);
                }
            }
            dict["button"].Connections.Add(dict["broadcaster"]);

            var button = dict["button"];

            var hashy = new HashSet<string>();
            var hashy2 = new HashSet<string>();
            var listy = new List<string>();
            var iterations = 1000;
            var trackingCounts = new List<(int, int)>();
            var personalList = new List<string>();
            for (var i = 0; i < iterations; i++)
            {
                var currentTotals = new Dictionary<bool, int>() { { false, 0 }, { true, 0 } };
                var ripple = button.Pulse(false, null);
                currentTotals[false]++;
                var key = string.Empty;
                while (ripple.Count > 0)
                {
                    var nextRipple = new List<(IModule, bool, IModule)>();
                    foreach (var item in ripple)
                    {
                        var result = item.Item1.Pulse(item.Item2, item.Item3);
                        if (result.Count > 0)
                        {
                            nextRipple.AddRange(result);
                        }
                    }
                    foreach (var item in ripple)
                    {
                        key = key + item.Item3.Key + ':' + item.Item2.ToString() + ';' ;
                    }
                    key += "$";
                    ripple = nextRipple;
                    foreach (var item in ripple)
                    {
                        currentTotals[item.Item2]++;
                    }
                }
                key += currentTotals[false] + "" + currentTotals[true];
                if (!hashy.Add(key))
                {
                    var cycleLength = IsCycle(listy);
                    if (cycleLength != -1)
                    {
                        var cycleCounts = trackingCounts[..cycleLength];
                        long falses = cycleCounts.Sum(x => x.Item1);
                        long trues = cycleCounts.Sum(x => x.Item2);
                        long totalCycles = iterations / cycleLength;
                        long val = (falses * totalCycles) * (trues * totalCycles);
                        return val;
                        /*
                        var divisor = i / cycleLength;
                        var rem = iterations % cycleLength;
                        var total = ((myTotals[false]/ divisor) * totalCycles) * ((myTotals[true] / divisor) * totalCycles);
                        total += ((myTotals[false] / divisor) * rem) * ((myTotals[true]/divisor) * rem);
                        return total;*/
                    }
                }
                else
                {
                    personalList.Add("" + currentTotals[false] + "" + currentTotals[true]);
                }
                myTotals[false] += currentTotals[false];
                myTotals[true] += currentTotals[true];
                listy.Add(key);
                trackingCounts.Add((currentTotals[false], currentTotals[true]));
            }
            return myTotals[false]*myTotals[true];
        }

        private int IsCycle(List<string> listy)
        {
            var fast = 0;
            var fastList = new List<string>();
            var slow = 0;
            var slowList = new List<string>();
            while (fast < listy.Count-1)
            {
                fastList.Add(listy[fast]);
                fastList.Add(listy[fast+1]);
                fast += 2;
                slowList.Add(listy[slow]);
                slow++;

                var mid = 0 + ((fastList.Count - 0) / 2);
                var secondHalf = fastList[mid..];

                if (slowList.SequenceEqual(secondHalf))
                {
                    return slowList.Count;
                }
            }
            return -1;
        }

        private object Part2(bool isTest = false)
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var dict = new Dictionary<string, IModule>();

            myTotals = new Dictionary<bool, int>() { { false, 0 }, { true, 0 } };

            foreach (var line in lines)
            {
                var firstTokens = line.Split('-', '>', ' ').ToList().GetRealStrings(delimiterChars);
                if (firstTokens[0].ToLower().Equals("broadcaster"))
                {
                    dict.Add("broadcaster", new BroadcastModule(myTotals, "broadcaster"));
                }
                else
                {
                    var tokens = firstTokens[0].Split('%', '&', ' ').ToList().GetRealStrings(delimiterChars);
                    if (firstTokens[0][0] == '%')
                    {
                        dict.Add(tokens[0], new FlipFlopModule(myTotals, tokens[0]));
                    }
                    else if (firstTokens[0][0] == '&')
                    {
                        dict.Add(tokens[0], new ConjunctionModule(myTotals, tokens[0]));
                    }
                }
            }
            dict.Add("button", new ButtonModule(myTotals, "button"));

            foreach (var line in lines)
            {
                var firstTokens = line.Split('-', '>').ToList().GetRealStrings(delimiterChars);
                var tokens = firstTokens[0].Split('%', '&').ToList().GetRealStrings(delimiterChars);
                var module = dict[tokens[0].RemoveWhitespace()];
                var connections = firstTokens[1].Split(',', ' ').ToList().GetRealStrings(delimiterChars);

                foreach (var con in connections)
                {
                    IModule moduleCon;
                    if (!dict.ContainsKey(con))
                    {
                        moduleCon = new DudModule(myTotals, "dud");
                        dict.Add(con, moduleCon);
                    }
                    else
                    {
                        moduleCon = dict[con];
                    }
                    if (moduleCon is ConjunctionModule conj)
                    {
                        conj.Inputs.Add(module);
                    }
                    module.Connections.Add(moduleCon);
                }
            }
            dict["button"].Connections.Add(dict["broadcaster"]);

            var button = dict["button"];

            var iterations = 20000;
            var personalDict = new Dictionary<string, List<int>>()
            {
                { "mf", new List<int>() },
                { "ss", new List<int>() },
                { "fz", new List<int>() },
                { "fh", new List<int>() },
            };
            for (var i = 1; i < iterations; i++)
            {
                var ripple = button.Pulse(false, null);
                while (ripple.Count > 0)
                {
                    // recepient, value sent, sender
                    var nextRipple = new List<(IModule, bool, IModule)>();
                    foreach (var item in ripple)
                    {
                        // this is the magic for part 2. RX attaches to 1 conjunction
                        // which attaches to the 4 conjunctions in the dict. Look for cycles
                        // in those and LCM
                        if (!isTest)
                        {
                            if (item.Item1.Key.Equals("ql"))
                            {
                                if (personalDict.ContainsKey(item.Item3.Key))
                                {
                                    if (item.Item2)
                                    {
                                        personalDict[item.Item3.Key].Add(i);
                                        if (personalDict.All(x => x.Value.Count >= 1))
                                        {
                                            var repeats = personalDict.Select(x => (long)x.Value[0]);
                                            return MathHelper.LCM(repeats);
                                        }
                                    }
                                }
                            }
                        }
                        var result = item.Item1.Pulse(item.Item2, item.Item3);
                        if (result.Count > 0)
                        {
                            nextRipple.AddRange(result);
                        }
                    }
                    ripple = nextRipple;
                }
            }
            return 0;
        }

        public interface IModule
        {
            public List<IModule> Connections { get; set; }

            public string Key { get; set; }

            public Dictionary<bool,int> Totals { get; set; }

            public List<(IModule, bool, IModule)> Pulse(bool isHigh, IModule inputSource);
        }

        public class FlipFlopModule : IModule
        {
            private bool _isOn;

            public FlipFlopModule(Dictionary<bool, int> totals, string key)
            {
                Connections = new List<IModule>();
                _isOn = false;
                Totals = totals;
                Key = key;
            }

            public List<IModule> Connections { get ; set; }

            public Dictionary<bool, int> Totals { get; set; }

            public string Key { get; set; }

            public List<(IModule, bool, IModule)> Pulse(bool isHigh, IModule inputSource)
            {
                var ret = new List<(IModule, bool, IModule)>();
                if (!isHigh)
                {
                    _isOn = !_isOn;
                    foreach (var con in Connections)
                    {
                        ret.Add((con, _isOn, this));
                    }
                }
                return ret;
            }
        }

        public class ConjunctionModule : IModule
        {
            private Dictionary<string, bool> _memory;
            public ConjunctionModule(Dictionary<bool, int> totals, string key)
            {
                Connections = new List<IModule>();
                Inputs = new List<IModule>();
                _memory = new Dictionary<string,bool>();
                Totals = totals;
                Key = key;
            }

            public List<IModule> Connections { get; set; }

            public Dictionary<bool, int> Totals { get; set; }

            public List<IModule> Inputs { get; set; }

            public string Key { get; set; }

            public List<(IModule, bool, IModule)> Pulse(bool isHigh, IModule inputSource)
            {
                var ret = new List<(IModule, bool, IModule)>();
                if (_memory.Count == 0)
                {
                    foreach( var input in Inputs)
                    {
                        _memory.Add(input.Key, false);
                    }
                }
                _memory[inputSource.Key] = isHigh;
                var valid = _memory.All(x => x.Value);

                foreach (var con in Connections)
                {
                    ret.Add((con, !valid, this));
                }
                return ret;
            }
        }

        public class BroadcastModule : IModule
        {
            public BroadcastModule(Dictionary<bool, int> totals, string key)
            {
                Connections = new List<IModule>();
                Totals = totals;
                Key = key;
            }

            public List<IModule> Connections { get; set; }

            public Dictionary<bool, int> Totals { get; set; }

            public string Key { get; set; }

            public List<(IModule, bool, IModule)> Pulse(bool isHigh, IModule inputSource)
            {
                var ret = new List<(IModule, bool, IModule)>();
                foreach (var con in Connections)
                {
                    ret.Add((con, isHigh, this));
                }
                return ret;
            }
        }

        public class ButtonModule : IModule
        {
            public ButtonModule(Dictionary<bool, int> totals, string key)
            {
                Connections = new List<IModule>();
                Totals = totals;
                Key = key;
            }

            public List<IModule> Connections { get; set; }

            public Dictionary<bool, int> Totals { get; set; }

            public string Key { get; set; }

            public List<(IModule, bool, IModule)> Pulse(bool isHigh, IModule inputSource)
            {
                var ret = new List<(IModule,bool,IModule)>();
                foreach (var con in Connections)
                {
                    ret.Add((con, false, this));
                }
                return ret;
            }
        }

        public class DudModule : IModule
        {
            public DudModule(Dictionary<bool, int> totals, string key)
            {
                Connections = new List<IModule>();
                Totals = totals;
                Key = key;
            }

            public List<IModule> Connections { get; set; }

            public Dictionary<bool, int> Totals { get; set; }

            public string Key { get; set; }

            public List<(IModule, bool, IModule)> Pulse(bool isHigh, IModule inputSource)
            {
                return new List<(IModule, bool, IModule)>();
            }
        }
    }
}