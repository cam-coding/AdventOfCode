using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;

namespace aoc2021
{
    public class Day21: ISolver
    {
		/*
		var sub = item.Substring(0, 1);
		Console.WriteLine();
		*/
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        private Random _random;
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(0, Part2());
        }

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
			var counter = 0;

            var playerPos = new List<int>() { AdventLibrary.StringParsing.GetNumbersFromString(lines[0])[1], AdventLibrary.StringParsing.GetNumbersFromString(lines[1])[1] };
			var playerScores = new List<int>() { 0, 0 };

            var rollCount = 0;
            var i = 1;
            var turn = 0;
            while (true)
            {
                var sum = RollDice(i, out i);
                var playerCount = i % 2;
                playerPos[turn] = MoveAndScore(sum, playerPos[turn]);
                playerScores[turn] = playerScores[turn] + playerPos[turn];
                rollCount = rollCount + 3;

                if (i == 91)
                {
                    Console.WriteLine("hi");
                }

                if (playerScores[turn] >= 1000)
                {
                    return Math.Min(playerScores[turn], playerScores[(i+1) % 2])* rollCount;
                }
                turn = turn == 0? 1: 0;
            }
            return 0;
        }
        
        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);
            _random = new Random();
			var counter = 0;
            var winnerCount = new List<int>() { 0, 0 };

            for (var j = 21; j >= 0; j--)
            {
                
            }

            for (var x = 0; x < 10000000; x++)
            {
                var playerPos = new List<int>() { AdventLibrary.StringParsing.GetNumbersFromString(lines[0])[1], AdventLibrary.StringParsing.GetNumbersFromString(lines[1])[1] };
                var playerScores = new List<int>() { 0, 0 };
                if (playerPos[0] != 4)
                {
                    return 0;
                }

                var rollCount = 0;
                var i = 1;
                var turn = 0;
                while (true)
                {
                    var sum = RollDice2();
                    playerPos[turn] = MoveAndScore(sum, playerPos[turn]);
                    playerScores[turn] = playerScores[turn] + playerPos[turn];
                    rollCount = rollCount + 3;

                    if (playerScores[turn] >= 21)
                    {
                        // var notTurn = turn == 0? 1: 0;
                        winnerCount[turn]++;
                        break;
                    }
                    turn = turn == 0? 1: 0;
                }
            }
            Console.WriteLine(winnerCount[0].ToString());
            Console.WriteLine(winnerCount[1].ToString());
            return 0;
        }

        private int MoveAndScore(int rolls, int currentSpot)
        {
            var newSpot = currentSpot + rolls;

            if ( newSpot < 11)
            {
                return newSpot;
            }
            else
            {
                var mod = newSpot % 10;
                if (mod == 0)
                {
                    return 10;
                }
                return mod;
            }
        }

        private int RollDice(int start, out int i)
        {
            i = start;
            var rolls = new List<int>();

            for (var j = 0; j < 3; j++)
            {
                rolls.Add(i);
                i++;
                if (i > 100)
                {
                    i = 1;
                }
            }

            return rolls.Sum();
        }

        private int RollDice2()
        {
            return _random.Next(1, 3) + _random.Next(1, 3) + _random.Next(1, 3);
        }
    }
}