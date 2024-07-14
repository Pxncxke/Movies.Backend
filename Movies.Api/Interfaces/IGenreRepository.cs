using Movies.Domain.Models;

namespace Movies.Api.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<Genre?> GetByNameAsync(string name);
    }
}
