using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day11: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            var password = lines[0];
            password = "hxbxxyzz";

            while (!IsValid(password))
            {
                password = RollString(password);
            }
            return password;
        }

        private string RollString(string password)
        {
            var arr = password.ToCharArray();
            for (var j = password.Length-1; j >= 0; j--)
            {
                arr[j] = (char)(arr[j] + 1);
                if (password[j] == 'i' ||
                    password[j] == 'l' ||
                    password[j] == 'o')
                {
                    arr[j] = (char)(arr[j] + 1);
                }
                if (arr[j] == 'z' + 1)
                {
                    arr[j] = 'a';
                }
                else
                {
                    return new string(arr);
                }
            }
            return new string(arr);
        }

        private bool IsValid(string password)
        {
            var pairpos = -1;
            var pairValid = StringExtensions.CountPairs_NonOverlapping(password) >= 2;
            var tripleValid = false;
            for (var i = 0; i < password.Length; i++)
            {
                if (password[i] == 'i' ||
                    password[i] == 'l' ||
                    password[i] == 'o')
                {
                    return false;
                }
                if (i < password.Length - 2)
                {
                    if (password[i] == (password[i+1] - 1) &&
                        password[i] == (password[i+2] - 2))
                    {
                        tripleValid = true;
                    }
                }
            }
            return pairValid && tripleValid;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var password = "hxbxxyzz";

            while (!IsValid(password) || password.Equals("hxbxxyzz"))
            {
                password = RollString(password);
            }
            return password;
        }
    }
}