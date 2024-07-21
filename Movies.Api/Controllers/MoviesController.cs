using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Interfaces;
using Movies.Api.Models.Genres;
using Movies.Api.Models.Movies;
using Movies.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "IsAdmin")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            this._movieService = movieService;
        }
        // GET: api/<MoviesController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int currentPage, int recordsPerPage, string? searchTitle = null, 
            bool? inTheaters = null, bool? upcomingReleases = null, string? searchGenre = null, string? sortOrder = null)
        {
            var response = await _movieService.GetMoviesWithPaginationAsync(searchTitle, inTheaters, 
                upcomingReleases, searchGenre, sortOrder, currentPage, recordsPerPage);

            return Ok(response);
        }

        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _movieService.GetMovieByIdAsync(id);
            return Ok(response);
        }

        [HttpGet("details/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWithDetails(Guid id)
        {
            var response = await _movieService.GetMovieWithDependenciesAsync(id);
            return Ok(response);
        }

        [HttpGet("home")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHome(int recordsToReturn, DateTime topDate)
        {
            var response = await _movieService.GetHomeMoviesAsync(recordsToReturn, topDate);
            return Ok(response);
        }

        // POST api/<MoviesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateMovieDto movieDto)
        {
            await _movieService.CreateMovieAsync(movieDto);
            return Ok();
        }

        // PUT api/<MoviesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateMovieDto movieDto)
        {
            await _movieService.UpdateMovieAsync(movieDto);
            return Ok();
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _movieService.DeleteMovieAsync(id);
            return Ok();
        }
    }
}
