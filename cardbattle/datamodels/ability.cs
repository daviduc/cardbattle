using System.Collections.Generic;
namespace CardBattle.DataModels
{
    public class Ability
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property for the many-to-many relationship with CardStats
        public ICollection<CardStatsAbility> CardStatsAbilities { get; set; }
        public ICollection<Card> SummonerCards { get; set; } // For Summoner abilities
    }
}
