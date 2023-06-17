﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EntertenimentManager.Infra.Contexts;
using Microsoft.AspNetCore.Authorization;
using EntertenimentManager.Domain.Handlers;
using EntertenimentManager.Domain.Commands.Item.Movie;
using EntertenimentManager.Domain.Commands;
using System;
using EntertenimentManager.API.Extensions;

namespace EntertenimentManager.API.Controllers
{
    [Authorize]
    public class MovieController : ControllerBase
    {       
        [HttpGet("v1/movies/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] GetMovieByIdCommand command,
            [FromServices] MovieHandler handler)
        {
            command.UserId = HttpContext.GetRequestUserId();
            command.IsRequestFromAdmin = HttpContext.IsRequestFromAdmin();
            command.Id = id;
            try
            {
                var result = await handler.Handle(command);
                var commandResult = (GenericCommandResult)result;
                return Ok(commandResult);
            }
            catch (Exception)
            {
                return StatusCode(500, new GenericCommandResult(false, "Falha interna no servidor", null));
            }
        }

        [HttpPost("v1/movies")]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateMovieCommand command,
            [FromServices] MovieHandler handler)
        {

            try
            {
                var result = await handler.Handle(command);
                var commandResult = (GenericCommandResult)result;
                return Ok(commandResult);
            }
            catch
            {
                return StatusCode(500, new GenericCommandResult(false, "Falha interna no servidor", null));
            }
        }

        [HttpPut("v1/movies/")]
        public async Task<IActionResult> PutAsync(
            [FromBody] UpdateMovieCommand command,
            [FromServices] MovieHandler handler)
        {
            try
            {
                var result = await handler.Handle(command);
                var commandResult = (GenericCommandResult)result;
                return Ok(commandResult);
            }
            catch
            {
                return StatusCode(500, new GenericCommandResult(false, "Falha interna no servidor", null));
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
                    return NotFound();

                context.Movies.Remove(movie);
                await context.SaveChangesAsync();

                return Ok(movie);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
