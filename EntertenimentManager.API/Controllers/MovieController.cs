using EntertenimentManager.API.ViewModels;
using EntertenimentManager.API.Data;
using EntertenimentManager.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntertenimentManager.Domain.Models.Itens;

namespace EntertenimentManager.API.Controllers
{
    public class MovieController : ControllerBase
    {
        [HttpGet("v1/movies")]
        public async Task<IActionResult> GetAsync([FromServices] EntertenimentManagementDataContext context)
        {
            try
            {
                var movies = await context.Movies.ToListAsync();
                return Ok(new ResultViewModel<List<Movie>>(movies));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Movie>>("Não foi possível buscar os filmes"));
            }
        }

        [HttpGet("v1/movies/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EntertenimentManagementDataContext context)
        {
            try
            {
                var movie = await context
                .Movies
                .FirstOrDefaultAsync(x => x.Id == id);

                if (movie == null)
                    return NotFound(new ResultViewModel<Movie>("Filme não encontrado"));

                return Ok(new ResultViewModel<Movie>(movie));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Movie>("Não foi possível buscar o filme"));
            }
        }

        [HttpPost("v1/movies")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Movie model,
            [FromServices] EntertenimentManagementDataContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<Movie>(ModelState.GetErrors()));

            try
            {
                await context.Movies.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/movies/{model.Id}", new ResultViewModel<Movie>(model));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Movie>("Não foi possível incluir o filme"));
            }
        }

        [HttpPut("v1/movies/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] Movie model,
            [FromServices] EntertenimentManagementDataContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<Movie>(ModelState.GetErrors()));

            try
            {
                var movie = await context
                    .Movies
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (movie == null)
                    return NotFound(new ResultViewModel<Movie>(ModelState.GetErrors()));

                movie.Title = model.Title;
                movie.Director = model.Director;
                movie.DurationInMinutes = model.DurationInMinutes;
                movie.Distributor = model.Distributor;
                movie.Genre = model.Genre;
                movie.ReleaseYear = model.ReleaseYear;
                movie.UrlImage = model.UrlImage;
                movie.BelongsTo = model.BelongsTo;

                context.Movies.Update(movie);
                await context.SaveChangesAsync();

                return Ok(movie);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Movie>("Não foi possível alterar o filme"));
            }
        }

        [HttpDelete("v1/movies/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] EntertenimentManagementDataContext context)
        {
            try
            {
                var movie = await context
                    .Movies
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (movie == null)
                    return NotFound(new ResultViewModel<Movie>("Filme não encontrado"));

                context.Movies.Remove(movie);
                await context.SaveChangesAsync();

                return Ok(movie);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Movie>("Não foi possível excluir o filme"));
            }
        }
    }
}
