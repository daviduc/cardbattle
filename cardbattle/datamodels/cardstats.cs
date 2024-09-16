using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
    public class CardStatsConfiguration : IEntityTypeConfiguration<CardStats>
    {
        public void Configure(EntityTypeBuilder<CardStats> builder)
        {
            // Define the many-to-many relationship with Ability through CardStatsAbility
            builder.HasMany(cs => cs.CardStatsAbilities)
                   .WithOne(csa => csa.CardStats)
                   .HasForeignKey(csa => csa.CardStatsId);

            // Additional configurations...
        }
    }
}
