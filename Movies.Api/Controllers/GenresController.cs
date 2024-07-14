using AutoMapper;
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
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            this._genreService = genreService;
        }

        // GET: api/<GenresController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var x = new List<GenreDto>();
            x.Add(new GenreDto(){ Id = Guid.NewGuid(), Name = "Action"});

           var result =  await _genreService.GetAllGenresAsync();


            return Ok(result);
        }

        // GET api/<GenresController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            throw new NotFoundException("name", new GenreDto());
        }

        // POST api/<GenresController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GenresController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
