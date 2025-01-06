using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Extensions;
using AdventLibrary.Helpers;
using AdventLibrary.Helpers.Grids;

namespace aoc2017
{
    public class Day18: ISolver
    {
        private string _filePath;
        private char[] _delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '=', '\t' };
        private Dictionary<int, Queue<long>> _queue;
        private List<string> _instructions;

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
			long count = 0;

            var registers = new Dictionary<char, long>();
            for (var i = 'a'; i <= 'z'; i++)
            {
                registers.Add(i, 0);
            }

            long lastSound = -1;
            var currentPos = 0;
            while (currentPos < lines.Count)
            {
                var line = lines[currentPos];
                var toks = line.Split(' ');
                long num = -1;
                if (toks.Count() == 3)
                {
                    if (!long.TryParse(toks[2], out num))
                    {
                        num = registers[toks[2][0]];
                    }
                }
                var reg = toks[1][0];
                var command = toks[0];
                if (command.Equals("snd"))
                {
                    lastSound = registers[reg];
                }
                else if (command.Equals("set"))
                {
                    registers[reg] = num;
                }
                else if (command.Equals("add"))
                {
                    registers[reg] += num;
                }
                else if (command.Equals("mul"))
                {
                    registers[reg] = registers[reg] * num;
                }
                else if (command.Equals("mod"))
                {
                    registers[reg] = registers[reg] % num;
                }
                else if (command.Equals("rcv"))
                {
                    if (registers[reg] != 0)
                    {
                        return lastSound;
                    }
                }
                else if (command.Equals("jgz"))
                {
                    if (registers[reg] > 0)
                    {
                        currentPos += (int)num - 1;
                    }
                }
                currentPos++;
            }
            return count;
        }

        private object Part2(bool isTest = false)
        {
            var input = new InputObjectCollection(_filePath);
            var lines = input.Lines;
            _instructions = lines;
            if (isTest)
            {
                var testInput = "snd 1\r\nsnd 2\r\nsnd p\r\nrcv a\r\nrcv b\r\nrcv c\r\nrcv d\r\n";
                _instructions = ParseInput.GetLinesFromText(testInput);
            }
            _queue = new Dictionary<int, Queue<long>>();
            _queue.Add(0, new Queue<long>());
            _queue.Add(1, new Queue<long>());

            var programs = new List<Program>();
            programs.Add(new Program(0));
            programs.Add(new Program(1));

            while (true)
            {
                var start = programs[0].TimesSent + programs[1].TimesSent;
                if (programs[1].IsWaiting)
                {
                    RunProgram(programs[0]);
                }
                if (programs[0].IsWaiting)
                {
                    RunProgram(programs[1]);
                }
                var finish = programs[0].TimesSent + programs[1].TimesSent;
                if (start == finish)
                {
                    return programs[1].TimesSent;
                }
            }
        }

        private class Program
        {
            public Program(int id)
            {
                var registers = new Dictionary<char, long>();
                for (var i = 'a'; i <= 'z'; i++)
                {
                    registers.Add(i, 0);
                }
                registers['p'] = id;
                Registers = registers;
                Id = id;
                TimesSent = 0;
                IsWaiting = true;
                CurrentPosition = 0;
            }

            public int Id { get; set; }

            public int TimesSent { get; set; }

            public bool IsWaiting { get; set; }

            public int CurrentPosition { get; set; }

            public Dictionary<char, long> Registers { get; set; }
        }

        private bool RunProgram(Program program)
        {
            program.IsWaiting = false;
            while (program.CurrentPosition < _instructions.Count)
            {
                var line = _instructions[program.CurrentPosition];
                var toks = line.Split(' ');
                long num = -1;
                if (toks.Count() == 3)
                {
                    if (!long.TryParse(toks[2], out num))
                    {
                        num = program.Registers[toks[2][0]];
                    }
                }

                long val1;
                if (!long.TryParse(toks[1], out val1))
                {
                    val1 = program.Registers[toks[1][0]];
                }
                var reg = toks[1][0];
                var command = toks[0];
                if (command.Equals("snd"))
                {
                    var otherId = Math.Abs(program.Id - 1);
                    _queue[otherId].Enqueue(val1);
                    program.TimesSent++;
                }
                else if (command.Equals("set"))
                {
                    program.Registers[reg] = num;
                }
                else if (command.Equals("add"))
                {
                    program.Registers[reg] += num;
                }
                else if (command.Equals("mul"))
                {
                    program.Registers[reg] = program.Registers[reg] * num;
                }
                else if (command.Equals("mod"))
                {
                    program.Registers[reg] = program.Registers[reg] % num;
                }
                else if (command.Equals("rcv"))
                {
                    // if (program.Registers[reg] != 0)
                    {
                        if (_queue[program.Id].Count == 0)
                        {
                            program.IsWaiting = true;
                            return false;
                        }
                        else
                        {
                            program.Registers[reg] = _queue[program.Id].Dequeue();
                        }
                    }
                }
                else if (command.Equals("jgz"))
                {
                    if (val1 > 0)
                    {
                        program.CurrentPosition += (int)num - 1;
                    }
                }
                program.CurrentPosition++;
            }

            return true;
        }
    }

}