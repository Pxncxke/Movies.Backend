using Movies.Api.Models;
using Movies.Domain.Models;

namespace Movies.Api.Interfaces;

public interface IActorRepository : IGenericRepository<Actor>
{
    Task<PagedList<Actor>> GetActorsWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize);
    Task<List<Actor>> SearchActorsByName(string query);
}
