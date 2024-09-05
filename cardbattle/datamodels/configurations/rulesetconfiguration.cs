using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBattle.DataModels.Configurations
{
    public class RulesetConfiguration : IEntityTypeConfiguration<Ruleset>
    {
        public void Configure(EntityTypeBuilder<Ruleset> builder)
        {
            // Define the many-to-many relationship with ...
            
        }
    }
}