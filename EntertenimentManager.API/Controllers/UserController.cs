using EntertenimentManager.API.Data;
using EntertenimentManager.API.Extensions;
using EntertenimentManager.API.ViewModels;
using EntertenimentManager.API.ViewModels.Accounts;
using EntertenimentManager.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EntertenimentManager.API.Controllers
{
    public class UserController : ControllerBase
    {
        [HttpGet("v1/users")]
        public async Task<IActionResult> GetAsync([FromServices] EntertenimentManagementDataContext context)
        {
            try
            {
                var users = await context.Users.ToListAsync();
                return Ok(new ResultViewModel<List<User>>(users));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<User>>("Não foi possível buscar os usuários"));
            }
        }

        [HttpGet("v1/users/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] EntertenimentManagementDataContext context)
        {
            try
            {
                var user = await context
                .Users
                .FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                    return NotFound(new ResultViewModel<User>("Usuário não encontrado"));

                return Ok(new ResultViewModel<User>(user));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Não foi possível buscar o usuário"));
            }
        }

        //[HttpPost("v1/users")]
        //public async Task<IActionResult> PostAsync(
        //    [FromBody] CreateUserViewModel model,
        //    [FromServices] EntertenimentManagementDataContext context)
        //{
        //    if (!ModelState.IsValid) return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

        //    try
        //    {
        //        var user = new User(model.Name, model.Email, PasswordHasher.Hash(password), new() { role });
        //        await context.Users.AddAsync(user);
        //        await context.SaveChangesAsync();

        //        return Created($"v1/users/{user.Id}", new ResultViewModel<User>(user));
        //    }
        //    catch
        //    {
        //        return StatusCode(500, new ResultViewModel<User>("Não foi possível incluir o usuário"));
        //    }
        //}

        [HttpPut("v1/users/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditUserViewModel model,
            [FromServices] EntertenimentManagementDataContext context)
        {
            if (!ModelState.IsValid) return BadRequest(new ResultViewModel<User>(ModelState.GetErrors()));

            try
            {
                var user = await context
                    .Users
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                    return NotFound(new ResultViewModel<User>(ModelState.GetErrors()));

                //user.Name = model.Name;
                //user.PasswordHash = model.PasswordHash;
                //user.Image = model.Image;

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Não foi possível alterar o usuário"));
            }
        }

        [HttpDelete("v1/users/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] EntertenimentManagementDataContext context)
        {
            try
            {
                var user = await context
                    .Users
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                    return NotFound(new ResultViewModel<User>("Usuário não encontrado"));

                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<User>("Não foi possível excluir o usuário"));
            }
        }
    }
}
