using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBattle.DataModels.Configurations
{
    public class PtrOptionAbilityConfiguration : IEntityTypeConfiguration<PtrOptionAbility>
    {
        public void Configure(EntityTypeBuilder<PtrOptionAbility> builder)
        {
            // Composite primary key (PtrOptionId, AbilityId)
            builder.HasKey(poa => new { poa.PtrOptionId, poa.AbilityId });

            // Foreign key relationship to PtrOption
            builder.HasOne(poa => poa.PtrOptions)
                .WithMany(po => po.PtrOptionAbilities)
                .HasForeignKey(poa => poa.PtrOptionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Foreign key relationship to Ability
            builder.HasOne(poa => poa.Ability)
                .WithMany(a => a.PtrOptionAbilities)
                .HasForeignKey(poa => poa.AbilityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
