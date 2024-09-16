using System;

namespace CardBattle
{
    public class EndOfRoundPhase : IPhase
    {
        public bool Validate(BattleContext context)
        {
            // Add validation logic here
            return true;
        }
    
        public int Order => 60;

        public bool CanEnter(BattleContext context)
        {
            return true; // Always runs at the end of a round
        }

        public void Execute(BattleContext context)
        {
            Console.WriteLine("Applying end-of-round effects...");
            // Apply end-of-round effects like poison, healing, etc.
        }

        public bool CanExit(BattleContext context)
        {
            return true;
        }
    }

}