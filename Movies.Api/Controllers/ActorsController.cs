using Microsoft.AspNetCore.Mvc;
using Movies.Api.Interfaces;
using Movies.Api.Models.Actors;
using Movies.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Movies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService actorService;

        public ActorsController(IActorService actorService)
        {
            this.actorService = actorService;
        }
        // GET: api/<ActorsController>
        [HttpGet]
        public async Task<IActionResult> Get(int currentPage, int recordsPerPage)
        {
            var response = await actorService.GetActorsWithPaginationAsync(null, null, null, currentPage, recordsPerPage);

            return Ok(response);
        }

        // GET api/<ActorsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await actorService.GetActorByIdAsync(id);
            return Ok(response);
        }

        // POST api/<ActorsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateActorDto actorDto)
        {
            await actorService.CreateActorAsync(actorDto);
            return Ok();
        }

        // PUT api/<ActorsController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] UpdateActorDto actorDto)
        {
            await actorService.UpdateActorAsync(actorDto);
            return Ok();
        }

        // DELETE api/<ActorsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await actorService.DeleteActorAsync(id);
            return Ok();
        }
    }
}
