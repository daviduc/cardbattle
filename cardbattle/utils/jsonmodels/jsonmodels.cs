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
                    // Handle Summoner with abilities
                    card.Stats = jsonObject["stats"].ToObject<CardStatsJsonModelSummonerAbilities>();
                }
                else if (jsonObject["stats"]["ptrOptions"] != null)
                {
                    // Handle Summoner with ptrOptions
                    card.Stats = jsonObject["stats"].ToObject<CardStatsJsonModelSummonerPtrOptions>();
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
    // public class IntListConverter : JsonConverter<List<int>>
    // {
    //     public override List<int> ReadJson(JsonReader reader, Type objectType, List<int> existingValue, bool hasExistingValue, JsonSerializer serializer)
    //     {
    //         var token = JToken.Load(reader);

    //         if (token.Type == JTokenType.Array)
    //         {
    //             var list = new List<int>();
    //             foreach (var item in token)
    //             {
    //                 list.Add(item.ToObject<int>());
    //             }
    //             return list;
    //         }
    //         else if (token.Type == JTokenType.Integer)
    //         {
    //             // If the token is a single integer, convert it to a list containing that integer
    //             return new List<int> { token.ToObject<int>() };
    //         }
    //         else
    //         {
    //             throw new JsonSerializationException($"Unexpected token type: {token.Type}");
    //         }
    //     }

    //     public override void WriteJson(JsonWriter writer, List<int> value, JsonSerializer serializer)
    //     {
    //         serializer.Serialize(writer, value);
    //     }
    // }
    [JsonConverter(typeof(CardJsonConverter))]
    public class CardJsonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public int Rarity { get; set; }
        // Use different models for different types
        public object Stats { get; set; }  // Can be either CardStatsJsonModelSummonerAbilities, CardStatsJsonModelSummonerPtrOptions, or CardStatsJsonModelMonster

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
    // public class CardStatsJsonModel
    // {
    //      // Use the custom converter for Monster-specific stats
    //     //[JsonConverter(typeof(IntListConverter))]
    //     public List<int> Mana { get; set; }
        
    //     //[JsonConverter(typeof(IntListConverter))]
    //     public List<int> Attack { get; set; }
        
    //     //[JsonConverter(typeof(IntListConverter))]
    //     public List<int> Ranged { get; set; }
        
    //     //[JsonConverter(typeof(IntListConverter))]
    //     public List<int> Magic { get; set; }
        
    //     //[JsonConverter(typeof(IntListConverter))]
    //     public List<int> Armor { get; set; }
        
    //     //[JsonConverter(typeof(IntListConverter))]
    //     public List<int> Health { get; set; }
        
    //     //[JsonConverter(typeof(IntListConverter))]
    //     public List<int> Speed { get; set; }
    //     public List<List<string>> Abilities { get; set; }  // This remains for Monster-specific abilities
    //     // These properties are used for Summoner cards, where each stat applies to the whole team
    //     public int SummonerMana { get; set; }   // Mana cost of using the Summoner
    //     public int SummonerAttack { get; set; } 
    //     public int SummonerRanged { get; set; }
    //     public int SummonerMagic { get; set; }
    //     public int SummonerArmor { get; set; }
    //     public int SummonerHealth { get; set; }
    //     public int SummonerSpeed { get; set; }
    //     public List<string> SummonerAbilities { get; set; }  // Abilities that the Summoner grants
    // }
    // public class PtrOptionsJsonModel
    // {
    //     public int Max { get; set; }
    //     public StatBuffJsonModel StatBuff { get; set; }
    //     public List<string> Abilities { get; set; }
    // }

    // public class StatBuffJsonModel
    // {
    //     public int Mana { get; set; }
    //     public int Attack { get; set; }
    //     public int Ranged { get; set; }
    //     public int Magic { get; set; }
    //     public int Armor { get; set; }
    //     public int Health { get; set; }
    //     public int Speed { get; set; }
    // }
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
    public class CardStatsJsonModelSummonerAbilities
    {
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Ranged { get; set; }
        public int Magic { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public List<string> Abilities { get; set; }  // List of abilities for Summoners
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

    public class SummonerStat
    {
        public int Mana { get; set; }
        public int Attack { get; set; }
        public int Ranged { get; set; }
        public int Magic { get; set; }
        public int Armor { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public List<Ability> ActiveSummonerAbilities { get; set; }
        public List<StatBuff> StatBuffs { get; set; }  // Add StatBuffs collection
    }

}
