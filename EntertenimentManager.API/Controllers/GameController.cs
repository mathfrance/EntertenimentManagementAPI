using EntertenimentManager.API.ViewModels;
using EntertenimentManager.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Infra.Contexts;

namespace EntertenimentManager.API.Controllers
{
    public class GameController : ControllerBase
    {
        [HttpGet("v1/games")]
        public async Task<IActionResult> GetAsync(
            [FromServices] EntertenimentManagementDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var count = await context.Games.AsNoTracking().CountAsync();
                var movies = await context
                    .Games
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    movies
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Game>>("Não foi possível buscar os jogos"));
            }
        }

        [HttpGet("v1/games/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EntertenimentManagementDataContext context)
        {
            try
            {
                var game = await context
                .Games
                .FirstOrDefaultAsync(x => x.Id == id);

                if (game == null)
                    return NotFound(new ResultViewModel<Game>("Jogo não encontrado"));

                return Ok(new ResultViewModel<Game>(game));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Game>("Não foi possível buscar o jogo"));
            }
        }

        [HttpPost("v1/games")]
        public async Task<IActionResult> PostAsync(
            [FromBody] Game model,
            [FromServices] EntertenimentManagementDataContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<Game>(ModelState.GetErrors()));

            if (model.Platforms != null)
            {
                var ids = model.Platforms.Select(x => x.Id).ToList();
                model.Platforms.Clear();

                var platforms = await context
                    .Platforms
                    .ToListAsync();


                foreach (var id in ids)
                {
                    var platform = platforms.FirstOrDefault(x => x.Id == id);
                    if (platform != null)
                        model.Platforms.Add(platform);
                }
            }

            try
            {
                await context.Games.AddAsync(model);
                await context.SaveChangesAsync();

                return Created($"v1/games/{model.Id}", new ResultViewModel<Game>(model));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Game>("Não foi possível incluir o jogo"));
            }
        }

        [HttpPut("v1/games/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] Game model,
            [FromServices] EntertenimentManagementDataContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<Game>(ModelState.GetErrors()));

            try
            {
                var game = await context
                    .Games
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (game == null)
                    return NotFound(new ResultViewModel<Game>(ModelState.GetErrors()));

                game.Update(model.Title, model.Genre, model.ReleaseYear, model.Developer, model.UrlImage, model.Platforms, model.BelongsTo);

                context.Games.Update(game);
                await context.SaveChangesAsync();

                return Ok(game);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Movie>("Não foi possível alterar o jogo"));
            }
        }

        [HttpDelete("v1/games/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] EntertenimentManagementDataContext context)
        {
            try
            {
                var game = await context
                    .Games
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (game == null)
                    return NotFound(new ResultViewModel<Game>("Jogo não encontrado"));

                context.Games.Remove(game);
                await context.SaveChangesAsync();

                return Ok(game);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Game>("Não foi possível excluir o jogo"));
            }
        }
    }
}
