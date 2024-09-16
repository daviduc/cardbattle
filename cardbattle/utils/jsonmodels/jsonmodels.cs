using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
namespace CardBattle.Utils.JsonModels
{

    public class CardJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(CardJsonModel));
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            var card = new CardJsonModel();

            // Deserialize common properties
            serializer.Populate(jsonObject.CreateReader(), card);

            // Check the "type" property to decide how to handle "stats"
            string cardType = jsonObject["type"]?.ToString();

            if (cardType == "Summoner")
            {
                // Determine whether this Summoner has "abilities" or "ptrOptions"
                if (jsonObject["stats"]["abilities"] != null)
                {
                    if(jsonObject["stats"]["ptrOptions"]== null)
                    {
                        // Handle Summoner with abilities
                        card.Stats = jsonObject["stats"].ToObject<CardStatsJsonModelSummonerAbilities>();
                    }
                    else
                    {
                        // Handle Summoner with ptrOptions
                        card.Stats = jsonObject["stats"].ToObject<CardStatsJsonModelSummonerPtrOptions>();
                    }
                }
                else if (jsonObject["stats"]["ptrOptions"] != null)
                {
                    // Handle Summoner with ptrOptions
                    card.Stats = jsonObject["stats"].ToObject<CardStatsJsonModelSummonerPtrOptions>();
                }
                else
                {
                    // Handle Summoner without abilities or ptrOptions
                    card.Stats = jsonObject["stats"].ToObject<CardStatsJsonModelSummoner>();
                }
            }
            else if (cardType == "Monster")
            {
                // Handle Monster-specific deserialization
                card.Stats = jsonObject["stats"].ToObject<CardStatsJsonModelMonster>();
            }

            return card;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
    [JsonConverter(typeof(CardJsonConverter))]
    public class CardJsonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        [JsonProperty("secondary_color")]  // Use the "secondary_color" field in the JSON
        public string SecondaryColor { get; set; } // New secondary_color field
        public string Type { get; set; }
        public int Rarity { get; set; }
        // Use different models for different types
        public object Stats { get; set; }  

        // Other properties
        public bool IsStarter { get; set; }
        public string Editions { get; set; }
        public string LastUpdateTx { get; set; }
        public bool IsPromo { get; set; }
        public string GameType { get; set; }
        public List<CardDistribution> Distribution { get; set; }
        
    }
    public class CardDistribution
    {
        public int CardDetailId { get; set; }
        public bool Gold { get; set; }
        public int Edition { get; set; }
        public string NumCards { get; set; }
        public string TotalXp { get; set; }
        public string NumBurned { get; set; }
        public string TotalBurnedXp { get; set; }
    }
    public class CardStatsJsonModelMonster
    {
        public List<int> Mana { get; set; }      // List of mana values for each level
        public List<int> Attack { get; set; }    // List of attack values for each level
        public List<int> Ranged { get; set; }    // List of ranged values for each level
        public List<int> Magic { get; set; }     // List of magic values for each level
        public List<int> Armor { get; set; }     // List of armor values for each level
        public List<int> Health { get; set; }    // List of health values for each level
        public List<int> Speed { get; set; }     // List of speed values for each level

        // Abilities for each level. Each index of the list corresponds to the abilities available at that level.
        public List<List<string>> Abilities { get; set; }
    }
    public class CardStatsJsonModelSummoner
    {
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Ranged { get; set; }
        public int Magic { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
    }
    public class CardStatsJsonModelSummonerAbilities
    {
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Ranged { get; set; }
        public int Magic { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public List<string>? Abilities { get; set; }  // List of abilities for Summoners
    }
    public class CardStatsJsonModelSummonerPtrOptions
    {
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Ranged { get; set; }
        public int Magic { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public List<string>? Abilities { get; set; }
        public List<PtrOption> PtrOptions { get; set; }  // List of ptrOptions for Summoners
    }

    public class PtrOption
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Effect { get; set; }
        public List<int> StatBuff { get; set; }
        public List<string> StatusEffects { get; set; }
        public string Target { get; set; }
        public string TargetType { get; set; }
        public int Max { get; set; }
        public List<string> Abilities { get; set; }
    }
    public class StatBuff
    {
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Ranged { get; set; }
        public int Magic { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int Max { get; set; }  // Add Max property
    }
    public class Ability
    {
        public int Id { get; set; }           // Unique identifier for the ability
        public string Name { get; set; }      // Name of the ability (e.g., "Void", "Triage")
        public string Description { get; set; } // Description of the ability's effects
    }

    // public class SummonerStat
    // {
    //     public int Mana { get; set; }
    //     public int Attack { get; set; }
    //     public int Ranged { get; set; }
    //     public int Magic { get; set; }
    //     public int Armor { get; set; }
    //     public int Health { get; set; }
    //     public int Speed { get; set; }
    //     public List<Ability> ActiveSummonerAbilities { get; set; }
    //     public List<StatBuff> StatBuffs { get; set; }  // Add StatBuffs collection
    // }

}
