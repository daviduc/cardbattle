using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardbattle.datamodels.migrations
{
    /// <inheritdoc />
    public partial class AddPtrOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PtrOptionsId",
                table: "Abilities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SummonerStatId",
                table: "Abilities",
                type: "int",
                nullable: true);

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
                name: "StatBuff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PtrOptionsId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_StatBuff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatBuff_PtrOptions_PtrOptionsId",
                        column: x => x.PtrOptionsId,
                        principalTable: "PtrOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_PtrOptionsId",
                table: "Abilities",
                column: "PtrOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Abilities_SummonerStatId",
                table: "Abilities",
                column: "SummonerStatId");

            migrationBuilder.CreateIndex(
                name: "IX_PtrOptions_CardId",
                table: "PtrOptions",
                column: "CardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatBuff_PtrOptionsId",
                table: "StatBuff",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_PtrOptions_PtrOptionsId",
                table: "Abilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Abilities_SummonerStat_SummonerStatId",
                table: "Abilities");

            migrationBuilder.DropTable(
                name: "StatBuff");

            migrationBuilder.DropTable(
                name: "PtrOptions");

            migrationBuilder.DropIndex(
                name: "IX_Abilities_PtrOptionsId",
                table: "Abilities");

            migrationBuilder.DropIndex(
                name: "IX_Abilities_SummonerStatId",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "PtrOptionsId",
                table: "Abilities");

            migrationBuilder.DropColumn(
                name: "SummonerStatId",
                table: "Abilities");
        }
    }
}
