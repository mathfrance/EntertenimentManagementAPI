using EntertenimentManager.API.Extensions;
using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EntertenimentManager.Domain.Commands.Item.Game;
using System;
using EntertenimentManager.Domain.Commands.Item;
using EntertenimentManager.Domain.Commands.Item.Movie;

namespace EntertenimentManager.API.Controllers
{
    public class GameController : ControllerBase
    {
        [HttpGet("v1/games/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] GetGameByIdCommand command,
            [FromServices] GameHandler handler)
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

        [HttpGet("v1/all/games/{personalListId:int}")]
        public async Task<IActionResult> GetAllByPersonalListIdAsync(
            [FromRoute] int personalListId,
            [FromServices] GetAllByPersonalListIdCommand command,
            [FromServices] GameHandler handler)
        {
            command.UserId = HttpContext.GetRequestUserId();
            command.IsRequestFromAdmin = HttpContext.IsRequestFromAdmin();
            command.PersonalListId = personalListId;
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

        [HttpPost("v1/games")]
        public async Task<IActionResult> PostAsync(
            [FromBody] CreateGameCommand command,
            [FromServices] GameHandler handler)
        {
            command.UserId = HttpContext.GetRequestUserId();
            command.IsRequestFromAdmin = HttpContext.IsRequestFromAdmin();
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

        [HttpPut("v1/games")]
        public async Task<IActionResult> PutAsync(
            [FromBody] UpdateGameCommand command,
            [FromServices] GameHandler handler)
        {
            command.UserId = HttpContext.GetRequestUserId();
            command.IsRequestFromAdmin = HttpContext.IsRequestFromAdmin();
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

        [HttpDelete("v1/games/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] DeleteGameCommand command,
            [FromServices] GameHandler handler)
        {
            command.Id = id;
            command.UserId = HttpContext.GetRequestUserId();
            command.IsRequestFromAdmin = HttpContext.IsRequestFromAdmin();
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
    }
}
