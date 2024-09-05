using System;
using Newtonsoft.Json;
using System.Collections.Generic;
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
        public async Task SeedDatabase(string jsonFilePath)
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
                            SummonerAbilities = new List<CardBattle.DataModels.Ability>()  // Initialize SummonerAbilities
                        };

                        // Handle Monster cards
                        if (card.Type == CardType.Monster)
                        {
                            //var monsterStats = cardJson.Stats as CardStatsJsonModelMonster;
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

                                    foreach (var abilityName in monsterStats.Abilities[i])
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
                            }
                        }
                        // Handle Summoner cards with abilities
                        else if (card.Type == CardType.Summoner && cardJson.Stats is CardStatsJsonModelSummonerAbilities summonerAbilitiesStats)
                        {
                            
                            var summonerStat = new CardBattle.DataModels.SummonerStat
                            {
                                Mana = summonerAbilitiesStats.Mana,
                                Attack = summonerAbilitiesStats.Attack,
                                Ranged = summonerAbilitiesStats.Ranged,
                                Magic = summonerAbilitiesStats.Magic,
                                Armor = summonerAbilitiesStats.Armor,
                                Health = summonerAbilitiesStats.Health,
                                Speed = summonerAbilitiesStats.Speed,
                                ActiveSummonerAbilities = new List<CardBattle.DataModels.Ability>()
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

                                if (new[] { "Resurrect", "Cleanse", "Triage", "Repair" }.Contains(abilityName))
                                {
                                    summonerStat.ActiveSummonerAbilities.Add(ability);
                                }
                                else
                                {
                                    card.SummonerAbilities.Add(ability);
                                }
                            }

                            card.SummonerStats = new List<CardBattle.DataModels.SummonerStat> { summonerStat };
                        }
                        // Handle Summoner cards with ptrOptions
                        else if (card.Type == CardType.Summoner && cardJson.Stats is CardStatsJsonModelSummonerPtrOptions summonerPtrOptionsStats)
                        {
                            var summonerStat = new CardBattle.DataModels.SummonerStat
                            {
                                Mana = summonerPtrOptionsStats.Mana,
                                Attack = summonerPtrOptionsStats.Attack,
                                Ranged = summonerPtrOptionsStats.Ranged,
                                Magic = summonerPtrOptionsStats.Magic,
                                Armor = summonerPtrOptionsStats.Armor,
                                Health = summonerPtrOptionsStats.Health,
                                Speed = summonerPtrOptionsStats.Speed,
                                StatBuffs = new List<CardBattle.DataModels.StatBuff>()  // Initialize StatBuffs
                            };
                            if (summonerPtrOptionsStats.PtrOptions != null)
                            {
                                foreach (var ptrOption in summonerPtrOptionsStats.PtrOptions)
                                {
                                    var ability = _context.Abilities.FirstOrDefault(a => a.Name == ptrOption.Name);
                                    if (ability == null)
                                    {
                                        ability = new CardBattle.DataModels.Ability { Name = ptrOption.Name };
                                        await _context.Abilities.AddAsync(ability);
                                        await _context.SaveChangesAsync();
                                    }

                                    var ptrStatBuff = new CardBattle.DataModels.StatBuff
                                    {
                                        Mana = ptrOption.StatBuff?[0] ?? 0,
                                        Attack = ptrOption.StatBuff?[1] ?? 0,
                                        Ranged = ptrOption.StatBuff?[2] ?? 0,
                                        Magic = ptrOption.StatBuff?[3] ?? 0,
                                        Armor = ptrOption.StatBuff?[4] ?? 0,
                                        Health = ptrOption.StatBuff?[5] ?? 0,
                                        Speed = ptrOption.StatBuff?[6] ?? 0,
                                        Max = ptrOption.Max
                                    };

                                    summonerStat.StatBuffs.Add(ptrStatBuff);
                                    card.SummonerAbilities.Add(ability);
                                }
                            }   
                            card.SummonerStats = new List<CardBattle.DataModels.SummonerStat> { summonerStat };
                        }

                        _context.Cards.Add(card);
                    }
                    // Save all changes to the database
                    await _context.SaveChangesAsync();

                    // Disable IDENTITY_INSERT after inserting records
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT [dbo].[Cards] OFF");

                    // Commit the transaction
                    await transaction.CommitAsync();    
                }
                catch(Exception ex)
                {
                    // Roll back the transaction if something goes wrong
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

    }
}

