using EntertenimentManager.API.Extensions;
using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Commands.Category;
using EntertenimentManager.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EntertenimentManager.API.Controllers
{
    public class CategoryController : ControllerBase
    {
        [HttpPost("v1/categories/")]
        public async Task<IActionResult> GetAsync(
            [FromBody] GetAllCategoriesCommand command,
            [FromServices] CategoryHandler handler)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível obter as categorias do usuário", ModelState.GetErrors()));
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
