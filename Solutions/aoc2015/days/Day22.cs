using AdventLibrary;
using System;

namespace aoc2015
{
    public class Day22 : ISolver
    {
        private string _filePath;
        private char[] delimiterChars = { ' ', ',', '.', ':', '-', '>', '<', '+', '\t' };

        private int best = int.MaxValue;
        public Solution Solve(string filePath)
        {
            _filePath = filePath;
            return new Solution(Part1(), Part2());
        }

        private object Part1()
        {
            var batlog = new BattleLogistcs(new HeroClass(), new BossClass(71, 10), false);

            return BFS(batlog, false);
        }

        private object Part2()
        {
            var batlog = new BattleLogistcs(new HeroClass(), new BossClass(71, 10), false);
            return BFS(batlog, true);
        }

        private int BFS(BattleLogistcs logistics, bool hardMode)
        {
            if (logistics.Hero.ManaSpent > best)
            {
                return int.MaxValue;
            }

            if (logistics.Hero.Hp < 10 && logistics.Boss.Hp > 50)
            {
                return int.MaxValue;
            }

            if (hardMode && !logistics.IsBossTurn)
            {
                logistics.Hero.TakeDmg(1);
                if (logistics.Hero.Hp <= 0)
                {
                    return int.MaxValue;
                }
            }

            logistics.TurnStart();

            if (logistics.IsBossDead())
            {
                return logistics.Hero.ManaSpent;
            }

            if (logistics.IsBossTurn)
            {
                logistics.BossAttacks();

                if (logistics.Hero.Hp <= 0)
                {
                    return int.MaxValue;
                }
                logistics.TurnEnd();
                if (hardMode && !logistics.IsBossTurn)
                {
                    logistics.Hero.TakeDmg(1);
                    if (logistics.Hero.Hp <= 0)
                    {
                        return int.MaxValue;
                    }
                }
                logistics.TurnStart();
            }

            if (logistics.IsBossDead())
            {
                return logistics.Hero.ManaSpent;
            }

            if (logistics.Hero.Mana > 52)
            {
                var nextStepMissle = logistics.Clone();
                nextStepMissle.Missle();
                nextStepMissle.TurnEnd();
                best = Math.Min(best, BFS(nextStepMissle, hardMode));
            }
            if (logistics.Hero.Recharge == 0 && logistics.Hero.Mana > 228)
            {
                var nextStep = logistics.Clone();
                nextStep.Recharge();
                nextStep.TurnEnd();
                best = Math.Min(best, BFS(nextStep, hardMode));
            }
            if (logistics.Hero.Poison == 0 && logistics.Hero.Mana > 172)
            {
                var nextStep = logistics.Clone();
                nextStep.Poison();
                nextStep.TurnEnd();
                best = Math.Min(best, BFS(nextStep, hardMode));
            }
            if (logistics.Hero.Mana > 72)
            {
                var nextStepDrain = logistics.Clone();
                nextStepDrain.Drain();
                nextStepDrain.TurnEnd();
                best = Math.Min(best, BFS(nextStepDrain, hardMode));
            }

            if (logistics.Hero.Shield == 0 && logistics.Hero.Mana > 112)
            {
                var nextStep = logistics.Clone();
                nextStep.Shield();
                nextStep.TurnEnd();
                best = Math.Min(best, BFS(nextStep, hardMode));
            }

            return best;
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
                if (Shield >= 1)
                {
                    Hp -= Math.Max(1, amount - 7);
                }
                else
                {
                    Hp -= Math.Max(1, amount);
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
                    Mana += 101;
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

        private class BattleLogistcs
        {
            public BattleLogistcs(HeroClass myHero, BossClass myBoss, bool isBossTurn)
            {
                Hero = myHero;
                Boss = myBoss;
                IsBossTurn = isBossTurn;
            }

            public BattleLogistcs Clone()
            {
                return new BattleLogistcs(Hero.Clone(), Boss.Clone(), IsBossTurn);
            }

            public HeroClass Hero { get; private set; }

            public BossClass Boss { get; private set; }

            public bool IsBossTurn { get; private set; }

            public void TurnStart()
            {
                HandlePoison();
                Hero.HandleEffects();
            }

            public void TurnEnd()
            {
                IsBossTurn = !IsBossTurn;
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
                return Boss.Hp <= 0;
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