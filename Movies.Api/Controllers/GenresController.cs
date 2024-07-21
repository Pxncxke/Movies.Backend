using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Exceptions;
using Movies.Api.Interfaces;
using Movies.Api.Models.Genres;
using Movies.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "IsAdmin")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            this._genreService = genreService;
        }

        // GET: api/<GenresController>
        [HttpGet]
        public async Task<IActionResult> Get(int currentPage, int recordsPerPage)
        {
           var response = await _genreService.GetGenresWithPaginationAsync(null, null, null, currentPage, recordsPerPage);

            return Ok(response);
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllGenres()
        {
            var response = await _genreService.GetAllGenresAsync();
            return Ok(response);
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _genreService.GetGenreByIdAsync(id);
            return Ok(response);
        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateGenreDto genreDto)
        {
            await _genreService.CreateGenreAsync(genreDto);
            return Ok();
        }

        // PUT api/<GenresController>/5
        [HttpPut]
        public async Task<IActionResult> Put(UpdateGenreDto genreDto)
        {
            await _genreService.UpdateGenreAsync(genreDto);
            return Ok();
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _genreService.DeleteGenreAsync(id);
            return Ok();
        }
    }
}
