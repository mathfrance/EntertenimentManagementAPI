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
            command.UserId = HttpContext.GetRequestUserId();
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

        [HttpGet("v1/categories/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] GetCategoryByIdCommand command,
            [FromServices] CategoryHandler handler)
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
    }
}
