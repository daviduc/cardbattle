using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBattle.DataModels
{
    public class Ability
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property for the many-to-many relationship with CardStats
        public ICollection<CardStatsAbility> CardStatsAbilities { get; set; }
        public ICollection<PtrOptionsAbility> PtrOptionsAbilities {get;set;}
    }
    public class AbilityConfiguration : IEntityTypeConfiguration<Ability>
    {
        public void Configure(EntityTypeBuilder<Ability> builder)
        {
            builder.HasIndex(a => a.Name)
                .IsUnique();
        }
    }
}
