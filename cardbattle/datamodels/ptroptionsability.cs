using System.Collections.Generic;

namespace CardBattle.DataModels
{
    public class PtrOptionAbility
    {
        public int PtrOptionId { get; set; }
        public PtrOptions PtrOptions { get; set; }

        public int AbilityId { get; set; }
        public Ability Ability { get; set; }
    }

}