using Movies.Api.Models;
using Movies.Api.Models.Genres;
using Movies.Domain.Models;

namespace Movies.Api.Interfaces;

public interface IGenreService
{
    Task<List<GenreDto>> GetAllGenresAsync();
    Task<GenreDto> GetGenreByIdAsync(Guid id);
    Task CreateGenreAsync(CreateGenreDto genre);
    Task UpdateGenreAsync(UpdateGenreDto genre);
    Task DeleteGenreAsync(Guid id);
    Task<PagedList<GenreDto>> GetGenresWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize);
}
