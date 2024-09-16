using System;
using System.Collections.Generic;
using CardBattle.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CardBattle.DataModels
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TeamCard> TeamCards { get; set; }
    }
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.TeamCards)
                   .WithOne(tc => tc.Team)
                   .HasForeignKey(tc => tc.TeamId)
                   .OnDelete(DeleteBehavior.SetNull); 
        }
    }
}