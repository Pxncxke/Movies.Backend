using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Interfaces;
using Movies.Api.Models;
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

    public async Task<PagedList<Genre>> GetGenresWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var genresQuery = _context.Genres.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            genresQuery = genresQuery.Where(x => x.Name.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortOrder))
        {
            //genresQuery = genresQuery.OrderBy(sortColumn + " " + sortOrder);
        }

        if(sortOrder == "desc")
        {
            genresQuery = genresQuery.OrderByDescending(x => x.Name);
        }
        else
        {
            genresQuery = genresQuery.OrderBy(x => x.Name);
        }


        //var genres = await genresQuery.Skip((page - 1) * pageSize). Take(pageSize).ToListAsync();

        var genres = await PagedList<Genre>.CreateAsync(genresQuery, page, pageSize);

        return genres;
    }
}
