using Microsoft.EntityFrameworkCore;
using Movies.Api.Data;
using Movies.Api.Interfaces;
using Movies.Domain.Models;

namespace Movies.Api.Repositories;

public class RatingRepository : GenericRepository<Rating>, IRatingRepository
{
    public RatingRepository(DatabaseContext context) : base(context)
    {
    }

    public async Task<decimal> GetAverageRatingAsync(Guid movieId)
    {
        var averaRating = 0m;
        var averageList = await _context.Ratings.Where(x => x.MovieId == movieId).Select(x => x.Rate).ToListAsync();
        if (averageList.Count > 0) 
        {
            averaRating = (decimal)averageList.Average();
        }
        return Math.Round(averaRating, 2);
    }

    public async Task<int> GetAverageUsersAsync(Guid movieId)
    {
        var averageUser = await _context.Ratings.Where(x => x.MovieId == movieId).CountAsync();
        return averageUser;
    }

    public async Task<Rating?> GetUserMovieRatingAsync(string userId, Guid movieId)
    {
        return await _context.Ratings.FirstOrDefaultAsync(x => x.MovieId == movieId && x.ModifiedBy == userId);
    }
}
