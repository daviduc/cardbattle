using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardbattle.datamodels.migrations
{
    /// <inheritdoc />
    public partial class AddMoreJoins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_PtrOptions_PtrOptionsId",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_SummonerStat_SummonerStatId",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_StatBuff_SummonerStat_SummonerStatId",
                table: "StatBuff");

            migrationBuilder.DropForeignKey(
                name: "FK_SummonerStat_Cards_CardId",
                table: "SummonerStat");

            migrationBuilder.DropIndex(
                name: "IX_Abilities_PtrOptionsId",
                table: "Abilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SummonerStat",
                table: "SummonerStat");

            migrationBuilder.DropColumn(
                name: "PtrOptionsId",
                table: "Abilities");

            migrationBuilder.RenameTable(
                name: "SummonerStat",
                newName: "SummonerStats");

            migrationBuilder.RenameIndex(
                name: "IX_SummonerStat_CardId",
                table: "SummonerStats",
                newName: "IX_SummonerStats_CardId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SummonerStats",
                table: "SummonerStats",
                column: "Id");

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
                name: "IX_PtrOptionAbility_AbilityId",
                table: "PtrOptionAbility",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_SummonerStatAbilities_AbilityId",
                table: "SummonerStatAbilities",
                column: "AbilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_SummonerStats_SummonerStatId",
                table: "Abilities",
                column: "SummonerStatId",
                principalTable: "SummonerStats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatBuff_SummonerStats_SummonerStatId",
                table: "StatBuff",
                column: "SummonerStatId",
                principalTable: "SummonerStats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SummonerStats_Cards_CardId",
                table: "SummonerStats",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_SummonerStats_SummonerStatId",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_StatBuff_SummonerStats_SummonerStatId",
                table: "StatBuff");

            migrationBuilder.DropForeignKey(
                name: "FK_SummonerStats_Cards_CardId",
                table: "SummonerStats");

            migrationBuilder.DropTable(
                name: "PtrOptionAbility");

            migrationBuilder.DropTable(
                name: "SummonerStatAbilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SummonerStats",
                table: "SummonerStats");

            migrationBuilder.RenameTable(
                name: "SummonerStats",
                newName: "SummonerStat");

            migrationBuilder.RenameIndex(
                name: "IX_SummonerStats_CardId",
                table: "SummonerStat",
                newName: "IX_SummonerStat_CardId");

            migrationBuilder.AddColumn<int>(
                name: "PtrOptionsId",
                table: "Abilities",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SummonerStat",
                table: "SummonerStat",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_PtrOptionsId",
                table: "Abilities",
                column: "PtrOptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_PtrOptions_PtrOptionsId",
                table: "Abilities",
                column: "PtrOptionsId",
                principalTable: "PtrOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Abilities_SummonerStat_SummonerStatId",
                table: "Abilities",
                column: "SummonerStatId",
                principalTable: "SummonerStat",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatBuff_SummonerStat_SummonerStatId",
                table: "StatBuff",
                column: "SummonerStatId",
                principalTable: "SummonerStat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SummonerStat_Cards_CardId",
                table: "SummonerStat",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
