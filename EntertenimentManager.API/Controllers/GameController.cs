using EntertenimentManager.API.Extensions;
using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EntertenimentManager.Domain.Commands.Item.Game;

namespace EntertenimentManager.API.Controllers
{
    public class GameController : ControllerBase
    {
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
    }
}
