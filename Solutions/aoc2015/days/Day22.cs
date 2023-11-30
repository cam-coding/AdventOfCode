using System;
using System.Collections.Generic;
using System.Linq;
using AdventLibrary;
using AdventLibrary.Helpers;

namespace aoc2015
{
    public class Day22 : ISolver
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
            var lines = ParseInput.GetLinesFromFile(_filePath);
            var numbers = ParseInput.GetNumbersFromFile(_filePath);
            var nodes = ParseInput.ParseFileAsGraph(_filePath);
            var grid = ParseInput.ParseFileAsGrid(_filePath);
            var total = 1000000;
            var counter = 0;

            foreach (var line in lines)
            {
                var tokens = line.Split(delimiterChars);
                var nums = AdventLibrary.StringParsing.GetNumbersFromString(line);

                foreach (var num in nums)
                {
                }

                for (var i = 0; i < 0; i++)
                {
                    for (var j = 0; j < 0; j++)
                    {

                    }
                }
            }
            return 0;
        }

        private object Part2()
        {
            return 0;
        }

        private class BossClass
        {
            public BossClass(int hp, int dmg)
            {
                Dmg = dmg;
                Hp = hp;
            }

            public BossClass Clone()
            {
                return new BossClass(Hp, Dmg);
            }

            public int Dmg { get; }

            public int Hp { get; private set; }

            public void TakeDmg(int amount)
            {
                Hp -= amount;
            }
        }

        private class HeroClass
        {
            public HeroClass()
            {
                Poison = 0;
                Shield = 0;
                Recharge = 0;
                Mana = 500;
                Hp = 50;
                ManaSpent = 0;
            }

            public HeroClass Clone()
            {
                var newHero = new HeroClass();
                newHero.Poison = Poison;
                newHero.Shield = Shield;
                newHero.Recharge = Recharge;
                newHero.Mana = Mana;
                newHero.Hp = Hp;
                newHero.ManaSpent = ManaSpent;
                return newHero;
            }

            public int Poison { get; set; }

            public int Shield { get; set; }

            public int Recharge { get; set; }

            public int Mana { get; private set; }

            public int Hp { get; private set; }

            public int ManaSpent { get; private set; }

            public void SpendMana(int amount)
            {
                if (Mana < amount)
                {
                    throw new Exception("OOM");
                }
                else
                {
                    Mana -= amount;
                    ManaSpent += amount;
                }
            }

            public void TakeDmg(int amount)
            {
                if (Shield > 1)
                {
                    Hp -= Math.Max(1, amount - 7);
                }
                else
                {
                    Hp -= Math.Max(1, amount);
                }
                if (Hp <= 0)
                {
                    throw new Exception("dead");
                }
            }

            public void Heal(int amount)
            {
                Hp += amount;
            }

            public void HandleEffects()
            {
                if (Recharge > 0)
                {
                    Mana += 229;
                    Recharge -= 1;
                }
                if (Shield > 0)
                {
                    Shield -= 1;
                }
                if (Poison > 0)
                {
                    Poison -= 1;
                }
            }
        }

        private class Battle
        {
            public Battle()
            {
            }

            public int BFS(BattleLogistcs logistics)
            {
                if (logistics.IsBossDead())
                {
                    return logistics.Hero.ManaSpent;
                }

                foreach (var action in GenerateActions(logistics))
                {
                    try
                    {
                        action.Invoke();
                        var returnValue = BFS(logistics.Clone());

                    }
                    catch { }
                }
                return 1;

            }

            private List<Action> GenerateActions(BattleLogistcs logistics)
            {
                var listy = new List<Action>();

                if (logistics.Hero.Shield == 0)
                {
                    listy.Add(logistics.Shield);
                }
                if (logistics.Hero.Poison == 0)
                {
                    listy.Add(logistics.Poison);
                }
                if (logistics.Hero.Recharge == 0)
                {
                    listy.Add(logistics.Recharge);
                }
                listy.Add(logistics.Missle);
                listy.Add(logistics.Drain);
                return listy;
            }
        }

        private class BattleLogistcs
        {
            public BattleLogistcs(HeroClass myHero, BossClass myBoss)
            {
                Hero = myHero;
                Boss = myBoss;
            }

            public BattleLogistcs Clone()
            {
                return new BattleLogistcs(Hero.Clone(), Boss.Clone());
            }

            public HeroClass Hero { get; private set; }

            public BossClass Boss { get; private set; }

            public void TurnStart()
            {
                HandlePoison();
                Hero.HandleEffects();
            }

            public void BossAttacks()
            {
                Hero.TakeDmg(Boss.Dmg);
            }

            public void HandlePoison()
            {
                if (Hero.Poison > 0)
                {
                    Boss.TakeDmg(3);
                }
            }

            public bool IsBossDead()
            {
                return Boss.Hp > 0;
            }

            public void Missle()
            {
                Hero.SpendMana(53);
                Boss.TakeDmg(4);
            }

            public void Drain()
            {
                Hero.SpendMana(73);
                Boss.TakeDmg(2);
                Hero.Heal(2);
            }

            public void Shield()
            {
                Hero.SpendMana(113);
                Hero.Shield = 6;
            }

            public void Poison()
            {
                Hero.SpendMana(173);
                Hero.Poison = 6;
            }

            public void Recharge()
            {
                Hero.SpendMana(229);
                Hero.Recharge = 5;
            }
        }
    }
}