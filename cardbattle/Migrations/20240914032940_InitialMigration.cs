using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardbattle.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManaCap = table.Column<int>(type: "int", nullable: false),
                    AllowableColors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Rarity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rulesets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rulesets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BattleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CardStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Mana = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Ranged = table.Column<int>(type: "int", nullable: false),
                    Magic = table.Column<int>(type: "int", nullable: false),
                    Armor = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardStats_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PtrOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtrOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PtrOptions_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SummonerStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Mana = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Ranged = table.Column<int>(type: "int", nullable: false),
                    Magic = table.Column<int>(type: "int", nullable: false),
                    Armor = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummonerStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SummonerStats_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BattleRulesets",
                columns: table => new
                {
                    BattleId = table.Column<int>(type: "int", nullable: false),
                    RulesetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleRulesets", x => new { x.BattleId, x.RulesetId });
                    table.ForeignKey(
                        name: "FK_BattleRulesets_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BattleRulesets_Rulesets_RulesetId",
                        column: x => x.RulesetId,
                        principalTable: "Rulesets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamCards",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamCards", x => new { x.TeamId, x.CardId });
                    table.ForeignKey(
                        name: "FK_TeamCards_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamCards_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SummonerStatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abilities_SummonerStats_SummonerStatId",
                        column: x => x.SummonerStatId,
                        principalTable: "SummonerStats",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StatBuff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SummonerStatId = table.Column<int>(type: "int", nullable: false),
                    Mana = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Ranged = table.Column<int>(type: "int", nullable: false),
                    Magic = table.Column<int>(type: "int", nullable: false),
                    Armor = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Max = table.Column<int>(type: "int", nullable: false),
                    PtrOptionsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatBuff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatBuff_PtrOptions_PtrOptionsId",
                        column: x => x.PtrOptionsId,
                        principalTable: "PtrOptions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StatBuff_SummonerStats_SummonerStatId",
                        column: x => x.SummonerStatId,
                        principalTable: "SummonerStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbilityCard",
                columns: table => new
                {
                    SummonerAbilitiesId = table.Column<int>(type: "int", nullable: false),
                    SummonerCardsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbilityCard", x => new { x.SummonerAbilitiesId, x.SummonerCardsId });
                    table.ForeignKey(
                        name: "FK_AbilityCard_Abilities_SummonerAbilitiesId",
                        column: x => x.SummonerAbilitiesId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbilityCard_Cards_SummonerCardsId",
                        column: x => x.SummonerCardsId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardStatsAbilities",
                columns: table => new
                {
                    CardStatsId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardStatsAbilities", x => new { x.CardStatsId, x.AbilityId });
                    table.ForeignKey(
                        name: "FK_CardStatsAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardStatsAbilities_CardStats_CardStatsId",
                        column: x => x.CardStatsId,
                        principalTable: "CardStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PtrOptionAbility",
                columns: table => new
                {
                    PtrOptionId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtrOptionAbility", x => new { x.PtrOptionId, x.AbilityId });
                    table.ForeignKey(
                        name: "FK_PtrOptionAbility_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PtrOptionAbility_PtrOptions_PtrOptionId",
                        column: x => x.PtrOptionId,
                        principalTable: "PtrOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SummonerStatAbilities",
                columns: table => new
                {
                    SummonerStatId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummonerStatAbilities", x => new { x.SummonerStatId, x.AbilityId });
                    table.ForeignKey(
                        name: "FK_SummonerStatAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SummonerStatAbilities_SummonerStats_SummonerStatId",
                        column: x => x.SummonerStatId,
                        principalTable: "SummonerStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_Name",
                table: "Abilities",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_SummonerStatId",
                table: "Abilities",
                column: "SummonerStatId");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityCard_SummonerCardsId",
                table: "AbilityCard",
                column: "SummonerCardsId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleRulesets_RulesetId",
                table: "BattleRulesets",
                column: "RulesetId");

            migrationBuilder.CreateIndex(
                name: "IX_CardStats_CardId",
                table: "CardStats",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardStatsAbilities_AbilityId",
                table: "CardStatsAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PtrOptionAbility_AbilityId",
                table: "PtrOptionAbility",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PtrOptions_CardId",
                table: "PtrOptions",
                column: "CardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatBuff_PtrOptionsId",
                table: "StatBuff",
                column: "PtrOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_StatBuff_SummonerStatId",
                table: "StatBuff",
                column: "SummonerStatId");

            migrationBuilder.CreateIndex(
                name: "IX_SummonerStatAbilities_AbilityId",
                table: "SummonerStatAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SummonerStats_CardId",
                table: "SummonerStats",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamCards_CardId",
                table: "TeamCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_BattleId",
                table: "Teams",
                column: "BattleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbilityCard");

            migrationBuilder.DropTable(
                name: "BattleRulesets");

            migrationBuilder.DropTable(
                name: "CardStatsAbilities");

            migrationBuilder.DropTable(
                name: "PtrOptionAbility");

            migrationBuilder.DropTable(
                name: "StatBuff");

            migrationBuilder.DropTable(
                name: "SummonerStatAbilities");

            migrationBuilder.DropTable(
                name: "TeamCards");

            migrationBuilder.DropTable(
                name: "Rulesets");

            migrationBuilder.DropTable(
                name: "CardStats");

            migrationBuilder.DropTable(
                name: "PtrOptions");

            migrationBuilder.DropTable(
                name: "Abilities");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "SummonerStats");

            migrationBuilder.DropTable(
                name: "Battles");

            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
