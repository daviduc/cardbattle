using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CardBattle.DataModels
{
    public class BattleRuleset
    {
        public int BattleId { get; set; }
        public Battle Battle { get; set; }

        public int RulesetId { get; set; }
        public Ruleset Ruleset { get; set; }
    }
    public class BattleRulesetConfiguration : IEntityTypeConfiguration<BattleRuleset>
    {
        public void Configure(EntityTypeBuilder<BattleRuleset> builder)
        {
            // Define composite key
            builder.HasKey(br => new { br.BattleId, br.RulesetId });

            // Define the relationship with Battle
            builder.HasOne(br => br.Battle)
                   .WithMany(b => b.BattleRulesets)
                   .HasForeignKey(br => br.BattleId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Define the relationship with Ruleset
            builder.HasOne(br => br.Ruleset)
                   .WithMany(r => r.BattleRulesets)
                   .HasForeignKey(br => br.RulesetId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}