using Microsoft.AspNetCore.Mvc;
using Movies.Api.Interfaces;
using Movies.Api.Models.MovieTheaters;
using Movies.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieTheatersController : ControllerBase
    {
        private readonly IMovieTheaterService movieTheaterService;

        public MovieTheatersController(IMovieTheaterService movieTheaterService)
        {
            this.movieTheaterService = movieTheaterService;
        }
        // GET: api/<MovieTheatersController>
        [HttpGet]
        public async Task<IActionResult> Get(int currentPage, int recordsPerPage)
        {
            var response = await movieTheaterService.GetMovieTheatersWithPaginationAsync(null, null, null, currentPage, recordsPerPage);

            return Ok(response);
        }

        // GET api/<MovieTheatersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await movieTheaterService.GetMovieTheaterByIdAsync(id);
            return Ok(response);
        }

        // POST api/<MovieTheatersController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateMovieTheaterDto movieTheaterDto)
        {
            await movieTheaterService.CreateMovieTheaterAsync(movieTheaterDto);
            return Ok();
        }

        // PUT api/<MovieTheatersController>/5
        [HttpPut]
        public async Task<IActionResult> Put(UpdateMovieTheaterDto movieTheaterDto)
        {
            await movieTheaterService.UpdateMovieTheaterAsync(movieTheaterDto);
            return Ok();
        }

        // DELETE api/<MovieTheatersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await movieTheaterService.DeleteMovieTheaterAsync(id);
            return Ok();
        }
    }
}
