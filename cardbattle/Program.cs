using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CardBattle.DataModels;
using System.Threading.Tasks;
using CardBattle.Utils; 
namespace CardBattle
{
    class Program  // Define the Program class
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<BattleSimulatorContext>();
                
                // Paths to your JSON files
                string cardsJsonFilePath = "c:\\Users\\david\\splsim\\cardbattle\\cards.json";
                string battlesettingsJsonFilePath = "c:\\Users\\david\\splsim\\cardbattle\\battlesettings.json";
                            
                // Initialize the database
                await context.InitializeDatabase(cardsJsonFilePath, battlesettingsJsonFilePath);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddDbContext<BattleSimulatorContext>());
    }
}
