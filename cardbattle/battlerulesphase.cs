using System;
using System.Linq;
namespace CardBattle
{
    public class ApplyBattleRulesPhase : IPhase
    {
        public int Order => 2;

        public bool CanEnter(BattleContext context)
        {
            return context.Rulesets.Any();
        }

        public void Execute(BattleContext context)
        {
            Console.WriteLine("Applying battle rules...");
            foreach (var ruleset in context.Rulesets)
            {
                // Apply each ruleset's effect to the battle
                Console.WriteLine($"Applying ruleset: {ruleset.Name}");
            }
        }

        public bool CanExit(BattleContext context)
        {
            return true;
        }
    }

}