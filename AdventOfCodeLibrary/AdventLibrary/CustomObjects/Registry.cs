using AdventLibrary.Extensions;
using System;
using System.Collections.Generic;

namespace AdventLibrary.CustomObjects
{
    public class Registry
    {
        public Registry(int count, char starting = 'A', int initialValue = 0)
        {
            Registers = new Dictionary<char, int>();

            for (var i = 0; i < count; i++)
            {
                Registers.Add(starting, initialValue);
                starting++;
            }
        }

        public Dictionary<char, int> Registers { get; set; }

        public int GetValue(string str)
        {
            if (str.IsNumeric())
            {
                return Convert.ToInt32(str);
            }
            else
            {
                return Registers[str[0]];
            }
        }

        public void Set(string reg, string value)
        {
            int val = GetValue(value);
            Registers[reg[0]] = val;
        }

        public void Add(string reg, string value)
        {
            int val = GetValue(value);
            Registers[reg[0]] = Registers[reg[0]] + val;
        }

        public void Sub(string reg, string value)
        {
            int val = GetValue(value);
            Registers[reg[0]] = Registers[reg[0]] - val;
        }

        public void Mul(string reg, string value)
        {
            int val = GetValue(value);
            Registers[reg[0]] = Registers[reg[0]] * val;
        }

        public void Div(string reg, string value)
        {
            int val = GetValue(value);
            Registers[reg[0]] = Registers[reg[0]] / val;
        }

        public void Mod(string reg, string value)
        {
            int val = GetValue(value);
            Registers[reg[0]] = Registers[reg[0]] % val;
        }
    }
}
