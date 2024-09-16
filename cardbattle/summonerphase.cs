using System;
using System.Linq;
namespace CardBattle
{
    public class ApplySummonerAbilitiesPhase : IPhase
    {
        public int Order => 30;

        public bool CanEnter(BattleContext context)
        {
            // Ensure that summoners and their abilities are present
            return false;
        }

        public void Execute(BattleContext context)
        {
            Console.WriteLine("Applying summoner abilities...");
        }

        public bool CanExit(BattleContext context)
        {
            return true;
        }

        public bool Validate(BattleContext context)
        {
            // Add validation logic here
            return true;
        }
    }

}