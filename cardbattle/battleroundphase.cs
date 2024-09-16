using System;
using System.Linq; // Add this using directive

namespace CardBattle
{
    public class RoundOfBattlePhase : IPhase
    {
        public bool Validate(BattleContext context)
        {
            // Implement validation logic here
            return true;
        }
    
        public int Order => 50;

        public bool CanEnter(BattleContext context)
        {
            return true;
        }

        public void Execute(BattleContext context)
        {
            Console.WriteLine("Starting round of battle...");
            
        }

        public bool CanExit(BattleContext context)
        {
            return true;
        }
    }
}