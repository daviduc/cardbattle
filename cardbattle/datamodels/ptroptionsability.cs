using CardBattle.DataModels;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CardBattle.DataModels
{
    public class PtrOptionsAbility
    {
        public int PtrOptionsId { get; set; }
        public PtrOptions PtrOptions { get; set; }

        public int AbilityId { get; set; }
        public Ability Ability { get; set; }
    }
    public class PtrOptionsAbilityConfiguration : IEntityTypeConfiguration<PtrOptionsAbility>
    {
        public void Configure(EntityTypeBuilder<PtrOptionsAbility> builder)
        {
            // Composite primary key (PtrOptionId, AbilityId)
            builder.HasKey(poa => new { poa.PtrOptionsId, poa.AbilityId });

            // Foreign key relationship to PtrOptions
            builder.HasOne(poa => poa.PtrOptions)
                .WithMany(po => po.PtrOptionsAbilities)
                .HasForeignKey(poa => poa.PtrOptionsId)
                .OnDelete(DeleteBehavior.Cascade);

            // Foreign key relationship to Ability
            builder.HasOne(poa => poa.Ability)
                .WithMany(a => a.PtrOptionsAbilities)
                .HasForeignKey(poa => poa.AbilityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}