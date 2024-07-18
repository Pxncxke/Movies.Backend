using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Interfaces;
using Movies.Domain.Models.Common;
namespace Movies.Api.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly DatabaseContext _context;

    public GenericRepository(DatabaseContext context)
    {
        this._context = context;
    }

    public async Task CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.FindAsync<T>(id);
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
