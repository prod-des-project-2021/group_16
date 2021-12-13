using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessWeb.DAL.Migrations
{
    public partial class WinnerAndSideReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WhitePiecesPlayer",
                table: "Games",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Winner",
                table: "Games",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhitePiecesPlayer",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Games");
        }
    }
}
