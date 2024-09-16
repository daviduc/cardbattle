using System;
using System.Collections.Generic;
using System.Linq; // Add this using directive
namespace CardBattle
{
    public class BattleManager
    {
        private readonly List<IPhase> _phases = new List<IPhase>();
        public BattleManager()
        {
           _phases = new List<IPhase>
            {
                new ValidationPhase(),           // Order 1
                new ApplyBattleRulesPhase(),     // Order 2
                new ApplySummonerAbilitiesPhase(),// Order 3
                new DetermineBattleOrderPhase(), // Order 4
                new RoundOfBattlePhase(),        // Order 5
                new EndOfRoundPhase()            // Order 6
            };

        }
        public void RunBattle(BattleContext context)
        {
            Console.WriteLine("Starting battle...");
            foreach (var phase in _phases.OrderBy(p => p.Order))
            {
                if (phase.CanEnter(context))
                {
                    phase.Execute(context);
                    if (!phase.CanExit(context))
                    {
                        throw new Exception($"Failed to exit phase {phase.GetType().Name}");
                    }
                }
            }
        }
    }
}