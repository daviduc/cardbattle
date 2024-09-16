using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CardBattle.DataModels
{
    public class Ruleset
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<BattleRuleset> BattleRulesets { get; set; }

    }
    public class RulesetConfiguration : IEntityTypeConfiguration<Ruleset>
    {
        public void Configure(EntityTypeBuilder<Ruleset> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.BattleRulesets)
                   .WithOne(br => br.Ruleset)
                   .HasForeignKey(br => br.RulesetId);
            
        }
    }
}