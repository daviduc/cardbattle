using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CardBattle.DataModels
{
    public class TeamCard
    {
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }
    }
    public class TeamCardConfiguration : IEntityTypeConfiguration<TeamCard>
    {
        public void Configure(EntityTypeBuilder<TeamCard> builder)
        {
            // Define composite key
            builder.HasKey(tc => new { tc.TeamId, tc.CardId });

            // Define the relationship with Team
            builder.HasOne(tc => tc.Team)
                   .WithMany(t => t.TeamCards)
                   .HasForeignKey(tc => tc.TeamId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Define the relationship with Card
            builder.HasOne(tc => tc.Card)
                   .WithMany(c => c.TeamCards)
                   .HasForeignKey(tc => tc.CardId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}