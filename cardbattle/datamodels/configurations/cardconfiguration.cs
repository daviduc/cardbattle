using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace CardBattle.DataModels.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            // Define the one-to-many relationship with CardStats
            builder.HasMany(c => c.CardStats)
                   .WithOne(cs => cs.Card)
                   .HasForeignKey(cs => cs.CardId);

            // Convert List<string> to a delimited string for database storage and vice versa
            builder.Property(c => c.Color)
                .HasConversion(
                    v => v != null ? string.Join(":", v) : null,  // From List<string> to delimited string
                    v => v != null ? v.Split(new[] { ':' }, StringSplitOptions.None).ToList() : null
                );
        }
    }
}
