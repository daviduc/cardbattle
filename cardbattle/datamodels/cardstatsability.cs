namespace CardBattle.DataModels
{
    public class CardStatsAbility
    {
        public int CardStatsId { get; set; }
        public CardStats CardStats { get; set; }

        public int AbilityId { get; set; }
        public Ability Ability { get; set; }
    }
}
