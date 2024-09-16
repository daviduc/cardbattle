using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using CardBattle.Utils.JsonModels;
using CardBattle.DataModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CardBattle.Utils
{
    public class DatabaseSeeder
    {
        private readonly BattleSimulatorContext _context;

        public DatabaseSeeder(BattleSimulatorContext context)
        {
            _context = context;
        }
        public async Task SeedDatabase(string jsonFilePath, string fileType)
        {
            if (fileType == "cards")
            {
                await SeedCards(jsonFilePath);
            }
            else if (fileType == "battlesettings")
            {
                await SeedRulesets(jsonFilePath);
            }
        }

        public async Task SeedCards(string jsonFilePath)
        {
            // Read and deserialize the JSON data
            var jsonData = File.ReadAllText(jsonFilePath);
            var cards = JsonConvert.DeserializeObject<List<CardJsonModel>>(jsonData);
            // Open a transaction to ensure data consistency
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Enable IDENTITY_INSERT for Cards table
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Cards] ON");
                    foreach (var cardJson in cards)
                    {
                        // Skip cards of type "Tower"
                        List<string> skipList = new List<string>(){"Tower","Spell","Hero"};
                        // Handle primary and secondary color logic
                        var colorList = new List<string> { cardJson.Color }; // Add primary color   
                        if (!string.IsNullOrEmpty(cardJson.SecondaryColor))
                        {
                            colorList.Add(cardJson.SecondaryColor); // Add secondary color if exists
                        }
                        if (skipList.Contains(cardJson.Type))
                        {
                            continue; // Skip to the next card
                        }
                        var card = new Card
                        {
                            Id = cardJson.Id,
                            Name = cardJson.Name,
                            Color = colorList,
                            Type = Enum.Parse<CardType>(cardJson.Type),
                            Rarity = (Rarity)cardJson.Rarity,
                        };
        
                        // Handle Monster cards and Summoner cards without PtrOptions
                        if (cardJson.Stats is CardStatsJsonModelMonster monsterStats)
                        {
                            card.CardStats = new List<CardStats>();
                            
                            for (int i = 0; i < monsterStats.Mana.Count; i++)
                            {
                                var cardStats = new CardStats
                                {
                                    Level = i + 1,
                                    Mana = monsterStats.Mana[i],
                                    Attack = monsterStats.Attack[i],
                                    Ranged = monsterStats.Ranged[i],
                                    Magic = monsterStats.Magic[i],
                                    Armor = monsterStats.Armor[i],
                                    Health = monsterStats.Health[i],
                                    Speed = monsterStats.Speed[i],
                                    CardStatsAbilities = new List<CardStatsAbility>()
                                };

                                // Determine the abilities list based on the card type
                                var abilitiesList = monsterStats.Abilities[i];

                                foreach (var abilityName in abilitiesList)
                                {
                                    var ability = _context.Abilities.FirstOrDefault(a => a.Name == abilityName);
                                    if (ability == null)
                                    {
                                        ability = new CardBattle.DataModels.Ability { Name = abilityName };
                                        await _context.Abilities.AddAsync(ability);
                                        await _context.SaveChangesAsync();
                                        Console.WriteLine($"Added ability: {abilityName}");
                                    }

                                    cardStats.CardStatsAbilities.Add(new CardStatsAbility
                                    {
                                        Ability = ability,
                                        CardStats = cardStats
                                    });
                                }
                                card.CardStats.Add(cardStats);
                            }
                        }
                        else if(cardJson.Stats is CardStatsJsonModelSummoner summonerStatsNoAbilities)
                        {
                            card.CardStats = new List<CardStats>();
                            
                            var cardStats = new CardStats
                            {
                                Level = 1,
                                Mana = summonerStatsNoAbilities.Mana,
                                Attack = summonerStatsNoAbilities.Attack,
                                Ranged = summonerStatsNoAbilities.Ranged,
                                Magic = summonerStatsNoAbilities.Magic,
                                Armor = summonerStatsNoAbilities.Armor,
                                Health = summonerStatsNoAbilities.Health,
                                Speed = summonerStatsNoAbilities.Speed,
                                CardStatsAbilities = new List<CardStatsAbility>()
                            };

                            card.CardStats.Add(cardStats);                    
                        }
                        else if(cardJson.Stats is CardStatsJsonModelSummonerAbilities summonerAbilitiesStats)
                        {
                            card.CardStats = new List<CardStats>();
                            
                            var cardStats = new CardStats
                            {
                                Level = 1,
                                Mana = summonerAbilitiesStats.Mana,
                                Attack = summonerAbilitiesStats.Attack,
                                Ranged = summonerAbilitiesStats.Ranged,
                                Magic = summonerAbilitiesStats.Magic,
                                Armor = summonerAbilitiesStats.Armor,
                                Health = summonerAbilitiesStats.Health,
                                Speed = summonerAbilitiesStats.Speed,
                                CardStatsAbilities = new List<CardStatsAbility>()
                            };

                            foreach (var abilityName in summonerAbilitiesStats.Abilities)
                            {
                                var ability = _context.Abilities.FirstOrDefault(a => a.Name == abilityName);
                                if (ability == null)
                                {
                                    ability = new CardBattle.DataModels.Ability { Name = abilityName };
                                    await _context.Abilities.AddAsync(ability);
                                    await _context.SaveChangesAsync();
                                }

                                cardStats.CardStatsAbilities.Add(new CardStatsAbility
                                {
                                    Ability = ability,
                                    CardStats = cardStats
                                });
                            }
                            card.CardStats.Add(cardStats);                    
                        }
                        // Handle Summoner cards with ptrOptions
                        else if (card.Type == CardType.Summoner && cardJson.Stats is CardStatsJsonModelSummonerPtrOptions summonerPtrOptionsStats)
                        {
                            var summonerStats = new CardStats
                            {
                                Level = 1,
                                Mana = summonerPtrOptionsStats.Mana,
                                Attack = summonerPtrOptionsStats.Attack,
                                Ranged = summonerPtrOptionsStats.Ranged,
                                Magic = summonerPtrOptionsStats.Magic,
                                Armor = summonerPtrOptionsStats.Armor,
                                Health = summonerPtrOptionsStats.Health,
                                Speed = summonerPtrOptionsStats.Speed,
                                CardStatsAbilities = new List<CardStatsAbility>()
                            };
                            if(summonerPtrOptionsStats.Abilities != null)
                            {
                                foreach (var abilityName in summonerPtrOptionsStats.Abilities)
                                {
                                    var ability = _context.Abilities.FirstOrDefault(a => a.Name == abilityName);
                                    if (ability == null)
                                    {
                                        ability = new CardBattle.DataModels.Ability { Name = abilityName };
                                        await _context.Abilities.AddAsync(ability);
                                        await _context.SaveChangesAsync();
                                    }
                                    summonerStats.CardStatsAbilities.Add(new CardStatsAbility
                                    {
                                        Ability = ability,
                                        CardStats = summonerStats
                                    });
                                }
                            }
                            card.CardStats = new List<CardStats>() {summonerStats};
                            if (summonerPtrOptionsStats.PtrOptions != null)
                            {
                                foreach (var ptrOption in summonerPtrOptionsStats.PtrOptions)
                                {
                                    PtrOptions newPtrOptions = new PtrOptions() {
                                        Card = card,
                                        Name = ptrOption.Name,
                                        Description = ptrOption.Description,
                                        Effect = ptrOption.Effect,
                                        StatBuff = new CardBattle.DataModels.StatBuff
                                        {
                                            AttackModifier = ptrOption.StatBuff?[0] ?? 0,
                                            RangedModifier = ptrOption.StatBuff?[1] ?? 0,
                                            MagicModifier = ptrOption.StatBuff?[2] ?? 0,
                                            ArmorModifier = ptrOption.StatBuff?[3] ?? 0,
                                            HealthModifier = ptrOption.StatBuff?[4] ?? 0,
                                            SpeedModifier = ptrOption.StatBuff?[5] ?? 0,
                                        },
                                        StatusEffects = ptrOption.StatusEffects,
                                        Target = ptrOption.Target,
                                        TargetType = ptrOption.TargetType,
                                        Max = ptrOption.Max    
                                    };
                                    if(ptrOption.Abilities != null)
                                    {
                                        foreach (var abilityName in ptrOption.Abilities)
                                        {
                                            var ability = _context.Abilities.FirstOrDefault(a => a.Name == abilityName);
                                            if (ability == null)
                                            {
                                                ability = new CardBattle.DataModels.Ability { Name = abilityName };
                                                await _context.Abilities.AddAsync(ability);
                                                await _context.SaveChangesAsync();
                                            }
                                            var PtrOptionsAbility = new PtrOptionsAbility
                                            {
                                                Ability = ability,
                                                PtrOptions = newPtrOptions
                                            };
                                        }   
                                    }
                                    card.PtrOptions.Add(newPtrOptions);
                                }
                            }   
                        }
                        else continue;
        
                        _context.Cards.Add(card);
                    }
                    // Save all changes to the database
                    await _context.SaveChangesAsync();
                    Console.WriteLine("SaveChangesAsync to Seeded Cards table");
        
                    // Disable IDENTITY_INSERT after inserting records
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Cards] OFF");
        
                    // Commit the transaction
                    await transaction.CommitAsync();  
                    Console.WriteLine("Transaction committed");  
                }
                catch(Exception ex)
                {
                    // Roll back the transaction if something goes wrong
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        
        public async Task SeedRulesets(string jsonFilePath)
        {
            // Read and deserialize the JSON data
            var jsonData = File.ReadAllText(jsonFilePath);
            var battlesettings = JObject.Parse(jsonData);
        
            // Extract the "battles" portion
            JObject battles = (JObject)battlesettings["battles"];
        
            // Extract rulesets
            JArray rulesets = (JArray)battles["rulesets"];
        
            // Create a list of Ruleset objects
            List<Ruleset> rulesetList = new List<Ruleset>();
            foreach (var ruleset in rulesets)
            {
                rulesetList.Add(new Ruleset
                {
                    Name = (string)ruleset["name"],
                    Description = (string)ruleset["description"]
                });
            }
        
            // Seed the Rulesets table if it is empty
            if (!_context.Rulesets.Any())
            {
                _context.Rulesets.AddRange(rulesetList);
                await _context.SaveChangesAsync();
            }
        }

    }
}

