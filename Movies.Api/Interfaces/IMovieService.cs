using Movies.Api.Models;
using Movies.Api.Models.Movies;

namespace Movies.Api.Interfaces;

public interface IMovieService
{
    Task<List<MovieDto>> GetAllMoviesAsync();
    Task<MovieToUpdateDto> GetMovieByIdAsync(Guid id);
    Task<MovieDto> GetMovieWithDependenciesAsync(Guid id);
    Task<HomeDto> GetHomeMoviesAsync(int recordsToReturn, DateTime topDate);
    Task CreateMovieAsync(CreateMovieDto movie);
    Task UpdateMovieAsync(UpdateMovieDto movie);
    Task DeleteMovieAsync(Guid id);
    Task<PagedList<MovieDto>> GetMoviesWithPaginationAsync(string? searchTitle, bool? inTheaters, bool? upcomingReleases, string? searchGenre, string? sortOrder, int page, int pageSize);
}