using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardbattle.datamodels.migrations
{
    /// <inheritdoc />
    public partial class AddStatBuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatBuff_PtrOptions_PtrOptionsId",
                table: "StatBuff");

            migrationBuilder.AlterColumn<int>(
                name: "PtrOptionsId",
                table: "StatBuff",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Max",
                table: "StatBuff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SummonerStatId",
                table: "StatBuff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StatBuff_SummonerStatId",
                table: "StatBuff",
                column: "SummonerStatId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatBuff_PtrOptions_PtrOptionsId",
                table: "StatBuff",
                column: "PtrOptionsId",
                principalTable: "PtrOptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatBuff_SummonerStat_SummonerStatId",
                table: "StatBuff",
                column: "SummonerStatId",
                principalTable: "SummonerStat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatBuff_PtrOptions_PtrOptionsId",
                table: "StatBuff");

            migrationBuilder.DropForeignKey(
                name: "FK_StatBuff_SummonerStat_SummonerStatId",
                table: "StatBuff");

            migrationBuilder.DropIndex(
                name: "IX_StatBuff_SummonerStatId",
                table: "StatBuff");

            migrationBuilder.DropColumn(
                name: "Max",
                table: "StatBuff");

            migrationBuilder.DropColumn(
                name: "SummonerStatId",
                table: "StatBuff");

            migrationBuilder.AlterColumn<int>(
                name: "PtrOptionsId",
                table: "StatBuff",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StatBuff_PtrOptions_PtrOptionsId",
                table: "StatBuff",
                column: "PtrOptionsId",
                principalTable: "PtrOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
