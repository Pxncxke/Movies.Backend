using Movies.Api.Models;
using Movies.Api.Models.Actors;

namespace Movies.Api.Interfaces;

public interface IActorService
{
    Task<List<ActorDto>> GetAllActorsAsync();
    Task<ActorDto> GetActorByIdAsync(Guid id);
    Task CreateActorAsync(CreateActorDto actor);
    Task UpdateActorAsync(UpdateActorDto actor);
    Task DeleteActorAsync(Guid id);
    Task<PagedList<ActorDto>> GetActorsWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize);
}
