using CardBattle.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CardBattle.Utils;
using System.Threading.Tasks;
using System;
namespace CardBattle.DataModels
{
    public class BattleSimulatorContext : DbContext
    {
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<BattleRuleset> BattleRulesets { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardStats> CardStats { get; set; }
        public DbSet<CardStatsAbility> CardStatsAbilities { get; set; }
        public DbSet<PtrOptions> PtrOptions {get;set;}
        public DbSet<PtrOptionsAbility> PtrOptionAbilities {get;set;}
        public DbSet<Ruleset> Rulesets { get; set; }
        public DbSet<StatBuff> StatBuffs {get;set;}
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamCard> TeamCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BattleSimulatorDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AbilityConfiguration());
            modelBuilder.ApplyConfiguration(new BattleConfiguration());
            modelBuilder.ApplyConfiguration(new BattleRulesetConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new CardStatsConfiguration());
            modelBuilder.ApplyConfiguration(new CardStatsAbilityConfiguration());
            modelBuilder.ApplyConfiguration(new PtrOptionsConfiguration());
            modelBuilder.ApplyConfiguration(new PtrOptionsAbilityConfiguration());
            modelBuilder.ApplyConfiguration(new RulesetConfiguration());
            modelBuilder.ApplyConfiguration(new StatBuffConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new TeamCardConfiguration());
            
        }
        public async Task InitializeDatabase(string cardsJsonFilePath, string battlesettingsJsonFilePath)
        {
            var seeder = new DatabaseSeeder(this);

            // Seed Cards if the table is empty
            if (!this.Cards.Any())
            {
                Console.WriteLine("Seeding Cards");
                await seeder.SeedDatabase(cardsJsonFilePath, "cards");
            }

            // Seed Rulesets if the table is empty
            if (!this.Rulesets.Any())
            {
                await seeder.SeedDatabase(battlesettingsJsonFilePath, "battlesettings");
            }
        }
    }
}
