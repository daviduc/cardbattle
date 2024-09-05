using CardBattle.DataModels.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CardBattle.Utils;
using System.Threading.Tasks;
namespace CardBattle.DataModels
{
    public class BattleSimulatorContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<CardStats> CardStats { get; set; }
        public DbSet<CardStatsAbility> CardStatsAbilities { get; set; }
        public DbSet<SummonerStat> SummonerStats {get;set;}
        public DbSet<PtrOptions> PtrOptions {get;set;}
        public DbSet<SummonerStatAbility> SummonerStatAbilities {get;set;}
        public DbSet<Ruleset> Rulesets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BattleSimulatorDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new CardStatsConfiguration());
            modelBuilder.ApplyConfiguration(new AbilityConfiguration());
            modelBuilder.ApplyConfiguration(new SummonerStatAbilityConfiguration());
            modelBuilder.ApplyConfiguration(new PtrOptionAbilityConfiguration());
            modelBuilder.ApplyConfiguration(new SummonerStatAbilityConfiguration());
            modelBuilder.ApplyConfiguration(new RulesetConfiguration());
            // Define composite key for the join table
            modelBuilder.Entity<CardStatsAbility>()
                .HasKey(csa => new { csa.CardStatsId, csa.AbilityId });
        }
        public async Task InitializeDatabase(string cardsJsonFilePath, string battlesettingsJsonFilePath)
        {
            var seeder = new DatabaseSeeder(this);

            // Seed Cards if the table is empty
            if (!this.Cards.Any())
            {
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
