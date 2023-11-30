using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var queueOfStates = new Queue<GameState>();
        queueOfStates.Enqueue(new GameState(true));

        var spells = new[]
        {
        Spell.Create("Magic Missle", 53, damage: 4),
        Spell.Create("Drain", 73, damage: 2, heal: 2),
        Spell.Create("Shield", 113, armour: 7, duration: 6),
        Spell.Create("Poison", 173, damage: 3, duration: 6),
        Spell.Create("Recharge", 229, manaCharge: 101, duration: 5)
    };

        var bestGame = default(GameState);
        var roundProcessed = 0;

        while (queueOfStates.Count > 0)
        {
            if (queueOfStates.Peek().RoundNumber > roundProcessed)
            {
                ++roundProcessed;
                Console.WriteLine("Finished round {0}...", roundProcessed);
            }

            var gameState = queueOfStates.Dequeue();
            if (bestGame != null && gameState.TotalManaSpent >= bestGame.TotalManaSpent) continue;

            foreach (var spell in spells.Except(gameState.ActiveSpells.Keys).Where(x => gameState.PlayerMana >= x.Mana))
            {
                var newGameState = new GameState(gameState);
                var result = newGameState.TakeTurn(spell);
                if (result == GameResult.Continue)
                {
                    queueOfStates.Enqueue(newGameState);
                }
                else if (result == GameResult.Win)
                {
                    if (bestGame == null || newGameState.TotalManaSpent < bestGame.TotalManaSpent)
                    {
                        bestGame = newGameState;
                    }
                }
            }
        }

        Console.WriteLine(bestGame);
    }

    class Spell
    {
        private Spell() { }

        public static Spell Create(string name, int mana, int damage = 0, int heal = 0, int armour = 0, int manaCharge = 0, int duration = 0)
        {
            return new Spell { Name = name, Mana = mana, Damage = damage, Heal = heal, Armour = armour, ManaCharge = manaCharge, Duration = duration };
        }

        public string Name { get; private set; }
        public int Mana { get; private set; }
        public int Duration { get; private set; }
        public int Damage { get; private set; }
        public int Heal { get; private set; }
        public int Armour { get; private set; }
        public int ManaCharge { get; private set; }
    }

    enum GameResult { Win, Loss, Continue }

    class GameState
    {
        public GameState(bool hardMode)
        {
            HardMode = hardMode;
            RoundNumber = 0;
            TotalManaSpent = 0;
            PlayerHealth = 50 - (hardMode ? 1 : 0);
            PlayerMana = 500;
            BossHealth = 71;
            BossAttack = 10;
            ActiveSpells = new Dictionary<Spell, int>();
        }

        public GameState(GameState g)
        {
            HardMode = g.HardMode;
            RoundNumber = g.RoundNumber;
            TotalManaSpent = g.TotalManaSpent;
            PlayerHealth = g.PlayerHealth;
            PlayerMana = g.PlayerMana;
            BossHealth = g.BossHealth;
            BossAttack = g.BossAttack;
            ActiveSpells = new Dictionary<Spell, int>(g.ActiveSpells);
        }

        public bool HardMode { get; private set; }
        public int RoundNumber { get; set; }
        public int TotalManaSpent { get; set; }
        public int PlayerHealth { get; set; }
        public int PlayerMana { get; set; }
        public int BossHealth { get; set; }
        public int BossAttack { get; set; }
        public Dictionary<Spell, int> ActiveSpells { get; set; }

        public GameResult TakeTurn(Spell spell)
        {
            ++RoundNumber;

            // Middle of my turn (no active spells at start!)
            CastSpell(spell);

            // Boss turn
            ProcessActiveSpells();
            if (BossHealth <= 0) return GameResult.Win;

            PlayerHealth -= Math.Max(1, BossAttack - ActiveSpells.Sum(x => x.Key.Armour));
            if (PlayerHealth <= 0) return GameResult.Loss;

            // Beginning of next turn
            if (HardMode)
            {
                PlayerHealth -= 1;
                if (PlayerHealth <= 0) return GameResult.Loss;
            }

            ProcessActiveSpells();
            if (BossHealth <= 0) return GameResult.Win;

            return GameResult.Continue;
        }

        void CastSpell(Spell spell)
        {
            TotalManaSpent += spell.Mana;
            PlayerMana -= spell.Mana;
            if (spell.Duration == 0)
            {
                ProcessSpell(spell);
            }
            else
            {
                ActiveSpells.Add(spell, spell.Duration);
            }
        }

        void ProcessActiveSpells()
        {
            foreach (var key in ActiveSpells.Keys)
            {
                ProcessSpell(key);
            }

            ActiveSpells.ToList().ForEach(x =>
            {
                if (x.Value == 1)
                    ActiveSpells.Remove(x.Key);
                else
                    ActiveSpells[x.Key] = x.Value - 1;

            });
        }

        void ProcessSpell(Spell spell)
        {
            BossHealth -= spell.Damage;
            PlayerHealth += spell.Heal;
            PlayerMana += spell.ManaCharge;
        }
    }
}