using Movies.Api.Models;
using Movies.Api.Models.Movies;
using Movies.Domain.Models;

namespace Movies.Api.Interfaces;

public interface IMovieRepository : IGenericRepository<Movie>
{
    Task<Movie> GetMovieWithDependenciesAsync(Guid id);
    Task<PagedList<Movie>> GetMoviesWithPaginationAsync(string? searchTitle, bool? inTheaters, bool? upcomingReleases, string? searchGenre, string? sortOrder, int page, int pageSize);
    Task<List<Movie>> GetUpComingReleases(int recordsToReturn, DateTime topDate);
    Task<List<Movie>> GetInTheaters(int recordsToReturn);
}