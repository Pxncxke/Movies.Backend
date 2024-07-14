using Movies.Api.Models.Genres;

namespace Movies.Api.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreDto>> GetAllGenresAsync();
        Task<GenreDto> GetGenreByIdAsync(Guid id);
        Task CreateGenreAsync(CreateGenreDto genre);
        Task UpdateGenreAsync(CreateGenreDto genre);
        Task DeleteGenreAsync(Guid id);
    }
}
