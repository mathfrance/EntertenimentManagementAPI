using EntertenimentManager.API.Extensions;
using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using EntertenimentManager.Domain.Commands.PersonalList;

namespace EntertenimentManager.API.Controllers
{
    public class PersonalListController : ControllerBase
    {
        [HttpPost("v1/categories/lists/")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(
            [FromBody] GetAllPersonalListsByCategoryIdCommand command,
            [FromServices] PersonalListHandler handler)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível obter a lista", ModelState.GetErrors()));

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

        [HttpGet("v1/categories/lists/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] GetPersonalListByIdCommand command,
            [FromServices] PersonalListHandler handler)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível obter a lista", ModelState.GetErrors()));
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
    }
}
