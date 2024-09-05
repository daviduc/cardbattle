using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBattle.DataModels.Configurations
{
    public class SummonerStatAbilityConfiguration : IEntityTypeConfiguration<SummonerStatAbility>
    {
        public void Configure(EntityTypeBuilder<SummonerStatAbility> builder)
        {
            // Composite primary key (SummonerStatId, AbilityId)
            builder.HasKey(ssa => new { ssa.SummonerStatId, ssa.AbilityId });

            // Foreign key relationship to SummonerStat
            builder.HasOne(ssa => ssa.SummonerStat)
                .WithMany(ss => ss.SummonerStatAbilities)
                .HasForeignKey(ssa => ssa.SummonerStatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Foreign key relationship to Ability
            builder.HasOne(ssa => ssa.Ability)
                .WithMany(a => a.SummonerStatAbilities)
                .HasForeignKey(ssa => ssa.AbilityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
