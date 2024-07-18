using Movies.Api.Models;
using Movies.Api.Models.MovieTheaters;

namespace Movies.Api.Interfaces;

public interface IMovieTheaterService
{
    Task<List<MovieTheaterDto>> GetAllMovieTheatersAsync();
    Task<MovieTheaterDto> GetMovieTheaterByIdAsync(Guid id);
    Task CreateMovieTheaterAsync(CreateMovieTheaterDto movieTheater);
    Task UpdateMovieTheaterAsync(UpdateMovieTheaterDto movieTheater);
    Task DeleteMovieTheaterAsync(Guid id);
    Task<PagedList<MovieTheaterDto>> GetMovieTheatersWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize);
}