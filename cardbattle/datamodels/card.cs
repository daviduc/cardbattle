using CardBattle.DataModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System;
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
        public List<string> Color { get; set; }
        public CardType Type { get; set; }
        public Rarity Rarity { get; set; }

        // Navigation property for related CardStats (one-to-many relationship)
        public virtual ICollection<CardStats> CardStats { get; set; }
        public virtual ICollection<TeamCard> TeamCards { get; set; } = new List<TeamCard>();
        // Navigation property for related PtrOptions (one-to-many relationship)
        public virtual ICollection<PtrOptions> PtrOptions { get; set; } = new List<PtrOptions>();
    }
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            // Define the one-to-many relationship with CardStats
            builder.HasMany(c => c.CardStats)
                .WithOne(cs => cs.Card)
                .HasForeignKey(cs => cs.CardId);

            builder.HasMany(c => c.TeamCards)
                .WithOne(tc => tc.Card)
                .HasForeignKey(tc => tc.CardId);
            // Define the one-to-many relationship with PtrOptions
            builder.HasMany(c => c.PtrOptions)
                .WithOne(po => po.Card)
                .HasForeignKey(po => po.CardId);
            // Convert List<string> to a delimited string for database storage and vice versa
            builder.Property(c => c.Color)
                .HasConversion(
                    v => v != null ? string.Join(":", v) : null,  // From List<string> to delimited string
                    v => v != null ? v.Split(new[] { ':' }, StringSplitOptions.None).ToList() : null
                );
        }
    }
}
