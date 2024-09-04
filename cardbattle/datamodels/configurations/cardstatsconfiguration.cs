using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBattle.DataModels.Configurations
{
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
