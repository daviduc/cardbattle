using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardbattle.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsForParsing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_SummonerStats_SummonerStatId",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_StatBuff_PtrOptions_PtrOptionsId",
                table: "StatBuff");

            migrationBuilder.DropForeignKey(
                name: "FK_StatBuff_SummonerStats_SummonerStatId",
                table: "StatBuff");

            migrationBuilder.DropTable(
                name: "AbilityCard");

            migrationBuilder.DropTable(
                name: "PtrOptionAbility");

            migrationBuilder.DropTable(
                name: "SummonerStatAbilities");

            migrationBuilder.DropTable(
                name: "SummonerStats");

            migrationBuilder.DropIndex(
                name: "IX_PtrOptions_CardId",
                table: "PtrOptions");

            migrationBuilder.DropIndex(
                name: "IX_Abilities_SummonerStatId",
                table: "Abilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatBuff",
                table: "StatBuff");

            migrationBuilder.DropIndex(
                name: "IX_StatBuff_PtrOptionsId",
                table: "StatBuff");

            migrationBuilder.DropIndex(
                name: "IX_StatBuff_SummonerStatId",
                table: "StatBuff");

            migrationBuilder.DropColumn(
                name: "SummonerStatId",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "Armor",
                table: "StatBuff");

            migrationBuilder.DropColumn(
                name: "Attack",
                table: "StatBuff");

            migrationBuilder.DropColumn(
                name: "Health",
                table: "StatBuff");

            migrationBuilder.DropColumn(
                name: "PtrOptionsId",
                table: "StatBuff");

            migrationBuilder.RenameTable(
                name: "StatBuff",
                newName: "StatBuffs");

            migrationBuilder.RenameColumn(
                name: "SummonerStatId",
                table: "StatBuffs",
                newName: "SpeedModifier");

            migrationBuilder.RenameColumn(
                name: "Speed",
                table: "StatBuffs",
                newName: "RangedModifier");

            migrationBuilder.RenameColumn(
                name: "Ranged",
                table: "StatBuffs",
                newName: "MagicModifier");

            migrationBuilder.RenameColumn(
                name: "Max",
                table: "StatBuffs",
                newName: "HealthModifier");

            migrationBuilder.RenameColumn(
                name: "Mana",
                table: "StatBuffs",
                newName: "AttackModifier");

            migrationBuilder.RenameColumn(
                name: "Magic",
                table: "StatBuffs",
                newName: "ArmorModifier");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PtrOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Effect",
                table: "PtrOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PtrOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatBuffId",
                table: "PtrOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StatusEffects",
                table: "PtrOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "PtrOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetType",
                table: "PtrOptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatBuffs",
                table: "StatBuffs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PtrOptionAbilities",
                columns: table => new
                {
                    PtrOptionsId = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtrOptionAbilities", x => new { x.PtrOptionsId, x.AbilityId });
                    table.ForeignKey(
                        name: "FK_PtrOptionAbilities_Abilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PtrOptionAbilities_PtrOptions_PtrOptionsId",
                        column: x => x.PtrOptionsId,
                        principalTable: "PtrOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PtrOptions_CardId",
                table: "PtrOptions",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PtrOptions_StatBuffId",
                table: "PtrOptions",
                column: "StatBuffId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PtrOptionAbilities_AbilityId",
                table: "PtrOptionAbilities",
                column: "AbilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PtrOptions_StatBuffs_StatBuffId",
                table: "PtrOptions",
                column: "StatBuffId",
                principalTable: "StatBuffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PtrOptions_StatBuffs_StatBuffId",
                table: "PtrOptions");

            migrationBuilder.DropTable(
                name: "PtrOptionAbilities");

            migrationBuilder.DropIndex(
                name: "IX_PtrOptions_CardId",
                table: "PtrOptions");

            migrationBuilder.DropIndex(
                name: "IX_PtrOptions_StatBuffId",
                table: "PtrOptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatBuffs",
                table: "StatBuffs");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PtrOptions");

            migrationBuilder.DropColumn(
                name: "Effect",
                table: "PtrOptions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PtrOptions");

            migrationBuilder.DropColumn(
                name: "StatBuffId",
                table: "PtrOptions");

            migrationBuilder.DropColumn(
                name: "StatusEffects",
                table: "PtrOptions");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "PtrOptions");

            migrationBuilder.DropColumn(
                name: "TargetType",
                table: "PtrOptions");

            migrationBuilder.RenameTable(
                name: "StatBuffs",
                newName: "StatBuff");

            migrationBuilder.RenameColumn(
                name: "SpeedModifier",
                table: "StatBuff",
                newName: "SummonerStatId");

            migrationBuilder.RenameColumn(
                name: "RangedModifier",
                table: "StatBuff",
                newName: "Speed");

            migrationBuilder.RenameColumn(
                name: "MagicModifier",
                table: "StatBuff",
                newName: "Ranged");

            migrationBuilder.RenameColumn(
                name: "HealthModifier",
                table: "StatBuff",
                newName: "Max");

            migrationBuilder.RenameColumn(
                name: "AttackModifier",
                table: "StatBuff",
                newName: "Mana");

            migrationBuilder.RenameColumn(
                name: "ArmorModifier",
                table: "StatBuff",
                newName: "Magic");

            migrationBuilder.AddColumn<int>(
                name: "SummonerStatId",
                table: "Abilities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Armor",
                table: "StatBuff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "StatBuff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Health",
                table: "StatBuff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PtrOptionsId",
                table: "StatBuff",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatBuff",
                table: "StatBuff",
                column: "Id");

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
                name: "SummonerStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Armor = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Magic = table.Column<int>(type: "int", nullable: false),
                    Mana = table.Column<int>(type: "int", nullable: false),
                    Ranged = table.Column<int>(type: "int", nullable: false),
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
                name: "IX_PtrOptions_CardId",
                table: "PtrOptions",
                column: "CardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_SummonerStatId",
                table: "Abilities",
                column: "SummonerStatId");

            migrationBuilder.CreateIndex(
                name: "IX_StatBuff_PtrOptionsId",
                table: "StatBuff",
                column: "PtrOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_StatBuff_SummonerStatId",
                table: "StatBuff",
                column: "SummonerStatId");

            migrationBuilder.CreateIndex(
                name: "IX_AbilityCard_SummonerCardsId",
                table: "AbilityCard",
                column: "SummonerCardsId");

            migrationBuilder.CreateIndex(
                name: "IX_PtrOptionAbility_AbilityId",
                table: "PtrOptionAbility",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SummonerStatAbilities_AbilityId",
                table: "SummonerStatAbilities",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SummonerStats_CardId",
                table: "SummonerStats",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_SummonerStats_SummonerStatId",
                table: "Abilities",
                column: "SummonerStatId",
                principalTable: "SummonerStats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatBuff_PtrOptions_PtrOptionsId",
                table: "StatBuff",
                column: "PtrOptionsId",
                principalTable: "PtrOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatBuff_SummonerStats_SummonerStatId",
                table: "StatBuff",
                column: "SummonerStatId",
                principalTable: "SummonerStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
