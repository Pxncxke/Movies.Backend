using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movies.Api.Migrations
{
    /// <inheritdoc />
    public partial class RatingUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieTheatersMovies_ModifiedBy",
                table: "MovieTheatersMovies",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MovieTheaters_ModifiedBy",
                table: "MovieTheaters",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesGenres_ModifiedBy",
                table: "MoviesGenres",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesActors_ModifiedBy",
                table: "MoviesActors",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_ModifiedBy",
                table: "Movies",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ModifiedBy",
                table: "Genres",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_ModifiedBy",
                table: "Actors",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ModifiedBy",
                table: "Ratings",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_AspNetUsers_ModifiedBy",
                table: "Actors",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_AspNetUsers_ModifiedBy",
                table: "Genres",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_ModifiedBy",
                table: "Movies",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesActors_AspNetUsers_ModifiedBy",
                table: "MoviesActors",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesGenres_AspNetUsers_ModifiedBy",
                table: "MoviesGenres",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTheaters_AspNetUsers_ModifiedBy",
                table: "MovieTheaters",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTheatersMovies_AspNetUsers_ModifiedBy",
                table: "MovieTheatersMovies",
                column: "ModifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_AspNetUsers_ModifiedBy",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_AspNetUsers_ModifiedBy",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_ModifiedBy",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesActors_AspNetUsers_ModifiedBy",
                table: "MoviesActors");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesGenres_AspNetUsers_ModifiedBy",
                table: "MoviesGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieTheaters_AspNetUsers_ModifiedBy",
                table: "MovieTheaters");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieTheatersMovies_AspNetUsers_ModifiedBy",
                table: "MovieTheatersMovies");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_MovieTheatersMovies_ModifiedBy",
                table: "MovieTheatersMovies");

            migrationBuilder.DropIndex(
                name: "IX_MovieTheaters_ModifiedBy",
                table: "MovieTheaters");

            migrationBuilder.DropIndex(
                name: "IX_MoviesGenres_ModifiedBy",
                table: "MoviesGenres");

            migrationBuilder.DropIndex(
                name: "IX_MoviesActors_ModifiedBy",
                table: "MoviesActors");

            migrationBuilder.DropIndex(
                name: "IX_Movies_ModifiedBy",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Genres_ModifiedBy",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Actors_ModifiedBy",
                table: "Actors");
        }
    }
}
