using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBattle.DataModels.Configurations
{
    public class AbilityConfiguration : IEntityTypeConfiguration<Ability>
    {
        public void Configure(EntityTypeBuilder<Ability> builder)
        {
            // Define the many-to-many relationship with CardStats through CardStatsAbility
            builder.HasMany(a => a.CardStatsAbilities)
                   .WithOne(csa => csa.Ability)
                   .HasForeignKey(csa => csa.AbilityId);

            // Additional configurations...
        }
    }
}
