
using System;
using System.Linq;

namespace CardBattle
{
    public class ValidationPhase : IPhase
    {
        public int Order => 10;

        public bool CanEnter(BattleContext context)
        {
            // Check if the necessary properties for the battle are set
            return context.ManaCap > 0 && context.AllowableColors.Any();
        }

        public void Execute(BattleContext context)
        {
            Console.WriteLine("Validating battle setup...");
            // Implement validation logic (e.g., mana cap, ruleset validation)
        }

        public bool CanExit(BattleContext context)
        {
            // If validation passes, we can proceed
            return true;
        }
    }
}