using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CardBattle.DataModels.Configurations
{
    public class StatBuffConfiguration : IEntityTypeConfiguration<StatBuff>
    {
        public void Configure(EntityTypeBuilder<StatBuff> builder)
        {
            // Table name
            builder.ToTable("StatBuffs");

            // Primary key
            builder.HasKey(sb => sb.Id);

            // Relationships
            builder.HasOne(sb => sb.SummonerStat)
                .WithMany(ss => ss.StatBuffs)  // A SummonerStat can have many StatBuffs
                .HasForeignKey(sb => sb.SummonerStatId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete to remove buffs if the summoner stat is deleted

            // Property configurations (optional: set max lengths, default values, etc.)
            builder.Property(sb => sb.Mana).IsRequired();
            builder.Property(sb => sb.Attack).IsRequired();
            builder.Property(sb => sb.Ranged).IsRequired();
            builder.Property(sb => sb.Magic).IsRequired();
            builder.Property(sb => sb.Armor).IsRequired();
            builder.Property(sb => sb.Health).IsRequired();
            builder.Property(sb => sb.Speed).IsRequired();
            builder.Property(sb => sb.Max).IsRequired();
        }
    }
}