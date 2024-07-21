using Microsoft.AspNetCore.Identity;
using Movies.Api.Models.Ratings;

namespace Movies.Api.Interfaces;

public interface IRatingService
{
    Task RateMovieAsync(RatingDto ratingDto, IdentityUser identityUser);
}