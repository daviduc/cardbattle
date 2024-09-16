using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBattle.DataModels
{
	public class Battle
	{
		public int Id { get; set; }
		public int ManaCap { get; set; }
		public virtual ICollection<BattleRuleset> BattleRulesets { get; set; }
		public virtual ICollection<string> AllowableColors { get; set; }
		public virtual ICollection<Team> Teams { get; set; }
		public string Name {get; set; } = string.Empty;
	}
    public class BattleConfiguration : IEntityTypeConfiguration<Battle>
    {
        public void Configure(EntityTypeBuilder<Battle> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasMany(b => b.Teams)
                   .WithOne()
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(b => b.BattleRulesets)
                   .WithOne(br => br.Battle)
                   .HasForeignKey(br => br.BattleId);

            builder.Property(b => b.ManaCap).IsRequired();
        }
    }
}