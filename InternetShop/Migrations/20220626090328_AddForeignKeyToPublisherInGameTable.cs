using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetShop.Migrations
{
    public partial class AddForeignKeyToPublisherInGameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Publisher_PublusherId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_PublusherId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "PublusherId",
                table: "Game");
        }
    }
}
