using Movies.Api.Models;
using Movies.Domain.Models;

namespace Movies.Api.Interfaces;

public interface IGenreRepository : IGenericRepository<Genre>
{
    Task<Genre?> GetByNameAsync(string name);
    Task<PagedList<Genre>> GetGenresWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize);
}
