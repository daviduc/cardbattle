using System.Collections.Generic;

namespace CardBattle.DataModels
{
    public class CardStats
    {
        public int Id { get; set; }
        public int CardId { get; set; }  // Foreign key to Card
        public Card Card { get; set; }

        public int Level { get; set; }  // The level of the card this set of stats represents

        // Stats that vary by level
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Ranged { get; set; }
        public int Magic { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }

        // Navigation property for related abilities (many-to-many relationship with ability)
        public ICollection<CardStatsAbility> CardStatsAbilities { get; set; }
    }
}
