using Movies.Domain.Models.Common;

namespace Movies.Api.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}