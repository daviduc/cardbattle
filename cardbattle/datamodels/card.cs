using System.Collections.Generic;

namespace CardBattle.DataModels
{
    public enum CardType
    {
        Monster,
        Summoner
    }

    public enum Rarity
    {
        Common = 1,
        Rare,
        Epic,
        Legendary
    }

    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public CardType Type { get; set; }
        public Rarity Rarity { get; set; }

        // Navigation property for related CardStats (one-to-many relationship)
        public ICollection<CardStats> CardStats { get; set; }
        // For Summoners: Abilities and stats that apply to the entire team during battle
        public ICollection<Ability> SummonerAbilities { get; set; }
        public ICollection<SummonerStat> SummonerStats { get; set; } // New model to track Summoner stats
        // For special ptrOptions abilities and stat buffs
        public PtrOptions PtrOptions { get; set; }
    }
}
