using System.Collections.Generic;
namespace CardBattle.DataModels
{
    public class SummonerStat
    {
        public int Id { get; set; }
        public int CardId { get; set; } // Foreign key to Card
        public Card Card { get; set; }

        // Summoner-specific stats (e.g., buffs/debuffs)
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Ranged { get; set; }
        public int Magic { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }

        // Abilities that the Summoner performs during the battle
        public ICollection<Ability> ActiveSummonerAbilities { get; set; } // Abilities like "Resurrect", "Cleanse", etc.

        // Stat buffs applied by the Summoner to other cards
        public ICollection<StatBuff> StatBuffs { get; set; }  // Buffs that the summoner grants (e.g., +2 Attack, +1 Speed)
        public ICollection<SummonerStatAbility> SummonerStatAbilities { get; set; }
    }
    public class PtrOptions
    {
        public int Id { get; set; }
        public int CardId { get; set; } // Foreign key to Card
        public Card Card { get; set; }

        // Maximum number of cards that can receive these special buffs/abilities
        public int Max { get; set; }

        // Special stat buffs
        public ICollection<StatBuff> StatBuffs { get; set; }
        public ICollection<PtrOptionAbility> PtrOptionAbilities { get; set; }
    }
    public class StatBuff
    {
        public int Id { get; set; }
        public int SummonerStatId { get; set; }  // Foreign key to SummonerStat
        public SummonerStat SummonerStat { get; set; }

        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Ranged { get; set; }
        public int Magic { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int Max {get;set;}
    }
}
