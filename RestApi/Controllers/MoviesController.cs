using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.Models;
using Microsoft.EntityFrameworkCore;



namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _movieContext;

        public MoviesController(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _movieContext.Movies.ToListAsync();
        }

        // GET: api/Movies/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _movieContext.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // POST : api/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            _movieContext.Movies.Add(movie);
            await _movieContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // PUT : api/Movies/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest("Movie ID in URL does not match movie ID in body.");
            }

            _movieContext.Entry(movie).State = EntityState.Modified;

            try
            {
                await _movieContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _movieContext.Movies.Any(e => e.Id == id);
        }

        // Delete : api/Movies/2
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            if (_movieContext.Movies is null)
            {
                return NotFound();
            }

            var movie = await _movieContext.Movies.FindAsync(id);
            if (movie is null)
            {
                return NotFound();
            }
            _movieContext.Movies.Remove(movie);
            await _movieContext.SaveChangesAsync();
            return NoContent();
        }


    }
}
