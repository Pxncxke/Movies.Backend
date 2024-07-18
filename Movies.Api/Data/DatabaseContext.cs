using Microsoft.EntityFrameworkCore;
using Movies.Domain.Models;
using Movies.Domain.Models.Common;
using System.Reflection.Emit;

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
    public DbSet<MoviesActors> MoviesActors { get; set; }
    public DbSet<MoviesGenres> MoviesGenres { get; set; }
    public DbSet<MovieTheatersMovies> MovieTheatersMovies { get; set; }
    //public DbSet<MovieGenre> MovieGenres { get; set; }
    //public DbSet<Rating> Ratings { get; set; }
    //public DbSet<User> Users { get; set; }
    //public DbSet<UserRating> UserRatings { get; set; }
    //public DbSet<UserWatchlist> UserWatchlists { get; set; }
    //public DbSet<Watchlist> Watchlists { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<MoviesActors>()
            .HasIndex(p => new { p.MovieId, p.ActorId })
            .IsUnique();
        builder.Entity<MoviesGenres>()
            .HasIndex(p => new { p.MovieId, p.GenreId })
            .IsUnique();
        builder.Entity<MovieTheatersMovies>()
            .HasIndex(p => new { p.MovieId, p.MovieTheaterId })
            .IsUnique();
        builder.HasPostgresExtension("postgis");
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            
            
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                //entry.Entity.ModifiedBy = _userService.UserId;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                //entry.Entity.ModifiedBy = _userService.UserId;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
