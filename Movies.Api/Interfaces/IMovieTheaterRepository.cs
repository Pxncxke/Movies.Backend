using Movies.Api.Models;
using Movies.Domain.Models;

namespace Movies.Api.Interfaces;

public interface IMovieTheaterRepository : IGenericRepository<MovieTheater>
{
    Task<PagedList<MovieTheater>> GetMovieTheatersWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize);
}
