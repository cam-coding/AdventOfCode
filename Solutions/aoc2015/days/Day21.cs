using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day21: ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };
        public Solution Solve(string filePath, bool isTest = false)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private List<(int cost, int dmg, int armour)> _weapons = new List<(int cost, int dmg, int armour)>()
        {
            { (8,4,0)},
            { (10,5,0)},
            { (25,6,0)},
            { (40,7,0)},
            { (74,8,0)},
        };

        private List<(int cost, int dmg, int armour)> _armours = new List<(int cost, int dmg, int armour)>()
        {
            { (13,0,1)},
            { (31,0,2)},
            { (53,0,3)},
            { (75,0,4)},
            { (102,0,5)},
        };

        private List<(int cost, int dmg, int armour)> _rings = new List<(int cost, int dmg, int armour)>()
        {
            { (25,1,0)},
            { (50,2,0)},
            { (100,3,0)},
            { (20,0,1)},
            { (40,0,2)},
            { (80,0,3)},
        };

        private object Part1()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            var armourCombinations = _armours.Get0toKCombinations(1);
            var ringCombinations = _rings.Get0toKCombinations(2);
            var lowestCost = int.MaxValue;

            foreach (var weapon in _weapons)
            {
                foreach (var armourChoice in armourCombinations)
                {
                    foreach (var ringChoice in ringCombinations)
                    {
                        var equipment = new List<(int cost, int dmg, int armour)>() { weapon };
                        equipment.AddRange(armourChoice);
                        equipment.AddRange(ringChoice);

                        var cost = equipment.Sum(x => x.cost);
                        var dmg = equipment.Sum(x => x.dmg);
                        var armour = equipment.Sum(x => x.armour);
                        var hero = new Battler(100, dmg, armour);
                        var villain = new Battler
                            (StringParsing.GetNumbersFromString(lines[0]).First(),
                            StringParsing.GetNumbersFromString(lines[1]).First(),
                            StringParsing.GetNumbersFromString(lines[2]).First()
                            );

                        if (Battle(new List<Battler>() { hero, villain }) == 0)
                        {
                            if (cost < lowestCost)
                            {
                                lowestCost = cost;
                            }
                        }

                    }
                }
            }
            return lowestCost;
        }

        private object Part2()
        {
            var lines = ParseInput.GetLinesFromFile(_filePath);

            var armourCombinations = _armours.Get0toKCombinations(1);
            var ringCombinations = _rings.Get0toKCombinations(2);
            var highestCost = 0;

            foreach (var weapon in _weapons)
            {
                foreach (var armourChoice in armourCombinations)
                {
                    foreach (var ringChoice in ringCombinations)
                    {
                        var equipment = new List<(int cost, int dmg, int armour)>() { weapon };
                        equipment.AddRange(armourChoice);
                        equipment.AddRange(ringChoice);

                        var cost = equipment.Sum(x => x.cost);
                        var dmg = equipment.Sum(x => x.dmg);
                        var armour = equipment.Sum(x => x.armour);
                        var hero = new Battler(100, dmg, armour);
                        var villain = new Battler
                            (StringParsing.GetNumbersFromString(lines[0]).First(),
                            StringParsing.GetNumbersFromString(lines[1]).First(),
                            StringParsing.GetNumbersFromString(lines[2]).First()
                            );

                        if (Battle(new List<Battler>() { hero, villain }) == 1)
                        {
                            if (cost > highestCost)
                            {
                                highestCost = cost;
                            }
                        }

                    }
                }
            }
            return highestCost;
        }

        private int Battle(List<Battler> battlers)
        {
            var attacker = 0;
            var defender = 1;
            while (battlers.All(x => x.Hp > 0))
            {
                var dmg = Math.Max(1, battlers[attacker].Dmg - battlers[defender].Armor);
                battlers[defender].Hp -= dmg;
                attacker = 1 - attacker;
                defender = 1 - defender;
            }
            return defender;
        }

        private class Battler
        {
            public Battler(int hp, int dmg, int armor)
            {
                Hp = hp;
                Dmg = dmg;
                Armor = armor;
            }

            public int Hp { get; set; }
            public int Dmg { get; }
            public int Armor { get; }
        }
    }
}