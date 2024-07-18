using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Api.Migrations
{
    /// <inheritdoc />
    public partial class ActorsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Awards",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "PictureUri",
                table: "Actors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "Awards",
                table: "Actors",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUri",
                table: "Actors",
                type: "text",
                nullable: true);
        }
    }
}
