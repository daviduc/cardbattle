using CardBattle.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using CardBattle.Utils;
using CardBattle.Utils.JsonModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System; // Added for Console

namespace CardBattle
{
    class Program
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

                // Check for the --start-battle argument
                if (args.Contains("--start-battle"))
                {
                    await StartBattle(context);
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddDbContext<BattleSimulatorContext>());

        private static async Task StartBattle(BattleSimulatorContext context)
        {
            // Implement your battle simulation logic here
            // For example:
            Console.WriteLine("Starting battle simulation...");
            var battleManager = new BattleManager();
            var battleContext = new BattleContext();
            battleManager.RunBattle(battleContext);

            // Output the result of the battle
            Console.WriteLine("Battle simulation completed.");
        }
    }
}
