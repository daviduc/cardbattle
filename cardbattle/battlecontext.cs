
using System.Collections.Generic;
using CardBattle.DataModels; 

namespace CardBattle
{
    public class BattleContext
    {
        public int ManaCap { get; set; }
        public List<Ruleset> Rulesets { get; set; } = new List<Ruleset>();
        public List<string> AllowableColors { get; set; } = new List<string>();
        // Additional properties like Teams, Cards, etc.
    }
}