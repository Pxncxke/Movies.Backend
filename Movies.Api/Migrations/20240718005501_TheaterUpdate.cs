using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Movies.Api.Migrations
{
    /// <inheritdoc />
    public partial class TheaterUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "MovieTheaters");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "MovieTheaters");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "MovieTheaters",
                type: "geometry (point)",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "MovieTheaters");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "MovieTheaters",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "MovieTheaters",
                type: "double precision",
                nullable: true);
        }
    }
}
