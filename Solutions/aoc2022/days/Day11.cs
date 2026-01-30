using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2022
{
    public class Day11 : ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private long _superMod;

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
            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var monkeys = new List<Monkey>();
            var counter = new List<int>();

            foreach (var group in groups)
            {
                counter.Add(0);
                var monkeyId = StringParsing.GetIntsFromString(group[0])[0];
                var items = StringParsing.GetIntsFromString(group[1]);
                var itemQueue = new Queue<long>();
                foreach (var item in items)
                {
                    itemQueue.Enqueue(item);
                }
                Func<long, long> op = GetOp(group[2]);
                var divNum = StringParsing.GetIntsFromString(group[3])[0];
                var trueNum = StringParsing.GetIntsFromString(group[4])[0];
                var falseNum = StringParsing.GetIntsFromString(group[5])[0];
                Func<long, bool> check = (num) => num % divNum == 0;
                var monk = new Monkey(monkeyId, itemQueue, op, check, trueNum, falseNum, false);
                monkeys.Add(monk);
            }

            for (var i = 0; i < 20; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.Items.Count > 0)
                    {
                        var item = monkey.Items.Dequeue();
                        counter[monkey.Id]++;
                        var result = monkey.DoThing(item);
                        monkeys[result.monkey].Items.Enqueue(result.worry);
                    }
                }
            }

            var nums = counter.SortDescending();
            return nums[0] * nums[1];
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var groups = input.LineGroupsSeperatedByWhiteSpace;
            var monkeys = new List<Monkey>();
            var counter = new List<long>();
            var supermod = 1;

            foreach (var group in groups)
            {
                counter.Add(0);
                var monkeyId = StringParsing.GetIntsFromString(group[0])[0];
                var items = StringParsing.GetLongsFromString(group[1]);
                var itemQueue = new Queue<long>();
                foreach (var item in items)
                {
                    itemQueue.Enqueue(item);
                }
                Func<long, long> op = GetOp(group[2]);
                var divNum = StringParsing.GetIntsFromString(group[3])[0];
                supermod = supermod * divNum;
                var trueNum = StringParsing.GetIntsFromString(group[4])[0];
                var falseNum = StringParsing.GetIntsFromString(group[5])[0];
                Func<long, bool> check = (num) => num % divNum == 0;
                var monk = new Monkey(monkeyId, itemQueue, op, check, trueNum, falseNum, true);
                monkeys.Add(monk);
            }

            for (var i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.SuperMod = supermod;
                    while (monkey.Items.Count > 0)
                    {
                        var item = monkey.Items.Dequeue();
                        counter[monkey.Id]++;
                        var result = monkey.DoThing(item);
                        monkeys[result.monkey].Items.Enqueue(result.worry);
                    }
                }
            }

            var nums = counter.SortDescending();
            return nums[0] * nums[1];
        }

        private Func<long, long> GetOp(string line)
        {
            Func<long, long> op = (num) =>
            {
                long num1 = num;
                long num2;
                var tokens = StringParsing.GetRealTokens(line);
                if (tokens.Count(x => x.Equals("old")) == 2)
                {
                    num2 = num;
                }
                else
                {
                    num2 = StringParsing.GetIntsFromString(line)[0];
                }
                if (line.Contains("+"))
                {
                    return num1 + num2;
                }
                else if (line.Contains("*"))
                {
                    return num1 * num2;
                }
                return 0;
            };

            return op;
        }

        private class Monkey
        {
            public Monkey(
                int id,
                Queue<long> items,
                Func<long, long> operation,
                Func<long, bool> check,
                int trueMonkey,
                int falseMonkey,
                bool part2,
                long superMod = 0)
            {
                Id = id;
                Items = items;
                Operation = operation;
                Check = check;
                TrueMonkey = trueMonkey;
                FalseMonkey = falseMonkey;
                Part2 = part2;
                SuperMod = superMod;
            }

            public int Id { get; set; }

            public Queue<long> Items { get; set; }

            public Func<long, long> Operation { get; set; }

            public Func<long, bool> Check { get; set; }

            public int TrueMonkey { get; set; }

            public int FalseMonkey { get; set; }

            public bool Part2 { get; set; }

            public long SuperMod { get; set; }

            public (int monkey, long worry) DoThing(long worry)
            {
                worry = Operation(worry);
                if (!Part2)
                {
                    worry = worry / 3;
                }
                else
                {
                    worry = worry % SuperMod;
                }
                var destMonkey = PerformCheck(worry);
                return (destMonkey, worry);
            }

            public int PerformCheck(long num)
            {
                if (Check(num))
                {
                    return TrueMonkey;
                }
                return FalseMonkey;
            }
        }
    }
}