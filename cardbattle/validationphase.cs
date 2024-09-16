
using System;
using System.Linq;

namespace CardBattle
{
    public class ValidationPhase : IPhase
    {
        public bool Validate(BattleContext context)
        {
            // Implement validation logic here
            return true;
        }
    
        public int Order => 10;

        public bool CanEnter(BattleContext context)
        {
            // Check if the necessary properties for the battle are set
            Console.WriteLine("Checking if battle is ready to start...");
            return true;
        }

        public void Execute(BattleContext context)
        {
            Console.WriteLine("Validating battle setup...");
            // Implement validation logic (e.g., mana cap, ruleset validation)
        }

        public bool CanExit(BattleContext context)
        {
            // If validation passes, we can proceed
            Console.WriteLine("Battle setup validated.");
            return true;
        }
    }
}