using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CardBattle.DataModels
{
    public class PtrOptions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Effect { get; set; }
        public int StatBuffId { get; set; }
        public StatBuff StatBuff { get; set; }
        public List<string> StatusEffects { get; set; }
        public string Target { get; set; }
        public string TargetType { get; set; }
        public int Max { get; set; }
        // Foreign key to Card
        public int CardId { get; set; }
        public Card Card { get; set; }
        public virtual ICollection<PtrOptionsAbility> PtrOptionsAbilities { get; set; }   
    }
    public class PtrOptionsConfiguration : IEntityTypeConfiguration<PtrOptions>
    {
        public void Configure(EntityTypeBuilder<PtrOptions> builder)
        {
            builder.HasKey(po => po.Id);
            builder.Property(po => po.StatusEffects)
                .HasConversion(
                    v => string.Join(':', v),
                    v => v.Split(':', StringSplitOptions.RemoveEmptyEntries).ToList()
                );
            builder.HasOne(po => po.StatBuff)
                .WithOne()
                .HasForeignKey<PtrOptions>(po => po.StatBuffId);
            // Define the foreign key relationship with Card
            builder.HasOne(po => po.Card)
                .WithMany(c => c.PtrOptions)
                .HasForeignKey(po => po.CardId);       
        }
    }
}