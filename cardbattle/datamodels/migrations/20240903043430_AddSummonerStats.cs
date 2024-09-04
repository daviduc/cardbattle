using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardbattle.datamodels.migrations
{
    /// <inheritdoc />
    public partial class AddSummonerStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "SummonerStat",
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
                    table.PrimaryKey("PK_SummonerStat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SummonerStat_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbilityCard_SummonerCardsId",
                table: "AbilityCard",
                column: "SummonerCardsId");

            migrationBuilder.CreateIndex(
                name: "IX_SummonerStat_CardId",
                table: "SummonerStat",
                column: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbilityCard");

            migrationBuilder.DropTable(
                name: "SummonerStat");
        }
    }
}
