using Microsoft.AspNetCore.Identity;
using Movies.Api.Interfaces;
using Movies.Api.Models.Ratings;
using Movies.Domain.Models;

namespace Movies.Api.Services;

public class RatingService : IRatingService
{
    private readonly IRatingRepository _ratingRepository;

    public RatingService(IRatingRepository ratingRepository)
    {
        this._ratingRepository = ratingRepository;
    }
    public async Task RateMovieAsync(RatingDto ratingDto, IdentityUser identityUser)
    {
        var userRating = await _ratingRepository.GetUserMovieRatingAsync(identityUser.Id, ratingDto.MovieId);

        if (userRating == null) 
        {
            var rating = new Rating()
            {
                Rate = ratingDto.Rating,
                MovieId = ratingDto.MovieId,
                ModifiedBy = identityUser.Id
            };
            await _ratingRepository.CreateAsync(rating);
            return;
        }

        userRating.Rate = ratingDto.Rating;
        userRating.CreatedAt = userRating.CreatedAt;
        await _ratingRepository.UpdateAsync(userRating);
    }
}
