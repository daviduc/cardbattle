
namespace CardBattle.DataModels
{
    public class SummonerStatAbility
    {
        public int SummonerStatId { get; set; }
        public SummonerStat SummonerStat { get; set; }

        public int AbilityId { get; set; }
        public Ability Ability { get; set; }
    }

}