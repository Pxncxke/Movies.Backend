using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Interfaces;
using Movies.Api.Models;
using Movies.Api.Models.Movies;
using Movies.Domain.Models;

namespace Movies.Api.Repositories;

public class MovieRepository : GenericRepository<Movie>, IMovieRepository
{
    public MovieRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<List<Movie>> GetInTheaters(int recordsToReturn)
    {
        var inTheaters = await _context.Movies.Where(x => x.InTheaters).OrderBy(x => x.ReleaseDate).Take(recordsToReturn).ToListAsync();
        return inTheaters;
    }

    public async Task<PagedList<Movie>> GetMoviesWithPaginationAsync(string? searchTitle, bool? inTheaters, bool? upcomingReleases, string? searchGenre, string? sortOrder, int page, int pageSize)
    {
        var moviesQuery = _context.Movies.AsQueryable();

        if (!String.IsNullOrWhiteSpace(searchTitle))
        {
            moviesQuery = moviesQuery.Where(x => x.Title.Contains(searchTitle));
        }

        if (inTheaters == true)
        {
            moviesQuery = moviesQuery.Where(x => x.InTheaters);
        }

        if (upcomingReleases == true)
        {
            moviesQuery = moviesQuery.Where(x => x.ReleaseDate > DateTime.Today.ToUniversalTime());
        }

        if(!String.IsNullOrWhiteSpace(searchGenre))
        {
            moviesQuery = moviesQuery.Where(x => x.MoviesGenres.Select(x => x.Genre.Name).Contains(searchGenre));
        }

        if (sortOrder == "desc")
        {
            moviesQuery = moviesQuery.OrderByDescending(x => x.Title);
        }
        else
        {
            moviesQuery = moviesQuery.OrderBy(x => x.Title);
        }


        //var genres = await moviesQuery.Skip((page - 1) * pageSize). Take(pageSize).ToListAsync();

        var movies = await PagedList<Movie>.CreateAsync(moviesQuery, page, pageSize);

        return movies;
    }

    public Task<Movie> GetMovieWithDependenciesAsync(Guid id)
    {
        var movie = _context.Movies
            .Include(x => x.MoviesActors).ThenInclude(x => x.Actor)
            .Include(x => x.MoviesGenres).ThenInclude(x => x.Genre)
            .Include(x => x.MovieTheatersMovies).ThenInclude(x => x.MovieTheater)
            .FirstOrDefaultAsync(x => x.Id == id);

        return movie;
    }

    public async Task<List<Movie>> GetUpComingReleases(int recordsToReturn, DateTime topDate)
    {
       var upcomingReleases = await _context.Movies.Where(x => x.ReleaseDate > topDate).OrderBy(x => x.ReleaseDate).Take(recordsToReturn).ToListAsync();
        return upcomingReleases;
    }
}
