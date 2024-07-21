using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Interfaces;
using Movies.Api.Models;
using Movies.Domain.Models;

namespace Movies.Api.Repositories;

public class ActorRepository : GenericRepository<Actor>, IActorRepository
{
    public ActorRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<PagedList<Actor>> GetActorsWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var actorsQuery = _context.Actors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            actorsQuery = actorsQuery.Where(x => x.Name.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(sortColumn) && !string.IsNullOrWhiteSpace(sortOrder))
        {
            //actorsQuery = actorsQuery.OrderBy(sortColumn + " " + sortOrder);
        }

        if (sortOrder == "desc")
        {
            actorsQuery = actorsQuery.OrderByDescending(x => x.Name);
        }
        else
        {
            actorsQuery = actorsQuery.OrderBy(x => x.Name);
        }


        //var actors = await actorsQuery.Skip((page - 1) * pageSize). Take(pageSize).ToListAsync();

        var actors = await PagedList<Actor>.CreateAsync(actorsQuery, page, pageSize);

        return actors;
    }

    public async Task<List<Actor>> SearchActorsByName(string query)
    {
        var actorsQuery = _context.Actors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            actorsQuery = actorsQuery.Where(x => x.Name.Contains(query));
        }

        actorsQuery = actorsQuery.OrderBy(x => x.Name);

        var actors = await actorsQuery.Take(5).ToListAsync();

        return actors;
    }
}
