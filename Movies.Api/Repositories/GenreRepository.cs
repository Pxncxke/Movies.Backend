using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Interfaces;
using Movies.Domain.Models;

namespace Movies.Api.Repositories;

public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    public GenreRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<Genre?> GetByNameAsync(string name)
    {
        var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Name == name);
        return genre;
    }
}
