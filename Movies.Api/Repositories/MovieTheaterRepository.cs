using Movies.Api.Data;
using Movies.Api.Interfaces;
using Movies.Api.Models;
using Movies.Domain.Models;

namespace Movies.Api.Repositories;

public class MovieTheaterRepository : GenericRepository<MovieTheater>, IMovieTheaterRepository
{
    public MovieTheaterRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<PagedList<MovieTheater>> GetMovieTheatersWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var theatersQuery = _context.MovieTheaters.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            theatersQuery = theatersQuery.Where(x => x.Name.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortOrder))
        {
            //genresQuery = genresQuery.OrderBy(sortColumn + " " + sortOrder);
        }

        if (sortOrder == "desc")
        {
            theatersQuery = theatersQuery.OrderByDescending(x => x.Name);
        }
        else
        {
            theatersQuery = theatersQuery.OrderBy(x => x.Name);
        }


        //var genres = await genresQuery.Skip((page - 1) * pageSize). Take(pageSize).ToListAsync();

        var theaters = await PagedList<MovieTheater>.CreateAsync(theatersQuery, page, pageSize);

        return theaters;
    }
}
