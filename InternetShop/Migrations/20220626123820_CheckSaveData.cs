using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetShop.Migrations
{
    public partial class CheckSaveData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Publisher_PublusherId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_PublusherId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "PublusherId",
                table: "Game");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PublisherId",
                table: "Game",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Publisher_PublisherId",
                table: "Game",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Publisher_PublisherId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_PublisherId",
                table: "Game");

            migrationBuilder.AddColumn<int>(
                name: "PublusherId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_PublusherId",
                table: "Game",
                column: "PublusherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Publisher_PublusherId",
                table: "Game",
                column: "PublusherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
