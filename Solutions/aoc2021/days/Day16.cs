using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day16: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
        private readonly Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
    { '0', "0000" },
    { '1', "0001" },
    { '2', "0010" },
    { '3', "0011" },
    { '4', "0100" },
    { '5', "0101" },
    { '6', "0110" },
    { '7', "0111" },
    { '8', "1000" },
    { '9', "1001" },
    { 'A', "1010" },
    { 'B', "1011" },
    { 'C', "1100" },
    { 'D', "1101" },
    { 'E', "1110" },
    { 'F', "1111" }
};

        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var line = lines[0];
            var binary = new List<string>();
            var longString = string.Empty;
            foreach (var c in line)
            {
                binary.Add(hexCharacterToBinary[c]);
                longString = longString + hexCharacterToBinary[c];
            }
            var notNeeded = 0;
            long notNeeded2 = 0;
            var total = SubPacket(0, longString, out notNeeded, out notNeeded2);
            return total;
        }
        
        private object Part2()
        {
            var lines = AdventLibrary.ParseInput.GetLinesFromFile(_filePath);
            var line = lines[0];
            var binary = new List<string>();
            var longString = string.Empty;
            foreach (var c in line)
            {
                binary.Add(hexCharacterToBinary[c]);
                longString = longString + hexCharacterToBinary[c];
            }
            var notNeeded = 0;
            long isNeeded = 0;
            var total = SubPacket(0, longString, out notNeeded, out isNeeded);
            return isNeeded;
        }

        private int SubPacket(int length, string str, out int packetEnd, out long litVal)
        {
            var version = Convert.ToInt32(str.Substring(0, 3), 2);
            var totalVersion = version;
            var typeID = str.Substring(3, 3);
            packetEnd = 0;

            if (string.Equals(typeID, "100"))
            {
                var current = 6;
                var nextString = str.Substring(current, 5);
                var totalString = string.Empty;
                while(nextString[0] != '0')
                {
                    totalString = totalString + nextString.Substring(1, 4);
                    current = current + 5;
                    nextString = str.Substring(current, 5);
                }
                totalString = totalString + nextString.Substring(1, 4);
                var literalValue = Convert.ToInt64(totalString, 2);
                litVal = (long)literalValue;
                packetEnd = current + 5;
            }
            else
            {
                var LengthTypeId = str[6];
                if (LengthTypeId.Equals('0'))
                {
                    var subpacketLength = Convert.ToInt32(str.Substring(7, 15), 2);
                    var j = 0;
                    var lengthUsed = 0;
                    long litValue1 = 0;
                    var litValues = new List<long>();
                    while (j < subpacketLength)
                    {
                        totalVersion = totalVersion + SubPacket(length, str.Substring(22 + j, subpacketLength - j), out lengthUsed, out litValue1);
                        litValues.Add(litValue1);
                        j = j + lengthUsed;
                    }
                    packetEnd = j + 22;
                    litVal = HandleType(litValues, Convert.ToInt32(typeID, 2));
                    return totalVersion;
                }
                else
                {
                    var subpacketCount = Convert.ToInt32(str.Substring(7, 11), 2);
                    var myEnd = 0;
                    totalVersion = totalVersion + ReadSubPackets(subpacketCount, str.Substring(18), typeID, out myEnd, out litVal);
                    packetEnd = packetEnd + myEnd + 18;
                    return totalVersion;
                }
            }
            return totalVersion;
        }

        private int ReadSubPackets(int count, string str, string type, out int packetEnd, out long litVal)
        {
            var i = 0;
            var versionTotal = 0;
            var currentLocation = 0;
            long litValue1 = 0;
            var litValues = new List<long>();
            while ( i < count)
            {
                var usedSpace = 0;
                versionTotal = versionTotal + SubPacket(0, str.Substring(currentLocation), out usedSpace, out litValue1);
                litValues.Add(litValue1);
                currentLocation = currentLocation + usedSpace;
                i++;
            }
            packetEnd = currentLocation;
            litVal = HandleType(litValues, Convert.ToInt32(type, 2));
            return versionTotal;
        }

        private long HandleType(List<long> listy, int type)
        {
            if (type == 0)
            {
                return listy.Sum();
            }
            else if (type == 1)
            {
                return listy.Aggregate((a, x) => a * x);
            }
            else if (type == 2)
            {
                return listy.Aggregate((a, x) => Math.Min(a, x));
            }
            else if (type == 3)
            {
                return listy.Aggregate((a, x) => Math.Max(a, x));
            }
            else if (type == 5)
            {
                if (listy[0] > listy[1])
                {
                    return 1;
                }
                return 0;
            }
            else if (type == 6)
            {
                if (listy[0] < listy[1])
                {
                    return 1;
                }
                return 0;
            }
            else if (type == 7)
            {
                if (listy[0] == listy[1])
                {
                    return 1;
                }
                return 0;
            }
            return -1;
        }
    }
}