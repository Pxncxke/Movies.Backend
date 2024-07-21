using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Interfaces;
using Movies.Api.Models.Ratings;
using System.Security.Claims;

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRatingService _ratingService;

        public RatingsController(UserManager<IdentityUser> userManager, IRatingService ratingService)
        {
            this._userManager = userManager;
            this._ratingService = ratingService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] RatingDto ratingDto)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailAsync(email);
            await _ratingService.RateMovieAsync(ratingDto, user);
            return Ok();
        }
    }
}
