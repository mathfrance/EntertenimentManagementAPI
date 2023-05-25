using EntertenimentManager.API.Extensions;
using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Category;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Domain.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EntertenimentManager.API.Controllers
{
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories/")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(
            [FromServices] GetAllCategoriesCommand command,
            [FromServices] CategoryHandler handler)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível obter as categorias do usuário", ModelState.GetErrors()));
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(int.TryParse(userId, out int userIdInt))
                command.UserId = userIdInt;
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

        [HttpPost("v1/categories/")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(
            [FromBody] GetCategoryByIdCommand command,
            [FromServices] CategoryHandler handler)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível obter a categoria informada", ModelState.GetErrors()));
           
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
