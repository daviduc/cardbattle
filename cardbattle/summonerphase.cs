using System;
using System.Linq;
namespace CardBattle
{
    public class ApplySummonerAbilitiesPhase : IPhase
    {
        public int Order => 3;

        public bool CanEnter(BattleContext context)
        {
            // Ensure that summoners and their abilities are present
            return context.Summoners.Any();
        }

        public void Execute(BattleContext context)
        {
            Console.WriteLine("Applying summoner abilities...");
            foreach (var summoner in context.Summoners)
            {
                // Apply each summoner's abilities to their team's cards
                Console.WriteLine($"Summoner {summoner.Name} applying abilities.");
            }
        }

        public bool CanExit(BattleContext context)
        {
            return true;
        }
    }

}