using System;
using System.Collections.Generic;   
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CardBattle.DataModels
{
    public class StatBuff
    {
        public int Id { get; set; }
        public int AttackModifier { get; set; } =0;
        public int RangedModifier { get; set; } =0;
        public int MagicModifier { get; set; } =0;
        public int ArmorModifier { get; set; }=0;
        public int HealthModifier { get; set; }=0;
        public int SpeedModifier { get; set; }=0;
    }

    public class StatBuffConfiguration : IEntityTypeConfiguration<StatBuff>
    {
        public void Configure(EntityTypeBuilder<StatBuff> builder)
        {
            builder.HasKey(sb => sb.Id);
        }
    }

}