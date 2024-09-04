using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CardBattle.DataModels;
using System.Threading.Tasks;
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

                // Path to your JSON file
                string jsonFilePath = "c:\\Users\\david\\splsim\\cardbattle\\cards.json";

                // Initialize the database
                await context.InitializeDatabase(jsonFilePath);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddDbContext<BattleSimulatorContext>());
    }
}
