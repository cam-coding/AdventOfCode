using AdventLibrary.Extensions;
using System;
using System.Collections.Generic;

namespace AdventLibrary.CustomObjects
{
    public class Registry
    {
        public Registry(int count, char starting = 'A', long initialValue = 0)
        {
            Registers = new Dictionary<char, long>();

            for (var i = 0; i < count; i++)
            {
                Registers.Add(starting, initialValue);
                starting++;
            }
        }

        public Dictionary<char, long> Registers { get; set; }

        public long GetValue(string str)
        {
            if (str.IsNumeric())
            {
                return Convert.ToInt64(str);
            }
            else
            {
                return Registers[str[0]];
            }
        }

        public void Set(string reg, string value)
        {
            long val = GetValue(value);
            Registers[reg[0]] = val;
        }
        public void Set(string reg, int value)
        {
            Registers[reg[0]] = value;
        }

        public void SetAll(int value)
        {
            foreach (var key in Registers.Keys)
            {
                Registers[key] = value;
            }
        }

        public void Add(string reg, string value)
        {
            long val = GetValue(value);
            Registers[reg[0]] = Registers[reg[0]] + val;
        }

        public void Sub(string reg, string value)
        {
            long val = GetValue(value);
            Registers[reg[0]] = Registers[reg[0]] - val;
        }

        public void Mul(string reg, string value)
        {
            long val = GetValue(value);
            Registers[reg[0]] = Registers[reg[0]] * val;
        }

        public void Div(string reg, string value)
        {
            long val = GetValue(value);
            Registers[reg[0]] = Registers[reg[0]] / val;
        }

        public void Mod(string reg, string value)
        {
            long val = GetValue(value);
            Registers[reg[0]] = Registers[reg[0]] % val;
        }
    }
}
