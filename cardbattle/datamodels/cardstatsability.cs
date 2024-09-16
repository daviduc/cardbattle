using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBattle.DataModels
{
    public class CardStatsAbility
    {
        public int CardStatsId { get; set; }
        public CardStats CardStats { get; set; }

        public int AbilityId { get; set; }
        public Ability Ability { get; set; }
    }
    public class CardStatsAbilityConfiguration : IEntityTypeConfiguration<CardStatsAbility>
    {
        public void Configure(EntityTypeBuilder<CardStatsAbility> builder)
        {
            builder.HasKey(csa => new { csa.CardStatsId, csa.AbilityId });
            builder.HasOne(csa => csa.CardStats)
                   .WithMany(cs => cs.CardStatsAbilities)
                   .HasForeignKey(csa => csa.CardStatsId);
            builder.HasOne(csa => csa.Ability)
                   .WithMany(a => a.CardStatsAbilities)
                   .HasForeignKey(csa => csa.AbilityId);
        }
    }
}
