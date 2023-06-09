using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2016
{
    public class Day14: ISolver
  {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var total = 0;
            var count = 0;
            var dict = new Dictionary<int, string>();
            var validKeys = new List<int>();
            using (System.Security.Cryptography.MD5 _md5 = System.Security.Cryptography.MD5.Create())
            {
                while (total < 64 || validKeys[63] < count - 1000)
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes("ihaygndm"+count);
                    byte[] hashBytes = _md5.ComputeHash(inputBytes);

                    var hash = Convert.ToHexString(hashBytes).ToLower();
                    for (var i = 0; i < hash.Length-2; i++)
                    {
                        if (hash[i].Equals(hash[i+1]) && hash[i].Equals(hash[i+2]))
                        {
                            var str = string.Empty + hash[i] + hash[i] + hash[i] + hash[i] + hash[i];
                            dict.Add(count, str);
                            break;
                        }
                    }

                    var deleteList = new List<int>();

                    foreach (var entry in dict)
                    {
                        if (entry.Key != count)
                        {
                            if (entry.Key < count - 1000)
                            {
                                deleteList.Add(entry.Key);
                            }
                            else if (hash.Contains(entry.Value))
                            {
                                deleteList.Add(entry.Key);
                                total = total + 1;
                                validKeys.Add(entry.Key);
                            }
                        }
                    }

                    foreach (var delete in deleteList)
                    {
                        dict.Remove(delete);
                    }

                    count++;
                }
            }
            validKeys.Sort();

            return validKeys[63];
        }
        
        private object Part2()
        {
            var total = 0;
            var count = 0;
            var dict = new Dictionary<int, string>();
            var validKeys = new List<int>();
            using (System.Security.Cryptography.MD5 _md5 = System.Security.Cryptography.MD5.Create())
            {
                while (total < 64 || validKeys[63] < count - 1000)
                {
                    var hash = "ihaygndm"+count;
                    var j = 0;
                    while (j < 2017)
                    {
                        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(hash);
                        byte[] hashBytes = _md5.ComputeHash(inputBytes);

                        hash = Convert.ToHexString(hashBytes).ToLower();
                        j++;
                    }
                    for (var i = 0; i < hash.Length-2; i++)
                    {
                        if (hash[i].Equals(hash[i+1]) && hash[i].Equals(hash[i+2]))
                        {
                            var str = string.Empty + hash[i] + hash[i] + hash[i] + hash[i] + hash[i];
                            dict.Add(count, str);
                            break;
                        }
                    }

                    var deleteList = new List<int>();

                    foreach (var entry in dict)
                    {
                        if (entry.Key != count)
                        {
                            if (entry.Key < count - 1000)
                            {
                                deleteList.Add(entry.Key);
                            }
                            else if (hash.Contains(entry.Value))
                            {
                                deleteList.Add(entry.Key);
                                total = total + 1;
                                validKeys.Add(entry.Key);
                            }
                        }
                    }

                    foreach (var delete in deleteList)
                    {
                        dict.Remove(delete);
                    }

                    count++;
                }
            }
            validKeys.Sort();

            return validKeys[63];
        }
    }
}
