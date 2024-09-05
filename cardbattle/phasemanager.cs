
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardBattle
{
    public interface IPhase
    {
        int Order {get;}
        bool CanEnter(BattleContext context); // Checks if the phase can start
        void Execute(BattleContext context);  // Executes phase logic
        bool CanExit(BattleContext context);  // Checks if the phase can exit
    }

    public class PhaseManager
    {
        private readonly List<IPhase> _phases;

        public PhaseManager(IEnumerable<IPhase> phases)
        {
            _phases = phases.OrderBy(p => p.Order).ToList(); // Sort phases by their order
        }

        public void ExecutePhases(BattleContext context)
        {
            foreach (var phase in _phases)
            {
                if (phase.CanEnter(context))
                {
                    System.Console.WriteLine($"Entering phase: {phase.GetType().Name} (Order: {phase.Order})");
                    phase.Execute(context);

                    if (!phase.CanExit(context))
                    {
                        System.Console.WriteLine($"Cannot exit phase: {phase.GetType().Name}, stopping phase execution.");
                        break;
                    }
                }
            }
        }
    }

}