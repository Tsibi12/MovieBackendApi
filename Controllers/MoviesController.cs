using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Repository;
using Movies.Models;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepo;
        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepo = movieRepository;
        }

        // GET api/values
        // [HttpGet]
        public async Task<List<Movie>> GetMovies()
        {
            return await _movieRepo.GetMovies();
        }

        // GET api/controller/rating
        [HttpGet("api/controller")]
        [Route("rating")]
        public async Task<List<Movie>> GetMoviesByRating()
        {
            return await _movieRepo.GetMoviesByRating();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieByID(int id)
        {
            var movie =  await _movieRepo.GetMovieByID(id);
            if (movie != null)  
            {  
                return Ok(movie);  
            }  
  
            return NotFound(new { Message = $"Movie with id {id} is not available." });
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddEditMovie([FromBody]Movie movie)
        {
            if (movie == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid State");
            }

            return await _movieRepo.AddEditMovie(movie);
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteById(int id)
        {
            return await _movieRepo.DeleteMovie(id);
        }
    }
}