using Movies.Domain.Models;

namespace Movies.Api.Interfaces;

public interface IRatingRepository : IGenericRepository<Rating>
{
    Task<Rating?> GetUserMovieRatingAsync(string userId, Guid movieId);    
    Task<decimal> GetAverageRatingAsync(Guid movieId);
    Task<int> GetAverageUsersAsync(Guid movieId);
}
