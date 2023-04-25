using EntertenimentManager.API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;
using EntertenimentManager.API.Services;
using EntertenimentManager.API.ViewModels.Accounts;
using EntertenimentManager.API.Extensions;
using EntertenimentManager.Domain.Commands;
using EntertenimentManager.Domain.Handlers;
using EntertenimentManager.Domain.SharedContext.ValueObjects;
using EntertenimentManager.Infra.Contexts;
using EntertenimentManager.Domain.Commands.Account;
using EntertenimentManager.Domain.Entities.Users;

namespace EntertenimentManager.API.Controllers
{

    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("v1/accounts/")]
        public async Task<IActionResult> Post(
            [FromBody] CreateAccountCommand command,
            [FromServices] AccountHandler handler,
            [FromServices] EmailService emailService)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível realizar o cadastro", ModelState.GetErrors()));

            try
            {
                var result = await handler.Handle(command);
                var commandResult = (GenericCommandResult)result;

                if (commandResult.Success)
                {
                    var login = (Login)commandResult.Data;

                    emailService.Send(
                        login.Name,
                        login.Email,
                        "Bem vindo!",
                        $"Sua senha é <strong>{login.Password}</strong>");
                }

                return Ok(commandResult);

            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new GenericCommandResult(false, "Não foi possível realizar o cadastro", null));
            }
            catch
            {
                return StatusCode(500, new GenericCommandResult(false, "Falha interna no servidor", null));
            }
        }

        [HttpPost("v1/accounts/login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginCommand command,
            [FromServices] AccountHandler handler,
            [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível realizar o login", ModelState.GetErrors()));

            try
            {
                var result = await handler.Handle(command);
                var commandResult = (GenericCommandResult)result;
                if (commandResult.Success)
                {
                    var token = tokenService.GenerateToken((User)commandResult.Data);
                    commandResult.Data = token;
                }                
                return Ok(commandResult);
            }
            catch (Exception)
            {
                return StatusCode(500, new GenericCommandResult(false, "Falha interna no servidor", null));
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("v1/accounts/allow-admin")]
        public async Task<IActionResult> AllowAdmin(
            [FromBody] AllowAdminCommand command,
            [FromServices] AccountHandler handler)
        {
            if (!ModelState.IsValid) return BadRequest(new GenericCommandResult(false, "Não foi possível alterar a permissão", ModelState.GetErrors()));

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

        [Authorize]
        [HttpPost("v1/accounts/upload-image")]
        public async Task<IActionResult> UploadImage(
        [FromBody] UploadImageViewModel model,
        [FromServices] EntertenimentManagementDataContext context)
        {
            var fileName = $"{Guid.NewGuid()}.jpg";
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(model.Base64Image, "");
            var bytes = Convert.FromBase64String(data);

            try
            {
                await System.IO.File.WriteAllBytesAsync($"wwwroot/images/{fileName}", bytes);
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }

            var user = await context
                .Users
                .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            if (user == null)
                return NotFound(new ResultViewModel<string>("Usuário não encontrado"));

            //user.UpdateImage($"https://localhost:5000/images/{fileName}");
            try
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor"));
            }

            return Ok(new ResultViewModel<string>("Imagem alterada com sucesso.", null));
        }
    }
}
