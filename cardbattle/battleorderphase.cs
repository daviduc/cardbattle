using System;

namespace CardBattle
{
    public class DetermineBattleOrderPhase : IPhase
    {
        public bool Validate(BattleContext context)
        {
            // Add validation logic here
            return true;
        }
    
        public int Order => 40;

        public bool CanEnter(BattleContext context)
        {
            return true;
        }

        public void Execute(BattleContext context)
        {
            Console.WriteLine("Determining battle order...");
            // Sort cards based on speed or other criteria
            
        }

        public bool CanExit(BattleContext context)
        {
            return true;
        }
    }
}