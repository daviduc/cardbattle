using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            // Additional configurations...
        }
    }
}
