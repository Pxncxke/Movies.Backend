using Microsoft.EntityFrameworkCore;
using Movies.Domain.Models;

namespace Movies.Api.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<MovieTheater> MovieTheaters { get; set; }
    //public DbSet<MovieGenre> MovieGenres { get; set; }
    //public DbSet<Rating> Ratings { get; set; }
    //public DbSet<User> Users { get; set; }
    //public DbSet<UserRating> UserRatings { get; set; }
    //public DbSet<UserWatchlist> UserWatchlists { get; set; }
    //public DbSet<Watchlist> Watchlists { get; set; }
}
